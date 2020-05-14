using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using StripePractice.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StripePractice.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [EnableCors("MyCorsPolicies")]
  public class PaymentController : Controller
  {
    [HttpPost("init")]
    public PaymentResponse InitPayment(CheckoutData data)
    {
      if (data.CurrentPaymentIntentId != null)
      {
        // TODO: If session is still valid, check if there is a need to update the payment intent (e.g. amount)
        return GetExistingSession(data.CurrentPaymentIntentId);
      }

      var service = new PaymentIntentService();
      var intent = service.Create(new PaymentIntentCreateOptions  
      {
        Amount = (long)(data.Amount * 100),
        Currency = data.Currency,
        PaymentMethodTypes = new List<string>(data.PaymentMethodTypes),
        StatementDescriptor = "ABC-Trading",
        Metadata = new Dictionary<string, string>
        {
          {"integration_check", "accept_a_payment" },
          {"OrderId", data.OrderId }
        }
      });

      return new PaymentResponse()
      {
        Status = "OK",
        SuccessData = new Dictionary<string, object>()
        {
          { "paymentIntent", intent.ToJson()}
        }
      };
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> StripeWebhook()
    {
      var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

      try
      {
        
        var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], AppConfig.WebhookKey, throwOnApiVersionMismatch: true);

        if (stripeEvent.Type == Events.PaymentIntentSucceeded)
        {
          var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
          Console.WriteLine("PaymentIntent was successful.");
        } else if(stripeEvent.Type == Events.PaymentMethodAttached)
        {
          var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
          Console.WriteLine("PaymentMethod was attached to Customer.");
        }
        return Ok();
      }
      catch(Exception e)
      {
        Console.WriteLine("Event hook error:" + e.ToString());
        return BadRequest();
      }
    }

    [HttpGet("get_session/{id}")]
    public PaymentResponse GetExistingSession(string id)
    {
      var service = new PaymentIntentService();
      return new PaymentResponse
      {
        Status = "OK",
        SuccessData = new Dictionary<string, object>()
        {
          {"paymentIntent", service.Get(id).ToJson()}
        }
      };
    }
  }
}

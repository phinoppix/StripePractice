using System;
using System.Collections.Generic;
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

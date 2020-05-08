using System;
namespace StripePractice.Models
{
  public class CheckoutData
  {
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string[] PaymentMethodTypes { get; set; }
    public string CurrentPaymentIntentId { get; set; }
    public string OrderId { get; set; }
  }
}

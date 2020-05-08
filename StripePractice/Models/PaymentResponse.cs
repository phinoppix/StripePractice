using System;
using System.Collections.Generic;

namespace StripePractice.Models
{
  public class PaymentResponse
  {
    public string Status { get; set; }
    public Dictionary<string, object> SuccessData { get; set; }
    public Exception Exception { get; set; }
  }
}

using System;
using Microsoft.Extensions.Configuration;

namespace StripePractice
{
  public static class AppConfig
  {
    internal static IConfiguration config;

    public static string ApiKey
    {
      get { return config["stripe:api_key"]; }
    }

    public static string WebhookKey
    {
      get { return config["stripe:webhook"]; }
    }
  }
}

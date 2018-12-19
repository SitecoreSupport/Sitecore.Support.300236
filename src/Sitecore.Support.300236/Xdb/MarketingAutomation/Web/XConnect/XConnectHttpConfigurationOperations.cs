using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sitecore.Framework.Conditions;
using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.DependencyInjection.Web.Abstractions;
using Sitecore.Xdb.MarketingAutomation.Core.Serialization;

namespace Sitecore.Support.Xdb.MarketingAutomation.Web.XConnect
{
  /// <summary>
  /// XConnect HTTP configuration bootstrap.
  /// </summary>
  /// <seealso cref="T:Sitecore.XConnect.DependencyInjection.Web.Abstractions.IHttpConfiguration" />
  public class XConnectHttpConfigurationOperations : IHttpConfiguration
  {
    internal const string MarketingAutomationServiceStatusRouteName = "MarketingAutomationServiceStatus";

    /// <summary>
    /// Configures <see cref="T:System.Web.Http.HttpConfiguration" />.
    /// </summary>
    /// <param name="config">The configuration to update.</param>
    public void ConfigureServices(HttpConfiguration config)
    {
      Condition.Requires<HttpConfiguration>(config, "config").IsNotNull<HttpConfiguration>();
      config.Routes.MapHttpRoute("MarketingAutomationContact", "marketing-automation/contact/{action}", new
      {
        controller = "MarketingAutomationContact",
        action = "Index"
      });
      config.Routes.MapHttpRoute("MarketingAutomationLiveEvent", "marketing-automation/liveevent/{action}", new
      {
        controller = "SupportMarketingAutomationLiveEvent",
        action = "Index"
      });
      config.Routes.MapHttpRoute("MarketingAutomationEnrollment", "marketing-automation/enrollment/{action}", new
      {
        controller = "MarketingAutomationEnrollment",
        action = "Index"
      });
      if (!config.Routes.ContainsKey("MarketingAutomationServiceStatus"))
      {
        config.Routes.MapHttpRoute("MarketingAutomationServiceStatus", "marketing-automation/{action}", new
        {
          controller = "MarketingAutomationServiceStatus",
          action = "Index"
        });
      }
      JsonSerializerSettings expr_C7 = config.Formatters.JsonFormatter.SerializerSettings;
      expr_C7.Converters.Add(new CustomValuesDictionaryJsonConverter());
      DefaultContractResolver defaultContractResolver = expr_C7.ContractResolver as DefaultContractResolver;
      if (defaultContractResolver != null)
      {
        defaultContractResolver.IgnoreSerializableAttribute = true;
      }
    }
  }
}

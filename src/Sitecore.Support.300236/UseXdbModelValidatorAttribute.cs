using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Validation;

namespace Sitecore.Support.Xdb.MarketingAutomation.Web
{
  /// <summary>
  /// A <see cref="T:System.Web.Http.Controllers.IControllerConfiguration" /> which applies the <see cref="T:Sitecore.Xdb.MarketingAutomation.Web.XdbModelValidator" /> for the controller.
  /// </summary>
  internal sealed class UseXdbModelValidatorAttribute : ActionFilterAttribute, IControllerConfiguration
  {
    /// <inheritdoc />
    public void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor)
    {
      controllerSettings.Services.Replace(typeof(IBodyModelValidator), new XdbModelValidator());
    }
  }
}

using System;
using System.Web.Http.Validation;
using Sitecore.XConnect;

namespace Sitecore.Support.Xdb.MarketingAutomation.Web
{
  /// <summary>
  ///     This class prevents the DefaultBodyModelValidator from maxing out the CPU and freeze the IIS indefinitely.
  /// </summary>
  /// <seealso cref="T:System.Web.Http.Validation.DefaultBodyModelValidator" />
  internal class XdbModelValidator : DefaultBodyModelValidator
  {
    /// <inheritdoc />
    public override bool ShouldValidateType(Type type)
    {
      return !typeof(Entity).IsAssignableFrom(type) && !typeof(Facet).IsAssignableFrom(type) && !typeof(Event).IsAssignableFrom(type) && base.ShouldValidateType(type);
    }
  }
}

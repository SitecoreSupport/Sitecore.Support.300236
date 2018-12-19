// Decompiled with JetBrains decompiler
// Type: Sitecore.Xdb.MarketingAutomation.OperationsClient.AutomationOperationsClient
// Assembly: Sitecore.Xdb.MarketingAutomation.OperationsClient, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B456A8A3-6E45-4917-8DE2-DF47B2881457
// Assembly location: C:\inetpub\wwwroot\sc901.sc\bin\Sitecore.Xdb.MarketingAutomation.OperationsClient.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sitecore.Framework.Conditions;
using Sitecore.XConnect.Client.Serialization;
using Sitecore.XConnect.Collection.Model;
using Sitecore.Xdb.Common.Web;
using Sitecore.Xdb.Common.Web.Synchronous;
using Sitecore.Xdb.MarketingAutomation.Core.Requests;
using Sitecore.Xdb.MarketingAutomation.Core.Results;
using Sitecore.Xdb.MarketingAutomation.Core.Serialization;
using Sitecore.Xdb.MarketingAutomation.OperationsClient;

namespace Sitecore.Support.Xdb.MarketingAutomation.OperationsClient
{
  /// <summary>
  /// A web API client implementation of <see cref="T:Sitecore.Xdb.MarketingAutomation.OperationsClient.IAutomationOperationsClient" />.
  /// </summary>
  public class AutomationOperationsClient : CommonWebApiClient<OperationRoutes>, IAutomationOperationsClient
  {
    /// <summary>
    /// The name of the connection string holding the URL for the endpoint to communicate with.
    /// </summary>
    public const string EndpointConnectionStringName = "xdb.marketingautomation.operations.client";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sitecore.Xdb.MarketingAutomation.OperationsClient.AutomationOperationsClient" /> class.
    /// </summary>
    /// <param name="logger">The logger to log messages to.</param>
    public AutomationOperationsClient(ILogger<AutomationOperationsClient> logger)
      : this(AutomationOperationsClient.ServiceAddressFromConnectionString, (IEnumerable<IHttpClientModifier>)null, (IEnumerable<IWebRequestHandlerModifier>)null, logger)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sitecore.Xdb.MarketingAutomation.OperationsClient.AutomationOperationsClient" /> class.
    /// </summary>
    /// <param name="logger">The logger to log messages to.</param>
    /// <param name="configuration">The configuration to load from.</param>
    public AutomationOperationsClient(ILogger<AutomationOperationsClient> logger, IConfiguration configuration)
      : base(new OperationRoutes(AutomationOperationsClient.ServiceAddressFromConnectionString), Condition.Requires<IConfiguration>(configuration, nameof(configuration)).IsNotNull<IConfiguration>().Value)
    {
      Condition.Requires<ILogger<AutomationOperationsClient>>(logger, nameof(logger)).IsNotNull<ILogger<AutomationOperationsClient>>();
      this.Logger = logger;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sitecore.Xdb.MarketingAutomation.OperationsClient.AutomationOperationsClient" /> class.
    /// </summary>
    /// <param name="serviceBaseAddress">The address of the controller hosting the service.</param>
    /// <param name="logger">The logger to log messages to.</param>
    public AutomationOperationsClient(Uri serviceBaseAddress, ILogger<AutomationOperationsClient> logger)
      : this(serviceBaseAddress, (IEnumerable<IHttpClientModifier>)null, (IEnumerable<IWebRequestHandlerModifier>)null, logger)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sitecore.Xdb.MarketingAutomation.OperationsClient.AutomationOperationsClient" /> class.
    /// </summary>
    /// <param name="clientModifiers">An ordered list of classes which modify the <see cref="T:System.Net.Http.HttpClient" /> after creation.</param>
    /// <param name="logger">The logger to log messages to.</param>
    public AutomationOperationsClient(IEnumerable<IHttpClientModifier> clientModifiers, ILogger<AutomationOperationsClient> logger)
      : this(AutomationOperationsClient.ServiceAddressFromConnectionString, clientModifiers, (IEnumerable<IWebRequestHandlerModifier>)null, logger)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sitecore.Xdb.MarketingAutomation.OperationsClient.AutomationOperationsClient" /> class.
    /// </summary>
    /// <param name="serviceBaseAddress">The address of the controller hosting the service.</param>
    /// <param name="clientModifiers">An ordered list of classes which modify the <see cref="T:System.Net.Http.HttpClient" /> after creation.</param>
    /// <param name="logger">The logger to log messages to.</param>
    public AutomationOperationsClient(Uri serviceBaseAddress, IEnumerable<IHttpClientModifier> clientModifiers, ILogger<AutomationOperationsClient> logger)
      : this(serviceBaseAddress, clientModifiers, (IEnumerable<IWebRequestHandlerModifier>)null, logger)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sitecore.Xdb.MarketingAutomation.OperationsClient.AutomationOperationsClient" /> class.
    /// </summary>
    /// <param name="webRequestHandlerModifiers">An ordered list of classes which modify the <see cref="T:System.Net.Http.WebRequestHandler" /> after creation.</param>
    /// <param name="logger">The logger to log messages to.</param>
    public AutomationOperationsClient(IEnumerable<IWebRequestHandlerModifier> webRequestHandlerModifiers, ILogger<AutomationOperationsClient> logger)
      : this(AutomationOperationsClient.ServiceAddressFromConnectionString, (IEnumerable<IHttpClientModifier>)null, webRequestHandlerModifiers, logger)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sitecore.Xdb.MarketingAutomation.OperationsClient.AutomationOperationsClient" /> class.
    /// </summary>
    /// <param name="serviceBaseAddress">The address of the controller hosting the service.</param>
    /// <param name="webRequestHandlerModifiers">An ordered list of classes which modify the <see cref="T:System.Net.Http.WebRequestHandler" /> after creation.</param>
    /// <param name="logger">The logger to log messages to.</param>
    public AutomationOperationsClient(Uri serviceBaseAddress, IEnumerable<IWebRequestHandlerModifier> webRequestHandlerModifiers, ILogger<AutomationOperationsClient> logger)
      : this(serviceBaseAddress, (IEnumerable<IHttpClientModifier>)null, webRequestHandlerModifiers, logger)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sitecore.Xdb.MarketingAutomation.OperationsClient.AutomationOperationsClient" /> class.
    /// </summary>
    /// <param name="clientModifiers">An ordered list of classes which modify the <see cref="T:System.Net.Http.HttpClient" /> after creation.</param>
    /// <param name="webRequestHandlerModifiers">An ordered list of classes which modify the <see cref="T:System.Net.Http.WebRequestHandler" /> after creation.</param>
    /// <param name="logger">The logger to log messages to.</param>
    public AutomationOperationsClient(IEnumerable<IHttpClientModifier> clientModifiers, IEnumerable<IWebRequestHandlerModifier> webRequestHandlerModifiers, ILogger<AutomationOperationsClient> logger)
      : this(AutomationOperationsClient.ServiceAddressFromConnectionString, clientModifiers, webRequestHandlerModifiers, logger)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sitecore.Xdb.MarketingAutomation.OperationsClient.AutomationOperationsClient" /> class.
    /// </summary>
    /// <param name="serviceBaseAddress">The address of the controller hosting the service.</param>
    /// <param name="clientModifiers">An ordered list of classes which modify the <see cref="T:System.Net.Http.HttpClient" /> after creation.</param>
    /// <param name="webRequestHandlerModifiers">An ordered list of classes which modify the <see cref="T:System.Net.Http.WebRequestHandler" /> after creation.</param>
    /// <param name="logger">The logger to log messages to.</param>
    [SuppressMessage("Data Flow", "SC1062:ValidateArgumentsOfPublicMethods", Justification = "clientModifiers can be null", MessageId = "1#")]
    public AutomationOperationsClient(Uri serviceBaseAddress, IEnumerable<IHttpClientModifier> clientModifiers, IEnumerable<IWebRequestHandlerModifier> webRequestHandlerModifiers, ILogger<AutomationOperationsClient> logger)
      : base(new OperationRoutes(serviceBaseAddress), clientModifiers, webRequestHandlerModifiers)
    {
      Condition.Requires<ILogger<AutomationOperationsClient>>(logger, nameof(logger)).IsNotNull<ILogger<AutomationOperationsClient>>();
      this.Logger = logger;
    }

    /// <summary>
    /// The <see cref="T:Microsoft.Extensions.Logging.ILogger`1" /> to log messages to.
    /// </summary>
    protected ILogger<AutomationOperationsClient> Logger { get; set; }

    /// <summary>Gets the settings to use during (de)serialization.</summary>
    protected static JsonSerializerSettings SerializerSettings
    {
      get
      {
        return new JsonSerializerSettings()
        {
          ContractResolver = (IContractResolver)new XdbJsonContractResolver(CollectionModel.Model, true, true),
          TypeNameHandling = TypeNameHandling.Auto,
          Converters = (IList<JsonConverter>)new List<JsonConverter>()
          {
            (JsonConverter) new LiveEventDataJsonConverter(CollectionModel.Model)
          }
        };
      }
    }

    /// <summary>
    /// Gets the service URI from the default connection string.
    /// </summary>
    /// <returns>The URI for the service, or null if the connection string couldn't be found or couldn't be parsed.</returns>
    protected static Uri ServiceAddressFromConnectionString
    {
      get
      {
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["xdb.marketingautomation.operations.client"];
        if (connectionString != null)
          return AutomationOperationsClient.EnsureRoutePrefixRemoved(new Uri(connectionString.ConnectionString));
        return (Uri)null;
      }
    }

    /// <inheritdoc />
    public async Task<BatchEnrollmentRequestResult> EnrollInPlanDirectAsync(IEnumerable<EnrollmentRequest> enrollments)
    {
      AutomationOperationsClient operationsClient = this;
      Condition.Requires<IEnumerable<EnrollmentRequest>>(enrollments, nameof(enrollments)).IsNotNull<IEnumerable<EnrollmentRequest>>().IsNotEmpty<IEnumerable<EnrollmentRequest>>();
      BatchEnrollmentRequestResult enrollmentRequestResult;
      using (HttpContent httpContent = operationsClient.CreateRequestContent<IEnumerable<EnrollmentRequest>>(enrollments))
      {
        using (HttpResponseMessage httpMessageResponse = await operationsClient.ExecutePutAsync(operationsClient.Routes.PutEnrollment, httpContent).ConfigureAwait(false))
        {
          if (!httpMessageResponse.IsSuccessStatusCode)
            throw new InvalidOperationException(string.Format((IFormatProvider)CultureInfo.CurrentCulture, "{0} {1}", "Enroll in plan direct did not complete successfully.", (object)httpMessageResponse));
          enrollmentRequestResult = await operationsClient.CreateResponseContentAsync<BatchEnrollmentRequestResult>(httpMessageResponse.Content).ConfigureAwait(false);
        }
      }
      return enrollmentRequestResult;
    }

    /// <inheritdoc />
    public BatchEnrollmentRequestResult EnrollInPlanDirect(IEnumerable<EnrollmentRequest> enrollments)
    {
      return ((Func<Task<BatchEnrollmentRequestResult>>)(() => this.EnrollInPlanDirectAsync(enrollments))).SuspendContextLock<BatchEnrollmentRequestResult>();
    }

    /// <inheritdoc />
    public async Task<BatchLiveEventRequestResult> RegisterLiveEventAsync(IEnumerable<LiveEventRequest> events)
    {
      AutomationOperationsClient operationsClient = this;
      Condition.Requires<IEnumerable<LiveEventRequest>>(events, nameof(events)).IsNotNull<IEnumerable<LiveEventRequest>>().IsNotEmpty<IEnumerable<LiveEventRequest>>();
      BatchLiveEventRequestResult eventRequestResult;
      using (HttpContent httpContent = operationsClient.CreateRequestContent<IEnumerable<LiveEventRequest>>(events))
      {
        using (HttpResponseMessage httpMessageResponse = await operationsClient.ExecutePutAsync(operationsClient.Routes.PutLiveEvent, httpContent).ConfigureAwait(false))
        {
          if (!httpMessageResponse.IsSuccessStatusCode)
            throw new InvalidOperationException(string.Format((IFormatProvider)CultureInfo.CurrentCulture, "{0} {1}", "Register live event did not complete successfully.", (object)httpMessageResponse));
          eventRequestResult = await operationsClient.CreateResponseContentAsync<BatchLiveEventRequestResult>(httpMessageResponse.Content).ConfigureAwait(false);
        }
      }
      return eventRequestResult;
    }

    /// <inheritdoc />
    public BatchLiveEventRequestResult RegisterLiveEvent(IEnumerable<LiveEventRequest> events)
    {
      return ((Func<Task<BatchLiveEventRequestResult>>)(() => this.RegisterLiveEventAsync(events))).SuspendContextLock<BatchLiveEventRequestResult>();
    }

    /// <inheritdoc />
    public async Task<BatchPurgeFromPlanRequestResult> PurgeFromPlanAsync(IEnumerable<PurgeFromPlanRequest> purges)
    {
      AutomationOperationsClient operationsClient = this;
      Condition.Requires<IEnumerable<PurgeFromPlanRequest>>(purges, nameof(purges)).IsNotNull<IEnumerable<PurgeFromPlanRequest>>().IsNotEmpty<IEnumerable<PurgeFromPlanRequest>>();
      BatchPurgeFromPlanRequestResult planRequestResult;
      using (HttpContent httpContent = operationsClient.CreateRequestContent<IEnumerable<PurgeFromPlanRequest>>(purges))
      {
        using (HttpResponseMessage httpMessageResponse = await operationsClient.ExecuteDeleteAsync(operationsClient.Routes.DeleteContactFromPlan, httpContent).ConfigureAwait(false))
        {
          if (!httpMessageResponse.IsSuccessStatusCode)
            throw new InvalidOperationException(string.Format((IFormatProvider)CultureInfo.CurrentCulture, "{0} {1}", "Purge from plan did not complete successfully.", (object)httpMessageResponse));
          planRequestResult = await operationsClient.CreateResponseContentAsync<BatchPurgeFromPlanRequestResult>(httpMessageResponse.Content).ConfigureAwait(false);
        }
      }
      return planRequestResult;
    }

    /// <inheritdoc />
    public BatchPurgeFromPlanRequestResult PurgeFromPlan(IEnumerable<PurgeFromPlanRequest> purges)
    {
      return ((Func<Task<BatchPurgeFromPlanRequestResult>>)(() => this.PurgeFromPlanAsync(purges))).SuspendContextLock<BatchPurgeFromPlanRequestResult>();
    }

    /// <inheritdoc />
    public async Task<BatchPurgeFromAllPlansRequestResult> PurgeFromAllPlansAsync(IEnumerable<PurgeFromAllPlansRequest> purges)
    {
      AutomationOperationsClient operationsClient = this;
      Condition.Requires<IEnumerable<PurgeFromAllPlansRequest>>(purges, nameof(purges)).IsNotNull<IEnumerable<PurgeFromAllPlansRequest>>().IsNotEmpty<IEnumerable<PurgeFromAllPlansRequest>>();
      BatchPurgeFromAllPlansRequestResult plansRequestResult;
      using (HttpContent httpContent = operationsClient.CreateRequestContent<IEnumerable<PurgeFromAllPlansRequest>>(purges))
      {
        using (HttpResponseMessage httpMessageResponse = await operationsClient.ExecuteDeleteAsync(operationsClient.Routes.DeleteContact, httpContent).ConfigureAwait(false))
        {
          if (!httpMessageResponse.IsSuccessStatusCode)
            throw new InvalidOperationException(string.Format((IFormatProvider)CultureInfo.CurrentCulture, "{0} {1}", "Purge from all plans did not complete successfully.", (object)httpMessageResponse));
          plansRequestResult = await operationsClient.CreateResponseContentAsync<BatchPurgeFromAllPlansRequestResult>(httpMessageResponse.Content).ConfigureAwait(false);
        }
      }
      return plansRequestResult;
    }

    /// <inheritdoc />
    public BatchPurgeFromAllPlansRequestResult PurgeFromAllPlans(IEnumerable<PurgeFromAllPlansRequest> purges)
    {
      return ((Func<Task<BatchPurgeFromAllPlansRequestResult>>)(() => this.PurgeFromAllPlansAsync(purges))).SuspendContextLock<BatchPurgeFromAllPlansRequestResult>();
    }

    /// <summary>
    /// Create the <see cref="T:System.Net.Http.HttpContent" /> for the request, with the provided object in the body.
    /// </summary>
    /// <typeparam name="T">The type of the object to format into the body of the request.</typeparam>
    /// <param name="content">The object to format into the body of the request.</param>
    /// <returns>The content to be sent.</returns>
    protected override HttpContent CreateRequestContent<T>(T content)
    {
      return (HttpContent)new StringContent(JsonConvert.SerializeObject((object)content, AutomationOperationsClient.SerializerSettings), Encoding.UTF8, JsonMediaTypeFormatter.DefaultMediaType.MediaType);
    }

    /// <summary>Create the object from the response.</summary>
    /// <typeparam name="T">The type of the object to create from the response content.</typeparam>
    /// <param name="httpContent">The <see cref="T:System.Net.Http.HttpContent" /> received in response.</param>
    /// <returns>The object from the response.</returns>
    protected override async Task<T> CreateResponseContentAsync<T>(HttpContent httpContent)
    {
      return JsonConvert.DeserializeObject<T>(await httpContent.ReadAsStringAsync().ConfigureAwait(false), AutomationOperationsClient.SerializerSettings);
    }

    /// <summary>
    /// Ensures and removes, if needed, <see cref="F:Sitecore.Xdb.MarketingAutomation.OperationsClient.OperationRoutes.DefaultRoutePrefix" /> from <paramref name="baseUri" />.
    /// </summary>
    /// <param name="baseUri">The base URI to remove <see cref="F:Sitecore.Xdb.MarketingAutomation.OperationsClient.OperationRoutes.DefaultRoutePrefix" /> from.</param>
    /// <returns>The base URI without <see cref="F:Sitecore.Xdb.MarketingAutomation.OperationsClient.OperationRoutes.DefaultRoutePrefix" />.</returns>
    protected static Uri EnsureRoutePrefixRemoved(Uri baseUri)
    {
      UriBuilder uriBuilder = new UriBuilder(baseUri);
      string str = uriBuilder.Path;
      if (str.EndsWith("/", StringComparison.Ordinal) || str.EndsWith("\\", StringComparison.Ordinal))
        str = str.Substring(0, str.Length - 1);
      if (str.EndsWith("/marketing-automation", StringComparison.OrdinalIgnoreCase) || str.EndsWith("\\marketing-automation", StringComparison.OrdinalIgnoreCase))
        uriBuilder.Path = str.Substring(0, str.Length - "marketing-automation".Length);
      return uriBuilder.Uri;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: Sitecore.Xdb.MarketingAutomation.Web.MarketingAutomationLiveEventController
// Assembly: Sitecore.Xdb.MarketingAutomation.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4855B4F1-0A79-4B76-86DA-56BBFF3CE1DE
// Assembly location: C:\inetpub\wwwroot\sc901EXMCloud.xconnect\bin\Sitecore.Xdb.MarketingAutomation.Web.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sitecore.Framework.Conditions;
using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.Schema;
using Sitecore.XConnect.Web.Infrastructure;
using Sitecore.Xdb.MarketingAutomation.Core.Factory;
using Sitecore.Xdb.MarketingAutomation.Core.Pool;
using Sitecore.Xdb.MarketingAutomation.Core.Pool.Collections;
using Sitecore.Xdb.MarketingAutomation.Core.Pool.Results;
using Sitecore.Xdb.MarketingAutomation.Core.Processing;
using Sitecore.Xdb.MarketingAutomation.Core.Processing.Results;
using Sitecore.Xdb.MarketingAutomation.Core.Requests;
using Sitecore.Xdb.MarketingAutomation.Core.Results;
using Sitecore.Xdb.MarketingAutomation.Core.Serialization;
using Sitecore.Xdb.MarketingAutomation.Web;

namespace Sitecore.Support.Xdb.MarketingAutomation.Web
{
  /// <summary>
  /// A web API controller which provides operations for live events.
  /// </summary>
  /// <seealso cref="T:System.Web.Http.ApiController" />
  [UseXdbModelValidator]
  public class SupportMarketingAutomationLiveEventController : ApiController
  {
    private readonly XdbModel _model;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sitecore.Xdb.MarketingAutomation.Web.MarketingAutomationLiveEventController" /> class.
    /// </summary>
    /// <param name="serviceScopeFactory">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceScopeFactory" /> used to create new service scopes.</param>
    /// <param name="processorRunner">The processor runner to run the live event with.</param>
    /// <param name="automationPool">The automation pool to execute operations against.</param>
    /// <param name="logger">The logger to write messages to.</param>
    public SupportMarketingAutomationLiveEventController(IServiceScopeFactory serviceScopeFactory, IProcessorRunner processorRunner, IAutomationPool automationPool, ILogger<SupportMarketingAutomationLiveEventController> logger, XdbEdmModel model)
    {
      Condition.Requires<IServiceScopeFactory>(serviceScopeFactory, nameof(serviceScopeFactory)).IsNotNull<IServiceScopeFactory>();
      Condition.Requires<IProcessorRunner>(processorRunner, nameof(processorRunner)).IsNotNull<IProcessorRunner>();
      Condition.Requires<IAutomationPool>(automationPool, nameof(automationPool)).IsNotNull<IAutomationPool>();
      Condition.Requires<ILogger<SupportMarketingAutomationLiveEventController>>(logger, nameof(logger)).IsNotNull<ILogger<SupportMarketingAutomationLiveEventController>>();
      this.ServiceScopeFactory = serviceScopeFactory;
      this.ProcessorRunner = processorRunner;
      this.Pool = automationPool;
      this.Logger = logger;
      _model = model?.Model ?? CollectionModel.Model;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sitecore.Xdb.MarketingAutomation.Web.MarketingAutomationLiveEventController" /> class.
    /// </summary>
    /// <param name="processorFactory">The <see cref="T:Sitecore.Xdb.MarketingAutomation.Core.Factory.IProcessorFactory" /> used to create instance of <see cref="T:Sitecore.Xdb.MarketingAutomation.Core.Processing.IProcessor" /> to handle the live events.</param>
    /// <param name="processorRunner">The processor runner to run the live event with.</param>
    /// <param name="logger">The logger to write messages to.</param>
    [Obsolete("Deprecated in Sitecore 9.0.1. Use ctor(IServiceScopeFactory, IProcessorRunner, ILogger<MarketingAutomationLiveEventController>) instead.", false)]
    public SupportMarketingAutomationLiveEventController(IProcessorFactory processorFactory, IProcessorRunner processorRunner, ILogger<SupportMarketingAutomationLiveEventController> logger, XdbEdmModel model)
    {
      Condition.Requires<IProcessorFactory>(processorFactory, nameof(processorFactory)).IsNotNull<IProcessorFactory>();
      Condition.Requires<IProcessorRunner>(processorRunner, nameof(processorRunner)).IsNotNull<IProcessorRunner>();
      Condition.Requires<ILogger<SupportMarketingAutomationLiveEventController>>(logger, nameof(logger)).IsNotNull<ILogger<SupportMarketingAutomationLiveEventController>>();
      logger.LogWarning("Obsolete constructor of {0} is called", (object)nameof(SupportMarketingAutomationLiveEventController));
      this.ProcessorFactory = processorFactory;
      this.ProcessorRunner = processorRunner;
      this.Logger = logger;
      _model = model?.Model ?? CollectionModel.Model;
    }

    /// <summary>
    /// The <see cref="T:Sitecore.Xdb.MarketingAutomation.Core.Factory.IProcessorFactory" /> used to create instance of <see cref="T:Sitecore.Xdb.MarketingAutomation.Core.Processing.IProcessor" /> to handle the live events.
    /// </summary>
    [Obsolete("Deprecated in Sitecore 9.0.1. IProcessorFactory is scoped and resolved via IServiceScopeFactory.", false)]
    protected IProcessorFactory ProcessorFactory { get; set; }

    /// <summary>The processor runner to run the live event with.</summary>
    protected IProcessorRunner ProcessorRunner { get; set; }

    /// <summary>
    /// The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceScopeFactory" /> used to create new service scopes.
    /// </summary>
    protected IServiceScopeFactory ServiceScopeFactory { get; set; }

    /// <summary>The automation pool to execute operations against.</summary>
    protected IAutomationPool Pool { get; set; }

    /// <summary>The logger to write messages to.</summary>
    protected ILogger<SupportMarketingAutomationLiveEventController> Logger { get; }

    /// <summary>Executes a batch of live events.</summary>
    /// <param name="events">The events to process.</param>
    /// <returns>
    /// Indicates whether the batch was successful and the results of each individual operation.
    /// It also includes the enrollments which were updated as a result of the invocation.
    /// </returns>
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Need to return HTTP status code")]
    [HttpPut]
    public async Task<IHttpActionResult> Put([FromBody] IEnumerable<LiveEventRequest> events)
    {
      SupportMarketingAutomationLiveEventController liveEventController1 = this;
      SupportMarketingAutomationLiveEventController liveEventController = liveEventController1;
      IEnumerable<LiveEventRequest> events1 = events;
      if (!liveEventController1.ModelState.IsValid)
      {
        liveEventController1.Logger.LogError("Failed to put live events. Model state is not valid.", Array.Empty<object>());
        return (IHttpActionResult)liveEventController1.BadRequest(liveEventController1.ModelState);
      }

      try
      {
        BatchLiveEventRequestResult content = await Task.Run<BatchLiveEventRequestResult>((Func<Task<BatchLiveEventRequestResult>>)(async () => await liveEventController.ProcessLiveEvents(events1)));
        return !content.Success ? (IHttpActionResult)liveEventController1.Ok<BatchLiveEventRequestResult>(content) : (IHttpActionResult)liveEventController1.Created<BatchLiveEventRequestResult>(string.Empty, content);
      }
      catch (ArgumentException ex)
      {
        liveEventController1.Logger.LogError((EventId)0, (Exception)ex, ex.Message, Array.Empty<object>());
        return (IHttpActionResult)liveEventController1.BadRequest(ex.Message);
      }
      catch (Exception ex)
      {
        liveEventController1.Logger.LogError((EventId)0, ex, ex.Message, Array.Empty<object>());
        return (IHttpActionResult)liveEventController1.InternalServerError(ex);
      }
    }

    /// <summary>Processes the live events.</summary>
    /// <param name="events">The live events to process.</param>
    /// <returns>Batch of processing results.</returns>
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Need to process all live event requests")]
    protected virtual async Task<BatchLiveEventRequestResult> ProcessLiveEvents(IEnumerable<LiveEventRequest> events)
    {
      Condition.Requires<IEnumerable<LiveEventRequest>>(events, nameof(events)).IsNotNull<IEnumerable<LiveEventRequest>>().IsNotEmpty<IEnumerable<LiveEventRequest>>();
      List<LiveEventRequestResult> eventRequestResults = new List<LiveEventRequestResult>();
      bool batchSuccess = true;
      foreach (LiveEventRequest liveEventRequest in events)
      {
        LiveEventRequest eventRequest = liveEventRequest;
        ProcessingResult processingResult;
        try
        {
          processingResult = await this.ProcessLiveEvent(eventRequest);
        }
        catch (Exception ex)
        {
          this.Logger.LogError((EventId)0, ex, "Failed to process live event request. Contact: '{0}'", (object)eventRequest.ContactId);
          processingResult = (ProcessingResult)new Failure();
        }
        LiveEventRequestResult eventRequestResult = new LiveEventRequestResult(eventRequest, processingResult.Success, processingResult.UpdatedEnrollments);
        batchSuccess = batchSuccess && eventRequestResult.Success;
        eventRequestResults.Add(eventRequestResult);
        eventRequest = (LiveEventRequest)null;
      }
      return new BatchLiveEventRequestResult(batchSuccess, (ICollection<LiveEventRequestResult>)eventRequestResults);
    }

    /// <summary>Processes the live event.</summary>
    /// <param name="eventRequest">The live event to process.</param>
    /// <returns>The outcome of processing the live event.</returns>
    protected virtual async Task<ProcessingResult> ProcessLiveEvent(LiveEventRequest eventRequest)
    {
      Condition.Requires<LiveEventRequest>(eventRequest, nameof(eventRequest)).IsNotNull<LiveEventRequest>();
      Condition.Requires<LiveEventData>(eventRequest.EventData, "EventData").IsNotNull<LiveEventData>();
      using (IServiceScope scope = this.ServiceScopeFactory.CreateScope())
      {
        using (ContactLeaser contactLeaser = new ContactLeaser(this.Pool, eventRequest.ContactId))
        {
          if (!contactLeaser.HasLease)
          {
            this.Logger.LogInformation(string.Format((IFormatProvider)CultureInfo.CurrentCulture, "Failed to take lease for contact '{0}'. Logging live event to automation pool.", (object)eventRequest.ContactId), Array.Empty<object>());
            return await this.AddLiveEventToAutomationPool(eventRequest);
          }
          IProcessor processor = scope.ServiceProvider.GetRequiredService<IProcessorFactory>().CreateProcessor(eventRequest.ContactId, (ExecutionData)eventRequest.EventData);
          if (processor != null)
            return await Task.FromResult<ProcessingResult>(this.ProcessorRunner.Run(processor, eventRequest.Priority));
        }
        this.Logger.LogError("Failed to create a processor for live event request. Contact: '{0}'", (object)eventRequest.ContactId);
        return (ProcessingResult)new Failure("No processor for live event request.");
      }
    }

    /// <summary>
    /// Adds the provided live event to the automation pool, to be processed by the engine.
    /// </summary>
    /// <param name="eventRequest">The live event to add to the pool.</param>
    /// <returns>The outcome of adding the live event to the pool.</returns>
    protected virtual async Task<ProcessingResult> AddLiveEventToAutomationPool(LiveEventRequest eventRequest)
    {
      WorkItem workItem = new WorkItem(eventRequest.ContactId, (ExecutionData)new LiveEventData(eventRequest.ContactId, eventRequest.EventData.Interaction, eventRequest.EventData.CustomValues), (byte)192);
      WorkItemCollection workItemCollection = new WorkItemCollection();
      workItemCollection.Add(workItem);
      WorkItemBatchResult workItemBatchResult = await this.Pool.AddAsync((IWorkItemCollection)workItemCollection);
      if (workItemBatchResult.Success)
        return (ProcessingResult)new Success();
      foreach (KeyValuePair<Guid, WorkItemResult> result in (IEnumerable<KeyValuePair<Guid, WorkItemResult>>)workItemBatchResult.Results)
      {
        if (!result.Value.Success)
          this.Logger.LogError("Failed to create work item '{0}'. Status code: {1}. Message: {2}", new object[3]
          {
            (object) result.Key,
            (object) result.Value.StatusCode,
            (object) result.Value.SystemMessage
          });
      }
      return (ProcessingResult)new Failure(string.Format((IFormatProvider)CultureInfo.CurrentCulture, "Failed to process live event request. Contact: '{0}'", (object)eventRequest.ContactId));
    }

    protected override void Initialize(HttpControllerContext controllerContext)
    {
      base.Initialize(controllerContext);

      // Configure the serializer to serialize the requests and responses properly.
      // Default serialization won't work as we have an array of an interface in the response.
      // We also need the correct runtime model for the LiveEventDataJsonConverter.
      Configuration.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
      Configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new LiveEventDataJsonConverter(_model));
    }
  }
}

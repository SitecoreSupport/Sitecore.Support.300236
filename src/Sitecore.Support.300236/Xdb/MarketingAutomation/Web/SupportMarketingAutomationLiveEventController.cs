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
  public class MarketingAutomationLiveEventController : ApiController
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sitecore.Xdb.MarketingAutomation.Web.MarketingAutomationLiveEventController" /> class.
    /// </summary>
    /// <param name="serviceScopeFactory">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceScopeFactory" /> used to create new service scopes.</param>
    /// <param name="processorRunner">The processor runner to run the live event with.</param>
    /// <param name="automationPool">The automation pool to execute operations against.</param>
    /// <param name="logger">The logger to write messages to.</param>
    public MarketingAutomationLiveEventController(IServiceScopeFactory serviceScopeFactory, IProcessorRunner processorRunner, IAutomationPool automationPool, ILogger<MarketingAutomationLiveEventController> logger)
    {
      Condition.Requires<IServiceScopeFactory>(serviceScopeFactory, nameof(serviceScopeFactory)).IsNotNull<IServiceScopeFactory>();
      Condition.Requires<IProcessorRunner>(processorRunner, nameof(processorRunner)).IsNotNull<IProcessorRunner>();
      Condition.Requires<IAutomationPool>(automationPool, nameof(automationPool)).IsNotNull<IAutomationPool>();
      Condition.Requires<ILogger<MarketingAutomationLiveEventController>>(logger, nameof(logger)).IsNotNull<ILogger<MarketingAutomationLiveEventController>>();
      this.ServiceScopeFactory = serviceScopeFactory;
      this.ProcessorRunner = processorRunner;
      this.Pool = automationPool;
      this.Logger = logger;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sitecore.Xdb.MarketingAutomation.Web.MarketingAutomationLiveEventController" /> class.
    /// </summary>
    /// <param name="processorFactory">The <see cref="T:Sitecore.Xdb.MarketingAutomation.Core.Factory.IProcessorFactory" /> used to create instance of <see cref="T:Sitecore.Xdb.MarketingAutomation.Core.Processing.IProcessor" /> to handle the live events.</param>
    /// <param name="processorRunner">The processor runner to run the live event with.</param>
    /// <param name="logger">The logger to write messages to.</param>
    [Obsolete("Deprecated in Sitecore 9.0.1. Use ctor(IServiceScopeFactory, IProcessorRunner, ILogger<MarketingAutomationLiveEventController>) instead.", false)]
    public MarketingAutomationLiveEventController(IProcessorFactory processorFactory, IProcessorRunner processorRunner, ILogger<MarketingAutomationLiveEventController> logger)
    {
      Condition.Requires<IProcessorFactory>(processorFactory, nameof(processorFactory)).IsNotNull<IProcessorFactory>();
      Condition.Requires<IProcessorRunner>(processorRunner, nameof(processorRunner)).IsNotNull<IProcessorRunner>();
      Condition.Requires<ILogger<MarketingAutomationLiveEventController>>(logger, nameof(logger)).IsNotNull<ILogger<MarketingAutomationLiveEventController>>();
      logger.LogWarning(Resources.ObsoleteConstructorIsCalled, (object)nameof(MarketingAutomationLiveEventController));
      this.ProcessorFactory = processorFactory;
      this.ProcessorRunner = processorRunner;
      this.Logger = logger;
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
    protected ILogger<MarketingAutomationLiveEventController> Logger { get; }

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
      MarketingAutomationLiveEventController liveEventController1 = this;
      MarketingAutomationLiveEventController liveEventController = liveEventController1;
      IEnumerable<LiveEventRequest> events1 = events;
      if (!liveEventController1.ModelState.IsValid)
      {
        liveEventController1.Logger.LogError(Resources.FailedToPutLiveEventsModelStateNotValid, Array.Empty<object>());
        return (IHttpActionResult)liveEventController1.BadRequest(liveEventController1.ModelState);
      }
      liveEventController1.Configuration.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
      liveEventController1.Configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add((JsonConverter)new LiveEventDataJsonConverter(CollectionModel.Model));
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
          this.Logger.LogError((EventId)0, ex, Resources.FailedToProcessLiveEventRequest, (object)eventRequest.ContactId);
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
            this.Logger.LogInformation(string.Format((IFormatProvider)CultureInfo.CurrentCulture, Resources.FailedToTakeLeaseLogWorkItem, (object)eventRequest.ContactId), Array.Empty<object>());
            return await this.AddLiveEventToAutomationPool(eventRequest);
          }
          IProcessor processor = scope.ServiceProvider.GetRequiredService<IProcessorFactory>().CreateProcessor(eventRequest.ContactId, (ExecutionData)eventRequest.EventData);
          if (processor != null)
            return await Task.FromResult<ProcessingResult>(this.ProcessorRunner.Run(processor, eventRequest.Priority));
        }
        this.Logger.LogError(Resources.FailedToCreateProcessorForLiveEventRequest, (object)eventRequest.ContactId);
        return (ProcessingResult)new Failure(Resources.NoProcessorForLiveEventRequest);
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
          this.Logger.LogError(Resources.FailedToCreateWorkItem, new object[3]
          {
            (object) result.Key,
            (object) result.Value.StatusCode,
            (object) result.Value.SystemMessage
          });
      }
      return (ProcessingResult)new Failure(string.Format((IFormatProvider)CultureInfo.CurrentCulture, Resources.FailedToProcessLiveEventRequest, (object)eventRequest.ContactId));
    }
  }
}

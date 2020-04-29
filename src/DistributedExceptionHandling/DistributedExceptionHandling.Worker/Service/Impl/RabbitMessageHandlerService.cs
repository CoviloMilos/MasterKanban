using System;
using Common.Dto;
using Common.Logging;
using DistributedExceptionHandling.Worker.RabbitMq;
using DistributedExceptionHandling.Worker.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace DistributedExceptionHandling.Worker.Service.Impl
{
    public class RabbitMessageHandlerService : RabbitBackgroundWorkerService
    {
        private readonly IServiceScopeFactory _services;
        private readonly ILoggerManager _logger;

        public RabbitMessageHandlerService(IServiceScopeFactory services,
                                    IPooledObjectPolicy<IModel> objectPolicy, 
                                    IOptions<RabbitQueueOptions> rabbitQueueOptions,
                                    ILoggerManager logger) 
            : base(objectPolicy, rabbitQueueOptions, logger)
        {
            _services = services;
            _logger = logger;
        }

        public override bool ProcessRabbitMqMessage(string exceptionModelString)
        {
            try
            {
                var queueModel = new QueueExceptionDto().DeserializeModel(exceptionModelString);
                var exceptionModel = Utils.mapQueueExceptionRequestDtoToExceptionModel(queueModel);

                using(var scope = _services.CreateScope())
                {
                    var exceptionService = (IExceptionService) scope
                            .ServiceProvider
                            .GetRequiredService(typeof(IExceptionService));

                    exceptionService.InsertExceptionModel(exceptionModel);

                    if (base.FirstStartOfWorkerFlag)
                    {
                        _logger.LogInfo("Restoring all Exception Model data from database for SignalR. This is first start of application.");
                    }
                    
                    base.FirstStartOfWorkerFlag = false;
                    return exceptionService.CommitChanges();
                }
            }
            catch (JsonReaderException jsonReaderException)
            {
                _logger.LogError("Could not Deserialize Exception Model. Exception: " + jsonReaderException.Message);
                return false;
            }
            catch (Exception exception)
            {
                _logger.LogError("Could not create Services Scoper or insert Exception Model in database. Exception: " + exception.Message);
                return false;
            }
        }

    }
}
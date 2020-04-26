using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using DistributedExceptionHandling.Worker.RabbitMq;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DistributedExceptionHandling.Worker.Service.Impl
{
    public class RabbitBackgroundWorkerService : IRabbitBackgroundWorkerService
    {
        private readonly DefaultObjectPool<IModel> _objectPool;  
        private readonly RabbitQueueOptions _rabbitQueueOptions;
        private readonly ILoggerManager _logger;
        public bool FirstStartOfWorkerFlag { get; set ; } = false;

        public RabbitBackgroundWorkerService(IPooledObjectPolicy<IModel> objectPolicy,
                                      IOptions<RabbitQueueOptions> rabbitQueueOptions,
                                      ILoggerManager logger)  
        {  
            _objectPool = new DefaultObjectPool<IModel>(objectPolicy, Environment.ProcessorCount * 2);  
            _rabbitQueueOptions = rabbitQueueOptions.Value;
            _logger = logger;
        } 

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInfo("Distributed exception handling worker started ...");
            FirstStartOfWorkerFlag = true;
            ConsumeMessage();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void ConsumeMessage()
        {
            var channel = _objectPool.Get();
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>  
            {     
                var body = Encoding.UTF8.GetString(ea.Body);
                _logger.LogInfo("Consuming queue messages ...");
                if(ProcessRabbitMqMessage(body))
                {
                    channel.BasicAck(ea.DeliveryTag, false);
                }
            };  
            channel.BasicConsume(_rabbitQueueOptions.Queue, false, consumer); 
        }

        public virtual bool ProcessRabbitMqMessage(String exceptionModelString) 
        {
            throw new NotImplementedException();
        }


    }
}
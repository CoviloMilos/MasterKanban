using System;
using System.Text;
using Common.ExceptionHanding.Exceptions;
using Common.Logging;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Common.DistribudetExcetionHandling
{
    public class RabbitMqManager : IRabbitMqManager
    {
        private readonly DefaultObjectPool<IModel> _objectPool;  
        private readonly ILoggerManager _logger;
        private readonly RabbitMqExchangeOptions _rabbitExchangeOptions;
  
        public RabbitMqManager(IPooledObjectPolicy<IModel> objectPolicy, 
                             ILoggerManager logger,
                             IOptions<RabbitMqExchangeOptions> rabbitExchangeOptions)  
        {  
            _logger = logger;
            _objectPool = new DefaultObjectPool<IModel>(objectPolicy, Environment.ProcessorCount * 2); 
            _rabbitExchangeOptions = rabbitExchangeOptions.Value; 
        } 
        public void Publish<T>(T message) where T : class
        {
            if (message == null)  
                return;  
  
            var channel = _objectPool.Get();  
            try  
            {  
                _logger.LogInfo("Publishing message to RabbitMQ");

                var sendBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));  
    
                var properties = channel.CreateBasicProperties();  
                properties.Persistent = true;  
    
                channel.BasicPublish(_rabbitExchangeOptions.Exchange, _rabbitExchangeOptions.RoutingKey, properties, sendBytes);  
            }  
            catch (Exception ex)  
            {  
                _logger.LogInfo("Publishing message to RabbitMQ failed");
                throw new RabbitMqException("Service_bus_publish_failed. Exception message: " + ex.Message);  
            }  
            finally  
            {  
                _objectPool.Return(channel);                  
            }  
        }
    }
}
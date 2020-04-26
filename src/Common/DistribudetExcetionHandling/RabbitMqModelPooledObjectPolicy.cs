using System;
using Common.ExceptionHanding.Exceptions;
using Common.Logging;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Common.DistribudetExcetionHandling
{
    public class RabbitMqModelPooledObjectPolicy : IPooledObjectPolicy<IModel> {
        private readonly RabbitMqOptions _options;
        private readonly IConnection _connection;
        private readonly ILoggerManager _logger;

        public RabbitMqModelPooledObjectPolicy (IOptions<RabbitMqOptions> options, ILoggerManager logger) {
            _options = options.Value;
            _logger = logger;
            _connection = GetConnection();
        }

        public IModel Create () {
            return _connection.CreateModel();
        }

        public bool Return (IModel obj) {
            if (obj.IsOpen)
            {
                return true;
            }
            else 
            {
                obj?.Dispose();
                return false;
            }
        }

        private IConnection GetConnection() 
        {
            try
            {
                _logger.LogInfo("Creating connection to RabbitMQ");

                var factory = new ConnectionFactory()
                {
                    HostName = _options.HostName,
                    UserName = _options.UserName,
                    Password = _options.Password,
                    Port = _options.Port,
                    VirtualHost = _options.VHost,
                };

                return factory.CreateConnection();
            }
            catch (Exception ex)
            {
                _logger.LogInfo("Connection to RabbitMQ failed.");
                throw new RabbitMqException("Rabbitmq_connetion_factory_failed. Excetion message: " + ex.Message);
            }
        }
    }
}
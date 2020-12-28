using System;
using System.Text;
using Bank.Digital.OrderService.Integration.RabbitMq.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Bank.Digital.OrderService.Integration.RabbitMq.Consumers
{
    /// <summary>
    /// RabbitMq queue consumer
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    internal abstract class RabbitConsumerBase<TMessage> : IDisposable
    {
        protected abstract string ExchangeName { get; }
        protected abstract string QueueName { get; }
        protected abstract string RoutingKey { get; }

        private readonly IConnection _connection;
        private readonly IModel _channel;

        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        protected RabbitConsumerBase(
            IOptions<RabbitMqConfig> configuration,
            ILogger logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            var credentials = configuration.Value.Credentials;

            var connectionFactory = new ConnectionFactory
            {
                HostName = credentials.HostName,
                UserName = credentials.UserName,
                Password = credentials.Password,
                VirtualHost = credentials.VirtualHost,
                Port = AmqpTcpEndpoint.UseDefaultPort,
                AutomaticRecoveryEnabled = true
            };

            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(QueueName, true, false, false);
            _channel.QueueBind(QueueName, ExchangeName, RoutingKey);
        }

        /// <summary>
        /// Subscribe for new messages
        /// </summary>
        public void Subscribe<THandler>() where THandler : IEventHandler<TMessage>
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var str = Encoding.UTF8.GetString(ea.Body);
                    var message = JsonConvert.DeserializeObject<TMessage>(str);

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var handler = scope.ServiceProvider.GetRequiredService<THandler>();
                        await handler.HandleAsync(message);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
                finally
                {
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
            };

            _channel.BasicConsume(QueueName, false, consumer);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _channel.Close();
            _connection.Close();

            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
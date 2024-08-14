using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace SistemaDeGerenciamentoDeTarefas.Service
{
    public class RabbitMqService
    {
        private readonly IModel _channel;
        private readonly string _exchange;
        private readonly string _routingKey;

        public RabbitMqService(IOptions<RabbitMqOptions> rabbitMqOptions)
        {
            var factory = new ConnectionFactory
            {
                HostName = rabbitMqOptions.Value.HostName,
                Port = rabbitMqOptions.Value.Port,
                UserName = rabbitMqOptions.Value.UserName,
                Password = rabbitMqOptions.Value.Password
            };

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _exchange = rabbitMqOptions.Value.Exchange;
            _routingKey = rabbitMqOptions.Value.RoutingKey;

            // Configurações do exchange
            _channel.ExchangeDeclare(exchange: _exchange, type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
        }

        public void PublishMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(
                exchange: _exchange,
                routingKey: _routingKey,
                basicProperties: null,
                body: body);
        }
    }

    public class RabbitMqOptions
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Exchange { get; set; }
        public string Queue { get; set; }
        public string RoutingKey { get; set; }
    }


}

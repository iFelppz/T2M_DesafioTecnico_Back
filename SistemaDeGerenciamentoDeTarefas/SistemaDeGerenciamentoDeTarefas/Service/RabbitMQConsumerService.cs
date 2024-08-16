using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace SistemaDeGerenciamentoDeTarefas.Service
{
    public class RabbitMQConsumerService
    {
        private readonly IModel _channel;
        private readonly string _queueName;

        public RabbitMQConsumerService(IOptions<RabbitMqOptions> rabbitMqOptions)
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

            _queueName = rabbitMqOptions.Value.Queue;

            // Configuração da fila
            _channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        public void StartConsuming()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Mensagem recebida: {message}");
            };

            _channel.BasicConsume(queue: _queueName,
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}

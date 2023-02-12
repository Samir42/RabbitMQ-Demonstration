namespace UserService.API.Infrastructure;

using RabbitMQ.Client;
using System.Text;

public class RabbitMQService : IRabbitMQService {
    public void PublishUserCreatedMessage(string message) {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "microservice_exchange", type: ExchangeType.Direct);

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "microservice_exchange",
            routingKey: "user_created",
            basicProperties: null,
            body: body);
    }

    public void PublishNotificationMessage()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };


        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "microservice_exchange", type: ExchangeType.Direct);

        var body = Encoding.UTF8.GetBytes("Your test account is created!");

        channel.BasicPublish(exchange: "microservice_exchange",
            routingKey: "user_created_notification",
            basicProperties: null,
            body: body);
    }
}
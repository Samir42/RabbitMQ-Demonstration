using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory
{
    HostName = "localhost"
};

var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "microservice_exchange", type: ExchangeType.Direct);
channel.QueueDeclare("notification_queue", durable: true, exclusive: false);
channel.QueueBind("notification_queue", "microservice_exchange", "user_created_notification");


var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"I got a notification message and I just sent it! : {message}");
};

channel.BasicConsume(queue: "notification_queue", autoAck: true, consumer: consumer);


Console.ReadKey();
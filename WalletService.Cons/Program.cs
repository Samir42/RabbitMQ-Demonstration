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
channel.QueueDeclare("wallet_queue", durable: true, exclusive: false);
channel.QueueBind("wallet_queue", "microservice_exchange", "user_created");


var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("I start to create new wallets for the recently created user");
    Console.ForegroundColor = ConsoleColor.White;

    Console.WriteLine($"Message received: {message}");
};

channel.BasicConsume(queue: "wallet_queue", autoAck: true, consumer: consumer);


Console.ReadKey();
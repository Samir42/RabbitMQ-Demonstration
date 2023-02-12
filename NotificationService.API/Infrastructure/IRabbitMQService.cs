namespace NotificationService.API.Infrastructure;

using RabbitMQ.Client;

public interface IRabbitMQService {
    void PublishMessage(string message);
}
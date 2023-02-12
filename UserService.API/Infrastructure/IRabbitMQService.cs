namespace UserService.API.Infrastructure;

public interface IRabbitMQService {
    void PublishUserCreatedMessage(string message);
    void PublishNotificationMessage();
}
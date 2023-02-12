namespace NotificationService.API.Entity;

public class Notification {
    public int Id { get; set; }
    public string Message { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Status { get; set; }
}
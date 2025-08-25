namespace GcpMessagingDemo.Models;

public class MessageDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Content { get; set; } = string.Empty;
}

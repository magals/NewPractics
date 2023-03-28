namespace SignalRGenerationClient.Hub
{
  public interface HubFirstContact
  {
    Task<Status> SendMessage(string user, string message);
    Task SomeHubMethod();
  }

  public class Status
  {
    public string? StatusMessage { get; set; }
  }
}
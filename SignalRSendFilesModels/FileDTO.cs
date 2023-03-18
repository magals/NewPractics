namespace SignalRSendFilesModels;

public record FileDTO
{
  public required string FileName { get; init; }
  public required string Extension { get; init; }
  public required byte[] Content { get; init; }
}
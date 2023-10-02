using System.ComponentModel.DataAnnotations;

namespace TemplateBackgroundService.Models;

public class LogMessage 
{
    public required DateTimeOffset DateTimeOffset { get; init; }
    public required LogLevel LogLevel { get; init; }
    public required string Message { get; init; }
    public required int ThreadId { get; init; }
}

namespace ClientCatalog.Core.Logging;

/// <summary>
/// In real-world applications Microsoft.Extensions.Logging would be used
/// </summary>
public interface ILogger
{
    void Info(string message);
    void Error(Exception ex, string message);
}

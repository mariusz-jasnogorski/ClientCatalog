using System.Text;

namespace ClientCatalog.Core.Logging;

public sealed class FileLogger : ILogger
{
    private readonly string _logFilePath;
    private readonly object _lock = new();

    public FileLogger(string logFilePath)
    {
        _logFilePath = logFilePath;

        var dir = Path.GetDirectoryName(_logFilePath);
        if (!string.IsNullOrWhiteSpace(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }

    public void Info(string message) => Write("INFO", message, null);

    public void Error(Exception ex, string message) => Write("ERROR", message, ex);

    private void Write(string level, string message, Exception? ex)
    {
        lock (_lock)
        {
            var sb = new StringBuilder();
            sb.Append(DateTime.UtcNow.ToString("O"));
            sb.Append(' ');
            sb.Append(level);
            sb.Append(" - ");
            sb.Append(message);

            if (ex is not null)
            {
                sb.AppendLine();
                sb.Append(ex.ToString());
            }

            sb.AppendLine();
            File.AppendAllText(_logFilePath, sb.ToString(), Encoding.UTF8);
        }
    }
}

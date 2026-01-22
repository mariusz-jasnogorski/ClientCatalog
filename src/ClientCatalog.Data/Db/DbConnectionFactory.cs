using ClientCatalog.Core.Logging;
using Microsoft.Data.SqlClient;

namespace ClientCatalog.Data.Db;

public sealed class DbConnectionFactory
{
    private readonly string _connectionString;
    private readonly ILogger _logger;

    public DbConnectionFactory(string connectionString, ILogger logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    public SqlConnection Create()
    {
        try
        {
            return new SqlConnection(_connectionString);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to create SQL connection.");
            throw;
        }
    }

    public string ConnectionString => _connectionString;
}

using ClientCatalog.Core.Models;
using ClientCatalog.Core.Repositories;
using ClientCatalog.Data.Db;
using Dapper;

namespace ClientCatalog.Data.Repositories;

/// <summary>
/// Uses parameterized queries to prevent SQL injection.
/// </summary>
public sealed class DapperCustomerRepository : ICustomerRepository
{
    private readonly DbConnectionFactory _factory;

    private static readonly Dictionary<string, string> SortMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Name"] = "Name",
        ["Nip"] = "Nip",
        ["Address"] = "Address",
        ["Phone"] = "Phone",
        ["Email"] = "Email",
        ["CreatedAtUtc"] = "CreatedAtUtc",
        ["UpdatedAtUtc"] = "UpdatedAtUtc",
    };

    public DapperCustomerRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<IReadOnlyList<Customer>> GetAsync(CustomerQueryParameters parameters, CancellationToken ct = default)
    {
        var (sql, dynParams) = BuildListQuery(parameters);

        using var conn = _factory.Create();
        await conn.OpenAsync(ct).ConfigureAwait(false);

        var rows = await conn.QueryAsync<Customer>(
            new CommandDefinition(sql, dynParams, cancellationToken: ct)).ConfigureAwait(false);

        return rows.ToList();
    }

    public async Task<Customer?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        const string sql = @"
SELECT Id, Name, Nip, Address, Phone, Email, CreatedAtUtc, UpdatedAtUtc
FROM dbo.Customers
WHERE Id = @id;
";
        using var conn = _factory.Create();
        await conn.OpenAsync(ct).ConfigureAwait(false);

        return await conn.QuerySingleOrDefaultAsync<Customer>(
            new CommandDefinition(sql, new { id }, cancellationToken: ct)).ConfigureAwait(false);
    }

    public async Task<int> CreateAsync(Customer customer, CancellationToken ct = default)
    {
        const string sql = @"
INSERT INTO dbo.Customers (Name, Nip, Address, Phone, Email, CreatedAtUtc, UpdatedAtUtc)
VALUES (@Name, @Nip, @Address, @Phone, @Email, @CreatedAtUtc, @UpdatedAtUtc);

SELECT CAST(SCOPE_IDENTITY() AS INT);
";

        customer.CreatedAtUtc = DateTime.UtcNow;
        customer.UpdatedAtUtc = DateTime.UtcNow;

        using var conn = _factory.Create();
        await conn.OpenAsync(ct).ConfigureAwait(false);

        var newId = await conn.ExecuteScalarAsync<int>(
            new CommandDefinition(sql, customer, cancellationToken: ct)).ConfigureAwait(false);

        return newId;
    }

    public async Task UpdateAsync(Customer customer, CancellationToken ct = default)
    {
        const string sql = @"
UPDATE dbo.Customers
SET Name = @Name,
    Nip = @Nip,
    Address = @Address,
    Phone = @Phone,
    Email = @Email,
    UpdatedAtUtc = @UpdatedAtUtc
WHERE Id = @Id;
";

        customer.UpdatedAtUtc = DateTime.UtcNow;

        using var conn = _factory.Create();
        await conn.OpenAsync(ct).ConfigureAwait(false);

        await conn.ExecuteAsync(
            new CommandDefinition(sql, customer, cancellationToken: ct)).ConfigureAwait(false);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        const string sql = "DELETE FROM dbo.Customers WHERE Id = @id;";
        using var conn = _factory.Create();
        await conn.OpenAsync(ct).ConfigureAwait(false);

        await conn.ExecuteAsync(
            new CommandDefinition(sql, new { id }, cancellationToken: ct)).ConfigureAwait(false);
    }

    public async Task<int> CountAsync(CancellationToken ct = default)
    {
        const string sql = "SELECT COUNT(1) FROM dbo.Customers;";
        using var conn = _factory.Create();
        await conn.OpenAsync(ct).ConfigureAwait(false);

        return await conn.ExecuteScalarAsync<int>(
            new CommandDefinition(sql, cancellationToken: ct)).ConfigureAwait(false);
    }

    private static (string Sql, DynamicParameters Params) BuildListQuery(CustomerQueryParameters parameters)
    {
        var dp = new DynamicParameters();

        var where = "";
        if (!string.IsNullOrWhiteSpace(parameters.Search))
        {
            // Add wildcard search term for LIKE comparisons.
            dp.Add("term", $"%{parameters.Search.Trim()}%");

            where = @"
WHERE Name LIKE @term
   OR Nip LIKE @term
   OR Address LIKE @term
   OR Phone LIKE @term
   OR Email LIKE @term
";
        }

        var sortColumn = SortMap.TryGetValue(parameters.SortBy ?? "Name", out var col) ? col : "Name";
        var sortDir = parameters.SortDescending ? "DESC" : "ASC";

        var sql = $@"
SELECT Id, Name, Nip, Address, Phone, Email, CreatedAtUtc, UpdatedAtUtc
FROM dbo.Customers
{where}
ORDER BY {sortColumn} {sortDir};
";

        return (sql, dp);
    }
}

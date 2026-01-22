using Bogus;
using ClientCatalog.Core.Logging;
using ClientCatalog.Core.Models;
using ClientCatalog.Core.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ClientCatalog.Data.Db;

public sealed class DbInitializer
{
    private readonly DbConnectionFactory _factory;
    private readonly ICustomerRepository _repo;
    private readonly ILogger _logger;

    public const string DatabaseName = "ClientCatalogDb";

    public DbInitializer(DbConnectionFactory factory, ICustomerRepository repo, ILogger logger)
    {
        _factory = factory;
        _repo = repo;
        _logger = logger;
    }

    public async Task EnsureCreatedAndSeededAsync(CancellationToken ct = default)
    {
        await EnsureDatabaseExistsAsync(ct).ConfigureAwait(false);
        await EnsureSchemaAsync(ct).ConfigureAwait(false);
        await SeedIfEmptyAsync(ct).ConfigureAwait(false);
    }

    private async Task EnsureDatabaseExistsAsync(CancellationToken ct)
    {        
        var builder = new SqlConnectionStringBuilder(_factory.ConnectionString)
        {
            InitialCatalog = "master"
        };

        using var conn = new SqlConnection(builder.ConnectionString);
        await conn.OpenAsync(ct).ConfigureAwait(false);

        var exists = await conn.ExecuteScalarAsync<int>(
            new CommandDefinition(
                "SELECT COUNT(1) FROM sys.databases WHERE name = @name",
                new { name = DatabaseName },
                cancellationToken: ct)).ConfigureAwait(false);

        if (exists == 0)
        {
            _logger.Info($"Database '{DatabaseName}' not found. Creating...");
            await conn.ExecuteAsync(
                new CommandDefinition(
                    $"CREATE DATABASE [{DatabaseName}]",
                    cancellationToken: ct)).ConfigureAwait(false);
        }
    }

    private async Task EnsureSchemaAsync(CancellationToken ct)
    {
        using var conn = _factory.Create();
        await conn.OpenAsync(ct).ConfigureAwait(false);

        var sql = @"
IF OBJECT_ID(N'dbo.Customers', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Customers
    (
        Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Customers PRIMARY KEY,
        Name NVARCHAR(200) NOT NULL,
        Nip NVARCHAR(10) NOT NULL,
        Address NVARCHAR(400) NOT NULL,
        Phone NVARCHAR(30) NOT NULL,
        Email NVARCHAR(254) NOT NULL,
        CreatedAtUtc DATETIME2 NOT NULL,
        UpdatedAtUtc DATETIME2 NOT NULL
    );

    CREATE UNIQUE INDEX IX_Customers_Nip ON dbo.Customers(Nip);
END
";
        await conn.ExecuteAsync(new CommandDefinition(sql, cancellationToken: ct)).ConfigureAwait(false);
    }

    private async Task SeedIfEmptyAsync(CancellationToken ct)
    {
        var count = await _repo.CountAsync(ct).ConfigureAwait(false);
        if (count > 0) return;

        _logger.Info("Database is empty - seeding fake customers with Bogus...");

        // Generates realistic data for demo purposes.
        var faker = new Faker<Customer>("pl")
            .RuleFor(x => x.Name, f => f.Company.CompanyName())
            .RuleFor(x => x.Nip, f => string.Concat(f.Random.Digits(10)))
            .RuleFor(x => x.Address, f => $"{f.Address.StreetAddress()}, {f.Address.ZipCode()} {f.Address.City()}")
            .RuleFor(x => x.Phone, f => f.Phone.PhoneNumber())
            .RuleFor(x => x.Email, (f, c) => $"kontakt@{Slugify(c.Name)}.pl")
            .RuleFor(x => x.CreatedAtUtc, _ => DateTime.UtcNow)
            .RuleFor(x => x.UpdatedAtUtc, _ => DateTime.UtcNow);

        var toCreate = 50;
        var attempts = 0;
        var created = 0;

        while (created < toCreate && attempts < 500)
        {
            attempts++;
            var customer = faker.Generate();
            try
            {
                await _repo.CreateAsync(customer, ct).ConfigureAwait(false);
                created++;
            }
            catch (SqlException ex)
            {
                _logger.Error(ex, "Seed insert failed. Retrying...");
            }
        }

        _logger.Info($"Seed finished: created {created} customers (attempts={attempts}).");
    }

    private static string Slugify(string input)
    {
        var s = new string(input
            .ToLowerInvariant()
            .Select(ch => char.IsLetterOrDigit(ch) ? ch : '-')
            .ToArray());

        while (s.Contains("--", StringComparison.Ordinal))
            s = s.Replace("--", "-", StringComparison.Ordinal);

        return s.Trim('-');
    }
}

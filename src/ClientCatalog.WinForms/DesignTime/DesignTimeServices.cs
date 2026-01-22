using ClientCatalog.Core.Logging;
using ClientCatalog.Core.Models;
using ClientCatalog.Core.Services;

namespace ClientCatalog.WinForms.DesignTime;

/// <summary>
/// WinForms Designer instantiates forms using a parameterless constructor.
/// Our runtime forms are created via DI and require services in the constructor.
/// To keep both worlds happy, we provide lightweight "design-time" implementations.
/// 
/// IMPORTANT:
/// - These implementations are ONLY used by the designer.
/// - They do not touch the database.
/// - They return empty data / throw when called in ways that would require real dependencies.
/// </summary>
internal sealed class DesignTimeLogger : ILogger
{
    public void Info(string message) { /* no-op */ }
    public void Error(Exception ex, string message) { /* no-op */ }
}

internal sealed class DesignTimeCustomerService : ICustomerService
{
    public Task<IReadOnlyList<Customer>> GetAsync(CustomerQueryParameters parameters, CancellationToken ct = default)
        => Task.FromResult<IReadOnlyList<Customer>>(Array.Empty<Customer>());

    public Task<Customer?> GetByIdAsync(int id, CancellationToken ct = default)
        => Task.FromResult<Customer?>(null);

    public Task<int> CreateAsync(Customer customer, CancellationToken ct = default)
        => throw new NotSupportedException("Design-time service cannot create customers.");

    public Task UpdateAsync(Customer customer, CancellationToken ct = default)
        => throw new NotSupportedException("Design-time service cannot update customers.");

    public Task DeleteAsync(int id, CancellationToken ct = default)
        => throw new NotSupportedException("Design-time service cannot delete customers.");
}

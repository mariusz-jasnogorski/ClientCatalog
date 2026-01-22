using ClientCatalog.Core.Models;

namespace ClientCatalog.Core.Services;

public interface ICustomerService
{
    Task<IReadOnlyList<Customer>> GetAsync(CustomerQueryParameters parameters, CancellationToken ct = default);
    Task<Customer?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<int> CreateAsync(Customer customer, CancellationToken ct = default);
    Task UpdateAsync(Customer customer, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}

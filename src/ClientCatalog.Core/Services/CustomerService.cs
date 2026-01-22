using ClientCatalog.Core.Logging;
using ClientCatalog.Core.Models;
using ClientCatalog.Core.Repositories;
using ClientCatalog.Core.Validation;

namespace ClientCatalog.Core.Services;

public sealed class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repo;
    private readonly ILogger _logger;

    public CustomerService(ICustomerRepository repo, ILogger logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public Task<IReadOnlyList<Customer>> GetAsync(CustomerQueryParameters parameters, CancellationToken ct = default)
        => _repo.GetAsync(parameters, ct);

    public Task<Customer?> GetByIdAsync(int id, CancellationToken ct = default)
        => _repo.GetByIdAsync(id, ct);

    public async Task<int> CreateAsync(Customer customer, CancellationToken ct = default)
    {
        var (isValid, error) = CustomerValidator.Validate(customer);
        if (!isValid)
            throw new ArgumentException(error);

        try
        {
            return await _repo.CreateAsync(customer, ct).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to create customer.");
            throw;
        }
    }

    public async Task UpdateAsync(Customer customer, CancellationToken ct = default)
    {
        var (isValid, error) = CustomerValidator.Validate(customer);
        if (!isValid)
            throw new ArgumentException(error);

        try
        {
            await _repo.UpdateAsync(customer, ct).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to update customer.");
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        try
        {
            await _repo.DeleteAsync(id, ct).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to delete customer.");
            throw;
        }
    }
}

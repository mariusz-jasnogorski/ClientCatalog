using ClientCatalog.Core.Logging;
using ClientCatalog.Core.Models;
using ClientCatalog.Core.Repositories;
using ClientCatalog.Core.Services;
using Moq;
using Xunit;

namespace ClientCatalog.Tests;

public sealed class CustomerServiceTests
{
    [Fact]
    public async Task CreateAsync_InvalidCustomer_ThrowsArgumentException()
    {
        var repo = new Mock<ICustomerRepository>(MockBehavior.Strict);
        var logger = new Mock<ILogger>();
        var sut = new CustomerService(repo.Object, logger.Object);

        var invalid = new Customer
        {
            Name = "",          // invalid
            Nip = "123"         // invalid
        };

        await Assert.ThrowsAsync<ArgumentException>(() => sut.CreateAsync(invalid));
    }

    [Fact]
    public async Task CreateAsync_ValidCustomer_CallsRepository()
    {
        var repo = new Mock<ICustomerRepository>(MockBehavior.Strict);
        var logger = new Mock<ILogger>();

        repo.Setup(r => r.CreateAsync(It.IsAny<Customer>(), default))
            .ReturnsAsync(123);

        var sut = new CustomerService(repo.Object, logger.Object);

        var valid = new Customer
        {
            Name = "Test Company",
            Nip = "1234567890",
            Address = "Street 1",
            Phone = "+48 123 456 789",
            Email = "test@example.com"
        };

        var id = await sut.CreateAsync(valid);

        Assert.Equal(123, id);
        repo.Verify(r => r.CreateAsync(It.IsAny<Customer>(), default), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ValidCustomer_CallsRepository()
    {
        var repo = new Mock<ICustomerRepository>(MockBehavior.Strict);
        var logger = new Mock<ILogger>();

        repo.Setup(r => r.UpdateAsync(It.IsAny<Customer>(), default))
            .Returns(Task.CompletedTask);

        var sut = new CustomerService(repo.Object, logger.Object);

        var valid = new Customer
        {
            Id = 1,
            Name = "Updated Company",
            Nip = "1234567890"
        };

        await sut.UpdateAsync(valid);

        repo.Verify(r => r.UpdateAsync(It.IsAny<Customer>(), default), Times.Once);
    }
}

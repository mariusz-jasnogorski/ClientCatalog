namespace ClientCatalog.Core.Models;

public sealed class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Nip { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}

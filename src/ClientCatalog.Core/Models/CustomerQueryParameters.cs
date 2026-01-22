namespace ClientCatalog.Core.Models;

public sealed class CustomerQueryParameters
{
    public string? Search { get; set; }

    public string SortBy { get; set; } = "Name";

    public bool SortDescending { get; set; }
}

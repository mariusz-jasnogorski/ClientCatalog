using System.Net.Mail;
using System.Text.RegularExpressions;
using ClientCatalog.Core.Models;

namespace ClientCatalog.Core.Validation;

public static class CustomerValidator
{
    private static readonly Regex NipRegex = new(@"^\d{10}$", RegexOptions.Compiled);

    public static (bool IsValid, string ErrorMessage) Validate(Customer customer)
    {
        if (customer is null)
            return (false, "Customer object is null.");

        if (string.IsNullOrWhiteSpace(customer.Name))
            return (false, "Name is required.");

        if (string.IsNullOrWhiteSpace(customer.Nip))
            return (false, "NIP is required.");

        if (!NipRegex.IsMatch(customer.Nip))
            return (false, "NIP should contain exactly 10 digits.");

        if (!string.IsNullOrWhiteSpace(customer.Email))
        {
            try
            {
                _ = new MailAddress(customer.Email);
            }
            catch
            {
                return (false, "Email has an invalid format.");
            }
        }

        if (!string.IsNullOrWhiteSpace(customer.Phone) && customer.Phone.Length > 30)
            return (false, "Phone is too long.");

        return (true, string.Empty);
    }
}

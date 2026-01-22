using System.ComponentModel;
using ClientCatalog.Core.Logging;
using ClientCatalog.Core.Models;
using ClientCatalog.Core.Services;
using ClientCatalog.WinForms.DesignTime;

namespace ClientCatalog.WinForms;

/// <summary>
/// Customer detail view used for both "Add" and "Edit".
/// 
/// Designer notes:
/// - WinForms Designer requires a parameterless constructor.
/// - We keep design-time instantiation free from DB/DI access.
/// </summary>
public partial class CustomerDetailForm : Form
{
    private ICustomerService _service = new DesignTimeCustomerService();
    private ILogger _logger = new DesignTimeLogger();

    private Customer? _customer; // null means "Add" mode.

    [EditorBrowsable(EditorBrowsableState.Never)]
    public CustomerDetailForm()
    {
        InitializeComponent();

        if (!IsInDesignMode())
            WireEvents();
    }

    public CustomerDetailForm(ICustomerService service, ILogger logger)
    {
        _service = service;
        _logger = logger;

        InitializeComponent();
        WireEvents();
    }

    public void SetCustomer(Customer? customer)
    {
        _customer = customer is null ? null : new Customer
        {
            Id = customer.Id,
            Name = customer.Name,
            Nip = customer.Nip,
            Address = customer.Address,
            Phone = customer.Phone,
            Email = customer.Email,
            CreatedAtUtc = customer.CreatedAtUtc,
            UpdatedAtUtc = customer.UpdatedAtUtc
        };

        Text = _customer is null ? "Add customer" : $"Edit customer (Id={_customer.Id})";
        FillFields();
    }

    private void WireEvents()
    {
        btnCancel.Click += (_, __) => { DialogResult = DialogResult.Cancel; Close(); };
        btnSave.Click += async (_, __) => await SaveAsync();

        AcceptButton = btnSave;
        CancelButton = btnCancel;
    }

    private void FillFields()
    {
        txtName.Text = _customer?.Name ?? string.Empty;
        txtNip.Text = _customer?.Nip ?? string.Empty;
        txtAddress.Text = _customer?.Address ?? string.Empty;
        txtPhone.Text = _customer?.Phone ?? string.Empty;
        txtEmail.Text = _customer?.Email ?? string.Empty;
    }

    private Customer ReadFromFields()
    {
        var c = _customer ?? new Customer();
        c.Name = txtName.Text.Trim();
        c.Nip = txtNip.Text.Trim();
        c.Address = txtAddress.Text.Trim();
        c.Phone = txtPhone.Text.Trim();
        c.Email = txtEmail.Text.Trim();
        return c;
    }

    private async Task SaveAsync()
    {
        try
        {
            var c = ReadFromFields();

            if (_customer is null)
                await _service.CreateAsync(c);
            else
                await _service.UpdateAsync(c);

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Save customer failed.");
            MessageBox.Show("Unable to save customer. Details were logged.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static bool IsInDesignMode()
        => LicenseManager.UsageMode == LicenseUsageMode.Designtime;
}

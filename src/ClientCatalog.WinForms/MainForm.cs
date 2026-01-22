using System.ComponentModel;
using System.ComponentModel.Design;
using ClientCatalog.Core.Logging;
using ClientCatalog.Core.Models;
using ClientCatalog.Core.Services;
using ClientCatalog.WinForms.DesignTime;
using Microsoft.Extensions.DependencyInjection;

namespace ClientCatalog.WinForms;

/// <summary>
/// Main view for the customer list.
/// 
/// Designer notes:
/// - WinForms Designer requires a parameterless constructor.
/// - The DI constructor is used at runtime.
/// - We keep design-time behavior completely "offline" (no DB, no DI usage that can crash designer).
/// </summary>
public partial class MainForm : Form
{
    // NOTE: These are not readonly because designer uses the parameterless constructor,
    // and we assign lightweight design-time stubs there.
    private ICustomerService _service = new DesignTimeCustomerService();
    private ILogger _logger = new DesignTimeLogger();
    private IServiceProvider _sp = new ServiceCollection().BuildServiceProvider();

    private readonly CustomerQueryParameters _query = new();

    /// <summary>
    /// Parameterless constructor used by WinForms Designer.
    /// Visual Studio creates the form using this constructor.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public MainForm()
    {
        InitializeComponent();

        // Do NOT wire events / load data in designer unless you really need it.
        // This prevents design-time exceptions caused by services.
        if (!IsInDesignMode())
            WireEvents();
    }

    /// <summary>
    /// Runtime constructor (DI).
    /// </summary>
    public MainForm(ICustomerService service, ILogger logger, IServiceProvider sp)
    {
        _service = service;
        _logger = logger;
        _sp = sp;

        InitializeComponent();
        WireEvents();
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (IsInDesignMode())
            return;

        await ReloadGridAsync();
    }

    private void WireEvents()
    {
        // Debounce search typing: we don't want to reload on every keystroke instantly.
        var timer = new System.Windows.Forms.Timer { Interval = 350 };
        timer.Tick += async (_, __) =>
        {
            timer.Stop();
            await ReloadGridAsync();
        };

        txtSearch.TextChanged += (_, __) =>
        {
            timer.Stop();
            timer.Start();
        };

        cmbSortBy.SelectedIndexChanged += async (_, __) => await ReloadGridAsync();
        chkDesc.CheckedChanged += async (_, __) => await ReloadGridAsync();
        btnRefresh.Click += async (_, __) => await ReloadGridAsync();

        btnAdd.Click += async (_, __) => await AddCustomerAsync();
        btnEdit.Click += async (_, __) => await EditSelectedCustomerAsync();
        btnDelete.Click += async (_, __) => await DeleteSelectedCustomerAsync();

        gridCustomers.CellDoubleClick += async (_, __) => await EditSelectedCustomerAsync();
    }

    private async Task ReloadGridAsync()
    {
        try
        {
            _query.Search = string.IsNullOrWhiteSpace(txtSearch.Text) ? null : txtSearch.Text.Trim();
            _query.SortBy = cmbSortBy.SelectedItem?.ToString() ?? "Name";
            _query.SortDescending = chkDesc.Checked;

            var rows = await _service.GetAsync(_query);
            gridCustomers.DataSource = rows;

            gridCustomers.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            // Allow header text to wrap onto multiple lines
            gridCustomers.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Make header height adjust automatically to wrapped text
            gridCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to reload grid.");
            MessageBox.Show("Failed to load customers. Details were logged.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private Customer? GetSelectedCustomer()
    {
        if (gridCustomers.CurrentRow?.DataBoundItem is Customer c)
            return c;

        return null;
    }

    private async Task AddCustomerAsync()
    {
        try
        {
            using var form = ActivatorUtilities.CreateInstance<CustomerDetailForm>(_sp);
            form.SetCustomer(null);

            if (form.ShowDialog(this) == DialogResult.OK)
                await ReloadGridAsync();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Add customer failed.");
            MessageBox.Show("Unable to add customer. Details were logged.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task EditSelectedCustomerAsync()
    {
        var selected = GetSelectedCustomer();
        if (selected is null)
        {
            MessageBox.Show("Please select a customer first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            using var form = ActivatorUtilities.CreateInstance<CustomerDetailForm>(_sp);

            var customer = await _service.GetByIdAsync(selected.Id);
            if (customer is null)
            {
                MessageBox.Show("Customer not found (it may have been deleted).", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await ReloadGridAsync();
                return;
            }

            form.SetCustomer(customer);

            if (form.ShowDialog(this) == DialogResult.OK)
                await ReloadGridAsync();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Edit customer failed.");
            MessageBox.Show("Unable to edit customer. Details were logged.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task DeleteSelectedCustomerAsync()
    {
        var selected = GetSelectedCustomer();
        if (selected is null)
        {
            MessageBox.Show("Please select a customer first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var confirm = MessageBox.Show(
            $"Delete customer '{selected.Name}' (NIP: {selected.Nip})?",
            "Confirm delete",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (confirm != DialogResult.Yes)
            return;

        try
        {
            await _service.DeleteAsync(selected.Id);
            await ReloadGridAsync();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Delete customer failed.");
            MessageBox.Show("Unable to delete customer. Details were logged.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Reliable design-time detection for WinForms:
    /// - DesignMode can be false in constructors.
    /// - LicenseManager works much better for Designer.
    /// </summary>
    private static bool IsInDesignMode()
        => LicenseManager.UsageMode == LicenseUsageMode.Designtime;

    private void gridCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
}

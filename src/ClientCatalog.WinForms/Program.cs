using ClientCatalog.Core.Logging;
using ClientCatalog.Core.Repositories;
using ClientCatalog.Core.Services;
using ClientCatalog.Data.Db;
using ClientCatalog.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ClientCatalog.WinForms;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        var logger = CreateLogger();

        Application.ThreadException += (_, e) =>
        {
            logger.Error(e.Exception, "Unhandled UI thread exception.");
            MessageBox.Show("An unexpected error occurred. Details were logged.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        };

        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
        {
            if (e.ExceptionObject is Exception ex)
                logger.Error(ex, "Unhandled domain exception.");
        };

        using var services = BuildServiceProvider(logger);

        try
        {
            var initializer = services.GetRequiredService<DbInitializer>();
            initializer.EnsureCreatedAndSeededAsync().GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Database initialization failed.");
            MessageBox.Show("Database initialization failed. See log file for details.", "Startup error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var mainForm = services.GetRequiredService<MainForm>();
        Application.Run(mainForm);
    }

    private static ServiceProvider BuildServiceProvider(ILogger logger)
    {
        var sc = new ServiceCollection();

        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = config.GetConnectionString("ClientCatalogDb")
            ?? throw new InvalidOperationException("Missing connection string 'ClientCatalogDb'.");

        sc.AddSingleton(logger);
        sc.AddSingleton(new DbConnectionFactory(connectionString, logger));

        sc.AddSingleton<ICustomerRepository, DapperCustomerRepository>();
        sc.AddSingleton<ICustomerService, CustomerService>();

        sc.AddSingleton<DbInitializer>();

        sc.AddTransient<MainForm>();
        sc.AddTransient<CustomerDetailForm>();

        return sc.BuildServiceProvider();
    }

    private static ILogger CreateLogger()
    {
        var baseDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "ClientCatalog",
            "logs");

        var logPath = Path.Combine(baseDir, "app.log");
        return new FileLogger(logPath);
    }
}

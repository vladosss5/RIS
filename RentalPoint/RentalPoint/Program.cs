using System.IO;
using Microsoft.Extensions.Configuration;
using Services.Implementations;
using Services.Interfaces;

namespace RentalPoint;

using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using System;

internal class Program
{
    public static IServiceProvider? ServiceProvider { get; private set; }

    [STAThread]
    public static void Main(string[] args)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    private static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<MainWindow>();
        services.AddTransient<AuthorizationWindow>();
        
        services.AddSingleton<IAuthService, AuthService>();
        
        services.AddSingleton<IConfiguration>(_ => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build());
    }
}
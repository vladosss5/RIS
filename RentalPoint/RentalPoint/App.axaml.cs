using System;
using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Data;
using Microsoft.EntityFrameworkCore;

namespace RentalPoint;

public class App : Application
{
    public static DBContext DbContext { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
            
        // Инициализация БД
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var dbFolder = Path.Combine(appData, "YourAppName");
        Directory.CreateDirectory(dbFolder);
        var dbPath = Path.Combine(dbFolder, "app.db");
            
        var options = new DbContextOptionsBuilder<DBContext>()
            .UseSqlite($"Data Source={dbPath}")
            .Options;
            
        DbContext = new DBContext(options);
        DbContext.Database.Migrate();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
using Avalonia;
using System;
using System.IO;
using System.Threading.Tasks;

namespace App;

public static class Program
{
    public static void Main(string[] args) 
        => BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .With<Win32PlatformOptions>(o => 
            {
                o.RenderingMode = new[] { Win32RenderingMode.Direct3D11 };
                o.AllowEglInitialization = true;
            });
}
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace App.Services;

public static class AuthService
{
    private static readonly IConfiguration Configuration;
    
    static AuthService()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public static bool IsFirstRun()
    {
        return string.IsNullOrEmpty(Configuration["Auth:PasswordHash"]);
    }

    public static void SetPassword(string password)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        
        config["Auth:PasswordHash"] = HashPassword(password);
        
        File.WriteAllText(
            "appsettings.json", 
            Newtonsoft.Json.JsonConvert.SerializeObject(
                config.GetChildren().ToDictionary(c => c.Key, c => c.Value),
                Newtonsoft.Json.Formatting.Indented
            ),
            Encoding.UTF8
        );
    }

    public static bool VerifyPassword(string password)
    {
        var storedHash = Configuration["Auth:PasswordHash"];
        return storedHash == HashPassword(password);
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }
}
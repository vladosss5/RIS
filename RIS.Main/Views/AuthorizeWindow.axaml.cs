using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using RIS.Core.Models;

namespace RIS.Main.Views;

public partial class AuthorizeWindow : Window
{
    private AuthSettings _settings;
    private const string SettingsFile = "appsettings.json";
    
    public AuthorizeWindow()
    {
        InitializeComponent();
        LoadSettings();
            
        LoginButton.Click += LoginButton_Click;
        ChangePasswordButton.Click += ChangePasswordButton_Click;
    }
    
    private void LoadSettings()
    {
        try
        {
            if (File.Exists(SettingsFile))
            {
                var json = File.ReadAllText(SettingsFile);
                _settings = JsonSerializer.Deserialize<AuthSettings>(json);
            }
            else
            {
                _settings = new AuthSettings
                {
                    Login = "admin",
                    Password = Encrypt("admin123"),
                    IsFirstLogin = true
                };
                SaveSettings();
            }
        }
        catch
        {
            _settings = new AuthSettings
            {
                Login = "admin",
                Password = Encrypt("admin123"),
                IsFirstLogin = true
            };
        }
    }

    private void SaveSettings()
    {
        var json = JsonSerializer.Serialize(_settings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(SettingsFile, json);
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        var login = LoginTextBox.Text;
        var password = PasswordTextBox.Text;
        
        if (login != _settings.Login)
        {
            ErrorMessageText.Text = "Неверный логин";
            return;
        }
        
        var decryptedPassword = Decrypt(_settings.Password);
        if (password != decryptedPassword)
        {
            ErrorMessageText.Text = "Неверный пароль";
            return;
        }
        
        if (_settings.IsFirstLogin)
        {
            ChangePasswordPanel.IsVisible = true;
            LoginButton.IsVisible = false;
            ErrorMessageText.Text = "Это ваш первый вход. Пожалуйста, смените пароль.";
        }
        else
        {
            Close(true);
        }
    }

    private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
    {
        var newPassword = NewPasswordTextBox.Text;
        var confirmPassword = ConfirmPasswordTextBox.Text;
        
        if (string.IsNullOrEmpty(newPassword))
        {
            ErrorMessageText.Text = "Новый пароль не может быть пустым";
            return;
        }
        
        if (newPassword != confirmPassword)
        {
            ErrorMessageText.Text = "Пароли не совпадают";
            return;
        }
        
        _settings.Password = Encrypt(newPassword);
        _settings.IsFirstLogin = false;
        SaveSettings();
        
        Close(true);
    }

    #region Encryption
    
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("YourSecretKey123");
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("YourIV4567890123");

    private static string Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.Key = Key;
        aes.IV = IV;

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
        using (var sw = new StreamWriter(cs))
        {
            sw.Write(plainText);
        }
        return Convert.ToBase64String(ms.ToArray());
    }

    private static string Decrypt(string cipherText)
    {
        using var aes = Aes.Create();
        aes.Key = Key;
        aes.IV = IV;

        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        var buffer = Convert.FromBase64String(cipherText);
        using var ms = new MemoryStream(buffer);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        return sr.ReadToEnd();
    }
    
    #endregion
}
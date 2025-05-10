using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Extensions.DependencyInjection;
using Services.Implementations;

namespace RentalPoint
{
    public partial class AuthorizationWindow : Window
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthorizationWindow(
            IAuthService authService, 
            IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            bool isAuthenticated = await _authService.AuthenticateAsync(LoginTextBox.Text, PasswordTextBox.Text);
            
            if (isAuthenticated)
            {
                var mainWindow = Program.ServiceProvider!.GetRequiredService<MainWindow>();
                mainWindow.Show();
                Close();
            }
        }

        private string DecryptPassword(string encryptedPassword)
        {
            try
            {
                byte[] key = "YourSecretKey123"u8.ToArray();
                byte[] iv = "YourIV4567890123"u8.ToArray();

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(encryptedPassword)))
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
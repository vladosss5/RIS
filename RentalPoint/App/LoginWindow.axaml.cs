using System.Threading.Tasks;
using App.Services;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace App;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        
        if (AuthService.IsFirstRun())
            ShowFirstRunMessage();
    }

    private async Task ShowFirstRunMessage()
    {
       var message =  MessageBoxManager
            .GetMessageBoxStandard(
                "Первичная настройка",
                "Приветствуем! Это первый запуск программы. Установите пароль.",
                ButtonEnum.Ok,
                MsBox.Avalonia.Enums.Icon.Info);

       await message.ShowAsync();
    }
    
    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        var password = PasswordBox.Text;
        
        if (AuthService.IsFirstRun())
        {
            AuthService.SetPassword(password);
            await ShowSuccessMessage("Пароль успешно установлен!");
            Close();
        }
        else
        {
            if (AuthService.VerifyPassword(password))
            {
                Close();
            }
            else
            {
                await ShowErrorMessage("Неверный пароль!");
            }
        }
    }

    private async Task ShowSuccessMessage(string text)
    {
        var message = MessageBoxManager
            .GetMessageBoxStandard(
                "Успех",
                text,
                ButtonEnum.Ok, 
                MsBox.Avalonia.Enums.Icon.Success);
        await message.ShowAsync();
    }

    private async Task ShowErrorMessage(string text)
    {
        var message = MessageBoxManager
            .GetMessageBoxStandard(
                "Ошибка",
                text,
                ButtonEnum.Ok,
                MsBox.Avalonia.Enums.Icon.Error);

        await message.ShowAsync();
    }
}
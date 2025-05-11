using System.Threading.Tasks;
using App.Services;
using Avalonia.Controls;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace App;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        if (!AuthService.IsFirstRun() && !AuthService.VerifyPassword(""))
        {
            _ = ShowErrorMessage("Доступ запрещен!");
            Close();
        }
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
using Avalonia.Controls;
using RIS.Data;

namespace RIS.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        using var con = new DataContext();
    }
}
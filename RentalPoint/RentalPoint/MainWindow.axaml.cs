using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace RentalPoint;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Хуй");
    }
}
using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Core.DBModels;
using Data;

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
        using var context = new DBContext();
        context.Clients.Add(new Client()
        {

        });
    }
}
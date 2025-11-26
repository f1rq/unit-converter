using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.ComponentModel;

namespace unit_converter;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // ConvertButton += OnConvert;
        ResetButton.Click += OnReset;
    }

    private void OnReset(object? sender, RoutedEventArgs e)
    {
        FromValue.Text = "";
        ResultValue.Text = "To...";
    }
}
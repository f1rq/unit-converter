using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using unit_converter.Units;

namespace unit_converter;

public partial class ConverterPage : UserControl
{
    private readonly UnitConverter _converter;
    private readonly string _category;
    private readonly MainWindow? _mainWindow;
    
    public ConverterPage()
    {
        InitializeComponent();
        _converter = new UnitConverter();
        _category = "Length";

        ConvertButton.Click += OnConvert;
        ResetButton.Click += OnReset;
        UnitSwapBtn.Click += SwapUnits;
        BackButton.Click += (_, _) => _mainWindow?.ShowCategoryPage();
        TitleText.Text = _category;
        
        UpdateUnits();
    }
    
    public ConverterPage(MainWindow? mainWindow, string category) : this()
    {
        _mainWindow = mainWindow;
        _category = category;
        TitleText.Text = _category;
        UpdateUnits();
    }

    private void UpdateUnits()
    {
        if (_category == "Currency")
        {
            FromUnitComboBox.IsVisible = false;
            ToUnitComboBox.IsVisible = false;

            FromCurrencyAutoCompleteBox.IsVisible = true;
            ToCurrencyAutoCompleteBox.IsVisible = true;

            var currencies = CurrencyRates.GetAvailableCurrencies().ToList();
            FromCurrencyAutoCompleteBox.ItemsSource = currencies;
            ToCurrencyAutoCompleteBox.ItemsSource = currencies;
        }
        
        var units = _converter.GetUnits(_category);
        FromUnitComboBox.Items.Clear();
        ToUnitComboBox.Items.Clear();

        foreach (var unit in units)
        {
            FromUnitComboBox.Items.Add(new ComboBoxItem { Content = unit });
            ToUnitComboBox.Items.Add(new ComboBoxItem { Content = unit });
        }

        FromUnitComboBox.SelectedIndex = 0;
        ToUnitComboBox.SelectedIndex = 1;
    }
    
    // Reset input and output fields
    private void OnReset(object? sender, RoutedEventArgs e)
    {
        FromValue.Text = "";
        ResultValue.Text = "0";
        FromUnitComboBox.SelectedIndex = 0;
        ToUnitComboBox.SelectedIndex = 1;
    }
    
    // Update units when category changes
    private void OnCategoryChanged(object? sender, SelectionChangedEventArgs e)
    {
        UpdateUnits();
    }
    
    // Perform conversion
    private void OnConvert(object? sender, RoutedEventArgs e)
    {
        
        if (!double.TryParse(FromValue.Text, out double fromValue))
        {
            ResultValue.Text = "Invalid input";
            return;
        }

        string fromUnit;
        string toUnit;

        if (_category == "Currency")
        {
            fromUnit = (FromCurrencyAutoCompleteBox.Text ?? string.Empty).Trim().ToUpperInvariant();
            toUnit = (ToCurrencyAutoCompleteBox.Text ?? string.Empty).Trim().ToUpperInvariant();
        }
        else
        {
            fromUnit = (FromUnitComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? string.Empty;
            toUnit = (ToUnitComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? string.Empty;
        }

        if (string.IsNullOrWhiteSpace(fromUnit) || string.IsNullOrWhiteSpace(toUnit))
        {
            ResultValue.Text = "Select units";
            return;
        }

        try
        {
            double result = _converter.Convert(_category, fromUnit, toUnit, fromValue);
            ResultValue.Text = Math.Abs(result) >= 0.01 ? result.ToString("F2") : result.ToString("G");
        }
        catch (KeyNotFoundException)
        {
            ResultValue.Text = "Invalid input";
        }
        catch (Exception ex)
        {
            ResultValue.Text = ex.Message;
        }
    }

    private void SwapUnits(object? sender, RoutedEventArgs e)
    {
        var fromVal = FromValue.Text;
        var resultVal = ResultValue.Text;
        FromValue.Text = resultVal;
        ResultValue.Text = fromVal;

        if (_category == "Currency")
        {
            (FromCurrencyAutoCompleteBox.Text, ToCurrencyAutoCompleteBox.Text) = (ToCurrencyAutoCompleteBox.Text, FromCurrencyAutoCompleteBox.Text);
        }
        else
        {
            var fromIdx = FromUnitComboBox.SelectedIndex;
            var toIdx = ToUnitComboBox.SelectedIndex;
            FromUnitComboBox.SelectedIndex = toIdx;
            ToUnitComboBox.SelectedIndex = fromIdx;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
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
        FromValue.KeyDown += OnFromValueKeyDown;
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
        FromUnitComboBox.Items.Clear();
        ToUnitComboBox.Items.Clear();
        
        if (_category == "Currency")
        {
            FromUnitComboBox.IsVisible = false;
            ToUnitComboBox.IsVisible = false;
            FromCurrencyAutoCompleteBox.IsVisible = true;
            ToCurrencyAutoCompleteBox.IsVisible = true;

            var currencies = CurrencyRates.GetAvailableCurrencies().ToList();
            FromCurrencyAutoCompleteBox.ItemsSource = currencies;
            ToCurrencyAutoCompleteBox.ItemsSource = currencies;
            return;
        }
        
        FromUnitComboBox.IsVisible = true;
        ToUnitComboBox.IsVisible = true;
        FromCurrencyAutoCompleteBox.IsVisible = false;
        ToCurrencyAutoCompleteBox.IsVisible = false;

        if (_category == "Data")
        {
            var units = DataUnits.Units.Values;

            FromUnitComboBox.ItemsSource = units;
            ToUnitComboBox.ItemsSource = units;
        }
        else
        {
            foreach (var unit in _converter.GetUnits(_category))
            {
                FromUnitComboBox.Items.Add(new ComboBoxItem { Content = unit });
                ToUnitComboBox.Items.Add(new ComboBoxItem { Content = unit });
            }
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

    private static string FormatResult(double result)
    {
        double absResult = Math.Abs(result);

        if (absResult < 1.0)
        {
            return result.ToString("0.########", CultureInfo.InvariantCulture);
        }

        double rounded = Math.Round(result, 2);
        return (Math.Abs(rounded - Math.Floor(rounded)) < 0.0001)
            ? rounded.ToString("0", CultureInfo.InvariantCulture)
            : rounded.ToString("0.00", CultureInfo.InvariantCulture);
    }
    
    // Perform conversion
    private void OnConvert(object? sender, RoutedEventArgs e)
    {
        
        if (!double.TryParse(
                FromValue.Text,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out double fromValue))
        {
            ResultValue.Text = "Invalid input";
            return;
        }

        if (_category == "Data")
        {
            var from = FromUnitComboBox.SelectedItem as DataUnit;
            var to = ToUnitComboBox.SelectedItem as DataUnit;

            if (from == null || to == null)
            {
                ResultValue.Text = "Select units";
                return;
            }

            double result = fromValue * from.Factor / to.Factor;
            ResultValue.Text = FormatResult(result);
            
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
            ResultValue.Text = FormatResult(result);
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

    private void OnFromValueKeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
    {
        if (e.Key == Avalonia.Input.Key.Enter)
        {
            OnConvert(sender, new RoutedEventArgs());
            e.Handled = true;
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
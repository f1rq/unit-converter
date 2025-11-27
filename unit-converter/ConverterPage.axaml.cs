using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace unit_converter;

public partial class ConverterPage : UserControl
{
    private readonly UnitConverter _converter;
    private readonly string _category;
    private readonly MainWindow _mainWindow;
    
    public ConverterPage()
    {
        InitializeComponent();
        _mainWindow = null;
        _converter = new UnitConverter();
        _category = "Length";

        ConvertButton.Click += OnConvert;
        ResetButton.Click += OnReset;
        UnitSwapBtn.Click += SwapUnits;
        BackButton.Click += (_, __) => _mainWindow?.ShowCategoryPage();
        TitleText.Text = $"{_category}";
        
        UpdateUnits();
    }
    
    public ConverterPage(MainWindow? mainWindow, string category) : this()
    {
        _mainWindow = mainWindow;
        _category = category;
        
        TitleText.Text = $"{_category}";
        
        UpdateUnits();
    }

    private void UpdateUnits()
    {
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
        
        var fromUnit = (FromUnitComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
        var toUnit = (ToUnitComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

        if (fromUnit == null || toUnit == null)
        {
            ResultValue.Text = "Select units";
            return;
        }

        double result = _converter.Convert(_category, fromUnit, toUnit, fromValue);
        ResultValue.Text = result.ToString("F2");
    }

    private void SwapUnits(object? sender, RoutedEventArgs e)
    {
        var fromIdx = FromUnitComboBox.SelectedIndex;
        var toIdx = ToUnitComboBox.SelectedIndex;
        
        var fromVal = FromValue.Text;
        var resultVal = ResultValue.Text;
        FromValue.Text = resultVal;
        ResultValue.Text = fromVal;
        
        FromUnitComboBox.SelectedIndex = toIdx;
        ToUnitComboBox.SelectedIndex = fromIdx;
        
    }
}
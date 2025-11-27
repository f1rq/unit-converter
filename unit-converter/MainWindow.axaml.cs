using Avalonia.Controls;
using Avalonia.Interactivity;

namespace unit_converter;

public partial class MainWindow : Window
{
    private readonly UnitConverter _converter;
    public MainWindow()
    {
        InitializeComponent();

        _converter = new UnitConverter();
        
        ConvertButton.Click += OnConvert;
        ResetButton.Click += OnReset;
        CategoryComboBox.SelectionChanged += OnCategoryChanged;
        UnitSwapBtn.Click += SwapUnits;
    }

    // Reset input and output fields
    private void OnReset(object? sender, RoutedEventArgs e)
    {
        FromValue.Text = "";
        ResultValue.Text = "0";
        CategoryComboBox.SelectedIndex = 0;
        FromUnitComboBox.SelectedIndex = 0;
        ToUnitComboBox.SelectedIndex = 1;
    }
    
    // Update units when category changes
    private void OnCategoryChanged(object? sender, SelectionChangedEventArgs e)
    {
        var selectedCategory = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
        if (selectedCategory == null) return;
        
        var units = _converter.GetUnits(selectedCategory);

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
    
    // Perform conversion
    private void OnConvert(object? sender, RoutedEventArgs e)
    {
        if (!double.TryParse(FromValue.Text, out double fromValue))
        {
            ResultValue.Text = "Invalid input";
            return;
        }
        
        var category = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
        var fromUnit = (FromUnitComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
        var toUnit = (ToUnitComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

        if (category == null || fromUnit == null || toUnit == null)
        {
            ResultValue.Text = "Select units";
            return;
        }

        double result = _converter.Convert(category, fromUnit, toUnit, fromValue);
        ResultValue.Text = result.ToString("F2");
    }

    private void SwapUnits(object? sender, RoutedEventArgs e)
    {
        var fromIdx = FromUnitComboBox.SelectedIndex;
        var toIdx = ToUnitComboBox.SelectedIndex;
        FromUnitComboBox.SelectedIndex = toIdx;
        ToUnitComboBox.SelectedIndex = fromIdx;
    }
}
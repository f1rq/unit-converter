using Avalonia.Controls;

namespace unit_converter;

public partial class CategoryPage : UserControl
{
    private readonly UnitConverter _converter;
    private readonly MainWindow? _mainWindow;

    public CategoryPage()
    {
        InitializeComponent();
        _mainWindow = null;
        _converter = new UnitConverter();

        PopulateCategoryButtons();
    }
    public CategoryPage(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        _converter = new UnitConverter();

        PopulateCategoryButtons();
    }

    private void PopulateCategoryButtons()
    {
        CatBtnStackPanel.Children.Clear();

        foreach (var category in _converter.GetCategories())
        {
            var button = new Button { Content = category, Height = 100, Width = 100};
            button.Click += (_, __) => _mainWindow?.ShowConverter(category);

            if (_mainWindow == null)
                button.IsEnabled = true;

            CatBtnStackPanel.Children.Add(button);
        }
    }
}
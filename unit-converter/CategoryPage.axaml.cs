using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

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
            var button = new Button { Height = 100, Width = 100, Padding = new Thickness(20) };

            var pathData = GetPathDataForCategory(category);
            
            var path = new Path
            {
                Data = Geometry.Parse(pathData),
                StrokeThickness = 1.5,
                Stroke = Brushes.White,
                Stretch = Stretch.Uniform,
                StrokeLineCap = PenLineCap.Round,
                Margin = new Thickness(2)
            };

            var viewbox = new Viewbox
            {
                Child = path,
                Margin = new Thickness(5)
            };

            button.Content = viewbox;
            ToolTip.SetTip(button, category);
            button.Click += (_, _) => _mainWindow?.ShowConverter(category);

            if (_mainWindow == null)
                button.IsEnabled = true;

            CatBtnStackPanel.Children.Add(button);
        }
    }

    private string GetPathDataForCategory(string category)
    {
        return category switch
        {
            "Length" =>
                "M17 3l4 4l-14 14l-4 -4z M16 7l-1.5 -1.5 M13 10l-1.5 -1.5 M10 13l-1.5 -1.5 M7 16l-1.5 -1.5",
            "Weight" =>
                "M12 6m-3 0a3 3 0 1 0 6 0a3 3 0 1 0 -6 0 M6.835 9h10.33a1 1 0 0 1 .984 .821l1.637 9a1 1 0 0 1 -.984 1.179h-13.604a1 1 0 0 1 -.984 -1.179l1.637 -9a1 1 0 0 1 .984 -.821z",
            "Area" =>
                "M4 19l16 0 M4 15l4 -6l4 2l4 -5l4 4l0 5l-16 0",
            "Currency" =>
                "M16.7 8a3 3 0 0 0 -2.7 -2h-4a3 3 0 0 0 0 6h4a3 3 0 0 1 0 6h-4a3 3 0 0 1 -2.7 -2 M12 3v3m0 12v3",
            _ => "M0 0h24v24H0z"
        };
    }
}
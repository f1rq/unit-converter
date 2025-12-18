using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
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
            var button = new Button { Height = 100, Width = 100, Padding = new Thickness(15) };

            var pathData = GetPathDataForCategory(category);
            
            var path = new Path
            {
                Data = Geometry.Parse(pathData),
                StrokeThickness = 0.8,
                Stroke = Brushes.White,
                Stretch = Stretch.Uniform,
                StrokeLineCap = PenLineCap.Round,
                Margin = new Thickness(2)
            };

            var viewbox = new Viewbox
            {
                Child = path,
                Height = 50,
                Margin = new Thickness(5)
            };

            var textBlock = new TextBlock
            {
                Text = category,
                TextAlignment = TextAlignment.Center,
                FontSize = 12,
                Margin = new Thickness(0, 5, 0 ,0)
            };

            var stackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Center,
                Children = { viewbox, textBlock }
            };
            
            button.Content = stackPanel;
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
            "Volume" =>
                "M7.502 19.423c2.602 2.105 6.395 2.105 8.996 0c2.602 -2.105 3.262 -5.708 1.566 -8.546l-4.89 -7.26c-.42 -.625 -1.287 -.803 -1.936 -.397a1.376 1.376 0 0 0 -.41 .397l-4.893 7.26c-1.695 2.838 -1.035 6.441 1.567 8.546z",
            "Currency" =>
                "M16.7 8a3 3 0 0 0 -2.7 -2h-4a3 3 0 0 0 0 6h4a3 3 0 0 1 0 6h-4a3 3 0 0 1 -2.7 -2 M12 3v3m0 12v3",
            "Data" =>
                "M11 10v-5h-1m8 14v-5h-1 M15 5m0 .5a.5 .5 0 0 1 .5 -.5h2a.5 .5 0 0 1 .5 .5v4a.5 .5 0 0 1 -.5 .5h-2a.5 .5 0 0 1 -.5 -.5z M10 14m0 .5a.5 .5 0 0 1 .5 -.5h2a.5 .5 0 0 1 .5 .5v4a.5 .5 0 0 1 -.5 .5h-2a.5 .5 0 0 1 -.5 -.5z M6 10h.01m-.01 9h.01",
            "Time" =>
                "M12 12m-9 0a9 9 0 1 0 18 0a9 9 0 1 0 -18 0 M12 12h3.5 M12 7v5",
            "Pressure" =>
                "M8 11a4 4 0 1 1 8 0v5h-8z M8 16v3a2 2 0 0 0 2 2h4a2 2 0 0 0 2 -2v-3 M9 4h6 M12 7v-3 M8 4m-1 0a1 1 0 1 0 2 0a1 1 0 1 0 -2 0",
            _ => "M0 0h24v24H0z"
        };
    }
}
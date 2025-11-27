using Avalonia.Controls;

namespace unit_converter;

public partial class MainWindow : Window
{
    public MainWindow(string? initialCategory = null)
    {
        InitializeComponent();
        MainContent.Content = new CategoryPage(this);
    }

    public void ShowCategoryPage()
    {
        MainContent.Content = new CategoryPage(this);
    }
    public void ShowConverter(string category)
    {
        MainContent.Content = new ConverterPage(this, category);
    }
}
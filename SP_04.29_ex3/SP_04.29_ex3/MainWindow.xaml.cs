using System.Windows;

namespace SP_04._29_ex3;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnButtonClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Кнопка натиснута!", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
using System;
using System.Threading;
using System.Windows;

namespace SP_04._29_ex3;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static Mutex _mutex;

    protected override void OnStartup(StartupEventArgs e)
    {
        const string appName = "Global\\SP_04.29_ex3_SingleInstanceApp";
        bool isNewInstance;

        _mutex = new Mutex(true, appName, out isNewInstance);

        if (!isNewInstance)
        {
            MessageBox.Show("Додаток вже запущений!", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
            Environment.Exit(0);
        }

        base.OnStartup(e);
    }
}
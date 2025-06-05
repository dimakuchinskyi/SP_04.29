using System;
using System.Threading;
using System.Windows;

namespace SP_04._29
{
    public partial class MainWindow : Window
    {
        private static Mutex mutex = new Mutex();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void StartThreads_Click(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Clear();

            await Task.Run(() => DisplayAscending());
            await Task.Run(() => DisplayDescending());

            OutputTextBox.AppendText("Всі потоки завершені.\n");
        }

        private void DisplayAscending()
        {
            mutex.WaitOne();
            for (int i = 0; i <= 20; i++)
            {
                Dispatcher.Invoke(() => OutputTextBox.AppendText($"Перший потік: {i}\n"));
                Thread.Sleep(100);
            }
            mutex.ReleaseMutex();
        }

        private void DisplayDescending()
        {
            mutex.WaitOne();
            for (int i = 10; i >= 0; i--)
            {
                Dispatcher.Invoke(() => OutputTextBox.AppendText($"Другий потік: {i}\n"));
                Thread.Sleep(100); 
            }
            mutex.ReleaseMutex();
        }
    }
}
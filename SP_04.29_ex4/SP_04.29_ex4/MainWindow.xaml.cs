using System;
using System.Threading;
using System.Windows;

namespace SP_04._29_ex4
{
    public partial class MainWindow : Window
    {
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(3);
        private static readonly Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartThreads_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                int threadNumber = i + 1;
                Thread thread = new Thread(() => PerformTask(threadNumber));
                thread.Start();
            }
        }

        private void PerformTask(int threadNumber)
        {
            semaphore.Wait();

            try
            {
                Dispatcher.Invoke(() => 
                    Console.WriteLine($"Потік {threadNumber} почав роботу."));
                for (int i = 0; i < 5; i++)
                {
                    Dispatcher.Invoke(() => 
                        Console.WriteLine($"Потік {threadNumber}: {random.Next(1, 101)}"));
                    Thread.Sleep(500);
                }
                Dispatcher.Invoke(() => 
                    Console.WriteLine($"Потік {threadNumber} завершив роботу."));
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
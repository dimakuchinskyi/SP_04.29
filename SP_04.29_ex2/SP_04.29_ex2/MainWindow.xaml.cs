using System;
using System.Linq;
using System.Threading;
using System.Windows;

namespace SP_04._29_ex2
{
    public partial class MainWindow : Window
    {
        private int[] _dataArray;
        private Mutex _mutex;

        public MainWindow()
        {
            InitializeComponent();
            _mutex = new Mutex();
            _dataArray = new int[] { 1, 2, 3, 4, 5 };

            StartThreads();
        }

        private void StartThreads()
        {
            Thread modifyThread = new Thread(ModifyArray);
            Thread findMaxThread = new Thread(FindMaxValue);

            modifyThread.Start();
            findMaxThread.Start();
        }

        private void ModifyArray()
        {
            _mutex.WaitOne();
            Random random = new Random();
            for (int i = 0; i < _dataArray.Length; i++)
            {
                _dataArray[i] += random.Next(1, 10); // Додаємо випадкове число
            }
            Console.WriteLine("Modified Array: " + string.Join(", ", _dataArray));
            _mutex.ReleaseMutex();
        }

        private void StartThreadsButton_Click(object sender, RoutedEventArgs e)
        {
            Thread modifyThread = new Thread(() =>
            {
                ModifyArray();
                Dispatcher.Invoke(() =>
                {
                    ModifiedArrayText.Text = "Модифікований масив: " + string.Join(", ", _dataArray);
                });
            });

            Thread findMaxThread = new Thread(() =>
            {
                FindMaxValue();
                Dispatcher.Invoke(() =>
                {
                    MaxValueText.Text = "Максимальне значення: " + _dataArray.Max();
                });
            });

            modifyThread.Start();
            findMaxThread.Start();
        }

        private void FindMaxValue()
        {
            _mutex.WaitOne();
            int maxValue = _dataArray.Max();
            Console.WriteLine("Maximum Value: " + maxValue);
            MessageBox.Show($"Maximum Value: {maxValue}");
            _mutex.ReleaseMutex();
        }
    }
}
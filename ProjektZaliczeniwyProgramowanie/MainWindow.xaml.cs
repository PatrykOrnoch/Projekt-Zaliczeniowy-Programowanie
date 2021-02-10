using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ObslugaHurtowniiApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateDateTime();
        }

        private void BtnExitApp_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show(
                "Czy na pewno chcesz wyjść ?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes) Close();
        }

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Produkty w bazie");
        }

        private void BtnNewOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnStorage_Click(object sender, RoutedEventArgs e)
        {

        }

        public void UpdateDateTime()
        {
            DispatcherTimer clock = new DispatcherTimer();

            clock.Interval = new TimeSpan(0, 0, 1);
            clock.Tick += TimerTick;
            clock.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            DayOfWeek day = DateTime.Now.DayOfWeek;

            lblDateTime.Content = DateTime.Now.ToLongTimeString();
        }
    }
}

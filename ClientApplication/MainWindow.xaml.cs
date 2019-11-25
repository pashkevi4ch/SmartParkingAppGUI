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
using SmartParkingApp;

namespace ClientApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ParkingManager pm;
        public MainWindow()
        {
            InitializeComponent();
            pm = ParkingManager.GetParkingManager();
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            if (Sign_in.IsChecked is true)
            {
                var logWindow = new LoginWindow(pm);
                Hide();
                logWindow.Show();
                Close();
            }
            else if (Sign_up.IsChecked is true)
            {
                var regWindow = new RegistrationWindow(pm);
                Hide();
                regWindow.Show();
                Close();
            }
            else
                MessageBox.Show("Сделайте выбор!!!");
        }
    }
}

using SmartParkingApp;
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

namespace OwnerApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ParkingManager pm;
        
        public MainWindow()
        {
            InitializeComponent();
            pm = ParkingManager.GetParkingManager();
            ParkingStatusBar.Value = pm.GetFilledPlaces();
            ParkingStatusBox.Text = ParkingStatusBar.Value.ToString() + "%";
        }

        private void GetPast_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GetActive_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

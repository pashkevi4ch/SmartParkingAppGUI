using SmartParkingApp;
using SmartParkingApp.Models;
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
        private class DataGridInfo
        {
            public int Quantity { get; set; }
        }
        ParkingManager pm;
        List<ParkingSession> activeSessions;
        List<ParkingSession> pastSessions;

        public MainWindow()
        {
            InitializeComponent();
            pm = ParkingManager.GetParkingManager();
            ParkingStatusBar.Value = pm.GetFilledPlaces() * 100;
            ParkingStatusBox.Text = (ParkingStatusBar.Value).ToString() + "%";
            activeSessions = pm.GetActiveSessions();
            pastSessions = pm.GetPastSessions();
            var activeList = new List<string>();
            foreach (var s in activeSessions)
            {
                activeList.Add(s.CarPlateNumber + " " + s.EntryDt.ToString());
            }
            GetActive.ItemsSource = activeList;
            var pastList = new List<string>();
            foreach (var s in pastSessions)
            {
                pastList.Add(s.CarPlateNumber + " " + s.EntryDt.ToString());
            }
            GetPast.ItemsSource = pastList;
        }

        private void GetPast_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var time = comboBox.SelectedItem.ToString().Split(" ",2)[1];
            var session = pastSessions.First(s => s.EntryDt.ToString() == time);
            TextBuilder(session);
        }

        private void GetActive_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var time = comboBox.SelectedItem.ToString().Split(" ",2)[1];
            var session = activeSessions.First(s => s.EntryDt.ToString() == time);
            TextBuilder(session);
        }
        private void TextBuilder(SmartParkingApp.Models.ParkingSession session)
        {
            if (session.ExitDt == null)
            {
                Timer.Text = "Total Minutes: " + (Convert.ToInt32((DateTime.Now - session.EntryDt).TotalMinutes)).ToString();
                ExitDt.Text = "Exit Time: No Info";
            }
            else
            {
                Timer.Text = "Total Minutes: " + (Convert.ToInt32((session.ExitDt - session.EntryDt)?.TotalMinutes).ToString());
                ExitDt.Text = "Exit Time: " + session.ExitDt.ToString();
            }
            Ticket.Text = "Ticket №" + session.TicketNumber.ToString();
            EntryDt.Text = "Entry Time: " + session.EntryDt.ToString();
            CarPlateNumber.Text = "Car Plate Number: " + session.CarPlateNumber;
            if (session.TotalPayment == null)
                TotalPayment.Text = "Total Payment: 0";
            else
                TotalPayment.Text = "Total Payment: " + session.TotalPayment.ToString();
        }

        private void GetProfit_Click(object sender, RoutedEventArgs e)
        {
            var startDate = StartDateProfit.SelectedDate;
            var endDate = EndDateProfit.SelectedDate;
            if (startDate != null && endDate != null)
            {
                var sessions = pm.GetSessionsByDate(startDate, endDate);
                TotalProfitBox.Text = pm.TotalProfit(sessions).ToString();
            }
            else
                MessageBox.Show("Please enter both dates");
        }

        private void AnalizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (Start != null && End != null)
            {
                var counter = new List<List<int>>();
                var tmpDate = Start.SelectedDate;
                while (tmpDate <= End.SelectedDate)
                {
                    var countList = new List<int>();
                    var tmpHour = tmpDate.Value;
                    while (tmpHour.Hour <= 23)
                    {
                        countList.Add(pm.GetNumberOfSessionsByTime(tmpHour, tmpHour.AddMinutes(59).AddSeconds(59)));
                        tmpHour.AddHours(1);
                    }
                    counter.Add(countList);
                    tmpHour.AddDays(1);
                }
                foreach(var c in counter)
                {
                    var dataColumn = new DataGridTextColumn();
                }
            }
            else
            {
                MessageBox.Show("Please enter both dates");
            }
                
        }
    }
}

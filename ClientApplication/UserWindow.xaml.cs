using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientApplication
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        SmartParkingApp.ParkingManager pm;
        SmartParkingApp.Models.User user;
        List<SmartParkingApp.Models.ParkingSession> sessions;
        public UserWindow(string login, SmartParkingApp.ParkingManager pm)
        {
            InitializeComponent();
            this.pm = pm;
            user = pm.FindUserByLogin(login);
            var sessionList = new List<SmartParkingApp.Models.ParkingSession>();
            if (pm.UsersParkingSession(user.Id, out sessionList))
            {
                var activeSession = sessionList[sessionList.Count - 1];
                TextBuilder(activeSession);
            }
            else
            {
                if (sessionList.Count != 0)
                {
                    var lastSession = sessionList[sessionList.Count - 1];
                    TextBuilder(lastSession);
                }
                else
                {
                    MessageBox.Show("История парковки отсутствует");
                }
            }
            var contentList = new List<DateTime>();
            foreach( var s in sessionList)
            {
                contentList.Add(s.EntryDt);
            }
            sessions = sessionList;
            GetAllComboBox.ItemsSource = contentList;
        }

        private void Get_Cost_Click(object sender, RoutedEventArgs e)
        {
            var sessionList = new List<SmartParkingApp.Models.ParkingSession>();
            if (pm.UsersParkingSession(user.Id, out sessionList))
                MessageBox.Show("Remaining Cost: "
                    + (pm.GetRemainingCost(sessionList[sessionList.Count - 1].TicketNumber).ToString()));
            else
                MessageBox.Show("Remaining Cost = 0");
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            var newUW = new UserWindow(user.Login, pm);
            Hide();
            newUW.Show();
            Close();
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

        private void GetAll_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var time = comboBox.SelectedItem.ToString();
            var session = sessions.First(s => s.EntryDt.ToString() == time);
            TextBuilder(session);
        }
    }
}

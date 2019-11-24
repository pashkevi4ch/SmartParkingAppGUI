using System;
using System.Collections.Generic;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        SmartParkingApp.ParkingManager pm;
        public LoginWindow(SmartParkingApp.ParkingManager pm)
        {
            InitializeComponent();
            this.pm = pm;
        }

        private void continueButton_Click(object sender, RoutedEventArgs e)
        {
            var login = loginTextBox.Text.ToString();
            var password = passwordTextBox.Password.ToString();
            if (login != "" && password != "")
            {
                var user = new SmartParkingApp.Models.User();
                if (pm.UserLogin(login, password, out user))
                {
                    var userWindow = new UserWindow(login, pm);
                    Hide();
                    userWindow.Show();
                    Close();
                }
                else
                    MessageBox.Show("Пароль и/или имя пользователя введены не правильно");
            }
            else
                MessageBox.Show("Введите пароль и имя пользователя");
        }
    }
}

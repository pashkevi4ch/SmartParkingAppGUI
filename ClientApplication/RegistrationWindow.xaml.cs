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
using SmartParkingApp;

namespace ClientApplication
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        SmartParkingApp.ParkingManager pm;
        public RegistrationWindow(SmartParkingApp.ParkingManager pm)
        {
            InitializeComponent();
            this.pm = pm;
        }
        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            var login = loginTextBox.Text.ToString().Replace(" ","");
            var password = passwordTextBox.Password.Replace(" ", "");
            var name = nameTextBox.Text.Replace(" ", "");
            var carPlateNumber = carPlNumTextBox.ToString().Replace(" ", "");
            var phone = phoneTextBox.ToString().Replace(" ", "");
            if (login != "" && password != "" && name != "" && carPlateNumber != "" && phone != "")
            {
                var newUser = new SmartParkingApp.Models.User();
                newUser.Login = login;
                newUser.Password = password;
                newUser.Name = name;
                newUser.CarPlateNumber = carPlateNumber;
                newUser.Phone = phone;
                newUser.Id = pm.GetNewUsersId();
                if (pm.CheckNewUser(newUser))
                {
                    pm.WriteUsersData(newUser);
                    var userWin = new UserWindow(login, pm);
                    Hide();
                    userWin.Show();
                    Close();
                }
                else
                    MessageBox.Show("Пользователь с таким login-ом уже существует");
            }
            else
                MessageBox.Show("Введите все данные");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace teethWo.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        User currUser;
        public ProfilePage(User user)
        {
            InitializeComponent();
            currUser = user;

            firNameTxt.Text = user.firName;
            nameTxt.Text = user.name;
            lastNameTxt.Text = user.lastName;
            loginTxt.Text = user.login;
            emailTxt.Text = user.email;
            phoneTxt.Text = user.phone;
            passTxt.Text = user.password;
            roleTxt.Text = user.Role != null ? user.Role.name : "—";
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(firNameTxt.Text) ||
                String.IsNullOrWhiteSpace(nameTxt.Text))
            {
                MessageBox.Show("Фамилия и имя обязательны");
                return;
            }

            if (!Regex.IsMatch(emailTxt.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Введите корректный email");
                return;
            }

            if (!Regex.IsMatch(phoneTxt.Text, @"^\+?\d{10,15}$"))
            {
                MessageBox.Show("Введите корректный телефон");
                return;
            }

            if (passTxt.Text.Length < 5)
            {
                MessageBox.Show("Пароль должен быть не короче 5 символов");
                return;
            }

            currUser.firName = firNameTxt.Text.Trim();
            currUser.name = nameTxt.Text.Trim();
            currUser.lastName = lastNameTxt.Text.Trim();
            currUser.login = loginTxt.Text.Trim();
            currUser.email = emailTxt.Text.Trim();
            currUser.phone = phoneTxt.Text.Trim();
            currUser.password = passTxt.Text;

            try
            {
                AppConnect.teethworld.SaveChanges();
                MessageBox.Show("Данные сохранены");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.GoBack();
        }
    }
}

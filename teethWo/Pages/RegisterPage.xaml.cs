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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        List<User> users;
        public RegisterPage()
        {
            InitializeComponent();
            users = AppConnect.teethworld.User.ToList();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(firNameTxt.Text) ||
                String.IsNullOrWhiteSpace(nameTxt.Text) ||
                String.IsNullOrWhiteSpace(loginTxt.Text))
            {
                MessageBox.Show("Заполните фамилию, имя и логин");
                return;
            }

            if (!Regex.IsMatch(emailTxt.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Введите корректный email");
                return;
            }

            if (!Regex.IsMatch(phoneTxt.Text, @"^\+?\d{10,15}$"))
            {
                MessageBox.Show("Введите корректный телефон (только цифры, 10-15 символов)");
                return;
            }

            if (passTxt.Password.Length < 5)
            {
                MessageBox.Show("Пароль должен быть не короче 5 символов");
                return;
            }

            if (users.Any(x => x.login != null && x.login.ToLower().Trim() == loginTxt.Text.ToLower().Trim()))
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
                return;
            }

            User newUser = new User
            {
                firName = firNameTxt.Text.Trim(),
                name = nameTxt.Text.Trim(),
                lastName = lastNameTxt.Text.Trim(),
                login = loginTxt.Text.Trim(),
                email = emailTxt.Text.Trim(),
                phone = phoneTxt.Text.Trim(),
                password = passTxt.Password,
                idRole = 3
            };

            try
            {
                AppConnect.teethworld.User.Add(newUser);
                AppConnect.teethworld.SaveChanges();
                MessageBox.Show("Регистрация прошла успешно!");
                MainWindow.MainFrame.Navigate(new Pages.LoginPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message);
            }
        }
    }
}

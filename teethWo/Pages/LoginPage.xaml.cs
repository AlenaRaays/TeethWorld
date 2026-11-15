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

namespace teethWo.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        List<User> users;
        public LoginPage()
        {
            InitializeComponent();
            users = AppConnect.teethworld.User.ToList();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(logintxt.Text) && !String.IsNullOrWhiteSpace(logintxt.Text))
            {

                string key = logintxt.Text.ToLower().Trim();
                var user = users.FirstOrDefault(x =>
                    (x.login != null && x.login.ToLower().Trim() == key) ||
                    (x.email != null && x.email.ToLower().Trim() == key) ||
                    (x.phone != null && x.phone.ToLower().Trim() == key));

                if (user != null)
                {
                    MessageBox.Show($"Здравствуйте, {user.name} {user.lastName}!");
                    MainWindow.MainFrame.Navigate(new Pages.ProductPage(user));
                }
                else
                {
                    MessageBox.Show("Не удалось войти. Вы зайдете под неавторизованным пользователем");
                    MainWindow.MainFrame.Navigate(new Pages.ProductPage(null));
                }
            }
            else
            {
                MessageBox.Show("Здравствуйте, неавторизованный пользователь");
                MainWindow.MainFrame.Navigate(new Pages.ProductPage(null));
            }
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.Navigate(new Pages.RegisterPage());
        }
    }
}

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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        User logineduser;
        public AdminPage(User user)
        {
            InitializeComponent();
            logineduser = user;
            AdminFrame.Navigate(new Pages.AdminTablePage(logineduser, "Product"));
        }

        private void ProductsBtn_Click(object sender, RoutedEventArgs e) { AdminFrame.Navigate(new Pages.AdminTablePage(logineduser, "Product")); }
        private void CategoriesBtn_Click(object sender, RoutedEventArgs e) { AdminFrame.Navigate(new Pages.AdminTablePage(logineduser, "Category")); }
        private void ManufacturesBtn_Click(object sender, RoutedEventArgs e) { AdminFrame.Navigate(new Pages.AdminTablePage(logineduser, "Manufacture")); }
        private void SizesBtn_Click(object sender, RoutedEventArgs e) { AdminFrame.Navigate(new Pages.AdminTablePage(logineduser, "Size")); }
        private void WarehouseBtn_Click(object sender, RoutedEventArgs e) { AdminFrame.Navigate(new Pages.AdminTablePage(logineduser, "Warehouse")); }
        private void OrdersBtn_Click(object sender, RoutedEventArgs e) { AdminFrame.Navigate(new Pages.AdminTablePage(logineduser, "Order")); }
        private void UsersBtn_Click(object sender, RoutedEventArgs e) { AdminFrame.Navigate(new Pages.AdminTablePage(logineduser, "User")); }
        private void BackBtn_Click(object sender, RoutedEventArgs e) { MainWindow.MainFrame.GoBack(); }
    }
}

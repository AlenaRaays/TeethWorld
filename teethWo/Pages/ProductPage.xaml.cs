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
using teethWo.Entities;

namespace teethWo.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        List<Product> products;
        List<Category> categories;
        public static List<CartItem> Cart;
        User logineduser;
        public ProductPage(User user)
        {
            InitializeComponent();

            Cart = new List<CartItem>();

            products = AppConnect.teethworld.Product.ToList();
            categories = AppConnect.teethworld.Category.ToList();
            logineduser = user;

            filterbox.Items.Add("Все категории");
            foreach (var item in categories)
            {
                filterbox.Items.Add(item.name);
            }
            filterbox.SelectedIndex = 0;


            sortbox.Items.Add("По умолчанию");
            sortbox.Items.Add("По возрастанию");
            sortbox.Items.Add("По убыванию");
            sortbox.SelectedIndex = 0;


            ProductList.ItemsSource = products;

            if (user != null)
            {
                username.Text = user.firName + " " + user.name + " " + user.lastName;

                ProfileBtn.Visibility = Visibility.Visible;
                CartBtn.Visibility = Visibility.Visible;

                OrdersBtn.Visibility = Visibility.Visible;

                if (user.idRole == 1 || user.idRole == 2)
                    AdminBtn.Visibility = Visibility.Visible;
                else
                    AdminBtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                username.Text = "Неавторизованный пользователь";
                OrdersBtn.Visibility = Visibility.Collapsed;
                ProfileBtn.Visibility = Visibility.Collapsed;
                CartBtn.Visibility = Visibility.Collapsed;
                AdminBtn.Visibility = Visibility.Collapsed;
            }
            

            foreach (var item in products)
            {
                if (item.CrossedPrice)
                {
                    
                }
            }
        }

        public void SearchFilterSort()
        {
            List<Product> productsF = AppConnect.teethworld.Product.ToList();

            if (!String.IsNullOrEmpty(searchbox.Text) && !String.IsNullOrWhiteSpace(searchbox.Text))
            {
                productsF = productsF.Where(x => x.name.ToLower().Trim().StartsWith(searchbox.Text.ToLower().Trim())
                || x.name.ToLower().Trim().Contains(searchbox.Text.ToLower().Trim())
                || x.description.ToLower().Trim().StartsWith(searchbox.Text.ToLower().Trim())
                || x.description.ToLower().Trim().Contains(searchbox.Text.ToLower().Trim())).ToList();
            }

            if (filterbox.SelectedIndex != 0)
            {
                productsF = productsF.Where(x => x.Category.id == filterbox.SelectedIndex).ToList();
            }

            if (sortbox.SelectedIndex != 0)
            {
                switch (sortbox.SelectedIndex)
                {
                    case 1:
                        productsF = productsF.OrderBy(x => x.price).ToList();
                        break;
                    case 2:
                        productsF = productsF.OrderByDescending(x => x.price).ToList();
                        break;
                }
            }


            if (productsF != null)
            {
                ProductList.ItemsSource = productsF;
            }
            else
            {
                ProductList.ItemsSource = products;
            }
        }

        private void searchbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchFilterSort();
        }

        private void filterbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchFilterSort();
        }

        private void sortbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchFilterSort();
        }

        private void ProductList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Product prod = ProductList.SelectedItem as Product;
            if (prod == null)
            {
                return;
            }
            else
            {
                MainWindow.MainFrame.Navigate(new Pages.ProductData(prod, logineduser));
            }
        }

        private void OrdersBtn_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.Navigate(new Pages.OrderPage(logineduser));
        }

        private void CartBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.Navigate(new Pages.CartPage(logineduser));
        }

        private void AdminBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.Navigate(new Pages.AdminPage(logineduser));
        }

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.Navigate(new Pages.ProfilePage(logineduser));
        }
    }
}

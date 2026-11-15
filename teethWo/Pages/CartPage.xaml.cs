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
    /// Логика взаимодействия для CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        User logineduser;
        public CartPage(User user)
        {
            InitializeComponent();
            logineduser = user;
            CartList.ItemsSource = ProductPage.Cart;
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = CartList.SelectedItem as CartItem;
            if (item == null)
            {
                MessageBox.Show("Выберите товар");
                return;
            }
            ProductPage.Cart.Remove(item);
            CartList.ItemsSource = null;
            CartList.ItemsSource = ProductPage.Cart;
        }

        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (logineduser == null)
            {
                MessageBox.Show("Войдите, чтобы оформить заказ");
                return;
            }

            if (ProductPage.Cart.Count == 0)
            {
                MessageBox.Show("Корзина пуста");
                return;
            }

            double total = ProductPage.Cart.Sum(x => x.Product.price * x.Quantity);

            List<Order> allOrders = AppConnect.teethworld.Order.ToList();
            double nextNum = allOrders.Count > 0 ? allOrders.Max(x => x.orderNum) + 1 : 1001;

            Order newOrder = new Order
            {
                orderNum = nextNum,
                orderDate = DateTime.Now,
                idUser = logineduser.id,
                amount = int.Parse(total.ToString())
            };

            try
            {
                AppConnect.teethworld.Order.Add(newOrder);
                AppConnect.teethworld.SaveChanges();

                var whs = AppConnect.teethworld.Warehouse.ToList();
                foreach (var c in ProductPage.Cart)
                {
                    var wh = whs.FirstOrDefault(w => w.idProduct == c.Product.id && w.Size.size1 == c.Size);
                    if (wh != null)
                    {
                        // списываем со склада
                        wh.allowedAmount -= c.Quantity;
                        AppConnect.teethworld.OrderWarehouse.Add(new OrderWarehouse
                        {
                            idOrder = newOrder.id,
                            idWarehouse = wh.id
                        });
                    }
                }
                AppConnect.teethworld.SaveChanges();

                ProductPage.Cart.Clear();
                MessageBox.Show("Заказ оформлен!");
                MainWindow.MainFrame.Navigate(new Pages.ProductPage(logineduser));
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

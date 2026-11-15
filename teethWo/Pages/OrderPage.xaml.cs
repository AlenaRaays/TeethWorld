using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        private Model1 _context = new Model1();

        List<Order> orders;
        User logineduser;
        public OrderPage(User user)
        {
            InitializeComponent();
            logineduser = user;
            _context.Order.Load();

            if (user.idRole != 5 && user.idRole != 1)
            {
                orders = AppConnect.teethworld.Order.Where(o => o.idUser == user.id).ToList();
                OrderList.IsReadOnly = true;

                OrderList.ItemsSource = orders;
                UserColumn.ItemsSource = AppConnect.teethworld.User.Where(o => o.id == user.id).ToList();
            }
            else
            {
                orders = AppConnect.teethworld.Order.ToList();

                OrderList.ItemsSource = _context.Order.Local.ToBindingList();
                UserColumn.ItemsSource = _context.User.ToList();
            }
        }

        public void SearchOrders()
        {
            List<Order> ordersS = null;
            if (!String.IsNullOrEmpty(SearchBox.Text) && !String.IsNullOrWhiteSpace(SearchBox.Text))
            {
                ordersS = orders.Where(x => x.User.login.ToLower().Trim().StartsWith(SearchBox.Text)).ToList();
            }

            if (ordersS != null)
            {
                OrderList.ItemsSource = ordersS;
            }
            else
            {
                OrderList.ItemsSource = orders;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchOrders();
        }

        private void OrderList_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (logineduser == null || (logineduser.idRole != 1 && logineduser.idRole != 5))
            {
                MessageBox.Show("Изменять содержимое заказа может только менеджер или администратор!");
                e.Cancel = true;
            }
        }

        private void OrderList_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        _context.SaveChanges();
                        MessageBox.Show($"Изменения сохранены");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
                    }
                }));

            }
        }
    }
}

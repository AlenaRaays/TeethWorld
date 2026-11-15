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
    /// Логика взаимодействия для AdminTablePage.xaml
    /// </summary>
    public partial class AdminTablePage : Page
    {
        User logineduser;
        string tableName;
        Model1 _context = new Model1();

        public AdminTablePage(User user, string table)
        {
            InitializeComponent();
            logineduser = user;
            tableName = table;

            if (user.idRole != 1)
                DelBtn.Visibility = Visibility.Collapsed;

            TitleTxt.Text = "Таблица: " + table;
            LoadTable();
        }

        private void LoadTable()
        {
            switch (tableName)
            {
                case "Product":
                    _context.Product.Load();
                    Table.ItemsSource = _context.Product.Local.ToBindingList();
                    break;
                case "Category":
                    _context.Category.Load();
                    Table.ItemsSource = _context.Category.Local.ToBindingList();
                    break;
                case "Manufacture":
                    _context.Manufacture.Load();
                    Table.ItemsSource = _context.Manufacture.Local.ToBindingList();
                    break;
                case "Size":
                    _context.Size.Load();
                    Table.ItemsSource = _context.Size.Local.ToBindingList();
                    break;
                case "Warehouse":
                    _context.Warehouse.Load();
                    Table.ItemsSource = _context.Warehouse.Local.ToBindingList();
                    break;
                case "Order":
                    _context.Order.Load();
                    Table.ItemsSource = _context.Order.Local.ToBindingList();
                    break;
                case "User":

                    if (logineduser.idRole != 1)
                    {
                        MessageBox.Show("Управление пользователями доступно только администратору");
                        Table.ItemsSource = null;
                        return;
                    }
                    _context.User.Load();
                    Table.ItemsSource = _context.User.Local.ToBindingList();
                    break;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _context.SaveChanges();
                MessageBox.Show("Сохранено");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {

            if (logineduser.idRole != 1)
            {
                MessageBox.Show("Удалять записи может только администратор");
                return;
            }

            var selected = Table.SelectedItem;
            if (selected == null)
            {
                MessageBox.Show("Выберите запись");
                return;
            }

            try
            {
                _context.Entry(selected).State = EntityState.Deleted;
                _context.SaveChanges();
                MessageBox.Show("Удалено");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невозможно удалить запись: " + ex.Message);
            }
        }
    }
}

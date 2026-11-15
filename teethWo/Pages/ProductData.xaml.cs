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
    /// Логика взаимодействия для ProductData.xaml
    /// </summary>
    public partial class ProductData : Page
    {
        List<Warehouse> warehouseList;
        List<Product> listProducts;
        Product currProduct;
        User logineduser;
        public ProductData(Product product, User user)
        {
            InitializeComponent();
            DataContext = product;
            currProduct = product;
            logineduser = user;


            warehouseList = AppConnect.teethworld.Warehouse.ToList();
            listProducts = AppConnect.teethworld.Product.ToList();

            warehouseList = warehouseList.Where(w => w.Product.id == product.id).ToList();

            sizes.Items.Add("--размеры--");

            foreach (var item in warehouseList)
            {
                sizes.Items.Add(item.Size.size1);
            }

            sizes.SelectedIndex = 0;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (logineduser == null)
            {
                MessageBox.Show("Войдите, чтобы добавить товар в корзину");
                return;
            }

            if (sizes.SelectedIndex == 0)
            {
                MessageBox.Show("Выберите размер");
                return;
            }

            if (!int.TryParse(quantity.Text, out int qty) || qty <= 0)
            {
                MessageBox.Show("Введите корректное количество");
                return;
            }

            double selectedSize = Convert.ToDouble(sizes.SelectedItem);
            var wh = warehouseList.FirstOrDefault(w => w.Size.size1 == selectedSize);
            if (wh == null || qty > wh.allowedAmount)
            {
                MessageBox.Show("Недостаточно товара на складе");
                return;
            }

            var existing = ProductPage.Cart.FirstOrDefault(c => c.Product.id == currProduct.id && c.Size == selectedSize);
            if (existing != null)
            {
                existing.Quantity += qty;
                existing.Sum += currProduct.price;
            }
            else
            {
                ProductPage.Cart.Add(new CartItem { Product = currProduct, Size = selectedSize, Quantity = qty, Sum = currProduct.price * qty});
            }
        }
    }
}

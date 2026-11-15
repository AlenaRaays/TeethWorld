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

namespace teethWo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame MainFrame;
        public MainWindow()
        {
            InitializeComponent();
            MainFrame = FrameMain;

            MainFrame.Navigate(new Pages.LoginPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MainFrame.CanGoBack)
                {
                    MainFrame.GoBack();
                }
            }
            catch { }
        }
    }
}

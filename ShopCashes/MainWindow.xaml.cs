using System.Diagnostics;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ShopCashesModel;

namespace ShopCashes
{
    public partial class MainWindow : Window
    {
        public Shop shop = new(4);
        int num;

        public MainWindow()
        {
            InitializeComponent();
            cashes.ItemsSource = shop.Cashes;
            hall.MouseLeftButtonDown += Hall_MouseLeftButtonDown;

            shop.Cashes[1].Customers.Add(
                new Customer(1, 10, 12, 5));
            shop.Cashes[1].Customers.Add(
                new Customer(2, 14, 12, 8));
        }

        private void Hall_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            shop.Cashes[0].Customers.Add(
                new Customer(num++, num * 10, num * 10 + num, 5));
        }
    }
}

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
        public MainWindow()
        {
            InitializeComponent();
            cashes.ItemsSource = shop.Cashes;
            shop.Cashes[1].Customers.Enqueue(
                new Customer(1, 10, 12, 5));
            shop.Cashes[1].Customers.Enqueue(
                new Customer(2, 14, 12, 8));
        }
    }
}

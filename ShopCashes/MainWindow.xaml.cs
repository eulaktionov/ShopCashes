using System.Diagnostics;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //List<int> list = new() { 1, 2, 3, 4 };
        public Shop shop = new(4);
        WrapPanel wrapPanel = new WrapPanel();
        public MainWindow()
        {
            InitializeComponent();
            cashes.ItemsSource = shop.Cashes;
            //cashes.Items[1]
              //  .queue.Items.Add(new Label { Content = "8" });
            //Debug.WriteLine("");
        }
    }
}
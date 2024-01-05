using System;
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
        public Shop shop;
        Label newCustomer;
        int num;

        public MainWindow()
        {
            InitializeComponent();

            shop = new()
            {
                CashCount = 4,
                
                MinNextCustomerInterval = 1,
                MaxNextCustomerInterval = 5,

                MinBuyingInterval = 5,
                MaxBuyingInterval = 15,

                MinPayingInterval = 1,
                MaxPayingInterval = 3,
            };
            shop.Initialize();
            shop.OnCustomerArrival += Shop_OnCustomerArrival;
            shop.OnCustomerIntoHall += Shop_OnCustomerIntoHalll;
            shop.OnCustomerToCash += Shop_OnCustomerToCash;

            cashes.ItemsSource = shop.Cashes;
        }

        private async void Shop_OnCustomerArrival(object? sender, Customer e)
        {
            newCustomer = new Label()
            {
                Template = FindResource("CustomerTemplate") as ControlTemplate,
                Width=20,
                Height=20,
                Tag = e
            };
            newCustomer.DataContext = e;
            Grid.SetColumn(newCustomer, 0);
            Grid.SetRow(newCustomer, 1);
            grid.Children.Add(newCustomer);

            Debug.WriteLine("???");
        }

        private async void Shop_OnCustomerIntoHalll(object? sender, Customer e)
        {
            grid.Children.Remove(newCustomer);
            double x = Shop.Random.NextDouble() *
                (hall.ActualWidth - newCustomer.ActualWidth);
            double y = Shop.Random.NextDouble() *
                (hall.ActualHeight - newCustomer.ActualHeight);
            Canvas.SetLeft(newCustomer, x);
            Canvas.SetTop(newCustomer, y);
            hall.Children.Add(newCustomer);

            Debug.WriteLine("!!!");
        }

        private void Shop_OnCustomerToCash(object? sender, Customer e)
        {
            Label customer = hall.Children.Cast<Label>().
                Single(c => c.Tag as Customer == e);
            hall.Children.Remove(customer);
        }
    }
}

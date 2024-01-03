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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //List<int> list = new() { 1, 2, 3, 4 };
        public Shop shop = new(4);
        //WrapPanel wrapPanel = new WrapPanel();
        public MainWindow()
        {
            InitializeComponent();
            //cashes.ItemsSource = shop.Cashes;
            //Debug.WriteLine("");
            foreach (var it in shop.Cashes)
            {
            local: CashControl control = new();
                //{
                //    number = new Label { Content = it.Id.ToString() },
                //    queue = new WrapPanel { Margin = new Thickness(5) }
                //};
                cashes.Items.Add(control);
            }

            for(int i = 0; i< cashes.Items.Count; i++)
            {
                CashControl control = (CashControl)cashes.Items[i];
                Debug.WriteLine($"==={control.number.Content}===");
                control.number.Content = $"{i}";

                Label label = new Label
                {
                    Content = $"Customer {i}",
                    Background = Brushes.LightGreen,
                    Margin = new Thickness(5)
                };
                control.queue.Children.Add(label);
            };


            Cash cash = shop.Cashes[1];
            Debug.WriteLine($"==={cash.Id}===");

            // Find the corresponding CashControl in the ListBox
            var cashControl = cashes.ItemContainerGenerator;
            //.ContainerFromItem(cash) as CashControl;

            // Access the WrapPanel inside the CashControl
            // WrapPanel wrapPanel = cashControl?.FindName("queue") as WrapPanel;

            // Add the label to the WrapPanel
            //wrapPanel?.Children.Add(label);

            cashes.UpdateLayout();
            var cp = cashes.ItemContainerGenerator.ContainerFromItem(cash);
            //var container = cp.Items.ContainerFromItem(cash);
            //Label label2 = cp.ContentTemplate.FindName("number", cp) as Label;

        }
    }
}
/*
ItemsControl itemsControl = ItemsControl_1;
for (int i = 0; i<itemsControl.Items.Count; i++)
{
    ContentPresenter cp = itemsControl.ItemContainerGenerator.ContainerFromIndex(i) as ContentPresenter;
    if (cp != null && VisualTreeHelper.GetChildrenCount(cp) > 0)
    {
        Button button = VisualTreeHelper.GetChild(cp, 0) as Button;
        //...
    }
}


private void AddObjectsToWrapPanel(Cash cash)
{
    // Example: Adding Labels to the WrapPanel
    for (int i = 1; i <= 5; i++)
    {
        Label label = new Label
        {
            Content = $"Customer {i}",
            Background = Brushes.LightGreen,
            Margin = new Thickness(5)
        };

        cash.Customers.Enqueue(new Customer(i, i, i, i)); // Add customer to the Cash

        // Find the corresponding CashControl in the ListBox
        CashControl cashControl = cashes
            .ItemContainerGenerator
            .ContainerFromItem(cash) as CashControl;

        // Access the WrapPanel inside the CashControl
        WrapPanel wrapPanel = cashControl?.FindName("queue") as WrapPanel;

        // Add the label to the WrapPanel
        wrapPanel?.Children.Add(label);
    }
}

*/
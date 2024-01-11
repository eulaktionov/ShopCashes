using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShopCashesModel
{
    public class Cash : INotifyPropertyChanged
    {
        public int Id { get; init; }
        public ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>();
        int downtime;
        public int Downtime 
        { 
            get => downtime;
            set
            { 
                downtime = value;
                OnPropertyChanged(nameof(Downtime));
            }
        }
        public int CustomersCount { get; set; }
        public int CustomersWaitingTime { get; set; }
        public string Title => $"{Id} : {Downtime}";

        public void ModelTimer_Tick(object? sender, EventArgs e)
        {
            if (Customers.Count > 0)
            {
                Customers[0].PayingInterval--;
                if (Customers[0].PayingInterval == 0)
                {
                    CustomersWaitingTime += 
                        Shop.ModelTime - Customers[0].CashboxTime;
                    Customers.RemoveAt(0);
                    CustomersCount++;
                }
            }
            else
            {
                Downtime++;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

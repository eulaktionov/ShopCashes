using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCashesModel
{
    public class Cash
    {
        public int Id { get; init; }
        public ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>();
        public int Downtime { get; set; }
    }
}

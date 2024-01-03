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
        public Queue<Customer> Customers { get; set; } = new Queue<Customer>();
        //public ObservableCollection<Customer> Customers { get; set; } =
        //    new ObservableCollection<Customer>();

        public int Downtime { get; set; }
        //public string Title => $"{Id}";

        //public Cash(int id) { Id = id; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCashesModel
{
    public class Customer
    {
        public int Id { get; }
        public int ArrivalTime { get; }
        public int CashboxTime { get; }
        public int PayingInterval { get; set; }
        public int WaitingInterval { get; set; }    // 0

        public string Title => $"{Id} : {CashboxTime}";

        public Customer(int id, int arrivalTime,
            int cashboxTime, int payingInterval)
        {
            Id = id;
            ArrivalTime = arrivalTime;
            CashboxTime = cashboxTime;
            PayingInterval = payingInterval;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCashesModel
{
    public class Customer
    {
        public int Id { get; init; }
        public int ArrivalTime { get; init; }
        public int CashboxTime { get; init; }
        public int PayingInterval { get; set; }
        public int WaitingInterval { get; } 

        //public Customer(int id, int arrivalTime,
        //    int cashboxTime, int payingInterval)
        //{
        //    Id = id;
        //    ArrivalTime = arrivalTime;
        //    CashboxTime = cashboxTime;
        //    PayingInterval = payingInterval;
        //}
    }
}

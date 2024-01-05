using System.Diagnostics;
using System.Windows.Threading;

namespace ShopCashesModel
{
    public class Shop
    {
        public static Random Random = new();
        public static DispatcherTimer ModelTimer = new();
        public static int ModelTime = 0;

        public int CashCount { get; set; }

        public int MinNextCustomerInterval { get; set; }
        public int MaxNextCustomerInterval { get; set; }
        int _nextCustomerInterval => Random.Next
            (MinNextCustomerInterval, MaxNextCustomerInterval + 1); 

        public int MinBuyingInterval { get; set; }
        public int MaxBuyingInterval { get; set; }
        int _nextBuyingInterval => Random.Next
            (MinBuyingInterval, MaxBuyingInterval + 1);

        public int MinPayingInterval { get; set; }
        public int MaxPayingInterval { get; set; }
        int _nextPayingInterval => Random.Next
            (MinPayingInterval, MaxPayingInterval + 1);

        public List<Cash> Cashes { get; } = new();

        int _nextCustomerTime;
        int _nextCustomerId;

        Customer _nextCustomer;
        PriorityQueue<Customer, int> _customers = new();

        bool _customerIntoHall;

        public event EventHandler<Customer> OnCustomerArrival;
        public event EventHandler<Customer> OnCustomerIntoHall;
        public event EventHandler<Customer> OnCustomerToCash;

        public void Initialize()
        {
            for(int i = 0; i < CashCount; i++)
            {
                Cashes.Add(new() { Id = i + 1 });
            }

            ModelTimer.Interval = TimeSpan.FromSeconds(0.5);
            ModelTimer.Tick += ModelTimer_Tick;
            ModelTimer.Start();
        }

        private void ModelTimer_Tick(object? sender, EventArgs e)
        {
            _nextCustomerTime = ModelTime + _nextCustomerInterval;
            int nextCustomerBuyingTime = ModelTime + _nextBuyingInterval;

            ModelTime++;
            Debug.WriteLine($"Time: {ModelTime}");

            if(_customerIntoHall)
            {
                _customers.Enqueue(_nextCustomer, nextCustomerBuyingTime);
                OnCustomerIntoHall?.Invoke(this, _nextCustomer);
                _customerIntoHall = false;
            }

            if(ModelTime == _nextCustomerTime)
            {
                int cashTime = ModelTime + _nextBuyingInterval;
                _nextCustomer = new(_nextCustomerId++, ModelTime, 
                    cashTime, _nextPayingInterval);

                OnCustomerArrival?.Invoke(this, _nextCustomer);
                _customerIntoHall = true;
            }

            while(_customers.Count > 0
                && _customers.Peek().CashboxTime <= ModelTime)
            {
                var lessQueueCash = Cashes.MinBy(x => x.Customers.Count);
                var customer = _customers.Dequeue();
                lessQueueCash.Customers.Add(customer);

                OnCustomerToCash?.Invoke(this, customer);
            }

        }
    }
}

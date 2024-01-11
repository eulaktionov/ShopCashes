using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace ShopCashesModel
{
    public class Shop : INotifyPropertyChanged
    {
        public static Random Random = new();

        public static DispatcherTimer ModelTimer = new()
        {
            Interval = TimeSpan.FromSeconds(0.5),
        };
        public static int ModelTime { get; set; }

        public List<Cash> Cashes { get; } = new();
        public int CashCount
        {
            get => Cashes.Count;
            set
            {
                for(int i = 0; i < value; i++)
                {
                    Cash cash = new() { Id = i + 1 };
                    ModelTimer.Tick += cash.ModelTimer_Tick;
                    Cashes.Add(cash);
                }
            }
        }

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

        int _customersCount;
        public int CustomersCount
        {
            get => _customersCount;
            set
            {
                _customersCount = value;
                OnPropertyChanged(nameof(CustomersCount));
            }
        }

        int _customersWaitingTime;
        public int CustomersWaitingTime
        {
            get => _customersWaitingTime;
            set
            {
                _customersWaitingTime = value;
                OnPropertyChanged(nameof(CustomersWaitingTime));
            }
        }

        int _cashesDowntime;
        public int CashesDowntime
        {
            get => _cashesDowntime;
            set
            {
                _cashesDowntime = value;
                OnPropertyChanged(nameof(CashesDowntime));
            }
        }

        public int AverageCustomerWaitingTime  => CustomersWaitingTime/CustomersCount;
        public int AverageCashDowntime => CashesDowntime/CashCount;

        int _nextCustomerTime;
        int _nextCustomerId;
        Customer _nextCustomer;
        bool _customerIntoHall;
        PriorityQueue<Customer, int> _customers = new();

        public event EventHandler<Customer> OnCustomerArrival;
        public event EventHandler<Customer> OnCustomerIntoHall;
        public event EventHandler<Customer> OnCustomerToCash;

        public event PropertyChangedEventHandler PropertyChanged;


        public void Run()
        {
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
                _nextCustomer = new()
                {
                    Id = ++_nextCustomerId,
                    ArrivalTime = ModelTime,
                    CashboxTime = cashTime,
                    PayingInterval = _nextBuyingInterval
                };

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

            int customersCount = 0;
            int waitingTime = 0;
            int downtime = 0;
            foreach(var cash in Cashes)
            {
                customersCount += cash.CustomersCount;
                waitingTime += cash.CustomersWaitingTime;
                downtime += cash.Downtime;
            }
            CustomersCount = customersCount;
            if (CustomersCount > 0)
            {
                CustomersWaitingTime = waitingTime / CustomersCount;
            }
            CashesDowntime = downtime / CashCount;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

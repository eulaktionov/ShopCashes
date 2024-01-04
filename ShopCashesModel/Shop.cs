using System.Diagnostics;

namespace ShopCashesModel
{
    public class Shop
    {
        public List<Cash> Cashes { get; } = new();

        public Shop(int cashCount)
        {
Debug.WriteLine("!!!\n Shop created \n!!!");
            for(int i = 0; i < cashCount; i++)
            {
                Cashes.Add(new() { Id = i + 1 });
            }
Debug.WriteLine($"!!!\n {Cashes[Cashes.Count-1].Id} \n!!!");
        }
    }

}

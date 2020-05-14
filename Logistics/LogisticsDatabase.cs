using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingDemo.Logistics
{
    public static class LogisticsDatabase
    {
        private static Dictionary<string, string> _shippingAddresses = new Dictionary<string, string>();
        private static Dictionary<Guid, int> _stock = new Dictionary<Guid, int>
        {
            {Guid.Parse("36947920-84c3-44c5-9cb8-8ca4017c6f91"), 1 },
            {Guid.Parse("a1093bcb-a090-4c4e-9b6b-6284e30851b0"), 3 },
        };

        public static void AddAddressToCustomer(string customderId, string address)
        {
            _shippingAddresses.Add(customderId, address);
        }
        
        public static bool ReserveProduct(Guid productId)
        {
            if (_stock.TryGetValue(productId, out int stock))
            {
                _stock.Remove(productId);
                return true;
            };
            return false;
        }
    }
}

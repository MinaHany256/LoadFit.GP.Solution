using LoadFit.Core.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Core.Specifications.OrderSpecifications
{
    public class OrdersForDriverSpecification : BaseSpecifications<Order>
    {
        public OrdersForDriverSpecification(int driverId)
            : base(o => o.Vehicle.DriverId == driverId) // Filter orders where the vehicle belongs to the driver
        {
            Includes.Add(o => o.Vehicle);
            Includes.Add(o => o.Items);
            
        }
    }
}

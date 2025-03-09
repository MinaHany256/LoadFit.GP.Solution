using LoadFit.Core.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Core.Specifications.OrderSpecifications
{
    public class OrderSpecifications : BaseSpecifications<Order>
    {
        public OrderSpecifications(string buyerEmail) : base(o => o.BuyerEmail == buyerEmail)
        {
            
            Includes.Add(o => o.Vehicle);
            Includes.Add(o => o.Items);

            AddOrderBy(o => o.OrderDate);

        }

        public OrderSpecifications(int orderId, string buyerEmail) 
            : base(o => o.Id == orderId &&  o.BuyerEmail == buyerEmail)
        {
            
            Includes.Add(o => o.Vehicle);
            Includes.Add(o => o.Vehicle.Brand);
            Includes.Add(o => o.Vehicle.Type);
            Includes.Add(o => o.Vehicle);
            Includes.Add(o => o.Items);
        }

    }
}

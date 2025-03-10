using LoadFit.Core.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Core.Specifications.OrderSpecifications
{
    public class OrderByBuyerEmailSpecification : BaseSpecifications<Order>
    {
        public OrderByBuyerEmailSpecification(string buyerEmail)
        : base(o => o.BuyerEmail == buyerEmail) { Includes.Add(o => o.Vehicle); }
    }
}

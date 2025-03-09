using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Core.Helpers
{
    public static class OrderHelper
    {
        public static decimal CalculateSubtotal(decimal vehiclePrice, decimal totalVolumeInMeters, decimal volumePricePerCubicMeter)
        {
            return vehiclePrice + (totalVolumeInMeters * volumePricePerCubicMeter);
        }
    }
}

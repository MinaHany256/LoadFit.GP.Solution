using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Core.Entities
{
    public class VehicleBrand : BaseEntity
    {
        public string Name { get; set; }


        // Navigational Property
        // public ICollection<Vehicle> Vehicles { get; set; } = new HashSet<Vehicle>();      // Navigational Property [MANY]

    }
}

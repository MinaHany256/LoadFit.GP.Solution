using LoadFit.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Core.Specifications.VehicleSpecifications
{
    public class VehicleWithBrandAndTypeSpecifications : BaseSpecifications<Vehicle>
    {
        public VehicleWithBrandAndTypeSpecifications(string? sort, int? brandId, int? typeId, string? search) : base(p => 
        // Search = null
        (string.IsNullOrEmpty(search) || p.Name.ToLower().Contains(search.ToLower())) &&
        // brandId == null
        ( !brandId.HasValue || p.BrandId == brandId.Value )   && // true
        // typeId == null
        ( !typeId.HasValue || p.TypeId == typeId.Value )
        )
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
            Includes.Add(P => P.Driver);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        // orderBy(P => P.price)
                        AddOrderBy(P => P.price);
                        break;
                    case "priceDesc":
                        // orderByDesc(P => P.price)
                        AddOrderByDesc(P => P.price);
                        break;
                    case "nameDesc":
                        // orderByDesc(P => P.Name)
                        AddOrderByDesc(P => P.Name);
                        break;
                    case "nameAsc":
                        // orderByDesc(P => P.Name)
                        AddOrderBy(P => P.Name);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }

            

        }

        public VehicleWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
            Includes.Add(P => P.Driver);
            

        }

    }
}

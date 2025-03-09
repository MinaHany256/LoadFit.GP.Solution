using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Core.Entities
{
    public enum TypeOfMaterial
    {
        Wood = 700,   // kg/m³
        Metal = 7850, // kg/m³
        Plastic = 950,
        Glass = 2500,
        Foam = 50
    }

    public enum TypeOfFragility
    {
        Fragile,
        NonFragile
    }

    public class BasketItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public TypeOfMaterial MaterialType { get; set; }
        public TypeOfFragility FragilityType { get; set; }
        public int Quantity { get; set; }

        [NotMapped]
        public decimal Volume
        {
            get => Length * Width * Height * Quantity;
            private set { }  // Prevents EF Core migration errors
        }

        [NotMapped]
        public decimal Weight
        {
            get => Volume * (decimal)MaterialType;
            private set { }  // Prevents EF Core migration errors
        }


    }
}

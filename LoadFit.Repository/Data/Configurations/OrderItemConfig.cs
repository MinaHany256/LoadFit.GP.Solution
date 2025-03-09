using LoadFit.Core.Order_Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Repository.Data.Configurations
{
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(item => item.Length)
                .HasColumnType("decimal(18,2)");

            builder.Property(item => item.Width)
                .HasColumnType("decimal(18,2)");

            builder.Property(item => item.Height)
                .HasColumnType("decimal(18,2)");

            builder.Property(item => item.Volume)
                .HasColumnType("decimal(18,2)");
        }
    }
}

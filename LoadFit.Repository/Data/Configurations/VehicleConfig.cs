using LoadFit.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Repository.Data.Configurations
{
    public class VehicleConfig : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.Property(P => P.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(P => P.PictureUrl).IsRequired();

            builder.Property(P => P.price)
                   .HasColumnType("decimal(18,2)");

            builder.Property(P => P.Length)
                   .HasColumnType("decimal(18,2)");

            builder.Property(P => P.Width)
                   .HasColumnType("decimal(18,2)");

            builder.Property(P => P.Height)
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(P => P.Brand)
                   .WithMany()
                   .HasForeignKey(P => P.BrandId);

            builder.HasOne(P => P.Type)
                .WithMany()
                .HasForeignKey(P => P.TypeId);
            


        }
    }
}

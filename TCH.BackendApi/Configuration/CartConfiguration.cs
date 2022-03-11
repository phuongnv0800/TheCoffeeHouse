//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using TCH.BackendApi.Entities;

//namespace TCH.BackendApi.Configuration
//{
//    public class CartConfiguration : IEntityTypeConfiguration<Cart>
//    {
//        public void Configure(EntityTypeBuilder<Cart> builder)
//        {
//            builder.ToTable("Carts");
//            builder.HasKey(x => new { x.ProductId, x.UserId});
//            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");

//            builder.Property(x => x.SubTotal).IsRequired().HasColumnType("decimal(18,2)");
//            builder.HasOne(x => x.Product).WithMany(x => x.Carts).HasForeignKey(x => x.ProductId);
//            builder.HasOne(x => x.AppUser).WithMany(x => x.Carts).HasForeignKey(x => x.UserId);
//        }

//    }
//}

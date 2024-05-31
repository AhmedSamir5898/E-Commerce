using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Orders;

namespace Talabat.Repository.Data.Config
{
    public class OrderItemConfigrations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(O => O.product, P => P.WithOwner());
            builder.Property(O => O.price)
                .HasColumnType("decimal(18,2)");

        }
    }
}

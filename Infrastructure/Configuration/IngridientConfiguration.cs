using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class IngridientConfiguration : IEntityTypeConfiguration<IngridientEntity>
    {
        public void Configure(EntityTypeBuilder<IngridientEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(a => a.StoragedIngridientEntity)
                .WithOne(b => b.Ingridient)
                .HasForeignKey<StoragedIngridientEntity>(c => c.IngridientId);
        }
    }
}

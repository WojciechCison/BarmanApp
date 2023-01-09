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
    public class CoctailIngridientConfiguration : IEntityTypeConfiguration<CoctailIngridientEntity>
    {
        public void Configure(EntityTypeBuilder<CoctailIngridientEntity> builder)
        {
            builder.HasKey(sc => new { sc.CoctailId, sc.IngridientId });

            builder.HasOne<CoctailEntity>(x => x.Coctail)
                .WithMany(s => s.CoctailIngridients)
                .HasForeignKey(x => x.CoctailId);

            builder.HasOne<IngridientEntity>(x => x.Ingridient)
               .WithMany(s => s.CoctailIngridients)
               .HasForeignKey(x => x.IngridientId);
        }
    }
}

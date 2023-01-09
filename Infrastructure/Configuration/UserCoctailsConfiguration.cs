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
    public class UserCoctailConfiguration : IEntityTypeConfiguration<UserCoctailEntity>
    {
        public void Configure(EntityTypeBuilder<UserCoctailEntity> builder)
        {
            builder.HasKey(sc => new { sc.CoctailId, sc.UserId });

            builder.HasOne<CoctailEntity>(x => x.Coctail)
                .WithMany(s => s.UserCoctails)
                .HasForeignKey(x => x.CoctailId);

            builder.HasOne<UserEntity>(x => x.User)
               .WithMany(s => s.FavoriteCoctailsList)
               .HasForeignKey(x => x.UserId);
        }
    }
}

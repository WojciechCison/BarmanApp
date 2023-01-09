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
    public class CoctailConfiguration : IEntityTypeConfiguration<CoctailEntity>
    {
        public void Configure(EntityTypeBuilder<CoctailEntity> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}

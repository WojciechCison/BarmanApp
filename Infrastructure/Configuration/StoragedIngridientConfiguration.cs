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
    public class StoragedIngridientConfiguration : IEntityTypeConfiguration<StoragedIngridientEntity>
    {
        public void Configure(EntityTypeBuilder<StoragedIngridientEntity> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}

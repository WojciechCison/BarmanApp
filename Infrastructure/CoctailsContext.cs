using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class CoctailsContext : DbContext
    {
        public CoctailsContext(DbContextOptions<CoctailsContext> options) : base(options)
        { 
        }

        public virtual DbSet<CoctailEntity> Coctails { get; set; }

        public virtual DbSet<CoctailIngridientEntity> CoctailIngridients { get; set; }

        public virtual DbSet<IngridientEntity> Ingridients { get; set; }

        public virtual DbSet<UserEntity> Users { get; set; }

        public virtual DbSet<UserCoctailEntity> UserCoctails { get; set; }

        public virtual DbSet<StoragedIngridientEntity> StoragedIngridients { get; set; }

        public virtual DbSet<CommentEntity> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder); 
        }
    }
}

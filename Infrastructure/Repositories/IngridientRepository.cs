using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class IngridientRepository : IIngridientRepository
    {
        private readonly CoctailsContext context;

        public IngridientRepository(CoctailsContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(IngridientEntity ingridient)
        {
            this.context.Ingridients.Add(ingridient);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var igridient = this.context.Ingridients.FirstOrDefault(i => i.Id == id);

            if (igridient != null)
            {
                this.context.Ingridients.Remove(igridient);
                await this.context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<IngridientEntity>> GetIngridientsAsync()
        {
            return await this.context.Ingridients.ToListAsync();
        }
    }
}

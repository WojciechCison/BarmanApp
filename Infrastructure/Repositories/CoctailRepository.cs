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
    public class CoctailRepository : ICoctailRepository
    {
        private readonly CoctailsContext context;

        public CoctailRepository(CoctailsContext context)
        {
            this.context = context;
        }

        public async Task<CoctailEntity> AddAsync(CoctailEntity coctail)
        {
            this.context.Coctails.Add(coctail);
            await this.context.SaveChangesAsync();
            return coctail;
        }

        public async Task DeleteAsync(int id)
        {
            var igridient = this.context.Coctails.FirstOrDefault(i => i.Id == id);

            if (igridient != null)
            {
                this.context.Coctails.Remove(igridient);
                await this.context.SaveChangesAsync();
            }
        }

        public async Task EditAsync(CoctailEntity coctail)
        {
            this.context.Coctails.Update(coctail);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CoctailEntity>> GetCoctailsAsync()
        {
            return await this.context.Coctails.ToListAsync();
        }
    }
}

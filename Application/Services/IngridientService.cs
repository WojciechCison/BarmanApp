using Application.Repositories;
using Application.Services.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class IngridientService : IIngridientService
    {
        private readonly IIngridientRepository igridientRepository;

        public IngridientService(IIngridientRepository igridientRepository)
        {
            this.igridientRepository = igridientRepository;
        }

        public async Task Add(IngridientEntity ingridient)
        {
            await this.igridientRepository.AddAsync(ingridient);
        }

        public async Task Delete(int id)
        {
            await this.igridientRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<IngridientEntity>> GetIngridientsAsync()
        {
            return await this.igridientRepository.GetIngridientsAsync();
        }
    }
}

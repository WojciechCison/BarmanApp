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
        private readonly IIngridientRepository ingridientRepository;

        public IngridientService(IIngridientRepository ingridientRepository)
        {
            this.ingridientRepository = ingridientRepository;
        }

        public async Task Add(IngridientEntity ingridient)
        {
            await this.ingridientRepository.AddAsync(ingridient);
        }

        public async Task Delete(int id)
        {
            await this.ingridientRepository.DeleteAsync(id);
        }

        public async Task EditStorage(int id, double dose, bool add)
        {
            await this.ingridientRepository.EditStorageAsync(id, dose, add);
        }

        public async Task<IEnumerable<IngridientEntity>> GetIngridientsAsync()
        {
            return await this.ingridientRepository.GetIngridientsAsync();
        }

        public async Task<double> GetQuantityAsync(int id)
        {
            return await this.ingridientRepository.GetQuantityAsync(id);
        }
    }
}

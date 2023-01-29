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
    public class CoctailService : ICoctailService
    {
        private readonly ICoctailRepository coctailRepository;
        private readonly IIngridientRepository ingridientRepository;

        public CoctailService(ICoctailRepository coctailRepository, IIngridientRepository ingridientRepository)
        {
            this.coctailRepository = coctailRepository;
            this.ingridientRepository = ingridientRepository;
        }

        public async Task Add(string name, string description, Dictionary<int, double> ingridients)
        {
            var coctail = new CoctailEntity
            { 
                Name = name, 
                Description = description
            };

            var createdCoctail = await this.coctailRepository.AddAsync(coctail);
            var ingridientsInMemory = await this.ingridientRepository.GetIngridientsAsync();

            var coctailIngridientsList = new List<CoctailIngridientEntity>();

            foreach (var item in ingridients)
            {
                coctailIngridientsList.Add(new CoctailIngridientEntity
                {
                    CoctailId = createdCoctail.Id,
                    Dose = item.Value,
                    IngridientId = item.Key,
                    Coctail = createdCoctail,
                    Ingridient = ingridientsInMemory.FirstOrDefault(x => x.Id == item.Key)
                });
            }

            createdCoctail.CoctailIngridients = coctailIngridientsList;

            await this.coctailRepository.EditAsync(createdCoctail);
        }

        public async Task Delete(int id)
        {
            await this.coctailRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CoctailEntity>> GetCoctailsAsync()
        {
            return await this.coctailRepository.GetCoctailsAsync();
        }
    }
}

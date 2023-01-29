using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IIngridientService
    {
        Task<IEnumerable<IngridientEntity>> GetIngridientsAsync();

        Task Add(IngridientEntity ingridient);

        Task Delete(int id);

        Task EditStorage(int id, double dose, bool add);

        Task<double> GetQuantityAsync(int id);
    }
}

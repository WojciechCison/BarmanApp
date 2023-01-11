using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IIngridientRepository
    {
        Task<IEnumerable<IngridientEntity>> GetIngridientsAsync();
        Task AddAsync(IngridientEntity ingridient);
        Task DeleteAsync(int id);
    }
}

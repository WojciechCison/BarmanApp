using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IUserRepository
    {
        Task<bool> CreateAsync(UserEntity user);

        Task<UserEntity> GetByIdAsync(int id);

        Task<int?> ExistAsync(string email, string password);
    }
}

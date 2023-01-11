using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CoctailsContext context;

        public UserRepository(CoctailsContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateAsync(UserEntity user)
        {
            try
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<int?> ExistAsync(string email, string password)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
                return user.Id;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<UserEntity> GetByIdAsync(int id)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

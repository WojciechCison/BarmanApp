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
            var user =  await this.context.Users.FirstOrDefaultAsync(x => x.Id == id);
            user.FavoriteCoctailsList = await this.context.UserCoctails.Where(x => x.UserId == id).ToListAsync();
            return user;
        }

        public async Task EditUserCoctail(int userId, int coctailId, bool add)
        {
            if(add)
            {

                var usercoctailList = new UserCoctailEntity()
                {
                    CoctailId = coctailId,
                    UserId = userId,
                };
                
                this.context.UserCoctails.Add(usercoctailList);
            }
            else
            {
                this.context.UserCoctails.Remove(this.context.UserCoctails.FirstOrDefault(x => x.CoctailId == coctailId && x.UserId == userId));
            }

            await this.context.SaveChangesAsync();
        }

        public async Task Authorize(string code)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.Code == code);

            user.Code = null;

            await this.context.SaveChangesAsync();
        }
    }
}

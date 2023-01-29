using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        public Task<int?> Login(string email, string password);

        public Task<bool> Register(UserEntity userEntity);

        public Task<UserEntity> GetById(int id);

        Task EditUserCoctail(int userId, int coctailId, bool add);

        public Task Authorize(string code);
    }
}

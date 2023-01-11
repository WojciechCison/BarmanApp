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
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserEntity> GetById(int id)
        {
            return await this.userRepository.GetByIdAsync(id);
        }

        public async Task<int?> Login(string email, string password)
        {
            return await this.userRepository.ExistAsync(email, password);
        }

        public async Task<bool> Register(UserEntity userEntity)
        {
            return await this.userRepository.CreateAsync(userEntity);
        }
    }
}

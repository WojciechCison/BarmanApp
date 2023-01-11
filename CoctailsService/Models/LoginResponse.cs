using Domain.Entities;
using Domain.Models;

namespace CoctailsService.Models
{
    public class LoginResponse
    {
        public UserEntity User { get ; set ; }

        public ValidationToken Token { get; set ; }
    }
}

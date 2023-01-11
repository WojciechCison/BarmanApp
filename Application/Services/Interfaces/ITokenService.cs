using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface ITokenService
    {
        bool ValidateToken(string token);

        ValidationToken CreateToken(int userId);
    }
}

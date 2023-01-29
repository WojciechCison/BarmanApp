using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface ITokenService
    {
        bool ValidateToken(string token, string action);

        ValidationToken CreateToken(int userId);
    }
}

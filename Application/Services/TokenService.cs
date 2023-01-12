using Application.Services.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class TokenService : ITokenService
    {
        private List<ValidationToken> Tokens = new List<ValidationToken>();

        public ValidationToken CreateToken(int userId)
        {
            var token = new ValidationToken
            {
                UserId = userId,
                Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
                ExpireTime = DateTime.Now.AddDays(1)
            };

            Tokens.Add(token);

            return token;
        }

        public bool ValidateToken(string token)
        {
            return Tokens.Any(t => t.Token == token && t.ExpireTime > DateTime.Now);
        }
    }
}

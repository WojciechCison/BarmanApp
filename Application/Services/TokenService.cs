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

            this.Tokens.Add(token);

            return token;
        }

        public bool ValidateToken(string token)
        {
            this.RemoveOutdatedToken();

            return Tokens.Any(t => t.Token == token);
        }

        private void RemoveOutdatedToken()
        {
            this.Tokens.RemoveAll(x => x.ExpireTime < DateTime.Now);
        }
    }
}

using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class TokenService : ITokenService
    {
        private List<ValidationToken> Tokens = new List<ValidationToken>();
        public ILogger<TokenService> Logger { get; }

        public TokenService(ILogger<TokenService> logger)
        {
            Logger = logger;
        }


        public ValidationToken CreateToken(int userId)
        {
            var token = new ValidationToken
            {
                UserId = userId,
                Token = Guid.NewGuid().ToString(),
                ExpireTime = DateTime.Now.AddDays(1)
            };

            this.Tokens.Add(token);

            return token;
        }

        public bool ValidateToken(string token, string action)
        {
            this.RemoveOutdatedToken();

            var validated = Tokens.Any(t => t.Token == token.TrimEnd());

            if(validated)
            {

                this.Logger.LogInformation("User with id {0} perform action {1}", Tokens.FirstOrDefault(t => t.Token == token.TrimEnd()).UserId, action);
            }
            else
            {
                this.Logger.LogInformation("User try to login with wrong token {0}", token);
            }

            return validated;
        }

        private void RemoveOutdatedToken()
        {
            this.Tokens.RemoveAll(x => x.ExpireTime < DateTime.Now);
        }
    }
}

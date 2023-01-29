using Application.Services;
using Application.Services.Interfaces;
using CoctailsService.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoctailsService.Controllers
{
    [Controller]
    [Route("users/")]
    public class UserController : Controller
    {
        private readonly ILogger<IngridientController> logger;
        private readonly ITokenService tokenService;
        private readonly IUserService userService;

        public UserController(ILogger<IngridientController> logger, ITokenService tokenService, IUserService userService)
        {
            this.logger = logger;
            this.tokenService = tokenService;
            this.userService = userService;
        }

        [HttpPost]
        [Route("Login/")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var userId = await this.userService.Login(loginRequest.Email, loginRequest.Password);

            if(userId == null)
            {
                return NotFound();
            }

            var user = await this.userService.GetById(userId.Value);
            var token = this.tokenService.CreateToken(userId.Value);

            this.logger.LogInformation("User with id {0} log in", userId.Value);

            return Ok(new LoginResponse { Token = token, User = user });
        }

        [HttpPost]
        [Route("Register/")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var user = new UserEntity
            {
                Surname = registerRequest.Surname,
                Email = registerRequest.Email,
                Name = registerRequest.Name,
                Password = registerRequest.Password,
                IsAdmin = registerRequest.IsAdmin,
            };

            if(!await this.userService.Register(user))
            {
                return BadRequest();
            }

            this.logger.LogInformation("User with id {0} registered", user.Id);

            return Ok();
        }

        [HttpPut]
        [Route("Coctails/{userId}/Add/{coctailId}")]
        public async Task<IActionResult> AddFavoriteCoctail(int userId, int coctailId,[FromBody] string token)
        {
            if (token == null || !this.tokenService.ValidateToken(token, "add new favorite ingridient"))
            {
                return Unauthorized();
            }

            try
            {
                await this.userService.EditUserCoctail(userId, coctailId, true);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("Coctails/{userId}/Remove/{coctailId}")]
        public async Task<IActionResult> RemoveFavoriteCoctail(int userId, int coctailId,[FromBody] string token)
        {
            if (token == null || !this.tokenService.ValidateToken(token, "remove favorite ingridient"))
            {
                return Unauthorized();
            }

            try
            {
                await this.userService.EditUserCoctail(userId, coctailId, false);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

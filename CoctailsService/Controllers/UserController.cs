﻿using Application.Services;
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
        private readonly ITokenService tokenService;
        private readonly IUserService userService;

        public UserController(ITokenService tokenService, IUserService userService)
        {
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
            };

            if(!await this.userService.Register(user))
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPut]
        [Route("Coctails/{userId}/Add{coctailId}")]
        public async Task<IActionResult> AddFavoriteCoctail(int userId, int coctailId, [FromBody] string token)
        {
            if (token == null || !this.tokenService.ValidateToken(token))
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
        [Route("Coctails/{userId}/Remove{coctailId}")]
        public async Task<IActionResult> RemoveFavoriteCoctail(int userId, int coctailId, [FromBody] string token)
        {
            if (token == null || !this.tokenService.ValidateToken(token))
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

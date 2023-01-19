using Application.Services.Interfaces;
using CoctailsService.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoctailsService.Controllers
{
    [Controller]
    [Route("coctails/")]
    public class CoctailController : Controller
    {
        private readonly ICoctailService coctailService;
        private readonly ITokenService tokenService;

        public CoctailController(ICoctailService coctailService, ITokenService tokenService)
        {
            this.coctailService = coctailService;
            this.tokenService = tokenService;
        }

        [HttpGet("{token}")]
        public async Task<IActionResult> GetAll(string token)
        {
            if (token == null || !this.tokenService.ValidateToken(token))
            {
                return Unauthorized();
            }

            try
            {
                var coctials = await this.coctailService.GetCoctailsAsync();

                return Ok(coctials);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost("{token}")]
        public async Task<IActionResult> Add([FromBody] CoctailRequest coctail, string token)
        {
            if (token == null || !this.tokenService.ValidateToken(token))
            {
                return Unauthorized();
            }

            try
            {
                await this.coctailService.Add(coctail.Name, coctail.Ingridients);

                return Ok("Ok");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromBody] string token)
        {
            if (token == null || !this.tokenService.ValidateToken(token))
            {
                return Unauthorized();
            }

            try
            {
                await this.coctailService.Delete(id);

                return Ok("Ok");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

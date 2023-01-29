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
        private readonly ILogger<IngridientController> logger;
        private readonly ICoctailService coctailService;
        private readonly ITokenService tokenService;

        public CoctailController(ILogger<IngridientController> logger, ICoctailService coctailService, ITokenService tokenService)
        {
            this.logger = logger;
            this.coctailService = coctailService;
            this.tokenService = tokenService;
        }

        [HttpGet("{token}")]
        public async Task<IActionResult> GetAll( string token)
        {
            if (token == null || !this.tokenService.ValidateToken(token, "requested coctails"))
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

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CoctailRequest coctail)
        {
            if (coctail.Token == null || !this.tokenService.ValidateToken(coctail.Token, "add coctail"))
            {
                return Unauthorized();
            }

            try
            {
                await this.coctailService.Add(coctail.Name, coctail.Description, coctail.Ingridients);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromBody] string token)
        {
            if (token == null || !this.tokenService.ValidateToken(token, "remove coctail"))
            {
                return Unauthorized();
            }

            try
            {
                await this.coctailService.Delete(id);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

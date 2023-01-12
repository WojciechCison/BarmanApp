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
                var ingridients = await this.coctailService.GetCoctailsAsync();
                var requestedIngridient = new List<CoctailResponse>();

                foreach (var item in ingridients)
                {
                    var coctail = new CoctailResponse
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Ingridients = new Dictionary<int, double>()
                    };

                    foreach (var item2 in item.CoctailIngridients)
                    {
                        coctail.Ingridients.Add(item2.IngridientId, item2.Dose);
                    }

                    requestedIngridient.Add(coctail);
                }

                return Ok(requestedIngridient);
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

                return NoContent();
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

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

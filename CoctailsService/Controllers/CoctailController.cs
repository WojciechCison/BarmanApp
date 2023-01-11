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

        public CoctailController(ICoctailService coctailService)
        {
            this.coctailService = coctailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var igridients = await this.coctailService.GetCoctailsAsync();

                return Ok(igridients);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CoctailRequest coctail)
        {
            try
            {
                await this.coctailService.Add(coctail.Name, coctail.ingridients);

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
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

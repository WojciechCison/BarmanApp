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

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CoctailRequest coctail)
        {
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

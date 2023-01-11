using Application.Services.Interfaces;
using CoctailsService.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoctailsService.Controllers
{
    [Controller]
    [Route("ingridients/")]
    public class IngridientController : Controller
    {
        private readonly IIngridientService igridientService;

        public IngridientController(IIngridientService igridientService)
        {
            this.igridientService = igridientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var igridients = await this.igridientService.GetIngridientsAsync();

                return Ok(igridients);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] IngridientRequest ingridient)
        {
            try
            {
                var newIngridient = new IngridientEntity
                {
                    Name = ingridient.Name,
                    Unit = ingridient.Unit,
                };

                await this.igridientService.Add(newIngridient);

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
                await this.igridientService.Delete(id);

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

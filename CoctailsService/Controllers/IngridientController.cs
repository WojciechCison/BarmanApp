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
        private readonly ILogger<IngridientController> logger;
        private readonly IIngridientService igridientService;
        private readonly ITokenService tokenService;

        public IngridientController(ILogger<IngridientController> logger, IIngridientService igridientService, ITokenService tokenService)
        {
            this.logger = logger;
            this.igridientService = igridientService;
            this.tokenService = tokenService;
        }

        [HttpGet("{token}")]
        public async Task<IActionResult> GetAll( string token)
        {
            if(token == null || !this.tokenService.ValidateToken(token, "requested ingridients"))
            {
                return Unauthorized();
            }

            try
            {
                var igridients = await this.igridientService.GetIngridientsAsync();
                var list = new List<IngridientResponse>();

                foreach (var ingridient in igridients)
                {
                    var quantity = await this.igridientService.GetQuantityAsync(ingridient.Id);
                    list.Add(new IngridientResponse
                    {
                        Id = ingridient.Id,
                        Name = ingridient.Name,
                        Unit = ingridient.Unit,
                        Quantity = quantity,
                    });
                }

                return Ok(list);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] IngridientRequest ingridient)
        {
            if (ingridient.Token == null || !this.tokenService.ValidateToken(ingridient.Token, "add ingridient"))
            {
                return Unauthorized();
            }

            try
            {
                var newIngridient = new IngridientEntity
                {
                    Name = ingridient.Name,
                    Unit = ingridient.Unit,
                };

                await this.igridientService.Add(newIngridient);

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
            if (token == null || !this.tokenService.ValidateToken(token, "remove ingridient"))
            {
                return Unauthorized();
            }

            try
            {
                await this.igridientService.Delete(id);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("/Storage/{id}/Add/{dose}")]
        public async Task<IActionResult> AddSoragedIngridient(int id, [FromBody] string token, double dose)
        {
            if (token == null || !this.tokenService.ValidateToken(token, "add quantity to ingridient"))
            {
                return Unauthorized();
            }

            try
            {
                await this.igridientService.EditStorage(id, dose, true);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("/Storage/{id}/Remove/{dose}")]
        public async Task<IActionResult> RemoveSoragedIngridient(int id, [FromBody] string token, double dose)
        {
            if (token == null || !this.tokenService.ValidateToken(token, "remove quantity from ingridient"))
            {
                return Unauthorized();
            }

            try
            {
                await this.igridientService.EditStorage(id, dose, false);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

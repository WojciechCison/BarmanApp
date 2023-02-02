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

        [HttpGet]
        [Route("/Comments/{token}")]
        public async Task<IActionResult> GEtComments(string token)
        {
            if (token == null || !this.tokenService.ValidateToken(token, "Add comment"))
            {
                return Unauthorized();
            }

            try
            {
                var comments = await this.coctailService.GetComments();

                return Ok(comments);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("/Comments/{token}")]
        public async Task<IActionResult> AddComment(string token, [FromBody] CommentRequest comment)
        {
            if (token == null || !this.tokenService.ValidateToken(token, "Add comment"))
            {
                return Unauthorized();
            }

            try
            {
                var com = new CommentEntity
                {
                    CoctailId = comment.CoctailId,
                    UserId = comment.UserId,
                    Comment = comment.Comment
                };

                var createdComment = await this.coctailService.AddCommentAsync(com);

                return Ok(createdComment);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("/Comments/{token}/{commentId}")]
        public async Task<IActionResult> EditComment(string token, int commentId, [FromBody] CommentRequest comment)
        {
            if (token == null || !this.tokenService.ValidateToken(token, "Edit comment"))
            {
                return Unauthorized();
            }

            try
            {
                var com = new CommentEntity
                {
                    Id = commentId,
                    CoctailId = comment.CoctailId,
                    UserId = comment.UserId,
                    Comment = comment.Comment
                };

                await this.coctailService.EditCommentAsync(com);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("/Comments/{token}/{commentId}")]
        public async Task<IActionResult> RemoveComment(string token, int commentId)
        {
            if (token == null || !this.tokenService.ValidateToken(token, "Delete comment"))
            {
                return Unauthorized();
            }

            try
            {
                await this.coctailService.DeleteCommentAsync(commentId);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

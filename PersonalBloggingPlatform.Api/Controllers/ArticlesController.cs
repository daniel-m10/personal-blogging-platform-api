using Microsoft.AspNetCore.Mvc;
using PersonalBloggingPlatform.Api.DTOs;
using PersonalBloggingPlatform.Application.Services;
using PersonalBloggingPlatform.Domain.Entities;

namespace PersonalBloggingPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController(ArticleService service) : ControllerBase
    {
        private readonly ArticleService _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetAll()
        {
            var articles = await _service.GetAllAsync();
            return Ok(articles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Article?>> GetById(Guid id)
        {
            var article = await _service.GetByIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }

        [HttpPost]
        public async Task<ActionResult<Article>> Create(CreateArticleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var article = await _service.CreateAsync(
                request.Title,
                request.Content,
                request.Tags,
                request.Author
            );
            return CreatedAtAction(nameof(GetById), new { id = article.Id }, article);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Article>> Update(Guid id, UpdateArticleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedArticle = await _service.UpdateAsync(
                id,
                request.Title,
                request.Content,
                request.Tags
            );

            if (updatedArticle == null)
            {
                return NotFound();
            }

            return Ok(updatedArticle);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

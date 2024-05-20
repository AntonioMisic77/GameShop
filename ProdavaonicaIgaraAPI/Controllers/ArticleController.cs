using Microsoft.AspNetCore.Mvc;
using ProdavaonicaIgaraAPI.Data.Articles;
using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Services;

namespace ProdavaonicaIgaraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        #region properties
        private readonly IArticleService _articleService;
        #endregion

        #region ctor
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        #endregion

        #region endpoints

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> GetArticleAsync(int id)
        {
            var article = await _articleService.GetArticleAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }

        [HttpPost("paged")]
        public async Task<ActionResult<PagedResult<ArticleDto>>> GetArticlesAsync(QueryParametars parametars)
        {
            var articles = await _articleService.GetArticlesAsync(parametars);

            return Ok(articles);
        }

        [HttpPost]
        public async Task<ActionResult<ArticleDto>> CreateArticleAsync(ArticleDto articleDto)
        {
            var createdArticle = await _articleService.CreateArticleAsync(articleDto);

            if (createdArticle == null)
            {
                return BadRequest();
            }   

            return Ok(createdArticle);
        }

        [HttpPut]
        public async Task<ActionResult<ArticleDto>> UpdateArticleAsync(ArticleDto articleDto)
        {
            var updatedArticle = await _articleService.UpdateArticleAsync(articleDto);

            if (updatedArticle == null)
            {
                return BadRequest();
            }

            return Ok(updatedArticle);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ArticleDto>> DeleteArticle(int id)
        {
            var deletedArticle = await _articleService.DeleteArticle(id);

            if (deletedArticle == null)
            {
                return BadRequest();
            }

            return Ok(deletedArticle);
        }

        #endregion
    }
}

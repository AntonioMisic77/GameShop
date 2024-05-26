using Microsoft.AspNetCore.Mvc;
using Moq;
using ProdavaonicaIgaraAPI.Controllers;
using ProdavaonicaIgaraAPI.Data.Articles;
using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Models;
using ProdavaonicaIgaraAPI.Services;

namespace ProdavaonicaIgara.Tests.Controllers
{
    public class ArticleControllerTests
    {
        private readonly Mock<IArticleService> _mockArticleService;
        private readonly ArticleController _controller;

        public ArticleControllerTests()
        {
            _mockArticleService = new Mock<IArticleService>();
            _controller = new ArticleController(_mockArticleService.Object);
        }

        [Fact]
        public async Task GetArticlesAsync_ReturnsOkResult_WithPagedResult()
        {
            // Arrange
            var parametars = new QueryParametars
            {
                PageNumber = 1,
                PageSize = 1,
                filterText = ""
            };
            var pagedResult = new PagedResult<ArticleDto> { Items = new List<ArticleDto> { new ArticleDto { Id = 1, Name = "Article 1" } } };
            _mockArticleService.Setup(service => service.GetArticlesAsync(parametars)).ReturnsAsync(pagedResult);

            
            var result = await _controller.GetArticlesAsync(parametars);


            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<PagedResult<ArticleDto>>(okResult.Value);
            Assert.Single(returnValue.Items);
        }

        [Fact]
        public async Task CreateArticleAsync_ReturnsOkResult_WithCreatedArticle()
        {
            
            var articleDto = new ArticleDto { Id = 1, Name = "Article 1" };
            _mockArticleService.Setup(service => service.CreateArticleAsync(articleDto)).ReturnsAsync(articleDto);

            
            var result = await _controller.CreateArticleAsync(articleDto);

            
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ArticleDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }


        [Fact]
        public async Task UpdateArticleAsync_ReturnsOkResult_WithUpdatedArticle()
        {
            var articleDto = new ArticleDto { Id = 1, Name = "Updated Article" };
            _mockArticleService.Setup(service => service.UpdateArticleAsync(articleDto)).ReturnsAsync(articleDto);

            var result = await _controller.UpdateArticleAsync(articleDto);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ArticleDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

      
        [Fact]
        public async Task DeleteArticle_ReturnsOkResult_WithDeletedArticle()
        {
            var articleDto = new ArticleDto { Id = 1, Name = "Article 1" };
            _mockArticleService.Setup(service => service.DeleteArticle(1)).ReturnsAsync(articleDto);

            var result = await _controller.DeleteArticle(1);


            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ArticleDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

       
    }
}

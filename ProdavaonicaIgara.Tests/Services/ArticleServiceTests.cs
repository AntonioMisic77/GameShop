using AutoMapper;
using Moq;
using ProdavaonicaIgaraAPI.Configurations;
using ProdavaonicaIgaraAPI.Data.Articles;
using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Models;
using ProdavaonicaIgaraAPI.Repositories;
using ProdavaonicaIgaraAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdavaonicaIgara.Tests.Services
{
    public class ArticleServiceTests
    {

        private async Task<IMapper> GetMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperConfig());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            return mapper;
        }

        [Fact]
        public async void ArticleService_GetPagedArticles_ReturnsPagedArticles()
        {
            var articlesList = new List<Article> { new Article { Id=1,Name="Kockice"}, new Article {Id=2,Name="Karte"} };
            var queryParameters = new QueryParametars 
            {
                StartIndex = 0,
                PageNumber = 1, 
                PageSize = 2,
                filterText = ""
            };
            var repoMock = new Mock<IArticleRepository>();
            var mapper  = await GetMapper();
 
            repoMock.Setup(repo => repo.GetPagedAsync(queryParameters)).Returns(Task.FromResult(articlesList));
            repoMock.Setup(repo => repo.GetArtilceCount("")).Returns(Task.FromResult(2));

            var articleService = new ArticleService(repoMock.Object,mapper);
            var pagedArticles = await articleService.GetArticlesAsync(queryParameters);

            Assert.Equal(2, pagedArticles.Items.Count);
            Assert.IsType<PagedResult<ArticleDto>>(pagedArticles);

        }

        [Fact]
        public async void ArticleService_GetPagedAndFilteredArticles_ReturnsPagedAndFilteredArticles()
        {
            var articlesList = new List<Article> { new Article { Id=1,Name="Kockice"}, new Article { Id=2,Name="Karte"} };
            var queryParameters = new QueryParametars
            {
                StartIndex = 0,
                PageNumber = 1, 
                PageSize = 2,
                filterText = "Kockice"
            };
            articlesList.RemoveAt(1);
            var repoMock = new Mock<IArticleRepository>();
            var mapper  =  await GetMapper();
            
            repoMock.Setup(repo => repo.GetPagedAsync(queryParameters)).Returns(Task.FromResult(articlesList));

            var articleService = new ArticleService(repoMock.Object,mapper);

            var pagedArticles = await articleService.GetArticlesAsync(queryParameters);

            Assert.Equal(1, pagedArticles.Items.Count);
            Assert.IsType<PagedResult<ArticleDto>>(pagedArticles);

        }

        [Fact]
        public async void ArticleService_CreateArticle_ReturnsCreatedArticle()
        {
            var article = new Article { Id=1,Name="Kockice"};
            var articleDto = new ArticleDto { Id=1,Name="Kockice"};
            var repoMock = new Mock<IArticleRepository>();
            var mapper  =  await GetMapper();
            
            repoMock.Setup(repo => repo.CreateAsync(It.IsAny<Article>())).Returns(Task.FromResult(article));

            var articleService = new ArticleService(repoMock.Object,mapper);

            var createdArticle = await articleService.CreateArticleAsync(articleDto);

            Assert.Equal(articleDto.Name, createdArticle.Name);
            Assert.IsType<ArticleDto>(createdArticle);

        }

        [Fact]
        public async void ArticleService_UpdateArticle_ReturnsUpdatedArticle()
        {
            var article = new Article { Id=1,Name="Nove Kockice"};
            var articleDto = new ArticleDto { Id=1,Name="Nove Kockice"};
            var repoMock = new Mock<IArticleRepository>();
            var mapper  =  await GetMapper();
            
            repoMock.Setup(repo => repo.UpdateAsync(It.IsAny<Article>())).Returns(Task.FromResult(article));

            var articleService = new ArticleService(repoMock.Object,mapper);

            var updatedArticle = await articleService.UpdateArticleAsync(articleDto);

            Assert.Equal(articleDto.Name, updatedArticle.Name);
            Assert.IsType<ArticleDto>(updatedArticle);

        }

        [Fact]
        public async void ArticleService_DeleteArticle_ReturnsDeletedArticle()
        {
            var article = new Article { Id=1,Name="Kockice"};
            var articleDto = new ArticleDto { Id=1,Name="Kockice"};
            var repoMock = new Mock<IArticleRepository>();
            var mapper  =  await GetMapper();
            
            repoMock.Setup(repo => repo.DeleteAsync(1)).Returns(Task.FromResult(article));

            var articleService = new ArticleService(repoMock.Object,mapper);

            var deletedArticle = await articleService.DeleteArticle(1);

            Assert.Equal(articleDto.Name, deletedArticle.Name);
            Assert.IsType<ArticleDto>(deletedArticle);

        }
    }
}

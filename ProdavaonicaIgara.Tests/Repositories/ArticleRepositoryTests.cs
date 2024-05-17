using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Models;
using ProdavaonicaIgaraAPI.Repositories;
using ProdavaonicaIgaraAPI.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProdavaonicaIgara.Tests.Repositories
{

    public class ArticleRepositoryTests
    {
        private PIGDbContext GetDbContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var dbOptions = new DbContextOptionsBuilder<PIGDbContext>()
                            .UseSqlite(connection)
                            .Options;

            var dbContext =  new PIGDbContext(dbOptions);

            dbContext.Database.EnsureCreated();

            return dbContext;
        }

        private void initalizeDb(PIGDbContext context)
        {
            context.Suppliers.AddRangeAsync(new List<Supplier>
            {
                new Supplier
                {
                    Id = 1,
                    Name = "Supplier1",
                    Address = "Address1",
                    Email = "Email1",
                }
            });

            context.SaveChanges();

            context.Articles.AddRangeAsync(new List<Article>
            {
                new Article
                {
                    Id = 1,
                    Name = "Article1",
                    Description = "Description1",
                    Price = 100,
                    SupplierId = 1
                },
                new Article
                {
                    Id = 2,
                    Name = "Article2",
                    Description = "Description2",
                    Price = 200,
                    SupplierId = 1
                },
                new Article
                {
                    Id = 3,
                    Name = "Article3",
                    Description = "Description3",
                    Price = 300,
                    SupplierId = 1
                },
                new Article
                {
                    Id = 4,
                    Name = "Article4",
                    Description = "Description4",
                    Price = 400,
                    SupplierId = 1
                },
                new Article
                {
                    Id = 5,
                    Name = "Article5",
                    Description = "Description5",
                    Price = 500,
                    SupplierId = 1
                }
            });

            context.SaveChanges();
        }

        [Fact]
        public async void ArticleRepository_GetPagedArticles_ReturnsPagedArticles()
        {
            var _context = GetDbContext();
            initalizeDb(_context);

            var mockGenericRepository = new Mock<IGenericRepository<Article>>();

            var articleRepository = new ArticleRepository(mockGenericRepository.Object,_context);

           
            var pagedArticles = await articleRepository.GetPagedAsync(new QueryParametars
            {
                filterText = "",
                PageSize = 2,
                StartIndex = 0,
                PageNumber = 1
            });

            
            Assert.Equal(2, pagedArticles.Count);
            Assert.Equal("Article1", pagedArticles[0].Name);
            Assert.Equal("Article2", pagedArticles[1].Name);
        }

        [Fact]
        public async void ArticleRepository_GetPagedAndFilteredArticles_ReturnsPagedAndFilteredArticles()
        {
            var _context = GetDbContext();
            initalizeDb(_context);

            var mockGenericRepository = new Mock<IGenericRepository<Article>>();

            var articleRepository = new ArticleRepository(mockGenericRepository.Object,_context);

           
            var pagedArticles = await articleRepository.GetPagedAsync(new QueryParametars
            {
                filterText = "Article1",
                PageSize = 2,
                StartIndex = 0,
                PageNumber = 1
            });

            
            Assert.Equal(1, pagedArticles.Count);
            Assert.Equal("Article1", pagedArticles[0].Name);

        }

        [Fact]
        public async void ArticleRepository_CreateArticle_ReturnsCreatedArticle()
        {
            var _context = GetDbContext();
            initalizeDb(_context);

            var mockGenericRepository = new Mock<IGenericRepository<Article>>();

            mockGenericRepository.Setup(repo => repo.CreateAsync(It.IsAny<Article>()))
                .ReturnsAsync((Article source) => source);

            var articleRepository = new ArticleRepository(mockGenericRepository.Object,_context);

            var article = new Article
            {
                Id = 6,
                Name = "Article6",
                Description = "Description6",
                Price = 600,
                SupplierId = 1
            };

            var createdArticle = await articleRepository.CreateAsync(article);

            Assert.Equal("Article6", createdArticle.Name);
            Assert.Equal("Description6", createdArticle.Description);
            Assert.Equal(600, createdArticle.Price);
            Assert.Equal(1, createdArticle.SupplierId);

        }

        [Fact]
        public async void ArticleRepository_UpdateArticle_ReturnsUpdatedArticle()
        {
            var _context = GetDbContext();
            initalizeDb(_context);

            var mockGenericRepository = new Mock<IGenericRepository<Article>>();

            mockGenericRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Article>()))
                .ReturnsAsync((Article source) => source);

            var articleRepository = new ArticleRepository(mockGenericRepository.Object,_context);

            var article = new Article
            {
                Id = 1,
                Name = "Article1Updated",
                Description = "Description1Updated",
                Price = 1000,
                SupplierId = 1
            };

            var updatedArticle = await articleRepository.UpdateAsync(article);

            Assert.Equal("Article1Updated", updatedArticle.Name);
            Assert.Equal("Description1Updated", updatedArticle.Description);
            Assert.Equal(1000, updatedArticle.Price);
            Assert.Equal(1, updatedArticle.SupplierId);

        }

        [Fact]
        public async void ArticleRepository_DeleteArticle_ReturnsDeletedArticle()
        {
            var _context = GetDbContext();
            initalizeDb(_context);

            var mockGenericRepository = new Mock<IGenericRepository<Article>>();

            mockGenericRepository.Setup(repo => repo.DeleteAsync(1))
                .ReturnsAsync((int id) => _context.Articles.FirstOrDefault(a => a.Id == id));

            var articleRepository = new ArticleRepository(mockGenericRepository.Object,_context);

            var deletedArticle = await articleRepository.DeleteAsync(1);

            Assert.Equal("Article1", deletedArticle.Name);
            Assert.Equal("Description1", deletedArticle.Description);
            Assert.Equal(100, deletedArticle.Price);
            Assert.Equal(1, deletedArticle.SupplierId);

        }
    }
}

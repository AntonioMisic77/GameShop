using AutoMapper;
using Moq;
using ProdavaonicaIgaraAPI.Configurations;
using ProdavaonicaIgaraAPI.Data.Articles;
using ProdavaonicaIgaraAPI.Data.Exceptions;
using ProdavaonicaIgaraAPI.Data.ReceiptItem;
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
    public class ReceiptItemServiceTests
    {
        #region metods
        private async Task<IMapper> GetMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperConfig());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            return mapper;
        }
        #endregion

        #region tests
        [Fact]
        public async void ReceiptItemService_CreateReceiptItem_ReturnsCreatedReceiptItem()
        {
            var mapper =  await GetMapper();
            var mockReceiptItemRepository = new Mock<IReceiptItemRepository>();
            var mockArticleService = new Mock<IArticleService>();

            var expectedReceiptItem = new ReceiptItemDto { Id = 1, ArticleId = 1, ReceiptId = 1, Quantity = 1 };

            mockArticleService.Setup(service => service.GetArticleAsync(It.IsAny<int>()))
                              .Returns(Task.FromResult(new ArticleDto { Id = 1, Name = "Article1", Description = "Description1", Price = 100, SupplierId = 1, StockQuantity= 100 }));

            mockReceiptItemRepository.Setup(repo => repo.checkUniqueRecipteItem(It.IsAny<int>(), It.IsAny<int>()))
                                     .Returns(Task.FromResult(false));

            mockReceiptItemRepository.Setup(repo => repo.CreateAsync(It.IsAny<ReceiptItem>()))
                                     .Returns(Task.FromResult(new ReceiptItem { Id = 1, ArticleId = 1, ReceiptId = 1, Quantity = 1 }));

            var receiptItemService = new ReceiptItemService(mockReceiptItemRepository.Object,mockArticleService.Object,mapper);

            var createdReceiptItem = await receiptItemService.CreateReceiptItemAsync(expectedReceiptItem);

            Assert.NotNull(createdReceiptItem);
            Assert.Equal(expectedReceiptItem.Id, createdReceiptItem.Id);
            Assert.Equal(expectedReceiptItem.ArticleId, createdReceiptItem.ArticleId);
            Assert.Equal(expectedReceiptItem.ReceiptId, createdReceiptItem.ReceiptId);
            Assert.Equal(expectedReceiptItem.Quantity, createdReceiptItem.Quantity);
        }


        [Fact]
        public async void ReceiptItemService_CreateReceiptItem_ThrowsUniqueConstraint()
        {
            var mapper = await GetMapper();
            var mockReceiptItemRepository = new Mock<IReceiptItemRepository>();
            var mockArticleService = new Mock<IArticleService>();

            var expectedReceiptItem = new ReceiptItemDto { Id = 1, ArticleId = 1, ReceiptId = 1, Quantity = 1 };

            mockReceiptItemRepository.Setup(repo => repo.checkUniqueRecipteItem(It.IsAny<int>(), It.IsAny<int>()))
                                     .Returns(Task.FromResult(true));

            mockReceiptItemRepository.Setup(repo => repo.CreateAsync(It.IsAny<ReceiptItem>()))
                                     .Returns(Task.FromResult(new ReceiptItem { Id = 1, ArticleId = 1, ReceiptId = 1, Quantity = 1 }));

            var receiptItemService = new ReceiptItemService(mockReceiptItemRepository.Object, mockArticleService.Object, mapper);

            await Assert.ThrowsAsync<UniqueConstraint>(() => receiptItemService.CreateReceiptItemAsync(expectedReceiptItem));
        }

        [Fact]
        public async void ReceiptItemService_CreateReceiptItem_ThrowsNotEnoughQuantity()
        {
            var mapper = await GetMapper();
            var mockReceiptItemRepository = new Mock<IReceiptItemRepository>();
            var mockArticleService = new Mock<IArticleService>();

            var expectedReceiptItem = new ReceiptItemDto { Id = 1, ArticleId = 1, ReceiptId = 1, Quantity = 1 };

            mockArticleService.Setup(service => service.GetArticleAsync(It.IsAny<int>()))
                              .Returns(Task.FromResult(new ArticleDto { Id = 1, Name = "Article1", Description = "Description1", Price = 100, SupplierId = 1, StockQuantity = 0 }));

            mockReceiptItemRepository.Setup(repo => repo.checkUniqueRecipteItem(It.IsAny<int>(), It.IsAny<int>()))
                                     .Returns(Task.FromResult(false));

            mockReceiptItemRepository.Setup(repo => repo.CreateAsync(It.IsAny<ReceiptItem>()))
                                     .Returns(Task.FromResult(new ReceiptItem { Id = 1, ArticleId = 1, ReceiptId = 1, Quantity = 1 }));

            var receiptItemService = new ReceiptItemService(mockReceiptItemRepository.Object, mockArticleService.Object, mapper);

            await Assert.ThrowsAsync<NotEnoughQuantity>(() => receiptItemService.CreateReceiptItemAsync(expectedReceiptItem));
        }

        [Fact]
        public async void ReceiptItemService_UpdateReceiptItem_ThrowsNotEnoughQuantity()
        {
            var mapper = await GetMapper();
            var mockReceiptItemRepository = new Mock<IReceiptItemRepository>();
            var mockArticleService = new Mock<IArticleService>();

            var expectedReceiptItem = new ReceiptItemDto { Id = 1, ArticleId = 1, ReceiptId = 1, Quantity = 2 };

            mockArticleService.Setup(service => service.GetArticleAsync(It.IsAny<int>()))
                              .Returns(Task.FromResult(new ArticleDto { Id = 1, Name = "Article1", Description = "Description1", Price = 100, SupplierId = 1, StockQuantity = 0 }));

            mockReceiptItemRepository.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                                     .Returns(Task.FromResult(new ReceiptItem { Id = 1, ArticleId = 1, ReceiptId = 1, Quantity = 1 }));

            var receiptItemService = new ReceiptItemService(mockReceiptItemRepository.Object, mockArticleService.Object, mapper);

            Assert.ThrowsAsync<NotEnoughQuantity>(() => receiptItemService.UpdateReceiptItemAsync(expectedReceiptItem));
        }

        [Fact]
        public async void ReceiptItemService_DeleteReceiptItem_ReturnsDeletedReceiptItem()
        {
            var mapper = await GetMapper();
            var mockReceiptItemRepository = new Mock<IReceiptItemRepository>();
            var mockArticleService = new Mock<IArticleService>();

            var expectedReceiptItem = new ReceiptItemDto { Id = 1, ArticleId = 1, ReceiptId = 1, Quantity = 1 };
            
            mockReceiptItemRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
                                     .Returns(Task.FromResult(new ReceiptItem { Id = 1, ArticleId = 1, ReceiptId = 1, Quantity = 1 }));

            var receiptItemService = new ReceiptItemService(mockReceiptItemRepository.Object, mockArticleService.Object, mapper);

            var deletedReceiptItem = await receiptItemService.DeleteReceiptItem(1);

            Assert.NotNull(deletedReceiptItem);
            Assert.Equal(expectedReceiptItem.Id, deletedReceiptItem.Id);
            Assert.Equal(expectedReceiptItem.ArticleId, deletedReceiptItem.ArticleId);
            Assert.Equal(expectedReceiptItem.ReceiptId, deletedReceiptItem.ReceiptId);
            Assert.Equal(expectedReceiptItem.Quantity, deletedReceiptItem.Quantity);
        }
        #endregion
    }
}

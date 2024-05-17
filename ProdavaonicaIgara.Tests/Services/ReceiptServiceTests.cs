using AutoMapper;
using Moq;
using ProdavaonicaIgaraAPI.Configurations;
using ProdavaonicaIgaraAPI.Data.Articles;
using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Data.Receipt;
using ProdavaonicaIgaraAPI.Data.ReceiptItem;
using ProdavaonicaIgaraAPI.Models;
using ProdavaonicaIgaraAPI.Repositories;
using ProdavaonicaIgaraAPI.Services;
using ProdavaonicaIgaraAPI.Services.Receipt;
using ProdavaonicaIgaraAPI.Services.ReceiptItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdavaonicaIgara.Tests.Services
{
    public class ReceiptServiceTests
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
        public async void ReceiptService_GetPagedReceipts_ReturnsPagedReceipts()
        {
            var receiptsList = new List<Receipt> { new Receipt { Id=1,PaymentMethod="Card"}, new Receipt { Id=2,PaymentMethod="Cash"} };
            var queryParameters = new QueryParametars
            {
                StartIndex = 0,
                PageNumber = 1, 
                PageSize = 2,
                filterText = ""
            };
            var repoMock = new Mock<IReceiptRepository>();
            var receiptItemMock = new Mock<IReceiptItemService>();
            var mapper  = await GetMapper();
 
            repoMock.Setup(repo => repo.GetPagedReceipts(queryParameters)).Returns(Task.FromResult(receiptsList));
            repoMock.Setup(repo => repo.GetReceiptsCount()).Returns(Task.FromResult(2));

            var receiptService = new ReceiptService(repoMock.Object,mapper,receiptItemMock.Object);
            var pagedReceipts = await receiptService.GetReceiptsAsync(queryParameters);


            Assert.Equal(2, pagedReceipts.Items.Count);
            Assert.IsType<PagedResult<ReceiptDto>>(pagedReceipts);
        }

        [Fact]
        public async void ReceiptService_CreateReceipt_ReturnsCreatedReceipt()
        {

            var receipt = new Receipt { Id = 1, PaymentMethod = "Cash"};
            var receiptDto = new ReceiptDto { Id = 1, PaymentMethod = "Cash" };

            var repoMock = new Mock<IReceiptRepository>();
            var receiptItemMock = new Mock<IReceiptItemService>();
            var mapper = await GetMapper();

            repoMock.Setup(repo => repo.CreateAsync(It.IsAny<Receipt>())).Returns(Task.FromResult(receipt));

            var receiptService = new ReceiptService(repoMock.Object, mapper,receiptItemMock.Object);

            var createdReceipt = await receiptService.CreateReceiptAsync(receiptDto);

            Assert.Equal(receiptDto.PaymentMethod, createdReceipt.PaymentMethod);
            Assert.IsType<ReceiptDto>(createdReceipt);
        }

        [Fact]
        public async void ReceiptService_UpdateReceipt_ReturnsUpdatedReceipt()
        {
            var receipt = new Receipt { Id = 1, PaymentMethod = "Cash"};
            var receiptDto = new ReceiptDto { Id = 1, PaymentMethod = "Cash" };

            var repoMock = new Mock<IReceiptRepository>();
            var receiptItemMock = new Mock<IReceiptItemService>();
            var mapper = await GetMapper();

            repoMock.Setup(repo => repo.UpdateAsync(It.IsAny<Receipt>())).Returns(Task.FromResult(receipt));

            var receiptService = new ReceiptService(repoMock.Object, mapper,receiptItemMock.Object);

            var updatedReceipt = await receiptService.UpdateReceiptAsync(receiptDto);

            Assert.Equal(receiptDto.PaymentMethod, updatedReceipt.PaymentMethod);
            Assert.IsType<ReceiptDto>(updatedReceipt);
        }

        [Fact]
        public async void ReceiptService_DeleteReceipt_DeletesAllReceiptItemsAndReturnsDeletedReceipt()
        {
            var receipt = new Receipt { Id = 1, PaymentMethod = "Cash", ReceiptItems = new List<ReceiptItem> { new ReceiptItem { Id = 1, ArticleId = 1, ReceiptId = 1, Quantity = 1, } } };
            var receiptDto = new ReceiptDto { Id = 1, PaymentMethod = "Cash" };
            var receiptItemDto = new ReceiptItemDto { Id = 1, ArticleId = 1, ReceiptId = 1, Quantity = 1, };

            var repoMock = new Mock<IReceiptRepository>();
            var receiptItemMock = new Mock<IReceiptItemService>();
            var mapper = await GetMapper();

            int receiptDeleteCount = 0;
            int receiptItemDeleteCount = 0;

            repoMock.Setup(repo => repo.DeleteAsync(1)).Returns(Task.FromResult(receipt)).Callback(() => receiptDeleteCount++);
            repoMock.Setup(repo => repo.GetAsync(1)).Returns(Task.FromResult(receipt));
            receiptItemMock.Setup(repo => repo.DeleteReceiptItem(It.IsAny<int>())).Returns(Task.FromResult(receiptItemDto)).Callback(() => receiptItemDeleteCount++);

            var receiptService = new ReceiptService(repoMock.Object, mapper,receiptItemMock.Object);

            var deletedReceipt = await receiptService.DeleteReceipt(1);

            Assert.Equal(receiptDto.PaymentMethod, deletedReceipt.PaymentMethod);
            Assert.IsType<ReceiptDto>(deletedReceipt);

            Assert.Equal(1, receiptDeleteCount);
            Assert.Equal(1, receiptItemDeleteCount);
        }
    }
}

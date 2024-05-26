using Microsoft.AspNetCore.Mvc;
using Moq;
using ProdavaonicaIgaraAPI.Controllers;
using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Data.ReceiptItem;
using ProdavaonicaIgaraAPI.Services.ReceiptItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdavaonicaIgara.Tests.Controllers
{
    public class ReceiptItemControllerTests
    {
        private readonly Mock<IReceiptItemService> _mockReceiptItemService;
        private readonly ReceiptItemController _controller;

        public ReceiptItemControllerTests()
        {
            _mockReceiptItemService = new Mock<IReceiptItemService>();
            _controller = new ReceiptItemController(_mockReceiptItemService.Object);
        }

        [Fact]
        public async Task GetReceiptItems_ReturnsOkResult_WithReceiptItems()
        {
            var receiptItems = new List<ReceiptItemDto>
            {
                new ReceiptItemDto { Id = 1, ArticleId = 1},
                new ReceiptItemDto { Id = 2, ArticleId = 2 }
            };
            _mockReceiptItemService.Setup(service => service.GetReceiptItemsAsync()).ReturnsAsync(receiptItems);

            var result = await _controller.GetReceiptItems();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<ReceiptItemDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        
        [Fact]
        public async Task GetReceiptItems_WithPaging_ReturnsOkResult_WithPagedResult()
        {
            var parametars = new QueryParametars
            {
                PageNumber = 1,
                PageSize = 1,
                filterText = ""
            };
            var pagedResult = new PagedResult<ReceiptItemDto>
            {
                Items = new List<ReceiptItemDto> { new ReceiptItemDto { Id = 1, ArticleId = 1 } }
            };
            _mockReceiptItemService.Setup(service => service.GetReceiptItemsAsync(parametars)).ReturnsAsync(pagedResult);

            var result = await _controller.GetReceiptItems(parametars);


            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<PagedResult<ReceiptItemDto>>(okResult.Value);
            Assert.Single(returnValue.Items);
        }

       
        [Fact]
        public async Task CreateReceiptItem_ReturnsOkResult_WithCreatedReceiptItem()
        {
            var receiptItemDto = new ReceiptItemDto { Id = 1, ArticleId = 1 };
            _mockReceiptItemService.Setup(service => service.CreateReceiptItemAsync(receiptItemDto)).ReturnsAsync(receiptItemDto);

            var result = await _controller.CreateReceiptItem(receiptItemDto);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ReceiptItemDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task UpdateReceiptItem_ReturnsOkResult_WithUpdatedReceiptItem()
        {
            var receiptItemDto = new ReceiptItemDto { Id = 1, ArticleId = 1 };
            _mockReceiptItemService.Setup(service => service.UpdateReceiptItemAsync(receiptItemDto)).ReturnsAsync(receiptItemDto);

            var result = await _controller.UpdateReceiptItem(receiptItemDto);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ReceiptItemDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task DeleteReceiptItem_ReturnsOkResult_WithDeletedReceiptItem()
        {
            var receiptItemDto = new ReceiptItemDto { Id = 1, ArticleId = 1 };
            _mockReceiptItemService.Setup(service => service.DeleteReceiptItem(1)).ReturnsAsync(receiptItemDto);

            var result = await _controller.DeleteReceiptItem(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ReceiptItemDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }
    }
}

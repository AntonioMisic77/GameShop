using Microsoft.AspNetCore.Mvc;
using Moq;
using ProdavaonicaIgaraAPI.Controllers;
using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Data.Receipt;
using ProdavaonicaIgaraAPI.Services.Receipt;

namespace ProdavaonicaIgara.Tests.Controllers
{
    public class ReceiptControllerTests
    {
        private readonly Mock<IReceiptService> _mockReceiptService;
        private readonly ReceiptController _controller;

        public ReceiptControllerTests()
        {
            _mockReceiptService = new Mock<IReceiptService>();
            _controller = new ReceiptController(_mockReceiptService.Object);
        }

        [Fact]
        public async Task GetReceiptAsync_WithPaging_ReturnsOkResult_WithPagedResult()
        {
            // Arrange
            var parametars = new QueryParametars 
            {
                PageNumber = 1,
                PageSize = 1,
                filterText = ""
            };
            var pagedResult = new PagedResult<ReceiptDto> { Items = new List<ReceiptDto> { new ReceiptDto { Id = 1, PaymentMethod = "Cash"} } };
            _mockReceiptService.Setup(service => service.GetReceiptsAsync(parametars)).ReturnsAsync(pagedResult);


            var result = await _controller.GetReceiptAsync(parametars);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<PagedResult<ReceiptDto>>(okResult.Value);
            Assert.Single(returnValue.Items);
        }

        [Fact]
        public async Task GetReceiptAsync_WithPaging_ReturnsNotFoundResult_WhenReceiptsNotFound()
        {
            var parametars = new QueryParametars();
            _mockReceiptService.Setup(service => service.GetReceiptsAsync(parametars)).ReturnsAsync((PagedResult<ReceiptDto>)null);

            var result = await _controller.GetReceiptAsync(parametars);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateReceiptAsync_ReturnsOkResult_WithCreatedReceipt()
        {
           
            var receiptDto = new ReceiptDto { Id = 1, PaymentMethod = "Cash" };
            _mockReceiptService.Setup(service => service.CreateReceiptAsync(receiptDto)).ReturnsAsync(receiptDto);


            var result = await _controller.CreateReceiptAsync(receiptDto);

          
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ReceiptDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }


        [Fact]
        public async Task UpdateReceiptAsync_ReturnsOkResult_WithUpdatedReceipt()
        {
            
            var receiptDto = new ReceiptDto { Id = 1, PaymentMethod = "Card" };
            _mockReceiptService.Setup(service => service.UpdateReceiptAsync(receiptDto)).ReturnsAsync(receiptDto);

         
            var result = await _controller.UpdateReceiptAsync(receiptDto);

          
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ReceiptDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

       

        [Fact]
        public async Task DeleteReceipt_ReturnsOkResult_WithDeletedReceipt()
        {
       
            var receiptDto = new ReceiptDto { Id = 1, PaymentMethod = "Cash" };
            _mockReceiptService.Setup(service => service.DeleteReceipt(1)).ReturnsAsync(receiptDto);

        
            var result = await _controller.DeleteReceipt(1);

         
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ReceiptDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }
    }
}

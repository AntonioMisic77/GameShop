using Microsoft.AspNetCore.Mvc;
using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Data.ReceiptItem;
using ProdavaonicaIgaraAPI.Services.ReceiptItem;

namespace ProdavaonicaIgaraAPI.Controllers
{
    [Route("api/receiptitem")]
    [ApiController]
    public class ReceiptItemController : ControllerBase
    {
        private readonly IReceiptItemService _receiptItemService;
        public ReceiptItemController(IReceiptItemService receiptItemService)
        {
            _receiptItemService = receiptItemService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptItemDto>> GetReceiptItem(int id)
        {
            var receiptItem = await _receiptItemService.GetReceiptItemAsync(id);

            if (receiptItem == null)
            {
                return NotFound();
            }

            return Ok(receiptItem);
        }

        [HttpGet]
        public async Task<ActionResult<ReceiptItemDto>> GetReceiptItems()
        {
            var receiptItems = await _receiptItemService.GetReceiptItemsAsync();

            if (receiptItems == null)
            {
                return NotFound();
            }

            return Ok(receiptItems);
        }

        [HttpPost("paged")]
        public async Task<ActionResult<PagedResult<ReceiptItemDto>>> GetReceiptItems(QueryParametars parametars)
        {
            var receiptItems = await _receiptItemService.GetReceiptItemsAsync(parametars);

            if (receiptItems == null)
            {
                return NotFound();
            }

            return Ok(receiptItems);
        }

        [HttpPost]
        public async Task<ActionResult<ReceiptItemDto>> CreateReceiptItem(ReceiptItemDto receiptItemDto)
        {
            var receiptItem = await _receiptItemService.CreateReceiptItemAsync(receiptItemDto);

            return Ok(receiptItem);
        }

        [HttpPut]
        public async Task<ActionResult<ReceiptItemDto>> UpdateReceiptItem(ReceiptItemDto receiptItemDto)
        {
            var receiptItem = await _receiptItemService.UpdateReceiptItemAsync(receiptItemDto);

            return Ok(receiptItem);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ReceiptItemDto>> DeleteReceiptItem(int id)
        {
            var receiptItem = await _receiptItemService.DeleteReceiptItem(id);

            return Ok(receiptItem);
        }

    }
}

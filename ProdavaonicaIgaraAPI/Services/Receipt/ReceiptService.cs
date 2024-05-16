using AutoMapper;
using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Data.Receipt;
using ProdavaonicaIgaraAPI.Repositories;
using ProdavaonicaIgaraAPI.Services.ReceiptItem;
using System.Collections.Generic;

namespace ProdavaonicaIgaraAPI.Services.Receipt
{
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly IReceiptItemService _receiptItemService;
        private readonly IMapper _mapper;

        public ReceiptService(IReceiptRepository receiptRepository, IMapper mapper,IReceiptItemService receiptItemService)
        {
            _receiptRepository = receiptRepository;
            _receiptItemService = receiptItemService;
            _mapper = mapper;
        }

        public async Task<ReceiptDto> GetReceiptAsync(int id)
        {
           var receipt = await _receiptRepository.GetAsync(id);
           return _mapper.Map<ReceiptDto>(receipt);
        }

        public async Task<PagedResult<ReceiptDto>> GetReceiptsAsync(QueryParametars parametars)
        {
            var receipts = await _receiptRepository.GetPagedReceipts(parametars);

            var receiptsCount  = await _receiptRepository.GetReceiptsCount();

            return new PagedResult<ReceiptDto>
            {
                Items = _mapper.Map<List<ReceiptDto>>(receipts),
                TotalCount = receiptsCount,
                PageSize = parametars.PageSize,
                PageNumber = parametars.PageNumber
            };
        }


        public async Task<ReceiptDto> CreateReceiptAsync(ReceiptDto articleDto)
        {
            var receipts = await _receiptRepository.CreateAsync(_mapper.Map<Models.Receipt>(articleDto));
            return _mapper.Map<ReceiptDto>(receipts);
        }

        public async Task<ReceiptDto> UpdateReceiptAsync(ReceiptDto articleDto)
        {
            var receipt = await _receiptRepository.UpdateAsync(_mapper.Map<Models.Receipt>(articleDto));
            return _mapper.Map<ReceiptDto>(receipt);
        }

        public async Task<ReceiptDto> DeleteReceipt(int id)
        {
            var receiptItems = (await GetReceiptAsync(id)).ReceiptItems.ToList();

            foreach(var item in receiptItems)
            {
                await _receiptItemService.DeleteReceiptItem(item.Id);
            }

            var deletedReceipt = await _receiptRepository.DeleteAsync(id);

            return _mapper.Map<ReceiptDto>(deletedReceipt);
        }  
    }
}

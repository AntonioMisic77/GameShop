using AutoMapper;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using ProdavaonicaIgaraAPI.Data.Exceptions;
using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Data.Receipt;
using ProdavaonicaIgaraAPI.Data.ReceiptItem;
using ProdavaonicaIgaraAPI.Repositories;
using ProdavaonicaIgaraAPI.Services.ReceiptItem;

namespace ProdavaonicaIgaraAPI.Services
{
    public class ReceiptItemService : IReceiptItemService
    {
        #region properties
        private readonly IReceiptItemRepository _receiptItemRepository;
        private readonly IArticleService _articleService;
        private readonly IMapper _mapper;
        #endregion

        #region ctor
        public ReceiptItemService(IReceiptItemRepository receiptItemRepository,IArticleService articleService,IMapper mapper)
        {
            _receiptItemRepository = receiptItemRepository;
            _articleService = articleService;
            _mapper = mapper;
        }
        #endregion

        #region methods
        public async Task<ReceiptItemDto> GetReceiptItemAsync(int id)
        {
            var receiptItem = await _receiptItemRepository.GetAsync(id);
            return _mapper.Map<ReceiptItemDto>(receiptItem);
        }

        public async Task<List<ReceiptItemDto>> GetReceiptItemsAsync()
        {
            var receiptItems = await _receiptItemRepository.GetAllAsync();
            return _mapper.Map<List<ReceiptItemDto>>(receiptItems);
        }

        public async Task<PagedResult<ReceiptItemDto>> GetReceiptItemsAsync(QueryParametars parametars)
        {
            var receiptItems = await  _receiptItemRepository.GetPagedReceiptItems(parametars);
            var count = await _receiptItemRepository.GetReceiptItemsCount();

            return new PagedResult<ReceiptItemDto>
            {
                Items = _mapper.Map<List<ReceiptItemDto>>(receiptItems),
                TotalCount = count,
                PageSize = parametars.PageSize,
                PageNumber = parametars.PageNumber
            };
        }

        public async Task<ReceiptItemDto> CreateReceiptItemAsync(ReceiptItemDto receiptItemDto)
        {
            var checkUnique = await _receiptItemRepository.checkUniqueRecipteItem(receiptItemDto.ArticleId,receiptItemDto.ReceiptId);

            if (checkUnique)
            {
                throw new UniqueConstraint("Receipt item already exists in Receipt");
            }

            var article = await _articleService.GetArticleAsync(receiptItemDto.ArticleId);

            if (article.StockQuantity < receiptItemDto.Quantity)
            {
                throw new NotEnoughQuantity("Not enough stock quantity");
            }

            article.StockQuantity -= receiptItemDto.Quantity;

            await _articleService.UpdateArticleAsync(article);

            var createdReceipteItem = await _receiptItemRepository.CreateAsync(_mapper.Map<Models.ReceiptItem>(receiptItemDto));

           return _mapper.Map<ReceiptItemDto>(createdReceipteItem);
        }

        public async Task<ReceiptItemDto> UpdateReceiptItemAsync(ReceiptItemDto receiptItemDto)
        {
            var article = await _articleService.GetArticleAsync(receiptItemDto.ArticleId);

            var oldReceiptItemQuantity = (await _receiptItemRepository.GetAsync(receiptItemDto.Id)).Quantity;

            int diff = receiptItemDto.Quantity - oldReceiptItemQuantity;


            if (( article.StockQuantity + oldReceiptItemQuantity) < receiptItemDto.Quantity)
            {
                throw new NotEnoughQuantity("Not enough stock quantity");
            }

            if ( diff < 0)
            {
                article.StockQuantity += Math.Abs(diff);
            } else
            {
                article.StockQuantity -= diff;
            }

            await _articleService.UpdateArticleAsync(article);

            var updatedReceiptItem = await _receiptItemRepository.UpdateAsync(_mapper.Map<Models.ReceiptItem>(receiptItemDto));

            return _mapper.Map<ReceiptItemDto>(updatedReceiptItem);
        }

        public async Task<ReceiptItemDto> DeleteReceiptItem(int id)
        {
           var deletedReceiptItem = await _receiptItemRepository.DeleteAsync(id);
           return _mapper.Map<ReceiptItemDto>(deletedReceiptItem);
        }

        #endregion
    }
}

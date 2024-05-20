using ProdavaonicaIgaraAPI.Data.Articles;
using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Repositories.GenericRepository;
using ProdavaonicaIgaraAPI.Models;
using ProdavaonicaIgaraAPI.Repositories;
using AutoMapper;

namespace ProdavaonicaIgaraAPI.Services
{
    public class ArticleService : IArticleService
    {
        #region properties

        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        #endregion

        #region ctor
        public ArticleService(IArticleRepository articleRepository,IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        #endregion

        #region methods
        public async Task<ArticleDto> GetArticleAsync(int id)
        {
            var article = await _articleRepository.GetAsync(id);
            return _mapper.Map<ArticleDto>(article);
        }

        public async Task<PagedResult<ArticleDto>> GetArticlesAsync(QueryParametars parametars)
        {
            var articles = await _articleRepository.GetPagedAsync(parametars);

            var count = await _articleRepository.GetArtilceCount(parametars.filterText);

            return new PagedResult<ArticleDto>
            {
                PageNumber = parametars.PageNumber,
                PageSize = parametars.PageSize,
                Items = _mapper.Map<List<ArticleDto>>(articles),
                TotalCount = count
            };

        }

        public async Task<ArticleDto> CreateArticleAsync(ArticleDto articleDto)
        {
            var createdArticle = await _articleRepository.CreateAsync(_mapper.Map<Article>(articleDto));

            return _mapper.Map<ArticleDto>(createdArticle);
        }

        public async Task<ArticleDto> UpdateArticleAsync(ArticleDto articleDto)
        {
           var updatedArticle = await  _articleRepository.UpdateAsync(_mapper.Map<Article>(articleDto));

            return _mapper.Map<ArticleDto>(updatedArticle);
        }

        public async Task<ArticleDto> DeleteArticle(int id)
        {
           var deletedArticle = await  _articleRepository.DeleteAsync(id);

            return _mapper.Map<ArticleDto>(deletedArticle);
        }
     
        #endregion
    }
}

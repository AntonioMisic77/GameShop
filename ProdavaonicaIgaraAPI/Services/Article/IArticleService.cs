using ProdavaonicaIgaraAPI.Data.Articles;
using ProdavaonicaIgaraAPI.Data.Pageing;

namespace ProdavaonicaIgaraAPI.Services
{
    public interface IArticleService
    {
        #region methods

        Task<ArticleDto> GetArticleAsync(int id);

        Task<PagedResult<ArticleDto>> GetArticlesAsync(QueryParametars parametars);

        Task<ArticleDto> CreateArticleAsync(ArticleDto articleDto);
        
        Task<ArticleDto> UpdateArticleAsync(ArticleDto articleDto);

        Task<ArticleDto> DeleteArticle(int id);

        #endregion
    }
}

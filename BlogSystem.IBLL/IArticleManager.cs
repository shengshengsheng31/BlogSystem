using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.IBLL
{
    public interface IArticleManager
    {
        Task CreateArticle(string title, string content, Guid[] categoryIds, Guid userId);
        Task CreateCategory(string name,Guid userId);
        Task<List<Dto.BlogCategoryDto>> GetAllCategories(Guid userId);
        Task<List<Dto.ArticleDto>> GetAllArticlesByUserId(Guid userId,int pageIndex,int pageSize);
        Task<List<Dto.ArticleDto>> GetAllArticlesByEmail(Guid email);
        Task<List<Dto.ArticleDto>> GetAllArticlesByCategoryId(Guid categoryId);
        Task RemoveCategory(string name);
        Task EditCategory(Guid categoryId, string newCategoryName);
        Task RemoveArticle(Guid articleId);
        Task EditArticle(Guid articleId, string title, string content, Guid[] categoryIds);
        Task<bool> ExistsArticle(Guid articleId);
        Task<Dto.ArticleDto> GetOneArticleById(Guid articleId);
        Task<int> GetDataCount(Guid userId);

        Task GoodCount(Guid articleId);
        Task BadCount(Guid articleId);
        Task CreateComment(Guid userId, Guid articleId, string Content);

        Task<List<Dto.CommentDto>> GetCommentsByArticleId(Guid articleId);
    }
}

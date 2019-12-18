using BlogSystem.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DAL
{
    public class ArticleToCategoryService:BaseService<Models.ArticleToCategory>,IArticleToCategoryService
    {
        public ArticleToCategoryService():base(new Models.BlogContext()) { }
    }
}

using BlogSystem.BLL;
using BlogSystem.MVCSite.Controllers.Filters;
using BlogSystem.MVCSite.Models.ArticleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace BlogSystem.MVCSite.Controllers
{
    [BlogSystemAuth]
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CreateCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                IBLL.IArticleManager articleManager = new ArticleManager();
                articleManager.CreateCategory(model.CategoryName, Guid.Parse(Session["userid"].ToString()));
                return RedirectToAction("CategoryList");
            }
            ModelState.AddModelError(key: "", errorMessage: "录入的信息有错误");
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> CategoryList()
        {
            var userId = Guid.Parse(Session["userId"].ToString());
            return View(await new ArticleManager().GetAllCategories(userId));
        }

        [HttpGet]
        public async Task<ActionResult> CreateArticle()
        {
            var userId = Guid.Parse(Session["userId"].ToString());
            ViewBag.CategoryIds = await new ArticleManager().GetAllCategories(userId);
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateArticle(Models.ArticleViewModels.CreateArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = Guid.Parse(Session["userId"].ToString());
                await new ArticleManager().CreateArticle(model.Title, model.Content, model.CategoryIds, userId);
                return RedirectToAction("ArticleList");
            }
            ModelState.AddModelError("", "添加失败");
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> ArticleList(int pageIndex=1,int pageSize=3)
        {
            //需要给页面前端，总页码数，当前页码，可显示的总页码数量
            var articleMgr = new ArticleManager();
            var userId = Guid.Parse(Session["userId"].ToString());
            var articles = await articleMgr.GetAllArticlesByUserId(userId,pageIndex-1,pageSize);//后台方法需要从0开始
            var dataCount = await articleMgr.GetDataCount(userId);
            ViewBag.PageCount = dataCount % pageSize==0?dataCount/pageSize:dataCount/pageSize+1;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageView = 7;//需要显示的页数
            return View(articles);
        }

        //插件页码
        [HttpGet]
        public async Task<ActionResult> ArticleListPro(int pageIndex = 1, int pageSize = 3)
        {
            //需要给页面前端，总页码数，当前页码，可显示的总页码数量
            var articleMgr = new ArticleManager();
            var userId = Guid.Parse(Session["userId"].ToString());
            //当前用户第n页数据
            var articles = await articleMgr.GetAllArticlesByUserId(userId, pageIndex - 1, pageSize);//后台方法需要从0开始
            //文章总数
            var dataCount = await articleMgr.GetDataCount(userId);
            
            return View(new PagedList<Dto.ArticleDto>(articles,pageIndex,pageSize,dataCount));
        }

        public async Task<ActionResult> ArticleDetails(Guid? id)
        {
            var articleMgr = new ArticleManager();
            if (id == null|| !await articleMgr.ExistsArticle(id.Value))
                return RedirectToAction(nameof(ArticleList));
            return View(await articleMgr.GetOneArticleById(id.Value)); 
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSystem.MVCSite.Models.ArticleViewModels
{
    //如果改viewmodel和dto中的类一样可以省略，直接使用dto中类，如果不同，必须创建
    public class ArticleDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public string[] CategoryIds { get; set; }
        public string[] CategoryNames { get; set; }
        public int GoodCount { get; set; }
        public int BadCount { get; set; }
    }
}
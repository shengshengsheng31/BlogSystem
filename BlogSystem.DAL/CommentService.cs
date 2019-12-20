﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DAL
{
    public class CommentService:BaseService<Models.Comment>,IDAL.ICommentService
    {
        public CommentService():base(new Models.BlogContext())
        {

        }
    }
}

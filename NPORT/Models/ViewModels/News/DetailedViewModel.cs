﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPORT.Models.Database;

namespace NPORT.Models.ViewModels.News
{
    public class DetailedViewModel
    {
        public Database.News News { get; set; }

        public List<Comment> CommentList { get; set; }
    }
}
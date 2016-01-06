﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _22222.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.Message = "You are admin";
            return View();
        }
        [Authorize(Roles = "editor")]
        public ActionResult Change()
        {
            ViewBag.Message = "You are editor!!";
            return View();
        }
        [Authorize(Roles = "correspondent")]
        public ActionResult Add()
        {
            ViewBag.Message = "You are correspondent!!";
            return View();
        }
    }
}
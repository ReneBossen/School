using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace Lesson02_Startup.Controllers
{
    public class Exercise01Controller : Controller
    {
        

        public ActionResult Index()
        {
            // create a new product object with instance name glass
            Product glass = new Product("Wine glass", 160.50);
            glass.ImageUrl = "grandcru.jpg";
            Product knife = new Product("Knife", 30);
            knife.ImageUrl = "st_knife.jpg";
            Product bin = new Product("Bin", 230, "bin.jpg", "Arla");
            //bin.ImageUrl = "bin.jpg";
            ViewBag.Glass = glass;
            ViewBag.Knife = knife;
            ViewBag.Bin = bin;
			
            return View();
        }

    }
}

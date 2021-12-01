using Microsoft.AspNetCore.Mvc;
using RequestFilter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestFilter.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind(include:"Name,Price,Description")]Product product)
        {
            if (ModelState.IsValid)
            {
                //farz edin ki eklendi...
                return Redirect("/");
            }

            return View();
         
        }
    }
}

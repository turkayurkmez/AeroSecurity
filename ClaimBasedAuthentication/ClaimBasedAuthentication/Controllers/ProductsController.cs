using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaimBasedAuthentication.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UrunEkle()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UrunEkle(string urunAdi)
        {
            return View();

            /*
             * 1. Least Privilage: En az ayrıcalık prensibi
             * 2. Seperation of Duties: Görev ayrımı ilkesi
             * 3. Defense In Depth: Derin savunma, yazılım dışında da önlem alın.
             * 4. Fail Securely: Güvenli bir biçimde hata alma ilkesi
             * 5. Principle of Open Design: Açık tasarım prensibi
             * 6. Avoiding Security by obscurty: Belirsizlik ile güvenlikten uzaklaşma
             * 7. Minimizing Attack Surface Area
             */
        }
    }
}

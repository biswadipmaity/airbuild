namespace Server.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Server.ViewModel;
    using Server.Helper;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var indexViewModel = new IndexViewModel();
            return View(indexViewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Switch(string id)
        {
            Database.switchEnvironmnet(id);
            return RedirectToAction("Index");
        }
    }
}

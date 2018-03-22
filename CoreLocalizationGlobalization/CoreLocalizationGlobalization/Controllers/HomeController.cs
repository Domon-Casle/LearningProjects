using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreLocalizationGlobalization.Models;
using Microsoft.Extensions.Localization;
//using Microsoft.AspNetCore.Mvc.Localization;

namespace CoreLocalizationGlobalization.Controllers
{
    public class HomeController : Controller
    {
        IStringLocalizer<SharedResources> _localizer; // For pure strings (Shared Resource)
        //IHtmlLocalizer<SharedResources> _htmlLocalizer; // For strings that contain HTML in them (Shared Resource)
        //IStringLocalizer<HomeController> _localizer; // For pure strings (By Controller)
        //IHtmlLocalizer<HomeController> _htmlLocalizer; // For strings that contain HTML in them (By Controller)

        public HomeController(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            ViewData["Message"] = _localizer["This is a standard message"];
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = _localizer["Your application description page."];

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = _localizer["Your contact page."];

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

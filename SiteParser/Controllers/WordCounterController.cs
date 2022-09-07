using Microsoft.AspNetCore.Mvc;

namespace SiteParser.Controllers
{
    public class WordCounterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

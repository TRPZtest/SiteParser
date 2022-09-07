using Microsoft.AspNetCore.Mvc;

namespace SiteParser.Controllers
{
    [Route("[controller]/[action]")]
    public class WordCounterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

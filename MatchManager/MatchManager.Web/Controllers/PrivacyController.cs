using Microsoft.AspNetCore.Mvc;

namespace MatchManager.Web.Controllers
{
    public class PrivacyController : Controller
    {
        [HttpGet]
        [Route("privacy-policy")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace MatchManager.Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

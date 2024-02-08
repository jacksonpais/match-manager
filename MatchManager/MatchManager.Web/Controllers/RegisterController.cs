using Microsoft.AspNetCore.Mvc;

namespace MatchManager.Web.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace MatchManager.Web.Controllers
{
    public class RegisterController : Controller
    {
        [Route("create-account")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

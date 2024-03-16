using Microsoft.AspNetCore.Mvc;

namespace MatchManager.Web.Controllers
{
    public class RegisterController : Controller
    {
        [HttpGet]
        [Route("create-account")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("account/verify")]
        public ActionResult Verify(string key, string hashtoken)
        {
            return View();
        }

        [HttpGet]
        [Route("account/request-verification")]
        public ActionResult RequestVerification()
        {
            return View();
        }
    }
}

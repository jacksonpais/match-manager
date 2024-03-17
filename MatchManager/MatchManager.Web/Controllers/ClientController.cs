using Microsoft.AspNetCore.Mvc;

namespace MatchManager.Web.Controllers
{
    public class ClientController : Controller
    {
        [HttpGet]
        [Route("add-clients")]
        public IActionResult AddClients()
        {
            return View();
        }
    }
}

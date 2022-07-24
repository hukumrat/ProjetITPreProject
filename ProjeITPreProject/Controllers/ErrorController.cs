using Microsoft.AspNetCore.Mvc;

namespace ProjeITPreProjectMvcUI.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return View("404");
                case 403:
                    return View("403");
                case 503:
                    return View("503");
                default:
                    return View("404");
            }
        }
    }
}

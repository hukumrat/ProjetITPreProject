using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using ProjeITPreProject.Models;
using System.Diagnostics;

namespace ProjeITPreProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICommentService _commentService;

        public HomeController(ILogger<HomeController> logger, ICommentService commentService)
        {
            _logger = logger;
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            var a = _commentService.ListAll();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
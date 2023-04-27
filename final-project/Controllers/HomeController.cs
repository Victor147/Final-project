using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using final_project.ViewModels;
using Microsoft.AspNetCore.Diagnostics;

namespace final_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Contact()
        {
            return View();
        }

        [Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            var feature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            return View(new ErrorViewModel
            {
                StatusCode = statusCode,
                OriginalPath = feature?.OriginalPath!
            });
        }
    }
}
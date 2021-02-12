using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TechnicalSupport.Models;
using TechnicalSupport.Data;

namespace TechnicalSupport.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private GL_SupportContext _db;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //_db = configuration.GetConnectionString("DefaultConnection");
            //Сделать контроллер где  будет бд
            _db = new GL_SupportContext(new Microsoft.EntityFrameworkCore.DbContextOptions<GL_SupportContext>());
        }

        public IActionResult Index()
        {

            //return View();
            var asd = _db.WorkTimes.First().From.ToString();
            return Content(content: asd);
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

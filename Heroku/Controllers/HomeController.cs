using Heroku.Context;
using Heroku.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Heroku.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            ViewData["Success"] = "Başarılı";
            return View();
        }
        [HttpGet]
        public IActionResult ProductList()
        {
            List<Product> products = _context.Products.ToList();
            return View(products);
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

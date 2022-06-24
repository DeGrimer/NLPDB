using Microsoft.AspNetCore.Mvc;
using NLPDB.Web.Data;
using NLPDB.Web.Models;
using System.Diagnostics;

namespace NLPDB.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index([FromServices]ApplicationDbContext context)
        {
            //var algorithm = new Algorithm()
            //{
            //    Category = context.Categories.Where(c => c.Name == "Text classification").First(),
            //    Name = "Topic Models",
            //    Content = "A topic model is a type of statistical model for discovering the abstract that occur in a collection of documents. Topic modeling is a frequently used text-mining tool for the discovery of hidden semantic structures in a text body.",
            //    Link = "https://github.com/mind-Lab/octis"
            //};
            //context.Algorithms.Add(algorithm);
            //context.SaveChanges();
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
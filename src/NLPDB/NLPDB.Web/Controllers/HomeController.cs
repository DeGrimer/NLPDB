using Microsoft.AspNetCore.Mvc;
using NLPDB.Web.Data;
using NLPDB.Web.Models;
using System.Diagnostics;
using System.Linq;

namespace NLPDB.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync([FromServices]ApplicationDbContext context)
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
            //var category = new Category()
            //{
            //    Name = "Text classification",
            //    Description = "Text classification is the task of assigning a sentence or document an appropriate category. The categories depend on the chosen dataset and can range from topics."
            //};
            //context.Categories.Add(category);
            //category = new Category()
            //{
            //    Name = "Sentiment Analysis",
            //    Description = "Sentiment analysis is the task of classifying the polarity of a given text. For instance, a text-based tweet can be categorized into either \"positive\", \"negative\", or \"neutral\". ",
            //};
            //context.Categories.Add(category);
            //context.SaveChanges();
            //var algorithm = new Algorithm()
            //{
            //    Category = context.Categories.Where(c => c.Name == "Text classification").First(),
            //    Name = "Topic Models",
            //    Content = "A topic model is a type of statistical model for discovering the abstract that occur in a collection of documents. Topic modeling is a frequently used text-mining tool for the discovery of hidden semantic structures in a text body.",
            //    Link = "https://github.com/mind-Lab/octis"
            //};
            //context.Algorithms.Add(algorithm);
            //algorithm = new Algorithm()
            //{
            //    Category = context.Categories.Where(c => c.Name == "Sentiment Analysis").First(),
            //    Name = "Twitter Sentiment Analysis",
            //    Content = "Twitter sentiment analysis is the task of performing sentiment analysis on tweets from Twitter.",
            //    Link = "https://github.com/lopezbec/COVID19_Tweets_Dataset"
            //};
            //context.Algorithms.Add(algorithm);
            //await context.SaveChangesAsync();
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
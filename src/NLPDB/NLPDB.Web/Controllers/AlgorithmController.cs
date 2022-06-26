using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLPDB.Web.Data;

namespace NLPDB.Web.Controllers
{
    public class AlgorithmController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AlgorithmController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(Guid? id)
        {
            var alg1 = _context.Categories.Where(x => x.Id == id).First();
            var alg2 = _context.Algorithms.Where(x => x.Category == alg1).ToList();
            ViewData["Category"] = alg1.Id;
            return View(alg2);
        }

        public async Task<IActionResult> Details(Guid? id, string cat)
        {
            if (id == null || _context.Algorithms == null)
            {
                return NotFound();
            }
            var algorithm = await _context.Algorithms
                .FirstAsync(m => m.Id == id);
            algorithm.Category = _context.Categories.Where(x => x.Id == Guid.Parse(cat)).First();
            if (algorithm == null)
            {
                return NotFound();
            }

            return View(algorithm);
        }
    }
}

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
            return View(alg2);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Algorithms == null)
            {
                return NotFound();
            }
            var algorithm = await _context.Algorithms
                .FirstAsync(m => m.Id == id);
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Algorithms.Contains(algorithm));
            algorithm.Category = category;
            if (algorithm == null)
            {
                return NotFound();
            }
            var tasks = await _context.TaskAlg.Where(x => x.Algorithms.Contains(algorithm)).ToListAsync();
            algorithm.TasksAlg = tasks;

            return View(algorithm);
        }
    }
}

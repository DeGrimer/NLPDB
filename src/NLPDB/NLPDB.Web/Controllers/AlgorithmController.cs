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
            return _context.Algorithms != null ?
                          View(await _context.Algorithms.Where(x => x.Category == 
                          _context.Categories.Where(x => x.Id == id).First()).ToListAsync()):
                          Problem("Entity set 'ApplicationDbContext.Algorithms'  is null.");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NLPDB.Web.Data;

namespace NLPDB.Web.Controllers
{
    public class TaskAlgsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskAlgsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaskAlgs
        public async Task<IActionResult> Index()
        {
              return _context.TaskAlg != null ? 
                          View(await _context.TaskAlg.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TaskAlg'  is null.");
        }

        // GET: TaskAlgs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.TaskAlg == null)
            {
                return NotFound();
            }

            var taskAlg = await _context.TaskAlg
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskAlg == null)
            {
                return NotFound();
            }
            var algs = await _context.Algorithms.Where(x => x.TasksAlg.Contains(taskAlg)).ToListAsync();
            if(algs != null)
            {
                taskAlg.Algorithms = algs;
            }
            return View(taskAlg);
        }

        // GET: TaskAlgs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaskAlgs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Id")] TaskAlg taskAlg)
        {
            if (ModelState.IsValid)
            {
                taskAlg.Id = Guid.NewGuid();
                _context.Add(taskAlg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taskAlg);
        }

        // GET: TaskAlgs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.TaskAlg == null)
            {
                return NotFound();
            }

            var taskAlg = await _context.TaskAlg.FindAsync(id);
            if (taskAlg == null)
            {
                return NotFound();
            }
            return View(taskAlg);
        }

        // POST: TaskAlgs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Description,Id")] TaskAlg taskAlg)
        {
            if (id != taskAlg.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskAlg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskAlgExists(taskAlg.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(taskAlg);
        }

        // GET: TaskAlgs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.TaskAlg == null)
            {
                return NotFound();
            }

            var taskAlg = await _context.TaskAlg
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskAlg == null)
            {
                return NotFound();
            }

            return View(taskAlg);
        }

        // POST: TaskAlgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.TaskAlg == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TaskAlg'  is null.");
            }
            var taskAlg = await _context.TaskAlg.FindAsync(id);
            if (taskAlg != null)
            {
                _context.TaskAlg.Remove(taskAlg);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskAlgExists(Guid id)
        {
          return (_context.TaskAlg?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

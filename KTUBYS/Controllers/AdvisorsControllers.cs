using Microsoft.AspNetCore.Mvc;
using KTUBYS.Data;
using KTUBYS.Models;
using System.Linq;
using System.Threading.Tasks;

namespace KTUBYS.Controllers
{
    public class AdvisorsController : Controller
    {
        private readonly KTUBYSContext _context;

        public AdvisorsController(KTUBYSContext context)
        {
            _context = context;
        }

        // GET: /Advisors/
        public async Task<IActionResult> Index()
        {
            var advisors = await _context.Advisors.ToListAsync();
            return View(advisors);
        }

        // GET: /Advisors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advisor = await _context.Advisors
                .FirstOrDefaultAsync(m => m.AdvisorID == id);
            if (advisor == null)
            {
                return NotFound();
            }

            return View(advisor);
        }

        // GET: /Advisors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Advisors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdvisorID,Fullname,Title,Department,Email")] Advisor advisor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advisor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(advisor);
        }

        // GET: /Advisors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advisor = await _context.Advisors.FindAsync(id);
            if (advisor == null)
            {
                return NotFound();
            }
            return View(advisor);
        }

        // POST: /Advisors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdvisorID,Fullname,Title,Department,Email")] Advisor advisor)
        {
            if (id != advisor.AdvisorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(advisor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(advisor);
        }

        // GET: /Advisors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advisor = await _context.Advisors
                .FirstOrDefaultAsync(m => m.AdvisorID == id);
            if (advisor == null)
            {
                return NotFound();
            }

            return View(advisor);
        }

        // POST: /Advisors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advisor = await _context.Advisors.FindAsync(id);
            _context.Advisors.Remove(advisor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

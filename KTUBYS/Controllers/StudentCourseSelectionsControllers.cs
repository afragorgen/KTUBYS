using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KTUBYS.Data;
using KTUBYS.Models;
using System.Linq;
using System.Threading.Tasks;

namespace KTUBYS.Controllers
{
    public class StudentCourseSelectionsController : Controller
    {
        private readonly KTUBYSContext _context;

        // Constructor: Context sınıfını enjekte eder
        public StudentCourseSelectionsController(KTUBYSContext context)
        {
            _context = context;
        }

        // GET: /StudentCourseSelections
        public async Task<IActionResult> Index()
        {
            var selections = await _context.StudentCourseSelections
                .Include(scs => scs.Student)
                .Include(scs => scs.Course)
                .ToListAsync();

            return View(selections);
        }

        // GET: /StudentCourseSelections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selection = await _context.StudentCourseSelections
                .Include(scs => scs.Student)
                .Include(scs => scs.Course)
                .FirstOrDefaultAsync(m => m.SelectionID == id);

            if (selection == null)
            {
                return NotFound();
            }

            return View(selection);
        }

        // GET: /StudentCourseSelections/Create
        public IActionResult Create()
        {
            ViewBag.Students = _context.Students.ToList();
            ViewBag.Courses = _context.Courses.ToList();
            return View();
        }

        // POST: /StudentCourseSelections/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SelectionID,StudentID,CourseID,SelectionDate,IsApproved")] StudentCourseSelection selection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(selection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Students = _context.Students.ToList();
            ViewBag.Courses = _context.Courses.ToList();
            return View(selection);
        }

        // GET: /StudentCourseSelections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selection = await _context.StudentCourseSelections.FindAsync(id);
            if (selection == null)
            {
                return NotFound();
            }

            ViewBag.Students = _context.Students.ToList();
            ViewBag.Courses = _context.Courses.ToList();
            return View(selection);
        }

        // POST: /StudentCourseSelections/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SelectionID,StudentID,CourseID,SelectionDate,IsApproved")] StudentCourseSelection selection)
        {
            if (id != selection.SelectionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(selection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentCourseSelectionExists(selection.SelectionID))
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
            ViewBag.Students = _context.Students.ToList();
            ViewBag.Courses = _context.Courses.ToList();
            return View(selection);
        }

        // GET: /StudentCourseSelections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selection = await _context.StudentCourseSelections
                .Include(scs => scs.Student)
                .Include(scs => scs.Course)
                .FirstOrDefaultAsync(m => m.SelectionID == id);

            if (selection == null)
            {
                return NotFound();
            }

            return View(selection);
        }

        // POST: /StudentCourseSelections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var selection = await _context.StudentCourseSelections.FindAsync(id);
            _context.StudentCourseSelections.Remove(selection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentCourseSelectionExists(int id)
        {
            return _context.StudentCourseSelections.Any(e => e.SelectionID == id);
        }
    }
}


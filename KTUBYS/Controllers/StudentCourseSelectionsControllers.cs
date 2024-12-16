using Microsoft.AspNetCore.Mvc;
using KTUBYS.Data;
using KTUBYS.Models;
using System.Linq;
using System.Threading.Tasks;

namespace KTUBYS.Controllers
{
    public class StudentCourseSelectionsController : Controller
    {
        private readonly KTUBYSContext _context;

        public StudentCourseSelectionsController(KTUBYSContext context)
        {
            _context = context;
        }

        // GET: /StudentCourseSelections/
        public async Task<IActionResult> Index()
        {
            var selections = await _context.StudentCourseSelections.ToListAsync();
            return View(selections);
        }

        // GET: /StudentCourseSelections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var selection = await _context.StudentCourseSelections.FirstOrDefaultAsync(m => m.SelectionID == id);
            if (selection == null)
                return NotFound();

            return View(selection);
        }

        // GET: /StudentCourseSelections/Create
        public IActionResult Create()
        {
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
            return View(selection);
        }
    }
}

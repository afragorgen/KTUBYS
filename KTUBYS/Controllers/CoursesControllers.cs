using Microsoft.AspNetCore.Mvc;
using KTUBYS.Data;
using KTUBYS.Models;
using System.Linq;
using System.Threading.Tasks;

namespace KTUBYS.Controllers
{
    public class CoursesController : Controller
    {
        private readonly KTUBYSContext _context;

        public CoursesController(KTUBYSContext context)
        {
            _context = context;
        }

        // GET: /Courses/
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.ToListAsync();
            return View(courses);
        }

        // GET: /Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseID == id);
            if (course == null)
                return NotFound();

            return View(course);
        }

        // GET: /Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseID,CourseCode,CourseName,IsMandatory,Credit,Department")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }
    }
}

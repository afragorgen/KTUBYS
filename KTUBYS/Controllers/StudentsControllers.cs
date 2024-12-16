using Microsoft.AspNetCore.Mvc;
using KTUBYS.Data;  // Veritabanı bağlantı context'in olduğu yer
using KTUBYS.Models;  // Öğrenci modeli (Students) burada tanımlanmıştır
using System.Linq;
using System.Threading.Tasks;

namespace KTUBYS.Controllers
{
    public class StudentsController : Controller
    {
        private readonly KTUBYSContext _context;  // Bağlantı için Context sınıfı

        // Constructor: Context bağımlılığı enjekte ediliyor
        public StudentsController(KTUBYSContext context)
        {
            _context = context;
        }

        // GET: /Students/Index
        public async Task<IActionResult> Index()
        {
            // Veritabanından öğrencileri alıp listeleme
            var students = await _context.Students.ToListAsync();
            return View(students);  // students listesini View'a gönderiyoruz
        }

        // GET: /Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Veritabanında ID'ye göre öğrenci bulma
            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);  // Öğrenci detaylarını View'a gönderiyoruz
        }

        // GET: /Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,FirstName,LastName,EnrollmentDate,AdvisorID")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();  // Yeni öğrenci kaydediliyor
                return RedirectToAction(nameof(Index));  // Başarılıysa listeye yönlendirme
            }
            return View(student);
        }
    }
}

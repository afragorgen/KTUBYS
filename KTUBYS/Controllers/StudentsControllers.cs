using Microsoft.AspNetCore.Mvc;
using KTUBYS.Data;  // Veritabanı bağlantı context'in olduğu yer
using KTUBYS.Models;  // Öğrenci modeli (Students) burada tanımlanmıştır
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            // Danışmanları ViewBag üzerinden gönderiyoruz
            ViewBag.Advisors = await _context.Advisors.ToListAsync();

            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentID,FirstName,LastName,EnrollmentDate,AdvisorID")] Student student)
        {
            if (id != student.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);  // Öğrenci veritabanında güncelleniyor
                    await _context.SaveChangesAsync();  // Değişiklikler kaydediliyor
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));  // Başarılı olursa ana listeye dönülür
            }
            return View(student);  // Geçersizse, form tekrar gösterilir
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);  // Öğrenciyi Delete.cshtml'ye gönderiyoruz
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);  // Öğrenci veritabanından siliniyor
                await _context.SaveChangesAsync();  // Değişiklikler kaydediliyor
            }

            return RedirectToAction(nameof(Index));  // Silme işlemi sonrası listeye dönülüyor
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
    ViewBag.AdvisorID = new SelectList(_context.Advisors, "AdvisorID", "FullName"); // Danışman listesini getiriyoruz
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


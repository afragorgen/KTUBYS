using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KTUBYS.Data;
using KTUBYS.Models;

namespace KTUBYS.Controllers
{
    public class CoursesController : Controller
    {
        private readonly KTUBYSContext _context;

        public CoursesController(KTUBYSContext context)
        {
            _context = context;
        }

        // GET: Courses/Index
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.ToListAsync();
            return View(courses); // Index view'ını döndürüyoruz
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.CourseID == id); // CourseID'ye göre kursu getiriyoruz
            if (course == null)
            {
                return NotFound();
            }

            return View(course); // Detayları gösteren view'ı döndürüyoruz
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View(); // Create view'ını döndürüyoruz
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseID,CourseCode,CourseName,IsMandatory,Credit,Department")] Course course)
        {
            if (ModelState.IsValid) // Model doğrulama
            {
                _context.Add(course); // Yeni kursu ekliyoruz
                await _context.SaveChangesAsync(); // Veritabanına kaydediyoruz
                return RedirectToAction(nameof(Index)); // Başarılıysa Index sayfasına yönlendiriyoruz
            }
            return View(course); // Hata varsa Create view'ına geri dönüyoruz
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id); // Kursu ID'ye göre buluyoruz
            if (course == null)
            {
                return NotFound();
            }
            return View(course); // Edit view'ını kurs verisi ile döndürüyoruz
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseID,CourseCode,CourseName,IsMandatory,Credit,Department")] Course course)
        {
            if (id != course.CourseID) // ID eşleşmesi kontrolü
            {
                return NotFound();
            }

            if (ModelState.IsValid) // Model doğrulama
            {
                try
                {
                    _context.Update(course); // Kursu güncelliyoruz
                    await _context.SaveChangesAsync(); // Değişiklikleri kaydediyoruz
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseID)) // Eğer kurs bulunamazsa, hata döndürüyoruz
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); // Başarılıysa Index'e yönlendiriyoruz
            }
            return View(course); // Hata varsa Edit view'ına geri dönüyoruz
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.CourseID == id); // Silinecek kursu getiriyoruz
            if (course == null)
            {
                return NotFound();
            }

            return View(course); // Delete view'ını döndürüyoruz
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id); // Kursu buluyoruz
            _context.Courses.Remove(course); // Kursu siliyoruz
            await _context.SaveChangesAsync(); // Veritabanında kaydediyoruz
            return RedirectToAction(nameof(Index)); // Başarılıysa Index sayfasına yönlendiriyoruz
        }

        // Kursun var olup olmadığını kontrol ediyoruz
        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseID == id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KTUBYS.Data;
using KTUBYS.Models;
using System.Linq;
using System.Threading.Tasks;

namespace KTUBYS.Controllers
{
    public class UsersController : Controller
    {
        private readonly KTUBYSContext _context;

        public UsersController(KTUBYSContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();  // Tüm kullanıcıları alıyoruz
            return View(users);  // Kullanıcıları view'a gönderiyoruz
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();  // Eğer id geçerli değilse NotFound döndürüyoruz
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserID == id);  // Veritabanından kullanıcıyı buluyoruz

            if (user == null)
            {
                return NotFound();  // Kullanıcı bulunamazsa NotFound döndürüyoruz
            }

            return View(user);  // Kullanıcıyı detaylar için view'a gönderiyoruz
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();  // Yeni kullanıcı eklemek için formu döndürüyoruz
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,FullName,Email,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);  // Yeni kullanıcıyı veritabanına ekliyoruz
                await _context.SaveChangesAsync();  // Veritabanına kaydediyoruz
                return RedirectToAction(nameof(Index));  // Kullanıcı başarılı bir şekilde eklenirse ana listeye yönlendiriyoruz
            }
            return View(user);  // Model geçersizse formu tekrar gösteriyoruz
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();  // Eğer id geçerli değilse NotFound döndürüyoruz
            }

            var user = await _context.Users.FindAsync(id);  // Veritabanında kullanıcıyı buluyoruz

            if (user == null)
            {
                return NotFound();  // Kullanıcı bulunamazsa NotFound döndürüyoruz
            }
            return View(user);  // Kullanıcıyı düzenleme formuna gönderiyoruz
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,FullName,Email,Role")] User user)
        {
            if (id != user.UserID)
            {
                return NotFound();  // Eğer id eşleşmezse NotFound döndürüyoruz
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);  // Kullanıcıyı güncelliyoruz
                    await _context.SaveChangesAsync();  // Değişiklikleri kaydediyoruz
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserID))
                    {
                        return NotFound();  // Kullanıcı bulunamazsa NotFound döndürüyoruz
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));  // Başarılı olursa listeye geri dönüyoruz
            }
            return View(user);  // Model geçersizse formu tekrar gösteriyoruz
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();  // Eğer id geçerli değilse NotFound döndürüyoruz
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserID == id);  // Kullanıcıyı veritabanından buluyoruz

            if (user == null)
            {
                return NotFound();  // Kullanıcı bulunamazsa NotFound döndürüyoruz
            }

            return View(user);  // Kullanıcıyı silme onayı için view'a gönderiyoruz
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);  // Silinecek kullanıcıyı veritabanından buluyoruz
            _context.Users.Remove(user);  // Kullanıcıyı veritabanından siliyoruz
            await _context.SaveChangesAsync();  // Değişiklikleri kaydediyoruz
            return RedirectToAction(nameof(Index));  // Silme işlemi tamamlandıktan sonra listeye yönlendiriyoruz
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);  // Kullanıcının var olup olmadığını kontrol ediyoruz
        }
    }
}

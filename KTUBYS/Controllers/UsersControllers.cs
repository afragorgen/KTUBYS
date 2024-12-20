// API Controller: UsersController
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly KTUBYSContext _context;

    public UsersController(KTUBYSContext context)
    {
        _context = context;
    }

    // Tüm Kullanıcıları Al (GET api/users)
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _context.Users.ToList();
        return Ok(users); // JSON olarak döner
    }

    // Belirli bir Kullanıcıyı Al (GET api/users/{id})
    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserID == id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user); // JSON olarak döner
    }

    // Yeni Kullanıcı Ekle (POST api/users)
    [HttpPost]
    public IActionResult CreateUser([FromBody] User user)
    {
        if (user == null)
        {
            return BadRequest();
        }

        _context.Users.Add(user);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetUserById), new { id = user.UserID }, user);
    }

    // Kullanıcıyı Güncelle (PUT api/users/{id})
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User user)
    {
        if (id != user.UserID)
        {
            return BadRequest();
        }

        var existingUser = _context.Users.Find(id);
        if (existingUser == null)
        {
            return NotFound();
        }

        existingUser.Username = user.Username;
        existingUser.Password = user.Password;
        existingUser.Email = user.Email;
        existingUser.Role = user.Role;

        _context.Users.Update(existingUser);
        _context.SaveChanges();
        return NoContent(); // Güncellenmiş veri döndürülmeden başarılı bir yanıt döner
    }

    // Kullanıcıyı Sil (DELETE api/users/{id})
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        _context.SaveChanges();
        return NoContent(); // Silme işlemi başarılı
    }
}

// API Controller: AdvisorsController
[Route("api/[controller]")]
[ApiController]
public class AdvisorsController : ControllerBase
{
    private readonly KTUBYSContext _context;

    public AdvisorsController(KTUBYSContext context)
    {
        _context = context;
    }

    // Tüm Danışmanları Al (GET api/advisors)
    [HttpGet]
    public IActionResult GetAllAdvisors()
    {
        var advisors = _context.Advisors.ToList();
        return Ok(advisors); // JSON olarak döner
    }

    // Belirli bir Danışmanı Al (GET api/advisors/{id})
    [HttpGet("{id}")]
    public IActionResult GetAdvisorById(int id)
    {
        var advisor = _context.Advisors.FirstOrDefault(a => a.AdvisorID == id);
        if (advisor == null)
        {
            return NotFound();
        }
        return Ok(advisor); // JSON olarak döner
    }

    // Yeni Danışman Ekle (POST api/advisors)
    [HttpPost]
    public IActionResult CreateAdvisor([FromBody] Advisor advisor)
    {
        if (advisor == null)
        {
            return BadRequest();
        }

        _context.Advisors.Add(advisor);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetAdvisorById), new { id = advisor.AdvisorID }, advisor);
    }

    // Danışmanı Güncelle (PUT api/advisors/{id})
    [HttpPut("{id}")]
    public IActionResult UpdateAdvisor(int id, [FromBody] Advisor advisor)
    {
        if (id != advisor.AdvisorID)
        {
            return BadRequest();
        }

        var existingAdvisor = _context.Advisors.Find(id);
        if (existingAdvisor == null)
        {
            return NotFound();
        }

        existingAdvisor.Fullname = advisor.Fullname;
        existingAdvisor.Title = advisor.Title;
        existingAdvisor.Department = advisor.Department;
        existingAdvisor.Email = advisor.Email;

        _context.Advisors.Update(existingAdvisor);
        _context.SaveChanges();
        return NoContent(); // Güncellenmiş danışman verisi döndürülmeden başarılı bir yanıt döner
    }

    // Danışmanı Sil (DELETE api/advisors/{id})
    [HttpDelete("{id}")]
    public IActionResult DeleteAdvisor(int id)
    {
        var advisor = _context.Advisors.Find(id);
        if (advisor == null)
        {
            return NotFound();
        }

        _context.Advisors.Remove(advisor);
        _context.SaveChanges();
        return NoContent(); // Silme işlemi başarılı
    }
}


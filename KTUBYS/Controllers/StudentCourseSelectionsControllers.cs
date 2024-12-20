// API Controller: StudentCourseSelectionsController
[Route("api/[controller]")]
[ApiController]
public class StudentCourseSelectionsController : ControllerBase
{
    private readonly KTUBYSContext _context;

    public StudentCourseSelectionsController(KTUBYSContext context)
    {
        _context = context;
    }

    // Tüm Öğrenci-Kurs Seçimlerini Al (GET api/studentcourseselections)
    [HttpGet]
    public IActionResult GetAllStudentCourseSelections()
    {
        var selections = _context.StudentCourseSelections
                                  .Include(scs => scs.Student)
                                  .Include(scs => scs.Course)
                                  .ToList();
        return Ok(selections); // JSON olarak döner
    }

    // Belirli bir Öğrenci-Kurs Seçimini Al (GET api/studentcourseselections/{id})
    [HttpGet("{id}")]
    public IActionResult GetStudentCourseSelectionById(int id)
    {
        var selection = _context.StudentCourseSelections
                                 .Include(scs => scs.Student)
                                 .Include(scs => scs.Course)
                                 .FirstOrDefault(scs => scs.SelectionID == id);
        if (selection == null)
        {
            return NotFound();
        }
        return Ok(selection); // JSON olarak döner
    }

    // Yeni Öğrenci-Kurs Seçimi Ekle (POST api/studentcourseselections)
    [HttpPost]
    public IActionResult CreateStudentCourseSelection([FromBody] StudentCourseSelection selection)
    {
        if (selection == null)
        {
            return BadRequest();
        }

        _context.StudentCourseSelections.Add(selection);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetStudentCourseSelectionById), new { id = selection.SelectionID }, selection);
    }

    // Öğrenci-Kurs Seçimini Güncelle (PUT api/studentcourseselections/{id})
    [HttpPut("{id}")]
    public IActionResult UpdateStudentCourseSelection(int id, [FromBody] StudentCourseSelection selection)
    {
        if (id != selection.SelectionID)
        {
            return BadRequest();
        }

        var existingSelection = _context.StudentCourseSelections.Find(id);
        if (existingSelection == null)
        {
            return NotFound();
        }

        existingSelection.StudentID = selection.StudentID;
        existingSelection.CourseID = selection.CourseID;
        existingSelection.SelectionDate = selection.SelectionDate;
        existingSelection.IsApproved = selection.IsApproved;

        _context.StudentCourseSelections.Update(existingSelection);
        _context.SaveChanges();
        return NoContent(); // Güncellenmiş veri döndürülmeden başarılı bir yanıt döner
    }

    // Öğrenci-Kurs Seçimini Sil (DELETE api/studentcourseselections/{id})
    [HttpDelete("{id}")]
    public IActionResult DeleteStudentCourseSelection(int id)
    {
        var selection = _context.StudentCourseSelections.Find(id);
        if (selection == null)
        {
            return NotFound();
        }

        _context.StudentCourseSelections.Remove(selection);
        _context.SaveChanges();
        return NoContent(); // Silme işlemi başarılı
    }
}


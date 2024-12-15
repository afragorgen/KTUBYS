using System.ComponentModel.DataAnnotations;

namespace KTUBYS.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }  // Primary Key
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public bool IsMandatory { get; set; }
        public int Credit { get; set; }
        public string Department { get; set; }
    }
}

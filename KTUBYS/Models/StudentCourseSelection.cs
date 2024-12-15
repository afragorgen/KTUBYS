using System.ComponentModel.DataAnnotations;

namespace KTUBYS.Models
{
    public class StudentCourseSelection
    {
        [Key]
        public int SelectionID { get; set; }  // Primary Key
        public int StudentID { get; set; }  // Foreign Key
        public int CourseID { get; set; }  // Foreign Key
        public DateTime SelectionDate { get; set; }
        public bool IsApproved { get; set; }

        public Student Student { get; set; }  // Navigation property
        public Course Course { get; set; }  // Navigation property
    }
}

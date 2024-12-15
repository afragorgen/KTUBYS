using System.ComponentModel.DataAnnotations;

namespace KTUBYS.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public int AdvisorID { get; set; } // Foreign Key
        public Advisor Advisor { get; set; } // Navigation Property

    }
}

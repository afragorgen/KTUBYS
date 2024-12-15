using System.ComponentModel.DataAnnotations;

namespace KTUBYS.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; } // Primary Key
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        
        public int RelatedID { get; set; } // Foreign Key (Öğrenci veya Danışman ID)
        public Student Student { get; set; } // Navigation Property (Öğrenci)
        public Advisor Advisor { get; set; } // Navigation Property (Danışman)

    }
}

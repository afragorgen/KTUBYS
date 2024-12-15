namespace KTUBYS.Models
{
    public class Advisor
    {

        [Key]
        public int AdvisorID { get; set; }; //Primary Key 
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }




    }
}

namespace E_Learning.Areas.Admin.Models
{
    public class InstructorViewModel
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateTime JoinDate { get; set; } 
        public string Profession { get; set; } = null!;
        public int Balance { get; set; } 

    }

}

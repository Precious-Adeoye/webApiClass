using System.ComponentModel.DataAnnotations;

namespace webApiClass.model
{
    public class Student
    {
        [Required]
        public int Id { get; set; }
        [StringLength(15)]
        public string FirstName { get; set; }
        [StringLength(15)]
        public string LastName { get; set; }
       
        [StringLength(1), MaxLength(1, ErrorMessage = "The gender is a single character that is M of F")]
        public string Gender { get; set; }
        [StringLength(15)]
        public string Stack { get; set; }
    }
}

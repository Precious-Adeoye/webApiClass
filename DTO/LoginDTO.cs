using System.ComponentModel.DataAnnotations;

namespace webApiClass.DTO
{
    public class LoginDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}

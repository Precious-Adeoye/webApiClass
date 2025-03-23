using System.ComponentModel.DataAnnotations;

namespace webApiClass.DTO
{
    public class RegisterDTO
    {
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}

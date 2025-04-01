using System.ComponentModel.DataAnnotations;

namespace LoanApplicationApp.API.Models.DTO.Auth
{
    public class RegisterRequestDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; } = "App User";

    }
}

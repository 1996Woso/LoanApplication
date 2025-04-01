using System.ComponentModel.DataAnnotations;

namespace LoanApplicationApp.API.Models.DTO.Auth
{
    public class LoginResponseDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PcShop.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; } = "";

        [Required(ErrorMessage = "Введите почту")]
        [EmailAddress(ErrorMessage = "Неверный формат почты")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Введите пароль")]
        [MinLength(4, ErrorMessage = "Минимум 4 символа")]
        public string Password { get; set; } = "";
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите почту")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; } = "";
    }
}
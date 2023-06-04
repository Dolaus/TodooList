using System.ComponentModel.DataAnnotations;

namespace TodooList.Models.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не вказаний Ім'я")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не вказаний Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не вказаний пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введений неверно")]
        public string ConfirmPassword { get; set; }
    }
}

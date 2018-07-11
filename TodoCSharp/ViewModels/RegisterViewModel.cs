using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoCSharp.ViewModels
{
    /// <summary>
    /// Модель для регистрации
    /// </summary>
    public class RegisterViewModel
    {
        public Byte[] ImageData { get; set; }
        public String ImageMimeType { get; set; }

        [Required(ErrorMessage = "Введите {0}")]
        [Display(Name = "Логин")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Длинна логина должна быть от 4 до 10 символов")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "Введите {0}")]
        [Display(Name = "Возраст")]
        public Int32 Age { get; set; }

        [Required(ErrorMessage = "Введите {0}")]
        [Display(Name = "Почта")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Введите {0}")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public String Password { get; set; }

        [Required(ErrorMessage = "Введите {0}")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        public String PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Введите число с картинки")]
        public String Captcha { get; set; }
    }
}

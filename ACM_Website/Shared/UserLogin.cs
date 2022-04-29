using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM_Website.Shared
{
    public class UserLogin
    {
        [Required(ErrorMessage ="Необходимо ввести имя пользователя")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Необходимо ввести пароль")]
        public string Password { get; set; } = string.Empty;
    }
}

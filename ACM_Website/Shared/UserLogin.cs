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
        [Required(ErrorMessage ="Необходимо ввести email")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Необходимо ввести пароль")]
        public string Password { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM_Website.Shared
{
    public class WebUser
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string Role { get; set; } 

    }
}

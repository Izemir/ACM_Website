using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Dtos.User
{
    public class UserDto
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string Role { get; set; } 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Models.Executor
{
    public class Executor
    {
        

        public long Id { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public List<Contact> Contacts { get; set; }

        public bool IsDelete { get; set; }

        public string INN { get; set; }

        public List<Competency> Competency { get; set; }

        public List<Speciality> Speciality { get; set; }

        public User User { get; set; }

        public List<Chat.Chat> Chats { get; set; }

        public bool Approved { get; set; } = false;

    }
}

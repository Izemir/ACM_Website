using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Models.Chat
{
    public class Chat
    {
        public long Id { get; set; }

        public List<Message> Messages { get; set; }
    }
}

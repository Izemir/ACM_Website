using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Dtos.Chat
{
    public class MessageDto
    {
        public long Id { get; set; }

        public string Text { get; set; }

        public string SenderName { get; set; }

        public long SenderId { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}

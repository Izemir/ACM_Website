using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Dtos.Chat
{
    public class ChatDto
    {
        public long Id { get; set; }

        public List<MessageDto> Messages { get; set; }

        public string ChatName { get; set; }

        public long OrderId { get; set; }
    }
}

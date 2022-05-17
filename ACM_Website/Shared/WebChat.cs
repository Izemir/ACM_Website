using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM_Website.Shared
{
    public class WebChat
    {
        public long Id { get; set; }

        public List<Message> Messages { get; set; }

        public string ChatName { get; set; }

        public string SenderName { get; set; }
    }
}

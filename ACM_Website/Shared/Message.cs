using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM_Website.Shared
{
    public class Message
    {
        public long Id { get; set; }

        public string Text { get; set; }

        public string SenderName { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}

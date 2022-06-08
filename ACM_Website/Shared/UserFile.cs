using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM_Website.Shared
{
    public class UserFile
    {
        public long Id { get; set; }

        public string Path { get; set; }

        public string FileName { get; set; }

        public byte[] FileContent { get; set; }
    }
}

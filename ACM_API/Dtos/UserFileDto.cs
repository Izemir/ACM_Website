using Microsoft.AspNetCore.Components.Forms;
using System;
using System.IO;
using System.Threading;

namespace ACM_API.Dtos
{
    public class UserFileDto
    {
        public long Id { get; set; }

        public string Path { get; set; }

        public string FileName { get; set; }

        public byte[] FileContent { get; set; }
    }
}

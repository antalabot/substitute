using System;
using System.Collections.Generic;
using System.Text;

namespace Substitute.Business.DataStructs.ImageResponse
{
    public class ImageResponseDigestModel
    {
        public ulong Id { get; set; }
        public string Command { get; set; }
        public string Filename { get; set; }
    }
}

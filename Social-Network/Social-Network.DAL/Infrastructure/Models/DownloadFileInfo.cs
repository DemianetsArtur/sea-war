using System.IO;

namespace Social_Network.DAL.Infrastructure.Models
{
    public class DownloadFileInfo
    {
        public Stream Content { get; set; }

        public string ContentType { get; set; }
    }
}
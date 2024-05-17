using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using NAudio.Wave;

namespace xrayimageproject
{
    internal class FileChecker
    {
        public static readonly List<string> imageExtensions = new List<string>
        {
            ".jpg", ".jpeg", ".png", ".bmp", ".gif"
        };

        public static readonly List<string> audioExtensions = new List<string>
        {
            ".wav", ".aiff", ".mp3", ".m4a", ".flac", ".aac", ".mid", ".midi"
        };

        string path;

        public FileChecker(string path)
        {
            this.path = path;
        }

        public string check()
        {
            if (File.Exists(this.path))
            {
                if (this.isImage(this.path))
                {
                    return "image";
                }
                else if (this.isAudio(this.path))
                {
                    return "audio";
                }
                else if (this.isPDFFile(this.path))
                {
                    return "pdf";
                }
                else
                {
                    throw new Exception("Not Supported");
                }
            }
            throw new FileNotFoundException();

        }
        private bool isImage(string path)
        {
            string extension = Path.GetExtension(path);
            return imageExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
        }
        private bool isAudio(string path)
        {
            string extension = Path.GetExtension(path);
            return audioExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
        }
        private bool isPDFFile(string path)
        {
            string extension = Path.GetExtension(path);
            return extension is ".pdf";
        }

    }
}

using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using xrayimageproject.Properties;

namespace xrayimageproject
{
    public class Searcher
    {
        private List<string> filePaths = new List<string>();
        public Searcher()
        {

        }
    }

    public class DateSearcher : Searcher
    {
        private DateTime date;
        public DateSearcher(DateTime date)
        {
            this.date = date;
        }

        public List<FileInfo> search()
        {
            string searchPath = Properties.Settings.Default.searchPath;
        
            DirectoryInfo searchDir = new DirectoryInfo(Properties.Settings.Default.searchPath);
            var files = searchDir.GetFiles("*", SearchOption.AllDirectories)
                                 .Where(file => file.CreationTime.Date == this.date.Date)
                                 .ToList();

            foreach (FileInfo file in files )
            {
                //Console.WriteLine(file.);
            }

            return files;

        }
    }

    public class SizeSearcher : Searcher
    {
        private int size;
        public SizeSearcher(int size)
        {
            this.size = size;
        }

        public List<FileInfo> search()
        {
            string searchPath = Properties.Settings.Default.searchPath;

            DirectoryInfo searchDir = new DirectoryInfo(searchPath);
            var files = searchDir.GetFiles("*", SearchOption.AllDirectories)
                                 .Where(file => (int)file.Length / 1048576 == this.size)
                                 .ToList();

            return files;

        }
    }
}

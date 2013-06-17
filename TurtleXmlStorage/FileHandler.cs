using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TurtleXmlStorage
{
    public class FileHandler
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public FileHandler(string fileName)
        {
            this.FileName = fileName;
        }

        public string ReadFile()
        {
            if (!this.IsFileExist())
            {
                this.CreateFile();
            }

            var fileContent = new StringBuilder();
            var stream = File.OpenText(this.FilePath);
            var contentLine = string.Empty;
            while ((contentLine = stream.ReadLine()) != null)
            {
                fileContent.Append(contentLine);
            }

            return fileContent.ToString();
        }

        public void CreateFile()
        {
            File.Create(this.FilePath);
        }

        public bool IsFileExist()
        {
            return File.Exists(this.FilePath);
        }

        public void SaveFile()
        {
            
        }
    }
}
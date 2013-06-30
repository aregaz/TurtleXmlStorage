﻿using System;
using System.IO;
using System.Text;

namespace TurtleXmlStorage
{
    public class FileHandler
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public FileHandler(string fileName)
        {
            this.FileName = fileName;
	        this.FilePath = this.GetPath(this.GetType()) + "\\" + fileName;
        }

        public string ReadFile()
        {
            if (!this.IsFileExist())
            {
                this.CreateFile();
            }

            var fileContent = new StringBuilder();
			using (var stream = File.OpenText(this.FilePath))
	        {
				var contentLine = string.Empty;
				while ((contentLine = stream.ReadLine()) != null)
				{
					fileContent.Append(contentLine);
				}

				return fileContent.ToString();
	        }
        }

        public void CreateFile()
        {
	        using (var fileStream = File.Create(this.FilePath))
	        {
		        using (var streamWriter = new StreamWriter(fileStream))
		        {
			        streamWriter.Write(this.GetDefaultContent());
		        }
	        }
        }

		public virtual string GetDefaultContent()
		{
			return "PLease ovverride GetDefaultContent method.";
		}

	    public bool IsFileExist()
        {
            return File.Exists(this.FilePath);
        }

        public void SaveFile(string content)
        {
			using (var outfile = new StreamWriter(this.FilePath))
			{
				outfile.Write(content);
			}
        }

		public string GetPath(Type type)
		{
			//get the full location of the assembly with DaoTests in it
			string fullPath = System.Reflection.Assembly.GetAssembly(type).Location;

			//get the folder that's in
			string theDirectory = Path.GetDirectoryName(fullPath);

			return theDirectory;
		}
    }
}
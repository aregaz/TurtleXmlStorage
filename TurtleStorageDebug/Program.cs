using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleXmlStorage;

namespace TurtleStorageDebug
{
	class Program
	{
		static void Main(string[] args)
		{
			var fileHandler = new TurtleConfigurationHandler();

			//get the full location of the assembly with DaoTests in it
			string fullPath = System.Reflection.Assembly.GetAssembly(typeof(Program)).Location;

			//get the folder that's in
			string theDirectory = Path.GetDirectoryName(fullPath);

			var s = fileHandler.GetPath(fileHandler.GetType());
			Console.WriteLine(s);
			
			var str = fileHandler.ReadFile();
			Console.WriteLine(str);
			Console.ReadLine();
		}
	}
}

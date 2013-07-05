using System;
using TurtleXmlStorage;

namespace TurtleStorageDebug
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("> 1 - Save Configuration\r\n> 2 - Get configuration\r\nPlease make choice...");
			var key = Console.ReadKey();
			Console.WriteLine();

			try
			{
				if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
				{
					SaveConfiguration();
				}
				else if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
				{
					GetConfiguration();
				}
			}
			catch (Exception exc)
			{
				Console.WriteLine("Error occured: {0}", exc.Message);
			}
			finally
			{
				Console.WriteLine("Press any key to exit...");
				Console.ReadKey();
			}

			////var fileHandler = new TurtleConfigurationHandler();

			//////get the full location of the assembly with DaoTests in it
			////string fullPath = System.Reflection.Assembly.GetAssembly(typeof(Program)).Location;

			//////get the folder that's in
			////string theDirectory = Path.GetDirectoryName(fullPath);

			////var s = fileHandler.GetPath(fileHandler.GetType());
			////Console.WriteLine(s);

			////var str = fileHandler.ReadFile();
			////Console.WriteLine(str);
			////Console.ReadLine();
		}

		private static void GetConfiguration()
		{
			Console.Write("> Enter project name: ");
			var projectName = Console.ReadLine();
			Console.Write("> Enter configuration name: ");
			var configurationName = Console.ReadLine();

			var storage = XmlStorage.GetInstace(projectName);

			Console.WriteLine("\r\n\r\n> Configuration value: {0}\r\n", storage[configurationName]);
			//Console.ReadLine();
		}

		private static void SaveConfiguration()
		{
			Console.Write("> Enter project name: ");
			var projectName = Console.ReadLine();
			Console.Write("> Enter configuration name: ");
			var configurationName = Console.ReadLine();
			Console.Write("> Enter configuration value: ");
			var configurationValue = Console.ReadLine();

			var storage = XmlStorage.GetInstace("TurtleStorageDebug");
			storage[configurationName] = configurationValue;

			Console.WriteLine("\r\n\r\n> Configuration value:\r\n{0}", storage[configurationName]);
			Console.ReadLine();
		}
	}
}

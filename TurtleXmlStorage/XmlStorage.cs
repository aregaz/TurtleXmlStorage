using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace TurtleXmlStorage
{
    public static class XmlStorage
    {
		static XmlStorage()
		{
			XmlStorage.FileHandler = new TurtleConfigurationHandler();
			XmlStorage.Configurations = XmlStorage.LoadXmlConfigurationFromFile();
		}

	    public static XElement Configurations { get; set; }
		public static TurtleConfigurationHandler FileHandler { get; set; }

		public static string GetConfiguration(string projectName, string configurationName)
		{
			try
			{
				// ReSharper disable PossibleNullReferenceException
				// ReSharper disable ReplaceWithSingleCallToSingleOrDefault
				// ReSharper disable PossibleNullReferenceException

				var projectConfigurations = XmlStorage.Configurations
					.Element("projects")
					.Elements("project")
					.SingleOrDefault(x => x.Attribute("name") != null && x.Attribute("name").Value == projectName); ;

				if (projectConfigurations == null) throw new NullReferenceException("Project with specified Project Name was not found.");

				var configuration = projectConfigurations
					.Elements("configuration")
					.SingleOrDefault(x => x.Attribute("name") != null && x.Attribute("name").Value == configurationName);

				if (configuration == null) throw new NullReferenceException("Configuration with specified Configuration Name was not found.");

				return configuration.Value;
			}
			catch (Exception exc)
			{
				throw exc;
			}
		}

		public static void SaveConfiguration(string projectName, string configurationName, string configurationValue)
		{
			var projectConfigurations = XmlStorage.Configurations
									.Element("projects")
									.Elements("project")
									.SingleOrDefault(x => x.Attribute("name") != null && x.Attribute("name").Value == projectName);
			if (projectConfigurations == null)
			{
				// add <project name="peojectName"> section:
				XmlStorage.Configurations.Element("projects").Add(new XElement("project", new XAttribute("name", projectName)));
				projectConfigurations = XmlStorage.Configurations
									.Element("projects")
									.Elements("project")
									.SingleOrDefault(x => x.Attribute("name") != null && x.Attribute("name").Value == projectName);
			}

			var configuration = projectConfigurations
				.Elements("configuration")
			    .SingleOrDefault(
				    x =>
				    x.Attribute("name") != null && x.Attribute("name").Value == configurationName);
			if (configuration == null)
			{
				// add <configuration name="configurationName">configurationValue</configuration> to the project section:
				projectConfigurations.Add(new XElement("configuration", new XAttribute("name", configurationName), configurationValue));
			}
			else
			{
				// update configuration:
				configuration.Value = configurationValue;
			}

			XmlStorage.SaveXmlToFile();
		}

		private static XElement LoadXmlConfigurationFromFile()
		{
			var fileContent = FileHandler.ReadFile();
			return XElement.Parse(fileContent);
		}

		private static void SaveXmlToFile()
		{
			XmlStorage.FileHandler.SaveFile(XmlStorage.Configurations.ToString());
		}
    }
}
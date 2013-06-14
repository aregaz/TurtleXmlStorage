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
			if (!XmlStorage.CheckFileExists())
			{
				XmlStorage.CreateFile();
			}

			XmlStorage.Configurations = XmlStorage.GetXmlConfigurationFromFile();
		}

	    public static string FileName { get { return "configuration.xml"; } }

	    public static string FileFullPath {get { return string.Format("c:\\{0}", XmlStorage.FileName); }}

		public static XElement Configurations { get; set; }

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
				var configuration = projectConfigurations
					.Elements("configuration")
					.SingleOrDefault(x => x.Attribute("name") != null && x.Attribute("name").Value == configurationName);
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

			XmlStorage.SaveChanges();
		}

		private static XElement GetXmlConfigurationFromFile()
		{
			if (!XmlStorage.CheckFileExists()) return null;

			return XElement.Load(XmlStorage.FileFullPath);
		}


		private static bool CheckFileExists()
		{
			return File.Exists(XmlStorage.FileFullPath);
		}

		private static void CreateFile()
		{
			XElement xml = new XElement("configurations", new XElement("projects"));
			xml.Save(XmlStorage.FileFullPath);
		}

		private static void SaveChanges()
		{
			XmlStorage.Configurations.Save(XmlStorage.FileFullPath);
		}
    }
}

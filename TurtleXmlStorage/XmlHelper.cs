using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XmlFileHendler
{
    public static class XmlHelper
    {
		static XmlHelper()
		{
			if (!XmlHelper.CheckFileExists())
			{
				XmlHelper.CreateFile();
			}

			XmlHelper.Configurations = XmlHelper.GetXmlConfigurationFromFile();
		}

	    public static string FileName { get { return "configuration.xml"; } }

	    public static string FileFullPath {get { return string.Format("c:\\{0}", XmlHelper.FileName); }}

		public static XElement Configurations { get; set; }

		public static string GetConfiguration(string projectName, string configurationName)
		{
			try
			{
				// ReSharper disable PossibleNullReferenceException
				// ReSharper disable ReplaceWithSingleCallToSingleOrDefault
				// ReSharper disable PossibleNullReferenceException

				var projectConfigurations = XmlHelper.Configurations
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
			var projectConfigurations = XmlHelper.Configurations
									.Element("projects")
									.Elements("project")
									.SingleOrDefault(x => x.Attribute("name") != null && x.Attribute("name").Value == projectName);
			if (projectConfigurations == null)
			{
				// add <project name="peojectName"> section:
				XmlHelper.Configurations.Element("projects").Add(new XElement("project", new XAttribute("name", projectName)));
				projectConfigurations = XmlHelper.Configurations
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

			XmlHelper.SaveChanges();
		}

		private static XElement GetXmlConfigurationFromFile()
		{
			if (!XmlHelper.CheckFileExists()) return null;

			return XElement.Load(XmlHelper.FileFullPath);
		}


		private static bool CheckFileExists()
		{
			return File.Exists(XmlHelper.FileFullPath);
		}

		private static void CreateFile()
		{
			XElement xml = new XElement("configurations", new XElement("projects"));
			xml.Save(XmlHelper.FileFullPath);
		}

		private static void SaveChanges()
		{
			XmlHelper.Configurations.Save(XmlHelper.FileFullPath);
		}
    }
}

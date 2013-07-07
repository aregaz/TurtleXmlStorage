using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TurtleXmlStorage
{
	public class XmlStorage : Dictionary<string, string>
	{
		#region Fields

		private SaveOption saveOption = SaveOption.EachTime;

		#endregion


		#region Singleton

		private static readonly object lockObj = new object();
		private static volatile XmlStorage mySingletonInstance;

		public static XmlStorage GetInstace(string projectName)
		{
			if (mySingletonInstance == null)
			{
				lock (lockObj)
				{
					if (mySingletonInstance == null)
					{
						mySingletonInstance = new XmlStorage(projectName);
					}
				}
			}

			return mySingletonInstance;
		}

		#endregion


		#region Constructor

		private XmlStorage(string projectName)
		{
			this.ProjectName = projectName;
			this.FileHandler = new TurtleConfigurationHandler();
			//this.Configurations = this.LoadProjectConfigurationsXMLFromFile(projectName);

			var configurations = this.LoadProjectConfigurationsXMLFromFile(projectName);
			if (configurations != null)
			{
				foreach (var configurationXML in configurations.Elements())
				{
					if (configurationXML != null)
					{
						var key = configurationXML.Attribute("name") == null
							          ? string.Empty
							          : configurationXML.Attribute("name").Value;
						var value = configurationXML.Value;
						this.Add(key, value);
					}
				}
			}
		}

		#endregion


		#region Properties

		public TurtleConfigurationHandler FileHandler { get; set; }

		public string ProjectName { get; private set; }

		public SaveOption SaveOption
		{
			get { return this.saveOption; }
			set { this.saveOption = value; }
		}


		public XElement XML
		{
			get
			{
				var xml = new XElement("configurations");
				var projectXML = new XElement("project", new XAttribute("name", this.ProjectName));
				foreach (var configuration in this)
				{
					projectXML.Add(new XElement("configuration", new XAttribute("name", configuration.Key), configuration.Value));
				}

				xml.Add(new XElement("projects", projectXML));

				return xml;
			}
		}

		#endregion


		#region Indexer

		public new string this[string configurationName]
		{
			get
			{
				//if (!this.ContainsKey(configurationName))
				//	this.Add(configurationName, null);
				return base[configurationName];
			}

			set
			{
				if (!this.ContainsKey(configurationName))
					this.Add(configurationName, null);
				base[configurationName] = value;

				if (this.SaveOption == SaveOption.EachTime) this.SaveXmlToFile();
			}
		}

		#endregion


		public void SaveXmlToFile()
		{
			this.FileHandler.SaveFile(this.XML);
		}


		private XElement LoadProjectConfigurationsXMLFromFile(string projectName)
		{
			try
			{
				var fileContent = this.FileHandler.ReadFile();
				var projects = fileContent
					.Element("projects")
					.Elements("project");
				var project = projects.SingleOrDefault(x => x.Attribute("name") != null && x.Attribute("name").Value == projectName);
				return project;
			}
			catch (NullReferenceException)
			{
				return this.FileHandler.GetDefaultXML();
			}
		}
	}
}
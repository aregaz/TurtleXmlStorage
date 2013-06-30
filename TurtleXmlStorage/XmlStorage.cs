﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;

namespace TurtleXmlStorage
{
    public class XmlStorage : Dictionary<string, string>, INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		public XmlStorage(string projectName)
		{
			this.ProjectName = projectName;
			this.FileHandler = new TurtleConfigurationHandler();
			//this.Configurations = this.LoadProjectConfigurationsXMLFromFile(projectName);

			foreach (var configurationXML in this.LoadProjectConfigurationsXMLFromFile(projectName).Elements())
			{
				this.Add(configurationXML.Attribute("configurationName").Value, configurationXML.Value);
			}

			this.PropertyChanged += this.OnPropertyChanged;
		}


	    #region INotifyPropertyChanged

	    private void RaiseConfigurationChanged()
	    {
		    if (this.PropertyChanged != null)
		    {
			    this.PropertyChanged(this, new PropertyChangedEventArgs("Color"));
		    }
	    }

	    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
	    {
		    this.SaveXmlToFile();
	    }

	    #endregion


		//public XElement Configurations { get; set; }
		public TurtleConfigurationHandler FileHandler { get; set; }
		public string ProjectName { get; private set; }

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

				xml.Add(projectXML);
				
			    return xml;
		    }
	    }

		public new string this[string configurationName]
		{
			get { return base[configurationName]; }

			set
			{
				//var configuration = this[configurationName];
				//if (configuration == null) this.Add(configurationName, string.Empty);

				base[configurationName] = value;

				this.RaiseConfigurationChanged(); // TODO: can be changed to just Save() method
			}
		}

		//public string GetConfiguration(string projectName, string configurationName)
		//{
		//	try
		//	{
		//		// ReSharper disable PossibleNullReferenceException
		//		// ReSharper disable ReplaceWithSingleCallToSingleOrDefault
		//		// ReSharper disable PossibleNullReferenceException

		//		var projectConfigurations = this.Configurations
		//			.Element("projects")
		//			.Elements("project")
		//			.SingleOrDefault(x => x.Attribute("name") != null && x.Attribute("name").Value == projectName); ;

		//		if (projectConfigurations == null) throw new NullReferenceException("Project with specified Project Name was not found.");

		//		var configuration = projectConfigurations
		//			.Elements("configuration")
		//			.SingleOrDefault(x => x.Attribute("name") != null && x.Attribute("name").Value == configurationName);

		//		if (configuration == null) throw new NullReferenceException("Configuration with specified Configuration Name was not found.");

		//		return configuration.Value;
		//	}
		//	catch (Exception exc)
		//	{
		//		throw exc;
		//	}
		//}

		//public  void SaveConfiguration(string projectName, string configurationName, string configurationValue)
		//{
		//	var projectConfigurations = this.Configurations
		//							.Element("projects")
		//							.Elements("project")
		//							.SingleOrDefault(x => x.Attribute("name") != null && x.Attribute("name").Value == projectName);
		//	if (projectConfigurations == null)
		//	{
		//		// add <project name="peojectName"> section:
		//		this.Configurations.Element("projects").Add(new XElement("project", new XAttribute("name", projectName)));
		//		projectConfigurations = this.Configurations
		//							.Element("projects")
		//							.Elements("project")
		//							.SingleOrDefault(x => x.Attribute("name") != null && x.Attribute("name").Value == projectName);
		//	}

		//	var configuration = projectConfigurations
		//		.Elements("configuration")
		//		.SingleOrDefault(
		//			x =>
		//			x.Attribute("name") != null && x.Attribute("name").Value == configurationName);
		//	if (configuration == null)
		//	{
		//		// add <configuration name="configurationName">configurationValue</configuration> to the project section:
		//		projectConfigurations.Add(new XElement("configuration", new XAttribute("name", configurationName), configurationValue));
		//	}
		//	else
		//	{
		//		// update configuration:
		//		configuration.Value = configurationValue;
		//	}

		//	this.SaveXmlToFile();
		//}

		private XElement LoadProjectConfigurationsXMLFromFile(string projectName)
		{
			var fileContent = this.FileHandler.ReadFile();
			var fileXML = XElement.Parse(fileContent);

			return
				fileXML.Elements("project")
				       .SingleOrDefault(x => x.Attribute("name") != null && x.Attribute("name").Value == projectName);
		}

		private void SaveXmlToFile()
		{
			this.FileHandler.SaveFile(this.XML.ToString());
		}
    }
}
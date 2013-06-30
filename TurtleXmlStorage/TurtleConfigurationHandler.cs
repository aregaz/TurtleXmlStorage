﻿using System.Xml.Linq;

namespace TurtleXmlStorage
{
    public class TurtleConfigurationHandler : FileHandler
    {
        public TurtleConfigurationHandler() : base("TurtleConfig.xml")
        {
            
        }

        public override string GetDefaultContent()
        {
            var xml = new XElement("configurations", new XElement("projects"));
            return xml.ToString();
        }

		public string GetPath()
		{
			var t = this.GetType();
			return base.GetPath(t);
		}
    }
}
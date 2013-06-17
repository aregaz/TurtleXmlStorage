using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

		public void SaveFile(XElement content)
		{

		}

		public string GetPath()
		{
			var t = this.GetType();
			return base.GetPath(t);
		}
    }
}

using System.Xml.Linq;

namespace TurtleXmlStorage
{
    public class TurtleConfigurationHandler : FileHandler
    {
        public TurtleConfigurationHandler() : base("TurtleConfig.xml")
        {
            
        }

		public XElement GetDefaultXML()
		{
			return new XElement("configurations", new XElement("projects"));
		}

        protected override string GetDefaultContent()
        {
            return this.GetDefaultXML().ToString();
        }

		private string GetPath()
		{
			var t = this.GetType();
			return base.GetPath(t);
		}

		public new XElement ReadFile()
		{
			return XElement.Parse(base.ReadFile());
		}

		public void SaveFile(XElement configurations)
		{
			base.SaveFile(configurations.ToString());
		}
    }
}
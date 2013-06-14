Turtle XML Storage
==================

Simple configuration storage for your projects in single XML file with simple structure.

Just add reference to TurtleXmlStorage.dll - and you can use it like this:

```C#
// get configuration:
var configuration = TurtleXmlStorage.XmlStorage.GetConfiguration("MyProject", "MyConfigurationName");

// save configuration:
TurtleXmlStorage.XmlStorage.SaveConfiguration("MyProject",
  "MyConfigurationName", "This is configuration value parameter");
```

I also plan to implement couple features in future:
- [ ] Add configuration functionality (for example, change file path) as TurtleXmlStorage_Configuration.xml.
- [ ] Separate file handling from XML handling.

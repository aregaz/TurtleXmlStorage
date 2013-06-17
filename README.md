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
- [ ] Add configuration file TurtleXmlStorage_Configuration.xml (for example, change file path) where you can specify where to store configuration.xml - in MyDocuments folder or in solution folder.
- [ ] Separate file handling from XML handling (refactoring).
- [ ] Store configurations in static variable (in memory) if it was already requested. This will minimize requests to the file. Optionaly an XML attribute can be added to the configuration XML element to indicate whether in-memory storing is required or not.

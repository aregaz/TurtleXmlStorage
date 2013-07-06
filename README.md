Turtle XML Storage
==================

Simple configuration storage in XML file with simple structure.

Just add reference to TurtleXmlStorage.dll - and you can use it like this:

```C#
// initialize storage instance (singleton):
var storage = TurtleXmlStorage.XmlStorage.GetInstance("my-project-name");

// get configuration:
var configuration = storage["MyConfigurationName"];

// save configuration:
storage["MyConfigurationName"] = myConfigurationValue;
```

I also plan to implement couple features in future:
- [ ] Add configuration file TurtleXmlStorage_Configuration.xml (for example, change file path) where you can specify where to store configuration.xml - in MyDocuments folder or in solution folder.
- [ ] Implement storage configurability via parameters (e.g. saving)
- [ ] Manual memory management

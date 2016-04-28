Namara
======

The official C# client for the Namara Open Data service. [namara.io](https://namara.io)
Disclaimer: This wrapper is built using .Net 2.0. It may not work on newer versions of .Net.

## Installation

```bash
git clone git@github.com:namara-io/namara-csharp.git
```

If using Visual Studio, add a reference to Newtonsoft: http://www.newtonsoft.com/json
If using Unity, add Newtonsoft to the /plugins folder.

## Usage

### Instantiation

You need a valid API key in order to access Namara (you can find it in your My Account details on namara.io).

```csharp
using ThinkData;
using Newtonsoft.Json.Linq;

Namara namara = new Namara({YOUR_API_KEY});
```

You can also optionally enable debug mode:

```csharp
Namara namara = new Namara({YOUR_API_KEY}, true);
```

### Getting Data

To make a basic request to the Namara API you can call `get` on your instantiated object and pass it the ID of the data set you want and the version of the data set:

Synchronous:

```csharp
var response = namara.Get<List<JObject>>("5885fce0-92c4-4acb-960f-82ce5a0a4650", "en-1");
```

Without a third options argument passed, this will return data with the Namara default offset (0) and limit (250) applied. To specify options, you can pass an options argument:

```csharp
Hashtable options = new Hashtable {{"offset", "1"}, {"limit", "20"}};

var response = namara.Get<List<JObject>>("5885fce0-92c4-4acb-960f-82ce5a0a4650", "en-1", options);
```

### Options

All [Namara data options](https://namara.io/#/api) are supported.

**Basic options**

```csharp
Hashtable options = new Hashtable();
options.Add("select", "town,geometry");
options.Add("where", "town = "TORONTO" AND nearby(geometry, 43.6, -79.4, 10km)");
options.Add("offset", "0");
options.Add("limit", "20");
```

**Aggregation options**
Only one aggregation option can be specified in a request, in the case of this example, all options are illustrated, but passing more than one in the options object will throw an error.

```csharp
Hashtable options = new Hashtable();
options.Add("operation", "sum(p0)");
options.Add("operation", "avg(p0)");
options.Add("operation", "min(p0)");
options.Add("operation", "max(p0)");
options.Add("operation", "count(*)");
options.Add("operation", "geocluster(p3, 10)");
options.Add("operation", "geobounds(p3)");
```

### Running Tests

Use Visual Studio to run the tests. Make sure to add Microsoft.VisualStudio.QualityTools.UnitTestFramework to the references.

### License

Apache License, Version 2.0

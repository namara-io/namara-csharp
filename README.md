Namara
======

The official C# client for the Namara Open Data service. [namara.io](http://namara.io)
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
using ThinkData
using Newtonsoft.Json.Linq;

Namara namara = new Namara({YOUR_API_KEY});
```

You can also optionally enable debug mode:

```csharp
Namara namara = new Namara({YOUR_API_KEY}, true);
```

### Getting Data

To make a basic request to the Namara API you can call `get` on your instantiated object and pass it the ID of the dataset you want and the ID of the version of the data set:

Synchronous:

```csharp
var response = namara.Get<List<JObject>>("18b854e3-66bd-4a00-afba-8eabfc54f524", "en-2");
```

Without a third options argument passed, this will return data with the Namara default offset (0) and limit (10) applied. To specify options, you can pass an options argument:

```csharp
Hashtable options = new Hashtable {{"offset", "1"}, {"limit", "20"}};

var response = namara.Get<List<JObject>>("18b854e3-66bd-4a00-afba-8eabfc54f524", "en-2", options);
```

### Options

All [Namara data options](http://namara.io/#/api) are supported.

**Basic options**

```csharp
Hashtable options = new Hashtable();
options.Add("select", "p0,p1");
options.Add("where", "p0 = 100 AND nearby(p3, 43.25, -123.1, 10km)");
options.Add("offset", "0");
options.Add("limit", "150");
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
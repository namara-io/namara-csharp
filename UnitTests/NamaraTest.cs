using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.QualityTools.UnitTestFramework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ThinkData
{
    [TestClass]
    public class NamaraTest
    {
        Namara subject;
        string dataset;
        string version;

        [TestInitialize]
        public void Initialize()
        {
            subject = new Namara("myapikey");
            dataset = "18b854e3-66bd-4a00-afba-8eabfc54f524";
            version = "en-2";
        }

        [TestMethod]
        public void TestCreateBasicRequest()
        {
            HttpWebRequest request = subject.CreateBasicRequest(dataset, version, null);
            Assert.AreEqual(request.RequestUri.AbsoluteUri, "https://api.namara.io/v0/data_sets/"+dataset+"/data/"+version+"?api_key=myapikey&");
        }

        [TestMethod]
        public void TestCreateBasicRequestOffset()
        {
            HttpWebRequest request = subject.CreateBasicRequest(dataset, version, new Hashtable { {"offset", "1" } });
            Assert.AreEqual(request.RequestUri.AbsoluteUri, "https://api.namara.io/v0/data_sets/" + dataset + "/data/" + version + "?api_key=myapikey&offset=1&");
        }

        [TestMethod]
        public void TestCreateBasicRequestLimit()
        {
            HttpWebRequest request = subject.CreateBasicRequest(dataset, version, new Hashtable { { "limit", "1" } });
            Assert.AreEqual(request.RequestUri.AbsoluteUri, "https://api.namara.io/v0/data_sets/" + dataset + "/data/" + version + "?api_key=myapikey&limit=1&");
        }

        [TestMethod]
        public void TestCreateBasicRequestSelect()
        {
            HttpWebRequest request = subject.CreateBasicRequest(dataset, version, new Hashtable { { "select", "facility_code" } });
            Assert.AreEqual(request.RequestUri.AbsoluteUri, "https://api.namara.io/v0/data_sets/" + dataset + "/data/" + version + "?api_key=myapikey&select=facility_code&");
        }

        [TestMethod]
        public void TestCreateBasicRequestWhere()
        {
            HttpWebRequest request = subject.CreateBasicRequest(dataset, version, new Hashtable { { "where", "facility_code>1000" } });
            Assert.AreEqual(request.RequestUri.AbsoluteUri, "https://api.namara.io/v0/data_sets/" + dataset + "/data/" + version + "?api_key=myapikey&where=facility_code%3E1000&");
        }

        [TestMethod]
        public void TestCreateBasicRequestSum()
        {
            HttpWebRequest request = subject.CreateBasicRequest(dataset, version, new Hashtable { { "operation", "sum(facility_code)" } });
            Assert.AreEqual(request.RequestUri.AbsoluteUri, "https://api.namara.io/v0/data_sets/" + dataset + "/data/" + version + "/aggregation?api_key=myapikey&operation=sum(facility_code)&");
        }

        [TestMethod]
        public void TestCreateBasicRequestCount()
        {
            HttpWebRequest request = subject.CreateBasicRequest(dataset, version, new Hashtable { { "operation", "count(*)" } });
            Assert.AreEqual(request.RequestUri.AbsoluteUri, "https://api.namara.io/v0/data_sets/" + dataset + "/data/" + version + "/aggregation?api_key=myapikey&operation=count(*)&");
        }

        [TestMethod]
        public void TestCreateBasicRequestMin()
        {
            HttpWebRequest request = subject.CreateBasicRequest(dataset, version, new Hashtable { { "operation", "min(facility_code)" } });
            Assert.AreEqual(request.RequestUri.AbsoluteUri, "https://api.namara.io/v0/data_sets/" + dataset + "/data/" + version + "/aggregation?api_key=myapikey&operation=min(facility_code)&");
        }

        [TestMethod]
        public void TestCreateBasicRequestMax()
        {
            HttpWebRequest request = subject.CreateBasicRequest(dataset, version, new Hashtable { { "operation", "max(facility_code)" } });
            Assert.AreEqual(request.RequestUri.AbsoluteUri, "https://api.namara.io/v0/data_sets/" + dataset + "/data/" + version + "/aggregation?api_key=myapikey&operation=max(facility_code)&");
        }

        //[TestMethod]
        //public void TestGetCount()
        //{
        //    var stub = new Fakes.StubNamara("myapikey", false);
        //    JObject obj = JsonConvert.DeserializeObject<JObject>(@"{result: 129}");
        //    stub.Get<JObject>((dataset, version) => obj);
        //    Assert.AreEqual(stub.Get<JObject>(dataset, version).GetValue("result"), 129);
        //}
    }
}

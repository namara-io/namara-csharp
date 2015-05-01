using System.Collections;
using System.IO;
using System.Net;

using Newtonsoft.Json;

namespace ThinkData
{
	/// <summary>
	/// Wrapper for the Namara API.
	/// All Namara data options are supported: http://namara.io/#/api
	/// </summary>
	public class Namara {

		/// <summary>
		/// Fires off ResponseReceived once a response of a namara dataset request has been received.
		/// </summary>
		/// <param name="datasetId">Dataset identifier. Ex.: 18b854e3-66bd-4a00-afba-8eabfc54f524</param>
		/// <param name="versionId">Version identifier. Ex.: en-0</param>
		/// <param name="parameters">Parameters. Ex.: new Hashtable {{"offset", 0}, {"limit", 100}}</param>
		public T Get<T>(string datasetId, string versionId = "en-0", Hashtable parameters = null) {
			HttpWebRequest request = CreateBasicRequest(datasetId, versionId, parameters);
			request.Method = "GET";
			
			AddHeadersToRequest (((Hashtable)this.namara_options["headers"]), request);
			return HandleWebRequestResponse<T> (request);
		}

        public HttpWebRequest CreateBasicRequest(string datasetId, string versionId, Hashtable parameters)
        {
            string parameters_string = GetParametersAsString(parameters);
            string option_path = this.namara_options["prefix"] + datasetId + "/data/" + versionId;
            if (parameters == null || !parameters.ContainsKey("operation"))
            {
                option_path += "?api_key=" + this.apiKey + "&" + parameters_string;
            } 
            else 
            {
                option_path += "/aggregation?api_key=" + this.apiKey + "&" + parameters_string;
            }
            string url = "http://" + this.namara_options["host"] + option_path;
            if (this.debug) System.Console.WriteLine(url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = this.namara_options["content-type"].ToString();

            return request;
        }
		
		#region constructor
		
		public Namara(string apiKey, bool debug = false) {
			this.apiKey = apiKey;
			this.debug = debug;
			
			this.namara_options = new Hashtable{
				{"host"        , "api.namara.io"    }, 
				{"prefix"      , "/v0/data_sets/"   },
				{"content-type", "application/json" },
				{"headers"     , new Hashtable()    }
			};
		}
		
		#endregion
		
		#region private methods
		
		void AddHeadersToRequest (Hashtable headers, HttpWebRequest request)
		{
			if (headers != null) {
				foreach (DictionaryEntry entry in headers) {
					request.Headers.Add (entry.Key.ToString(), entry.Value.ToString());
				}
			}
		}
		
		static string GetParametersAsString(Hashtable parameters)
		{
			if (parameters == null) return "";
			
			string postData = "";
			foreach (DictionaryEntry entry in parameters)
			{
				postData += entry.Key + "=" + entry.Value + "&";  // We should probably encode the key/value
			}
			return postData;
		}
		
		T HandleWebRequestResponse<T> (HttpWebRequest request)
		{
			string data = "";
			T response = default(T);
			try {
				//request.Proxy = GlobalProxySelection.GetEmptyWebProxy();
				using (HttpWebResponse requestResponse = (HttpWebResponse)request.GetResponse ()) {
					using (Stream stream = requestResponse.GetResponseStream ()) {
						using (StreamReader reader = new StreamReader (stream)) {
							data = reader.ReadToEnd ();
						}
					}
				}
				response = JsonConvert.DeserializeObject<T>(data);
			}
			catch (WebException ex) {
                throw ex;
			}
			catch (System.Exception ex) { // Should not go in there
                throw ex;
			}

			return response;
		}
		
		#endregion
		
		#region declarations
		
		string apiKey;
		bool debug = false;
		Hashtable namara_options;
		
		#endregion
	}
}
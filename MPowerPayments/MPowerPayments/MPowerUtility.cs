//MpowerPayments Utility Portable-lized , substituting this class with MpowerUtility class in your code will enable you to create a portable library for .NET
//Portable class library will allow you to use this code in windows phone and windows 8/8.1 and silverlight applications 
//Changes: Class modified to use HttpClient instead of WebClient
//Author: Jojo Appiah
//Date: 08-10-2013
//Tested with : Windows Phone 8 and Windows 8 app


using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.Net.Http;

namespace MPowerPayments
{
	public class MPowerUtility
	{
		private MPowerSetup setup;

		public MPowerUtility (MPowerSetup setup)
		{
			this.setup = setup;
            
		}
        /// <summary>
        //
        /// </summary>
        /// <param name="url"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
		public JObject HttpPostJson (string url, string payload)
		{

            //seems the MPowerPayments API server does not support compression so this is currently disabled in code..  (re-enable the two lines below to support compression)
           //    HttpClientHandler handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip };
            //HttpClient client = new HttpClient(handler);       
    
		HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue(new System.Net.Http.Headers.ProductHeaderValue("MPowerPortable","1.0")));
			client.DefaultRequestHeaders.Add("mp-master-key", setup.MasterKey);
            		client.DefaultRequestHeaders.Add("mp-private-key", setup.PrivateKey);
            		client.DefaultRequestHeaders.Add("mp-public-key", setup.PublicKey);
            		client.DefaultRequestHeaders.Add("mp-token", setup.Token);
            		client.DefaultRequestHeaders.Add("mp-mode", setup.Mode);
            		client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
		var response = client.PostAsync(new Uri(url), new System.Net.Http.StringContent(payload));
         
                var resp=response.Result.EnsureSuccessStatusCode();
                return JObject.Parse(resp.Content.ReadAsStringAsync().Result);   
           
		}

		public JObject HttpGetRequest(string url)
{
    //seems the MPowerPayments API server does not support compression so this is currently disabled in code..  (re-enable the two lines below to support compression)
    //    HttpClientHandler handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip };
    //HttpClient client = new HttpClient(handler);       
        
			HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue(new System.Net.Http.Headers.ProductHeaderValue("MPowerPortable", "1.0")));
                        client.DefaultRequestHeaders.Add("mp-master-key", setup.MasterKey);
        		client.DefaultRequestHeaders.Add("mp-private-key", setup.PrivateKey);
            		client.DefaultRequestHeaders.Add("mp-public-key", setup.PublicKey);
            		client.DefaultRequestHeaders.Add("mp-token", setup.Token);
            		client.DefaultRequestHeaders.Add("mp-mode", setup.Mode);
            		client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

			var response = client.GetStringAsync(url);
            
            		var rep = response.Result;
			return JObject.Parse(rep);
		}

		public JObject ParseJSON(object jsontext)
		{
			string JsonString = "{}";

			try{
				JsonString = jsontext.ToString();
			}catch(NullReferenceException){
			}

			return JObject.Parse(JsonString);
		}

	}
}


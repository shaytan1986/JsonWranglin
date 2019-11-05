using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JsonWrangling
{
    public class JsonClient
    {
        public const string DEFAULT_LOCALHOST = $"http://localhost:3000/";
        public const int DEFAULT_BATCH_SIZE = 100;

        public JsonClient()
        {

        }



        public async Task<Stream> GetWebResponseStream(int batchSize)
        {
            WebRequest req = WebRequest.Create($"{DEFAULT_LOCALHOST}?{batchSize}");
            WebResponse res = await req.GetResponseAsync();

            return res.GetResponseStream();
        }
        public async Task<Stream> GetWebResponseStream()
        {
            return await GetWebResponseStream(DEFAULT_BATCH_SIZE);
        }

        public async Task<JsonTextReader> GetJsonTextReader()
        {
            using (Stream stream = await GetWebResponseStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    return new JsonTextReader(streamReader);
                }
            }
        }
    }
}

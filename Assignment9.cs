using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Dynamic;
using Amazon.Lambda.Core;
//using Amazon.Lambda.APIGatewayEvents;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Assignment9
{
    public class Assignment9
    {
        public static readonly HttpClient client = new HttpClient();

        public async Task<ExpandoObject> FunctionHandler(string input, ILambdaContext context)
        {
            HttpResponseMessage response = await client.GetAsync("https://api.nytimes.com/svc/books/v3/lists.json");
            response.EnsureSuccessStatusCode();
            object responseBody = await response.Content.ReadAsStringAsync();
            //string jsonText = JsonConvert.SerializeObject(responseBody);
            //var obj = JsonConvert.DeserializeObject<ExpandoObject>(jsonText, new ExpandoObjectConverter());
            //string strCust = JsonConvert.SerializeObject(responseBody, new ExpandoObjectConverter());
            //string output = JsonConvert.SerializeObject(responseBody);
            //return JsonConvert.DeserializeObject<ExpandoObject>(output);
            return (ExpandoObject)responseBody;
            //return obj;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Dynamic;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Assignment9
{
    public class Assignment9
    {
        public static readonly HttpClient client = new HttpClient();

        public async Task<ExpandoObject> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {
            string bookInput = Convert.ToString(input.Body);
            string url = $@"https://api.nytimes.com/svc/books/v3/lists/current/{bookInput}.json?api-key=tZ1EIvr6jm64GpPWcD08RfoH9YWzXhEA";
            string message = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<ExpandoObject>(message);
        }
    }
}

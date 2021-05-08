using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LastAssignment
{

    [Serializable]

    public class LastAssignment
    {

        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        private static string tableName = "LastAssignment";

        public async Task<string> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        { 
            return tableName; 
        }

        public class Item
        {
            public int id; // Primary key
            public string title;
            public string author;
            public int rank;
        }

        static void GetBooks()
        {
            // Connect to and download data from API
            string json = new WebClient().DownloadString("https://api.nytimes.com/svc/books/v3/lists/current/hardcover-fiction.json?api-key=tZ1EIvr6jm64GpPWcD08RfoH9YWzXhEA");

            // Deserialize items in API data for use
            List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);

            // Load created table
            Table LastAssignment = Table.LoadTable(client, tableName);

            // Add items in table from API data
            foreach (var field in items)
            {
                //title
                //author
                //rank
            }
        }
             
        }
    } 
    



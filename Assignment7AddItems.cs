using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.DynamoDBEvents;
using Amazon.Lambda.Core;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]


namespace Assignment7AddItems
{
    public class Item
    {
        //public string itemId; // Primary key
        //public string description;
        public int rating;
        public string type;
        //public string company;
        //public string lastInstanceOfWord;
        //public int count;
        //public float avgNumRatings;
    }
    public class Assignment7AddItems
    {
        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();

        public async Task<List<Item>> FunctionHandler(DynamoDBEvent input, ILambdaContext content)
        {
            Table table = Table.LoadTable(client, "RatingsByType");
            List<Item> items = new List<Item>();
            List<DynamoDBEvent.DynamodbStreamRecord> records = (List<DynamoDBEvent.DynamodbStreamRecord>)input.Records;
            if (records.Count > 0)
            {
                DynamoDBEvent.DynamodbStreamRecord record = records[0];
                if (record.EventName.Equals("INSERT"))
                {
                    Document myDoc = Document.FromAttributeMap(record.Dynamodb.NewImage);
                    Item myItem = JsonConvert.DeserializeObject<Item>(myDoc.ToJson());

                    //int avgNumRating;
                    //avgNumRating = avgNumRating.Average();

                    var request = new UpdateItemRequest
                    {
                        TableName = "RatingsByType",
                        Key = new Dictionary<string, AttributeValue>
                        {
                            { "type", new AttributeValue { S = myItem.type } }

                        },
                        AttributeUpdates = new Dictionary<string, AttributeValueUpdate>()
                        {
                            {
                                "count",
                                new AttributeValueUpdate { Action = "ADD", Value = new AttributeValue { N = "1" } }
                            },
                            //{
                            //    "averageRating",
                            //    new AttributeValueUpdate { Action = "ADD", Value = new AttributeValue { N = "myItem.rating.Average()" } }
                            //},
                        },
                    };
                    await client.UpdateItemAsync(request);
                }               
            }
            return items;
        }
    }
}

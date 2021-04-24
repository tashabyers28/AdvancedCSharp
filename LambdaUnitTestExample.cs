using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaUnitTestExample
{
    public class LambdaUnitTestExample
    {
        public string FunctionHandler(string input, ILambdaContext context)
        {
            return input?.ToUpper();
        }
    }


    public class Car
    {
        public string Make;
        public int Speed;
        public enum Condition
        {
            EXCELLENT,
            GOOD,
            FAIR,
            BAD
        }
        public Condition condition;


        public Car (string Make, Condition condition)
        {
            this.Make = Make;
            this.condition = condition;
        }

        public void Constructor ()
        {
            Car car = new Car(Make, condition)
            {
                Make = "Chevy",
                condition = Condition.EXCELLENT
            };

            car.Speed = 0;
        }

        public Condition Conditions (Condition condition)
        {
            return Condition.EXCELLENT;
        }

        public int CarSpeed(int Speed)
        {
            return Speed;
        }
    }
}

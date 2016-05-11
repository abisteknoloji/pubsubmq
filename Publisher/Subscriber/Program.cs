using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Subscriber
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Subcriber1");
            var conf = new RedisEndpoint() { Host = "127.0.01", Port = 6379 };
            using (IRedisClient client = new RedisClient(conf))
            {
                IRedisSubscription sub = null;
                using (sub = client.CreateSubscription())
                {
                    sub.OnMessage += (channel, message) =>
                    {
                        Console.WriteLine((string)channel);
                        Console.WriteLine((string)message);
                    };
                }
                sub.SubscribeToChannels(new string[] { "pub" });
            }

            Console.ReadLine();
        }
    }

}

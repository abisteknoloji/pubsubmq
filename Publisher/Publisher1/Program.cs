using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace Publisher1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Publisher");
            RedisEndpoint conf = new RedisEndpoint(host: "127.0.0.1", port: 6379);
         
            using (IRedisClient client = new RedisClient(conf))
            {
                for (int i = 1; i<5; i++) { 
                string send = Console.ReadLine();
                    client.PublishMessage("pub", send);
                }
                
            }
            
        }
    }
    
}

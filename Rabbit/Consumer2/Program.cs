using System;
using System.Text;
using RabbitMQ.Client;

namespace Consumer2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CONSUMER2");
            var connectionFactory = new ConnectionFactory();
            IConnection connection = connectionFactory.CreateConnection();
            IModel channel = connection.CreateModel();
            channel.QueueDeclare("New", false, false, false, null);

            while (true)
            {
                BasicGetResult result = channel.BasicGet("RabbitMQ", true);
                if (result != null)
                {
                    string message = Encoding.UTF8.GetString(result.Body);
                    Console.WriteLine(message);
                }
            }

            
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            channel.Close();
            connection.Close();
        }
    }
}

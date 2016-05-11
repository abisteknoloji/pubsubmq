using System.Text;
using RabbitMQ.Client;
using System;

namespace Rabbit
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory();
            IConnection connection = connectionFactory.CreateConnection();
            IModel channel = connection.CreateModel();
            //channel.QueueDeclare("RabbitMQ", false, false, false, null);
            channel.ExchangeDeclare("RabbitFanout", "fanout");
            byte[] message = Encoding.UTF8.GetBytes("Rabbit MQ Örnek");
            channel.BasicPublish("RabbitFanout", "", null, message);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            channel.Close();
            connection.Close();
        }
    }
}
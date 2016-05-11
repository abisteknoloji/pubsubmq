using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CONSUMER1");
            var connectionFactory = new ConnectionFactory();
            IConnection connection = connectionFactory.CreateConnection();
            IModel channel = connection.CreateModel();
            //channel.QueueDeclare("RabbitMQ", false, false, false, null);

            channel.ExchangeDeclare("RabbitFanout", "fanout");
            string queueName = channel.QueueDeclare();
            channel.QueueBind(queueName, "RabbitFanout", "");
            Console.WriteLine("Waiting for messages");

            QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
            channel.BasicConsume(queueName, true, consumer);

            while (true)
            {
                BasicDeliverEventArgs e = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                Console.WriteLine(Encoding.ASCII.GetString(e.Body));
            }

            //while (true)
            //{
            //    BasicGetResult result = channel.BasicGet("RabbitMQ", true);
            //    if (result != null)
            //    {
            //        string message = Encoding.UTF8.GetString(result.Body);
            //        Console.WriteLine(message);
            //    }
            //}            
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            channel.Close();
            connection.Close();
        }
    }
}
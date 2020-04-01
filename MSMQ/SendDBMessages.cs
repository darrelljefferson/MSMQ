using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace MSMQ2
{
    class SendDBMessages
    {
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "SSMessage", durable: false, exclusive: false, autoDelete: false, arguments: null);
                for (int i = 0; i < 10; i++)
                {
                    string message = "Select Server Data Here";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: "SSMessage", basicProperties: null, body: body);
                    Console.WriteLine(i + " [x] Sent {0}", message);
                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }

}
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class RabbitMQService
    {
        public void BasicRabbitReceiver()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: "hello",
                                     noAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        public void BasicRabbitSender()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                var message = "Hello world";

                while (!message.Equals("exit"))
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare("hello", false, false, false, null);

                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish("", "hello", null, body);

                        Trace.TraceInformation($"[x] send {message}");
                    }

                    Console.WriteLine("type \"exit\" to exit");
                    message = Console.ReadLine();
                }
            }
        }
    }
}

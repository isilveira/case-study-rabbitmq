using RabbitMQ.Client;
using RabbitMQ.Messages;
using System;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.Console.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            string op = string.Empty;
            do
            {
                System.Console.Clear();
                System.Console.WriteLine(" - Welcome to the RabbitMQ Publisher! - Exit [X] - \n");

                System.Console.WriteLine("  [01] - Send message to 'Message queue...'");
                System.Console.WriteLine("  [10] - Send message to 'Message queue... x10'");
                System.Console.WriteLine("  [10] - Send message to 'Message queue... x1000'");
                System.Console.WriteLine();
                op = System.Console.ReadLine();

                switch (op)
                {
                    case "01":
                        {
                            System.Console.Write("Message: ");
                            var message = System.Console.ReadLine();
                            System.Console.Write("Criteria: ");
                            var criteria = System.Console.ReadLine();
                            System.Console.Write("Delay: ");
                            var delay = Convert.ToInt32(System.Console.ReadLine());

                            SendMessageOne(message, criteria, delay);

                            System.Console.ReadLine();
                        }
                        break;
                    case "10":
                        {
                            System.Console.Write("Message: ");
                            var message = System.Console.ReadLine();
                            System.Console.Write("Criteria: ");
                            var criteria = System.Console.ReadLine();
                            System.Console.Write("Delay: ");
                            var delay = Convert.ToInt32(System.Console.ReadLine());

                            for (int i = 0; i < 10; i++)
                            {
                                SendMessageOne(message, criteria, delay * i);
                            }

                            System.Console.ReadLine();
                        }
                        break;
                    case "1000":
                        {
                            System.Console.Write("Message: ");
                            var message = System.Console.ReadLine();
                            System.Console.Write("Criteria: ");
                            var criteria = System.Console.ReadLine();
                            System.Console.Write("Delay: ");
                            var delay = Convert.ToInt32(System.Console.ReadLine());

                            for (int i = 0; i < 1000; i++)
                            {
                                SendMessageOne(message, criteria, delay);
                            }

                            System.Console.ReadLine();
                        }
                        break;
                    default:
                        {

                        }
                        break;
                }
            } while (op != "x" && op != "X");
            System.Console.WriteLine("  exiting...");
            System.Console.ReadLine();
        }

        private static void SendMessageOne(string message, string criteria, int delay)
        {
            var factory = new ConnectionFactory()
            {
                //VirtualHost = "",
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: "hello",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    var helloMessage = new HelloMessage { EventDate = DateTime.Now, Message = message, Criteria = criteria, DelaySeconds = delay };
                                        
                    var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(helloMessage));

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                    System.Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }
    }
}

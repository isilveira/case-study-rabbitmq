using RabbitMQ.Client;
using System.Text;

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
                System.Console.WriteLine(" - Welcome to the RabbitMQ Publisher! - Exit [X] - ");

                System.Console.WriteLine("\n\n  [01] - Send message to 'Message queue...'");
                System.Console.WriteLine();
                op = System.Console.ReadLine();

                switch (op)
                {
                    case "01":
                        {
                            System.Console.WriteLine("Option one!");

                            SendMessageOne();

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

        private static void SendMessageOne()
        {
            var factory = new ConnectionFactory()
            {
                //VirtualHost = "",
                HostName = "hornet",
                Port = 1883,
                UserName = "tvudjnyz:tvudjnyz",
                Password = "Qvhh8Gv8qs6BHOGq1EQgO0SPah4d-Ywd",
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: "hello",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    string message = "Hello World! MessageOne";
                    var body = Encoding.UTF8.GetBytes(message);

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

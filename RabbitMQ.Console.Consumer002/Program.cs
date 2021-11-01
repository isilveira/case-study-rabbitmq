using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Messages;
using System;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.Console.Consumer002
{
    class Program
    {
        static void Main(string[] args)
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

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();

                        var helloMessage = JsonSerializer.Deserialize<HelloMessage>(Encoding.UTF8.GetString(body));
                        if (helloMessage.Criteria.Equals("Consumer002") || DateTime.Now < helloMessage.EventDate.AddSeconds(helloMessage.DelaySeconds))
                        {
                            channel.BasicNack(ea.DeliveryTag, false, true);
                            //channel.BasicReject(ea.DeliveryTag, true);
                            System.Console.WriteLine($" [x] - [{ea.DeliveryTag}] - Receive fail {helloMessage.Message}");
                        }
                        else
                        {
                            channel.BasicAck(ea.DeliveryTag, false);
                            System.Console.WriteLine($" [x] - [{ea.DeliveryTag}] - Received {helloMessage.Message} - body: {Encoding.UTF8.GetString(body)}");
                        }
                    };

                    channel.BasicConsume(
                        queue: "hello",
                        autoAck: false,
                        consumerTag: "consumer002",
                        consumer: consumer
                    );

                    System.Console.WriteLine(" Consumer002 - Press [enter] to exit.");

                    System.Console.ReadLine();
                }
            }
        }
    }
}

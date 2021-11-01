using System;

namespace RabbitMQ.Messages
{
    public class HelloMessage
    {
        public DateTime EventDate { get; set; }
        public string Message { get; set; }
        public string Criteria { get; set; }
        public int DelaySeconds { get;set; }
    }
}

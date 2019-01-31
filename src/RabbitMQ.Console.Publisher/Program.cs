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
    }
}

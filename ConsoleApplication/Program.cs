using System;
using System.Diagnostics;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Process.Start(@"C:\RabbitMQ\rabbitmq_server-3.6.1\sbin\rabbitmq-plugins.bat", "enable rabbitmq_management");
            Console.WriteLine("Wait for plugin process to finish and then press any key");
            Console.ReadKey();
            Process.Start(@"C:\RabbitMQ\rabbitmq_server-3.6.1\sbin\rabbitmq-server.bat");
            Console.WriteLine("Wait for broker to start and then press any key");
            Console.ReadKey();

            Process.Start("ReceiverConsole.exe");

            Process.Start("SenderConsole.exe");
        }        
    }
}

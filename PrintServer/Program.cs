using System;

namespace PrintServer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                HttpServer server = new HttpServer(bindAddress: args[0]);
            }
            else if (args.Length == 2)
            {
                HttpServer server = new HttpServer(bindAddress: args[0], port: Convert.ToInt32(args[1]));
            }
            else
            {
                HttpServer server = new HttpServer();
            }

            Console.Read();
        }
    }
}

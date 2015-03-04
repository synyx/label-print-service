using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintServer
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpServer server = new HttpServer();
            Console.Read();
        }
    }
}

namespace PrintServer
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    class WebServer
    {
        private TcpListener tcpListener;
        private int port = 8080;

        public WebServer()
        {
            tcpListener = new TcpListener(IPAddress.Any ,port);
            tcpListener.Start();
            Console.WriteLine("Web server running on port " + port + ".. Press ^C to stop..");
        }
    }
}

using System;
using System.ServiceProcess;
using System.Threading;

namespace PrintServer
{
    partial class LabelPrintService : ServiceBase
    {
        private HttpServer server;

        public LabelPrintService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Thread thread = new Thread(new ThreadStart(() => ThreadStarter(args)));
            thread.Start();
        }

        private void ThreadStarter(string[] args)
        {
            if (args.Length == 1)
            {
                server = new HttpServer(bindAddress: args[0]);
            }
            else if (args.Length == 2)
            {
                server = new HttpServer(bindAddress: args[0], port: Convert.ToInt32(args[1]));
            }
            else
            {
                server = new HttpServer();
            }
            server.start();
        }

        protected override void OnStop()
        {
            if (server != null)
            {
                server.stop();
                server.Dispose();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

            Thread thread = new Thread(new ThreadStart(server.start));
            thread.Start();
        }

        protected override void OnStop()
        {
            server.stop();
        }
    }
}

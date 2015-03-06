using System;
using System.ServiceProcess;

namespace PrintServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new LabelPrintService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}

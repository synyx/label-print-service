namespace PrintServer
{
    using System;
    using System.Net;

    class HttpServer
    {
        private HttpListener httpListener;

        public HttpServer(String bindAddress = "localhost", int port = 8080)
        {
            httpListener = new HttpListener();
            httpListener.Prefixes.Add(string.Format("http://{0}:{1}/print/", bindAddress, port));
            httpListener.Start();
            Console.WriteLine(string.Format("Web server running on {0}, port {1}.. Press ^C to stop..", bindAddress, port));
            lala();
        }

        private void lala()
        {
            IAsyncResult result = httpListener.BeginGetContext(new AsyncCallback(listenerCallback), httpListener);
            result.AsyncWaitHandle.WaitOne();
            lala();
        }

        private void listenerCallback(IAsyncResult result) 
        {
            HttpListenerContext context;

            try
            {
                context = httpListener.EndGetContext(result);

                String requestInput = context.Request.ToString();
                new LabelPrintJob(context.Request.QueryString.Get("label")).print();
                
                byte[] chars = System.Text.Encoding.ASCII.GetBytes("Hello");
                HttpListenerRequest request = context.Request;
                context.Response.OutputStream.Write(chars, 0, chars.Length);
                context.Response.Close();
            }
            catch (HttpListenerException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }
    }
}

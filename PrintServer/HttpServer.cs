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

                String label = context.Request.QueryString.Get("label");

                if (label == null || label.Length == 0)
                {
                    byte[] chars = System.Text.Encoding.ASCII.GetBytes("Gib'n Label an, du Honk");
                    HttpListenerRequest request = context.Request;
                    context.Response.StatusCode = 400;
                    context.Response.OutputStream.Write(chars, 0, chars.Length);
                }
                else
                {
                    String tape = context.Request.QueryString.Get("tape");
                    if (tape == null) { new LabelPrintJob(label).print();}
                    else { new LabelPrintJob(label, tape).print(); }

                    byte[] chars = System.Text.Encoding.ASCII.GetBytes("Penis");
                    HttpListenerRequest request = context.Request;
                    context.Response.OutputStream.Write(chars, 0, chars.Length);
                }
                
                context.Response.Close();
            }
            catch (HttpListenerException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }
    }
}

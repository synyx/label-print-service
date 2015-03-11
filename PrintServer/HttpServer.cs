namespace PrintServer
{
    using System;
    using System.Net;

    class HttpServer : IDisposable
    {
        private HttpListener httpListener;
        private bool _disposed = false;

        public HttpServer(String bindAddress = "*", int port = 80)
        {
            httpListener = new HttpListener();
            httpListener.Prefixes.Add(string.Format("http://{0}:{1}/print/", bindAddress, port));
            httpListener.Start();
        }

        public void start()
        {
            awaitRequest();
        }

        public void stop()
        {
            httpListener.Stop();
        }

        private void awaitRequest()
        {
            IAsyncResult result = httpListener.BeginGetContext(new AsyncCallback(processRequest), httpListener);
            result.AsyncWaitHandle.WaitOne();
            awaitRequest();
        }

        private void processRequest(IAsyncResult result)
        {
            HttpListenerContext context;

            try
            {
                context = httpListener.EndGetContext(result);

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {

                    httpListener.Close();
                    httpListener = null;
                }
                _disposed = true;
            }
        }
    }
}

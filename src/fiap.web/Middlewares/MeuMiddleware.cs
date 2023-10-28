using Newtonsoft.Json;
using Serilog;
using System.Text;

namespace fiap.web.Middlewares
{
    public class MeuMiddleware
    {
        private RequestDelegate _next;

        public MeuMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = await FormatRequest(context.Request);

            var log = new LoggerConfiguration()
                .WriteTo.Logentries("771f777c-2319-404a-9b5a-9241fb0d3344")
                .CreateLogger();
            log.Information(request);

            context.Request.Body.Position = 0;


            await _next.Invoke(context);
           
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            

            //request.EnableRewind();
            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            
            

            var messageObjToLog = new { scheme = request.Scheme, host = request.Host, path = request.Path, queryString = request.Query, requestBody = bodyAsText };

            return JsonConvert.SerializeObject(messageObjToLog);
        }
    }
}

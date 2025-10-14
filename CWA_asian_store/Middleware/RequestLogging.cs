namespace CWA_asian_store.Middleware
{
    public class RequestLogging
    {
        private readonly RequestDelegate _next;

        public RequestLogging (RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"[{DateTime.Now}] {context.Request.Method} {context.Request.Path}");
            await _next(context);
        }
    }
}

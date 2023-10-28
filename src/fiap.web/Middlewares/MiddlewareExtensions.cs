namespace fiap.web.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMeuLogger(this IApplicationBuilder builder ) 
        { 
            return builder.UseMiddleware<MeuMiddleware>();
        }
        
    }
}

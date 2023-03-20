using RepositoryAplication.Tools;
using System.Net;
using System.Text.Json;

namespace API
{
    public class ExcaptionMidleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExcaptionMidleware> logger;
        private readonly IHostEnvironment env;

        public ExcaptionMidleware(RequestDelegate next,
            ILogger<ExcaptionMidleware> logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // if the bit of midleware is ok
                await next(context);
            }
            //if we have an error we did not handle 
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = env.IsDevelopment()
                    ? new AppExeption(context.Response.StatusCode
                    , ex.Message, ex.StackTrace?.ToString())
                    : new AppExeption(context.Response.StatusCode, "internal server error");

                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                var json= JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}

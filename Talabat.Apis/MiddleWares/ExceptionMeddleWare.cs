using System.Linq.Expressions;
using System.Net;
using System.Text.Json;
using Talabat.Apis.Errors;

namespace Talabat.Apis.MiddleWares
{
    public class ExceptionMeddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMeddleWare> logger;
        private readonly IHostEnvironment env;

        public ExceptionMeddleWare(RequestDelegate next , ILogger<ExceptionMeddleWare> logger,IHostEnvironment env) 
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext httpContent)
        {
            try
            {
               await next.Invoke(httpContent);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                httpContent.Response.ContentType = "application/json";
                httpContent.Response.StatusCode =(int) HttpStatusCode.InternalServerError;

                var responce = env.IsDevelopment() ?
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                var json = JsonSerializer.Serialize(responce);
                await httpContent.Response.WriteAsync(json);

            }
           
        }
    }
}

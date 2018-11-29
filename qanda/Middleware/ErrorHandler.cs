using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Qanda.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Qanda.Api.Middleware
{

    public static class ErrorHandler
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
                app.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";
                        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (contextFeature != null)
                        {
                            context.Response.StatusCode = (int)GetStatusCode(contextFeature.Error);
                            await context.Response.WriteAsync(new ErrorDetails(contextFeature.Error.Message).ToString());
                        }
                    });
        }

        private static HttpStatusCode GetStatusCode(Exception exception)
        {
            var type = exception.GetType();
            if (type == typeof(AggregateException) && exception.InnerException != null)
            {
                // Unwrap async task exceptions
                type = exception.InnerException.GetType();
            }
            if (statusCodeMap.ContainsKey(type))
            {
                return statusCodeMap[type];
            }
            return HttpStatusCode.InternalServerError;
          
        }

        private static Dictionary<Type, HttpStatusCode> statusCodeMap = new Dictionary<Type, HttpStatusCode> {
            { typeof(NotFoundException), HttpStatusCode.NotFound }
        };
    }

    public class ErrorDetails
    {
        public ErrorDetails(string message)
        {
            Message = message;
        }
        public string Message { get;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

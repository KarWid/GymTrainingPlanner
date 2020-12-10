using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Npgsql;

namespace GymTrainingPlanner.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (NpgsqlException sqlException)
            {
                await HandleExceptionAsync(httpContext, sqlException);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception);

            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var result = JsonConvert.SerializeObject(new
            {
                Type = "General Exception",
                Exception = new
                {
                    Message = ex.Message,
                    Inner = ex.InnerException
                }
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            return context.Response.WriteAsync(result);
        }

        private static Task HandleExceptionAsync(HttpContext context, NpgsqlException ex)
        {
            var result = JsonConvert.SerializeObject(new
            {
                Type = "General Exception",
                Exception = new
                {
                    Message = ex.Message,
                    Inner = ex.InnerException
                }
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            return context.Response.WriteAsync(result);
        }
    }

    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}

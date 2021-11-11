namespace GymTrainingPlanner.Api.Middleware
{
    using System;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Npgsql;
    using GymTrainingPlanner.Common.Exceptions;
    using GymTrainingPlanner.Common.Models.Responses;
    using GymTrainingPlanner.Common.Resources;
    using GymTrainingPlanner.Common.Services;

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            _loggerService.LogError(httpContext, ex);

            var response = httpContext.Response;
            response.ContentType = "application/json";

            ApiResponse apiResponseResult = new BussinessLogicErrorApiResponse(GeneralResource.Something_Went_Wrong);

            switch (ex)
            {
                case NotFoundApiManagerException notFoundApiManagerException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    apiResponseResult = new NotFoundApiResponse(notFoundApiManagerException.Message);
                    break;
                case ApiManagerException apiManagerException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    apiResponseResult = new BussinessLogicErrorApiResponse(apiManagerException.Message);
                    break;
                case IdentityApiException identityApiException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    apiResponseResult = new BussinessLogicErrorApiResponse(identityApiException.GetErrorMessages());
                    break;
                case NpgsqlException npgsqlException:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    apiResponseResult = new BussinessLogicErrorApiResponse(npgsqlException.Message);
                    break;
                case BaseApiException baseApiException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    apiResponseResult = new BussinessLogicErrorApiResponse(baseApiException.Message);
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    apiResponseResult = new BussinessLogicErrorApiResponse(GeneralResource.Something_Went_Wrong);
                    break;
            }

            var result = JsonSerializer.Serialize(apiResponseResult);
            await response.WriteAsync(result);
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

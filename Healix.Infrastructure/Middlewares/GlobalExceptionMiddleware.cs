using System.Net;
using FluentValidation;
using Healix.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Healix.Infrastructure.Middlewares
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleGlobalException(context, ex);
            }
        }

        private Task HandleGlobalException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex switch
            {
                ValidationException => (int)HttpStatusCode.BadRequest,
                UnauthorizedException x => x.ErrorCode,
                BadRequestException x => x.ErrorCode,
                NotFoundException x => x.ErrorCode,
                ConflictException x => x.ErrorCode,
                _ => (int)HttpStatusCode.InternalServerError,
            };

            Console.WriteLine(ex);

            var result = JsonConvert.SerializeObject(
                new
                {
                    error = ex.Message,
                    details = ex is ValidationException validateEx
                        ? validateEx.Errors.Select(e => new { e.ErrorMessage, e.ErrorCode })
                        : null,
                }
            );

            return context.Response.WriteAsync(result);
        }
    }
}

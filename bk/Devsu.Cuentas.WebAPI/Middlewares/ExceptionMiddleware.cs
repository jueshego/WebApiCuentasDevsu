using Devsu.Cuentas.Aplicacion.DTO.Responses;
using Devsu.Cuentas.Aplicacion.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Devsu.Cuentas.WebAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ha ocurrido un evento inesperado: {ex.Message} \n en: {ex.StackTrace}");

                await HandleError(context, ex);
            }
        }

        private async Task HandleError(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var responseModel = new GenericResponse<string>()
            {
                Succeeded = false,
                Message = ex?.Message
            };

            switch (ex)
            {
                case BusinessException e:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case ArgumentNullException e:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case KeyNotFoundException e:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var objSerializado = JsonConvert.SerializeObject(responseModel);

            await context.Response.WriteAsync(objSerializado);
        }
    }
}

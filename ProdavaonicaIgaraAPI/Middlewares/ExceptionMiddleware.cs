
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using ProdavaonicaIgaraAPI.Data.Exceptions;
using System.Net;

namespace ProdavaonicaIgaraAPI.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        #region ctor
        public ExceptionMiddleware() {}
        #endregion

        #region helper
        private class ErrorDetails
        {
            public string? ErrorType { get; set; }

            public string? Message { get; set; }
        }
        #endregion

        #region methods
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {

                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            context.Response.ContentType = "application/json";

            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            var errorDetails = new ErrorDetails
            {
                ErrorType = "Internal Server Error",
                Message = e.Message
            };

            switch (e)
            {
                case UniqueConstraint:
                    statusCode = HttpStatusCode.Conflict;
                    errorDetails.ErrorType = "Conflict";
                    break;
                case NotEnoughQuantity:
                    statusCode = HttpStatusCode.NotAcceptable;
                    errorDetails.ErrorType = "Not Acceptable";
                    break;
            }

            string response = JsonConvert.SerializeObject(errorDetails);
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(response);

        }

        #endregion
        
    }
}

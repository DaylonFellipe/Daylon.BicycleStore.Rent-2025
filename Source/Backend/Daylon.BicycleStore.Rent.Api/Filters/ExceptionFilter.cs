using Daylon.BicycleStore.Rent.Communication.Responses;
using Daylon.BicycleStore.Rent.Exceptions;
using Daylon.BicycleStore.Rent.Exceptions.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace Daylon.BicycleStore.Rent.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BicycleStoreException)
                HandleBicycleStoreException(context);
            else
                ThrowUnknowException(context);
        }

        private void HandleBicycleStoreException(ExceptionContext context)
        {
            if (context.Exception is ErrorOnValidationExeption)
            {
                var exception = context.Exception as ErrorOnValidationExeption;

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest; // 400
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception.ErrorMessages));
            }
        }

        private void ThrowUnknowException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500
            context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOW_ERROR));
        }
    }
}

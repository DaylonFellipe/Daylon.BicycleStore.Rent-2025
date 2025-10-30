using Daylon.BicycleStore.Rent.Communication.Responses;
using Daylon.BicycleStore.Rent.Exceptions.ExceptionBase;
using Daylon.BicycleStore.Rent.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ErrorOnValidationExeption validationEx:
                HandleValidationException(context, validationEx);
                break;

            case BicycleStoreException storeEx:
                HandleBicycleStoreException(context, storeEx);
                break;

            default:
                HandleUnknownException(context);
                break;
        }
    }

    private void HandleValidationException(ExceptionContext context, ErrorOnValidationExeption ex)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new BadRequestObjectResult(new ResponseErrorJson(ex.ErrorMessages));
        context.ExceptionHandled = true;
    }

    private void HandleBicycleStoreException(ExceptionContext context, BicycleStoreException ex)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ResponseErrorJson(ex.Message));
        context.ExceptionHandled = true;
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOW_ERROR));
        context.ExceptionHandled = true;
    }
}

using Financas.Communication.Responses;
using Financas.Exeption;
using Financas.Exeption.ExeptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Financas.filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is FinancasExeption)
        {
            HandleProjectExeption(context);
        }
        else
        {
            ThrowUnkowError(context);
        }
    }

    private void HandleProjectExeption(ExceptionContext context)
    {
        if(context.Exception is ErrorOnValidationException)
        {
            var ex = context.Exception as ErrorOnValidationException;

           
            var errorResponse = new ResponseErrorJson(ex.Errors);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(errorResponse);
        }
    }

    private void ThrowUnkowError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMassages.ERRO_DESCONHECIDO);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}

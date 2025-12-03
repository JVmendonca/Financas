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

        var financasExeption = context.Exception as FinancasExeption;
        var errorResponse = new ResponseErrorJson(financasExeption!.GetErros());

        context.HttpContext.Response.StatusCode = financasExeption.StatusCode;
        context.Result = new ObjectResult(errorResponse);

    }

    private void ThrowUnkowError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMassages.ERRO_DESCONHECIDO);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}

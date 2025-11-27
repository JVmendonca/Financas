using Financas.Application.UseCases.Dispesas.GetAll;
using Financas.Application.UseCases.Dispesas.GetById;
using Financas.Application.UseCases.Dispesas.Register;
using Financas.Communication.Request;
using Financas.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Financas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DispesasController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDespesaJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterDispensaUseCase useCase,
        [FromBody] RequestDispesaJson request)  
    {

        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseDespesasjson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllExpensesUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.Despesas.Count != 0)
            return Ok(response);

        return NoContent();
     }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseDespesaIdJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetById(
        [FromServices] IGetDespesasByIdUseCases useCase,
        [FromRoute] long id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

}

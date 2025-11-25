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
    [ProducesResponseType(typeof(ResponseDispesaJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterDispensaUseCase useCase,
        [FromBody] RequestDispesaJson request)  
    {

        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}

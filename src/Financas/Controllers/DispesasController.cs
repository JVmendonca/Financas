using Financas.Application.UseCases.Dispesas.Register;
using Financas.Communication.Request;
using Microsoft.AspNetCore.Mvc;

namespace Financas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DispesasController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterDispensaUseCase useCase,
        [FromBody] RequestDispesaJson request)  
    {

        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}

using Financas.Application.UseCases.Dispesas.Register;
using Financas.Communication.Request;
using Microsoft.AspNetCore.Mvc;

namespace Financas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DispesasController : ControllerBase
{
    [HttpPost]

    public IActionResult Register([FromBody] RequestDispesaJson request)
    {
        var useCase = new RegisterDispesasUseCase();

        var response = useCase.Execute(request);

        return Created(string.Empty, response);
    }
}

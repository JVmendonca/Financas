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

    public IActionResult Register([FromBody] RequestDispesaJson request)
    {
        try
        {
            var useCase = new RegisterDispesasUseCase();

            var response = useCase.Execute(request);
            
            return Created(string.Empty, response);
        }
        catch (ArgumentException ex)
        {
            var errorResponse = new ResponseErrorJson(ex.Message);

            return BadRequest(errorResponse);
        }
        catch
        {
            var errorResponse = new ResponseErrorJson("erro desconhecido");

            return StatusCode(500);
        }
    }
}

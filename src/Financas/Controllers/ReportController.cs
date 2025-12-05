using Financas.Application.UseCases.Dispesas.Reports.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Financas.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExel(
        [FromServices] IGenereteDespesaReportExcelUseCase useCase,
        [FromHeader] DateOnly Mes)
    {
        byte[] file = await useCase.Execute(Mes);

        if (file.Length > 0)
             return File(file, MediaTypeNames.Application.Octet, "report.xlsx");

        return NoContent();

    }
}

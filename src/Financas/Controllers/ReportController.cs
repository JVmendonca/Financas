using Financas.Application.UseCases.Despesas.Reports.Excel;
using Financas.Application.UseCases.Despesas.Reports.Pdf;
using Financas.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Financas.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Regras.ADMIN )]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExel(
        [FromServices] IGenereteDespesaReportExcelUseCase useCase,
        [FromQuery] DateOnly Mes)
    {
        byte[] file = await useCase.Execute(Mes);

        if (file.Length > 0)
             return File(file, MediaTypeNames.Application.Octet, "report.xlsx");

        return NoContent();

    }


    [HttpGet("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPdf(
        [FromServices] IGenereteDespesasReportPdfUseCase useCase,
        [FromQuery] DateOnly Mes)
    {
        byte[] file = await useCase.Execute(Mes);
        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, "report.pdf");

        return NoContent();
    }

}

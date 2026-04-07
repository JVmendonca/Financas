using FluentAssertions;
using System.Net;
using System.Net.Mime;

namespace WebApi.Test.Despesas.Reports;
public class GenerateDespesasReportTest : FinancasClassFixture
{
    private const string METHOD = "api/Report";

    private readonly string _adminToken;
    private readonly string _TeamMemberToken;
    private readonly DateTime _despesaDate;

    public GenerateDespesasReportTest(CustomWepApplicationFactory wepApplicationFactory) : base(wepApplicationFactory)
    {
        _adminToken = wepApplicationFactory.User_Admin.GetToken();
        _TeamMemberToken = wepApplicationFactory.User_Team_Member.GetToken();
        _despesaDate = wepApplicationFactory.Despesa_User_Admin.GetDate();
    }

    
    [Fact]
    public async Task Success_pdf()
    {
        var mes = _despesaDate.ToString("yyyy-MM");
        var result = await DoGet(requestUri: $"{METHOD}/pdf?mes={mes}", token: _adminToken);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Content.Headers.ContentType.Should().NotBeNull();
        result.Content.Headers.ContentType!.MediaType.Should().Be(MediaTypeNames.Application.Pdf);
    }

    [Fact]
    public async Task Success_Excecl()
    {
        var mes = _despesaDate.ToString("yyyy-MM");
        var result = await DoGet(requestUri: $"{METHOD}/excel?mes={mes}", token: _adminToken);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Content.Headers.ContentType.Should().NotBeNull();
        result.Content.Headers.ContentType!.MediaType.Should().Be(MediaTypeNames.Application.Octet);
    }


    [Fact]
    public async Task Error_Forbidden_User_Not_Allowed_Excel()
     {
        var result = await DoGet(requestUri: $"{METHOD}/excel?mes={_despesaDate:Y}", token: _TeamMemberToken);
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }


    [Fact]
    public async Task Error_Forbidden_User_Not_Allowed_Pdf()
    {
        var result = await DoGet(requestUri: $"{METHOD}/pdf?mes={_despesaDate:Y}", token: _TeamMemberToken);
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}
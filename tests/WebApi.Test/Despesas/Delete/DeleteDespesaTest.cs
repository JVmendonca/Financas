using Financas.Exeption;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Despesas.Delete;
public class DeleteDespesaTest : FinancasClassFixture
{
    private const string METHOD = "api/Despesas";

    private readonly string _token;
    private readonly long _despesaId;

    public DeleteDespesaTest(CustomWepApplicationFactory wepApplicationFactory) : base(wepApplicationFactory)
    {
        _token = wepApplicationFactory.User_Team_Member.GetToken();
        _despesaId = wepApplicationFactory.Despesa_Member_Team.GetId();
    }

    [Fact]
    public async Task Sucess()
    {
        var result = await DoDelete(requestUri: $"{METHOD}/{_despesaId}", token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);

        result = await DoGet(requestUri: $"{METHOD}/{_despesaId}", token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Despesa_Not_Foud(string culture)
    {
        var result = await DoDelete(requestUri: $"{METHOD}/{1000}", token: _token, culture: culture);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMassages.ResourceManager.GetString("DESPESA_NAO_ENCONTRADA", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(e => e.GetString()!.Equals(expectedMessage));
    }
}

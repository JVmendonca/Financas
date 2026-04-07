using CommonTestUtilities.Requests;
using Financas.Exeption;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Despesas.Update;
public class UpdateDespesaTest : FinancasClassFixture
{
    private const string METHOD = "api/Despesas";

    private readonly string _token;
    private readonly long _despesaId;

    public UpdateDespesaTest(CustomWepApplicationFactory wepApplicationFactory) : base(wepApplicationFactory)
    {
        _token = wepApplicationFactory.User_Team_Member.GetToken();
        _despesaId = wepApplicationFactory.Despesa_Member_Team.GetId();
    }
    
    [Fact]
    public async Task Success()
    {
        var request = RequestDespesaJsonBuilder.Build();

        var result = await DoPut(requestUri: $"{METHOD}/{_despesaId}", request: request, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]

    public async Task Error_Title_Empty(string culture)
    {
        var request = RequestDespesaJsonBuilder.Build();
        request.Titulo = string.Empty;

        var result = await DoPut(requestUri: $"{METHOD}/{_despesaId}", request: request, token: _token, culture: culture);
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expecteMessage = ResourceErrorMassages.ResourceManager.GetString("TITULO_OBRIGATORIO", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expecteMessage));
    }


    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]

    public async Task Error_Despesa_Not_Found(string culture)
    {
        var request = RequestDespesaJsonBuilder.Build();

        var result = await DoPut(requestUri: $"{METHOD}/1000", request: request, token: _token, culture: culture);
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expecteMessage = ResourceErrorMassages.ResourceManager.GetString("DESPESA_NAO_ENCONTRADA", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expecteMessage));
    }

}

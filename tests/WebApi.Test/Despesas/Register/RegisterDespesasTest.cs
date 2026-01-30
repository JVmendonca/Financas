using CommonTestUtilities.Requests;
using Financas.Exeption;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Despesas.Register;
public class RegisterDespesasTest : FinancasClassFixture
{
    private const string METHOD = "api/Dispesas";

    private readonly string _token;

    public RegisterDespesasTest(CustomWepApplicationFactory wepApplicationFactory) : base(wepApplicationFactory)
    {
        _token = wepApplicationFactory.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestDispesaJsonBuilder.Build();


        var result = await DoPost(requestUri: METHOD, request: request, token: _token);
        
        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("titulo").GetString().Should().Be(request.Titulo);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Title_Empty(string culture)
    {
        var request = RequestDispesaJsonBuilder.Build();
        request.Titulo = string.Empty;

        var result = await DoPost(requestUri: METHOD, request: request, token: _token, culture: culture);

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMassages.ResourceManager.GetString("TITULO_OBRIGATORIO", new CultureInfo(culture));
        
        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}

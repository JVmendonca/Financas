using FluentAssertions;
using System.Text.Json;

namespace WebApi.Test.Despesas.GetAll;
public class GetAllDespesasTest : FinancasClassFixture 
{
    private const string METHOD = "api/Dispesas";

    private readonly string _token;

    public GetAllDespesasTest(CustomWepApplicationFactory wepApplicationFactory) : base(wepApplicationFactory)
    {
        _token = wepApplicationFactory.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet(requestUri: METHOD, token: _token);

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("despesas").EnumerateArray().Should().NotBeNullOrEmpty();
    }

}

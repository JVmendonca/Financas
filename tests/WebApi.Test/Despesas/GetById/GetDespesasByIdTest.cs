using Financas.Communication.Enuns;
using FluentAssertions;
using System.Net;
using System.Text.Json;


namespace WebApi.Test.Despesas.GetById;
public class GetDespesasByIdTest : FinancasClassFixture
{
    private const string METHOD = "api/Dispesas";

    private readonly string _token;
    private readonly long _despesaId;

    public GetDespesasByIdTest(CustomWepApplicationFactory wepApplicationFactory) : base(wepApplicationFactory)
    {
       _token = wepApplicationFactory.User_Team_Member.GetToken();
       _despesaId = wepApplicationFactory.Despesa_Member_Team.GetId();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet(requestUri: $"{METHOD}/{_despesaId}", token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();
        
        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("id").GetInt64().Should().Be(_despesaId);
        response.RootElement.GetProperty("titulo").GetString().Should().NotBeNullOrWhiteSpace();
        response.RootElement.GetProperty("descricao").GetString().Should().NotBeNullOrWhiteSpace();
        response.RootElement.GetProperty("data").GetDateTime().Should().NotBeAfter(DateTime.Today);
        response.RootElement.GetProperty("valor").GetDecimal().Should().BeGreaterThan(0);
        response.RootElement.GetProperty("tags").EnumerateArray().Should().NotBeNullOrEmpty();
        
        var paymantType = response.RootElement.GetProperty("pagamento").GetInt32();
        Enum.IsDefined(typeof(PaymentType), paymantType).Should().BeTrue();
    }

    //[Theory]
    //[ClassData(typeof(CultureInlineDataTest))]
    //public async Task Error_Not_Found()
    //{

    //}
}

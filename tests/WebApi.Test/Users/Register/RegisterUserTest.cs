using CommonTestUtilities.Requests;
using Financas.Exeption;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Users.Register;
public class RegisterUserTest : FinancasClassFixture
{
    private const string METHOD = "api/User";

    private readonly HttpClient _httpClient;

    public RegisterUserTest(CustomWepApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
    }
     
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
       
        var result = await DoPost(METHOD, request);

        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("nome").GetString().Should().Be(request.Nome);
        response.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Empty_Name(string culture) 
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Nome = string.Empty;

        var result = await DoPost(requestUri: METHOD, request: request, culture: culture);

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var erros = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMassages.ResourceManager.GetString("NOME_VAZIO", new CultureInfo(culture));

        erros.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));

    }
}

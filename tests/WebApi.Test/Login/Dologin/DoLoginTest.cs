using CommonTestUtilities.Requests;
using Financas.Communication.Request;
using Financas.Exeption;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Login.Dologin;
public class DoLoginTest : FinancasClassFixture
{
    private const string METHOD = "/api/Login";

    
    private readonly string _email;
    private readonly string _nome;
    private readonly string _senha;

    public DoLoginTest(CustomWepApplicationFactory wepApplicationFactory) : base(wepApplicationFactory)
    {
        _email = wepApplicationFactory.GetEmail();
        _nome = wepApplicationFactory.GetName();
        _senha = wepApplicationFactory.GetSenha();
    }

    [Fact]
    public async Task Success()
    {
        var request = new RequestLoginJson
        {
            Email = _email,
            Senha = _senha
        };

        var response = await DoPost(requestUri : METHOD,request: request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("nome").GetString().Should().Be(_nome);
        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();

    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Login(string culture)
    {
        var request = RequestLoginJsonBuilder.Build();

        
        var response = await DoPost(requestUri: METHOD, request: request, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMassages.ResourceManager.GetString("EMAIL_OU_SENHA_ERRADO", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(c => c.GetString()!.Equals(expectedMessage));
    }
}
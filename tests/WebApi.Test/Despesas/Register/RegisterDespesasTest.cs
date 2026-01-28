using CommonTestUtilities.Requests;
using Financas.Exeption;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Despesas.Register;
public class RegisterDespesasTest : IClassFixture<CustomWepApplicationFactory>
{
    private const string METHOD = "api/Dispesas";

    private readonly HttpClient _httpClient;
    private readonly string _token;

    public RegisterDespesasTest(CustomWepApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();

        _token = factory.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestDispesaJsonBuilder.Build();

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var result = await _httpClient.PostAsJsonAsync(METHOD, request);
        
        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("titulo").GetString().Should().Be(request.Titulo);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Title_Empty(string cultureInfo)
    {
        var request = RequestDispesaJsonBuilder.Build();
        request.Titulo = string.Empty;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(cultureInfo));

        var result = await _httpClient.PostAsJsonAsync(METHOD, request);

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMassages.ResourceManager.GetString("TITULO_OBRIGATORIO", new CultureInfo(cultureInfo));
        
        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}

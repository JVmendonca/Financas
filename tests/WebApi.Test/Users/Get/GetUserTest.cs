using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Users.Get;
public class GetUserTest : FinancasClassFixture
{
    private const string METHOD = "/api/User";

    private readonly string _token;
    private readonly string _userNome;
    private readonly string _UserEmail;

    public GetUserTest(CustomWepApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
        _userNome = webApplicationFactory.User_Team_Member.GetName();
        _UserEmail = webApplicationFactory.User_Team_Member.GetEmail();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet(METHOD, _token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("nome").GetString().Should().Be(_userNome);
        response.RootElement.GetProperty("email").GetString().Should().Be(_UserEmail);
    }
}

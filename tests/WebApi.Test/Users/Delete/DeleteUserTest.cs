using Financas.Communication.Request;
using FluentAssertions;
using System.Net;

namespace WebApi.Test.Users.Delete;
public class DeleteUserTest : FinancasClassFixture
{
    private const string METHOD = "api/User";

    private readonly string _token;
    private readonly string _email;
    private readonly string _senha;

    public DeleteUserTest(CustomWepApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
        _email = webApplicationFactory.User_Team_Member.GetEmail();
        _senha = webApplicationFactory.User_Team_Member.GetSenha();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoDelete(METHOD, _token);
        
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var request = new RequestLoginJson
        {
            Email = _email,
            Senha = _senha
        };

        result = await DoPost(requestUri: "api/Login", request: request);
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}

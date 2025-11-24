using Financas.Application.UseCases.Dispesas.Register;
using Microsoft.Extensions.DependencyInjection;

namespace Financas.Application;
public static class DependecyInjectionExtension
{
    public static void AddApplicationDependecies(this IServiceCollection services)
    {
        services.AddScoped<IRegisterDispensaUseCase, RegisterDispesasUseCase>();
    }
}

using Financas.Application.AutoMapper;
using Financas.Application.UseCases.Despesas.Delete;
using Financas.Application.UseCases.Despesas.GetAll;
using Financas.Application.UseCases.Despesas.GetById;
using Financas.Application.UseCases.Despesas.Register;
using Financas.Application.UseCases.Despesas.Reports.Excel;
using Financas.Application.UseCases.Despesas.Reports.Pdf;
using Financas.Application.UseCases.Despesas.Update;
using Financas.Application.UseCases.Login;
using Financas.Application.UseCases.User.Delete;
using Financas.Application.UseCases.User.Get;
using Financas.Application.UseCases.User.Password;
using Financas.Application.UseCases.User.Register;
using Financas.Application.UseCases.User.Update;
using Microsoft.Extensions.DependencyInjection;


namespace Financas.Application;
public static class DependecyInjectionExtension
{
    public static void AddApplicationDependecies(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterDespesaUseCase, RegisterDespesasUseCase>();
        services.AddScoped<IGetAllExpensesUseCase, GetAllExpensesUseCase>();
        services.AddScoped<IGetDespesasByIdUseCases, GetDespesasByIdUseCases>();
        services.AddScoped<IDeleteDespesaUseCase, DeleteDespesaUseCase>();
        services.AddScoped<IUpdateDespesaUseCase, UpdateDespesaUseCase>();
        services.AddScoped<IGenereteDespesaReportExcelUseCase, GenereteDespesaReportExcelUseCase>();
        services.AddScoped<IGenereteDespesasReportPdfUseCase, GenereteDespesasReportPdfUseCase>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IGetUserUseCase, GetUserUseCase>();
        services.AddScoped<IUpdateUserUserCase, UpdateUserUserCase>();
        services.AddScoped<IUpdatePasswordUseCase, UpdatePasswordUseCase>();
        services.AddScoped<IDeleteProfileUseCase, DeleteProfileUseCase>();
    }
}

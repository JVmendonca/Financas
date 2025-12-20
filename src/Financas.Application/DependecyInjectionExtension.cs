using Financas.Application.AutoMapper;
using Financas.Application.UseCases.Dispesas.Delete;
using Financas.Application.UseCases.Dispesas.GetAll;
using Financas.Application.UseCases.Dispesas.GetById;
using Financas.Application.UseCases.Dispesas.Register;
using Financas.Application.UseCases.Dispesas.Reports.Excel;
using Financas.Application.UseCases.Dispesas.Reports.Pdf;
using Financas.Application.UseCases.Dispesas.Update;
using Financas.Application.UseCases.User.Register;
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
        services.AddScoped<IRegisterDispensaUseCase, RegisterDispesasUseCase>();
        services.AddScoped<IGetAllExpensesUseCase, GetAllExpensesUseCase>();
        services.AddScoped<IGetDespesasByIdUseCases, GetDespesasByIdUseCases>();
        services.AddScoped<IDeleteDespesaUseCase, DeleteDespesaUseCase>();
        services.AddScoped<IUpdateDespesaUseCase, UpdateDespesaUseCase>();
        services.AddScoped<IGenereteDespesaReportExcelUseCase, GenereteDespesaReportExcelUseCase>();
        services.AddScoped<IGenereteDespesasReportPdfUseCase, GenereteDespesasReportPdfUseCase>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }
}

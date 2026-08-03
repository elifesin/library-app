using Domain.Repositories;
using DataEF.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataEF;

public static class DataEFModule
{
    public static IServiceCollection AddDataEFLayerServices(this IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<IPublisherRepository, PublisherRepository>();

        return services;
    }
}
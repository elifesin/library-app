using Domain.Repositories;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class DataModule
{
    public static IServiceCollection AddDataLayerServices(this IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IPublisherRepository, PublisherRepository>();
        
        services.AddScoped<DbConnectionFactory>();

        return services;
    }
}
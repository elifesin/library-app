using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class DataModules
{
    public static IServiceCollection AddDataLayerServices(this IServiceCollection services)
    {
        services.AddScoped<CategoryRepository>();
        services.AddScoped<LoanRepository>();
        services.AddScoped<AuthorRepository>();
        services.AddScoped<BookRepository>();
        services.AddScoped<MemberRepository>();
        services.AddScoped<PublisherRepository>();
        services.AddScoped<DbConnectionFactory>();

        return services;
    }
}
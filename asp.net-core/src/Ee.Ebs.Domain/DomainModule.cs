using Ee.Ebs.Domain.Authors;
using Microsoft.Extensions.DependencyInjection;

namespace Ee.Ebs.Domain;

public static class DomainModule
{
    public static IServiceCollection AddDomainModuleServices(this IServiceCollection services)
    {
        services.AddTransient<AuthorDomainService>();
        
        return services;
    }
}
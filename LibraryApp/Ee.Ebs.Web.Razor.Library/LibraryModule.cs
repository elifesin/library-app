using Microsoft.Extensions.DependencyInjection;

namespace Ee.Ebs.Web.Razor.Library;

public static class LibraryModule
{
    public static IServiceCollection AddLibraryModuleServices(this IServiceCollection services)
    {
        // services.AddScoped<IFoo, Foo>();

        return services;
    }
}
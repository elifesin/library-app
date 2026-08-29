using Ee.Ebs.Web.Razor.Library.Areas.Library.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace Ee.Ebs.Web.Razor.Library;

public static class LibraryModule
{
    public static IServiceCollection AddLibraryModuleServices(this IServiceCollection services)
    {
        
        // var menu = new EbsMenu();
        // menu.Items.Add(new EbsMenuItem
        // {
        //     Name = LibraryPageNames.AuthorsIndex,
        //     Url = "/Authors/index",
        // });
        //
        // services.AddSingleton<EbsMenu>(menu);
        //
        // return services;
        return services;
    }
}
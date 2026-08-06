using Ee.Ebs.Application.Authors;
using Ee.Ebs.Application.Books;
using Ee.Ebs.Application.Categories;
using Ee.Ebs.Application.Loans;
using Ee.Ebs.Application.Members;
using Ee.Ebs.Application.Publishers;
using Microsoft.Extensions.DependencyInjection;

namespace Ee.Ebs.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IPublisherService, PublisherService>();
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<ILoanService, LoanService>();
        
        return services;
    }
}
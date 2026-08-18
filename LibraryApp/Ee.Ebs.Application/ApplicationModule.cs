using Ee.Ebs.Application.Authors;
using Ee.Ebs.Application.Books;
using Ee.Ebs.Application.Categories;
using Ee.Ebs.Application.Contracts.Authors;
using Ee.Ebs.Application.Contracts.Books;
using Ee.Ebs.Application.Contracts.Categories;
using Ee.Ebs.Application.Contracts.Loans;
using Ee.Ebs.Application.Contracts.Members;
using Ee.Ebs.Application.Contracts.Publishers;
using Ee.Ebs.Application.Loans;
using Ee.Ebs.Application.Members;
using Ee.Ebs.Application.Publishers;
using Microsoft.Extensions.DependencyInjection;
using Ee.Ebs.Application.Authors.Validators;
using FluentValidation;

namespace Ee.Ebs.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModuleServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorAppService, AuthorAppService>();
        services.AddScoped<IBookAppService, BookAppService>();
        services.AddScoped<ICategoryAppService, CategoryAppService>();
        services.AddScoped<IPublisherAppService, PublisherAppService>();
        services.AddScoped<IMemberAppService, MemberAppService>();
        services.AddScoped<ILoanAppService, LoanAppService>();
        
        services.AddValidatorsFromAssemblyContaining<AuthorEditValidator>();
        
        services.AddAutoMapper(cfg => { }, 
            typeof(ApplicationModule)  
        );

        
        return services;
    }
}
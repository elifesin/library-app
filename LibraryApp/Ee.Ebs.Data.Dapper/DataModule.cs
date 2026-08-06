using Ee.Ebs.Domain.Authors;
using Ee.Ebs.Domain.Books;
using Ee.Ebs.Domain.Categories;
using Ee.Ebs.Domain.Loans;
using Ee.Ebs.Domain.Members;
using Ee.Ebs.Domain.Publishers;
using Ee.Ebs.Data.Dapper.Authors;
using Ee.Ebs.Data.Dapper.Books;
using Ee.Ebs.Data.Dapper.Categories;
using Ee.Ebs.Data.Dapper.Connection;
using Ee.Ebs.Data.Dapper.Loans;
using Ee.Ebs.Data.Dapper.Members;
using Ee.Ebs.Data.Dapper.Publishers;
using Microsoft.Extensions.DependencyInjection;

namespace Ee.Ebs.Data.Dapper;

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
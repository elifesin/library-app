using Data.Authors;
using Data.Books;
using Data.Categories;
using Data.Connection;
using Data.Loans;
using Data.Members;
using Data.Publishers;
using Domain.Authors;
using Domain.Books;
using Domain.Categories;
using Domain.Loans;
using Domain.Members;
using Domain.Publishers;
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
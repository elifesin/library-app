using DataEF.Authors;
using DataEF.Books;
using DataEF.Categories;
using DataEF.Loans;
using DataEF.Members;
using DataEF.Publishers;
using Domain.Authors;
using Domain.Books;
using Domain.Categories;
using Domain.Loans;
using Domain.Members;
using Domain.Publishers;
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
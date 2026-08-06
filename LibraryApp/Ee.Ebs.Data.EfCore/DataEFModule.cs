using Ee.Ebs.Domain.Authors;
using Ee.Ebs.Domain.Books;
using Ee.Ebs.Domain.Categories;
using Ee.Ebs.Domain.Loans;
using Ee.Ebs.Domain.Members;
using Ee.Ebs.Domain.Publishers;
using Ee.Ebs.Data.EfCore.Authors;
using Ee.Ebs.Data.EfCore.Books;
using Ee.Ebs.Data.EfCore.Categories;
using Ee.Ebs.Data.EfCore.Loans;
using Ee.Ebs.Data.EfCore.Members;
using Ee.Ebs.Data.EfCore.Publishers;
using Microsoft.Extensions.DependencyInjection;

namespace Ee.Ebs.Data.EfCore;

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
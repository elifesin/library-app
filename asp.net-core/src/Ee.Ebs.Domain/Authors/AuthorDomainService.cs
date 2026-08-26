using System;
using System.Linq;

namespace Ee.Ebs.Domain.Authors;

public class AuthorDomainService // AuthorManager
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorDomainService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }
        
    public Author Create(string firstName, string lastName)
    {
        bool ifExists = _authorRepository.GetAll().Any(a =>
            string.Equals(a.FirstName, firstName, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(a.LastName, lastName, StringComparison.OrdinalIgnoreCase));
        
        if (ifExists)
            throw new InvalidOperationException("Author already exists");
        
        return new Author(firstName, lastName);
    }
}
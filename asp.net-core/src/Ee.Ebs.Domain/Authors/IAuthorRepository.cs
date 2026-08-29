using System.Collections.Generic;

namespace Ee.Ebs.Domain.Authors;

public interface IAuthorRepository
{
    List<Author> GetAll();
    Author GetById(int id);
    void Insert(Author author);
    void Update(Author author);
    void Delete(int id);
}
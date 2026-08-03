using Domain.Entities;

namespace Domain.Repositories;

public interface IAuthorRepository
{
    List<Author> GetAll();
    Author GetById(int id);
    void Insert(Author author);
    void Update(Author author);
    void Delete(int id);
}
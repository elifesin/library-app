using System.Collections.Generic;

namespace Ee.Ebs.Domain.Books;

public interface IBookRepository
{
    List<Book> GetAll();
    Book GetById(int id);
    void Insert(Book book);
    void Update(Book book);
    void Delete(Book book);
    List<Book> GetAvailableBooks();
    List<Book> GetBooksByCategory(int categoryId);
}
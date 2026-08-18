using Ee.Ebs.Application.Contracts.Books.DTOs;

namespace Ee.Ebs.Application.Contracts.Books;

public interface IBookAppService
{
    List<BookDto>  GetAll();
    BookDto GetById(int id);
    void Insert(BookCreateDto book);
    void Update(int id, BookEditDto book);
    void Delete(int id);
    List<BookDto> GetAvailableBooks();
    List<BookDto> GetBooksByCategory(int categoryId);
}
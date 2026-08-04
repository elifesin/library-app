using Application.Books.DTOs;

namespace Application.Books;

public interface IBookService
{
    List<BookDto>  GetAll();
    BookDto GetById(int id);
    void Insert(BookCreateDto book);
    void Update(BookEditDto book);
    void Delete(int id);
    List<BookDto> GetAvailableBooks();
}
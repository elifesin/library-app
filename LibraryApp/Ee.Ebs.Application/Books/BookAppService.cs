using System.ComponentModel;
using AutoMapper;
using Ee.Ebs.Application.Contracts.Books;
using Ee.Ebs.Application.Contracts.Books.DTOs;
using Ee.Ebs.Domain.Books;

namespace Ee.Ebs.Application.Books;

public class BookAppService : IBookAppService
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public BookAppService(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }
    
    public List<BookDto> GetAll()
    {
        var books = _bookRepository.GetAll();
        return _mapper.Map<List<BookDto>>(books);
    }
    
    public BookDto GetById(int id)
    {
        var book = _bookRepository.GetById(id);
        return _mapper.Map<BookDto>(book);
    }
    
    public void Insert(BookCreateDto dto)
    {
        bool ifExists = _bookRepository.GetAll().Any(b =>
            b.Title.ToLower() == dto.Title.ToLower() &&
            b.AuthorID == dto.AuthorID &&
            b.CategoryID == dto.CategoryID &&
            b.PublisherId == dto.PublisherId);

        if (ifExists)
        {
            throw new InvalidOperationException("Kayıtlı bir kitabı tekrar kaydedemezsiniz!");
        }
        
        var bookEntity = _mapper.Map<Book>(dto);
        bookEntity.IsActive = true; 
        _bookRepository.Insert(bookEntity);
    }
    
    public void Update(int id, BookEditDto dto)
    {
        var bookEntity = _bookRepository.GetById(id);
        _mapper.Map(dto, bookEntity);
        _bookRepository.Update(bookEntity);
    }
    
    public void Delete(int id)
    {
        var book = new Book { Id = id };
        _bookRepository.Delete(book);
    }

    public List<BookDto> GetAvailableBooks()
    {
        var availableBooks = _bookRepository.GetAvailableBooks();
        return _mapper.Map<List<BookDto>>(availableBooks);
    }

    public List<BookDto> GetBooksByCategory(int categoryId)
    {
        var categoricBooks = _bookRepository.GetBooksByCategory(categoryId);
        return _mapper.Map<List<BookDto>>(categoricBooks);
    }
}
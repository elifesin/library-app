using AutoMapper;
using Ee.Ebs.Application.Contracts.Authors;
using Ee.Ebs.Application.Contracts.Authors.DTOs;
using Ee.Ebs.Domain.Authors;

namespace Ee.Ebs.Application.Authors;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    
    public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }
    
    public List<AuthorDto> GetAll()
    {
        var authors = _authorRepository.GetAll();
        return _mapper.Map<List<AuthorDto>>(authors);
    }
    
    public AuthorDto GetById(int id)
    {
        var author = _authorRepository.GetById(id);
        return _mapper.Map<AuthorDto>(author);
    }

    public void Insert(AuthorCreateDto dto)
    {
        var authorEntity = _mapper.Map<Author>(dto);
        // authorEntity.IsActive = true; // Eğer Entity içinde default true yapmadıysan burada yapabilirsin
        _authorRepository.Insert(authorEntity);
    }

    public void Update(AuthorEditDto dto)
    {
        var authorEntity = _mapper.Map<Author>(dto);
        _authorRepository.Update(authorEntity);
    }

    public void Delete(int id)
    { 
        _authorRepository.Delete(id);
    }
}
using AutoMapper;
using Ee.Ebs.Application.Contracts.Authors;
using Ee.Ebs.Application.Contracts.Authors.DTOs;
using Ee.Ebs.Domain.Authors;

namespace Ee.Ebs.Application.Authors;

public class AuthorAppService : IAuthorAppService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    
    public AuthorAppService(IAuthorRepository authorRepository, IMapper mapper)
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
        bool ifExists = _authorRepository.GetAll().Any(a =>
            a.FirstName.ToLower() == dto.FirstName.ToLower() &&
            a.LastName.ToLower() == dto.LastName.ToLower());

        if (ifExists)
        {
            throw new InvalidOperationException($" '{dto.FirstName} {dto.LastName}' adlı yazar zaten kayıtlı! ");
        }
        
        var authorEntity = _mapper.Map<Author>(dto);
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
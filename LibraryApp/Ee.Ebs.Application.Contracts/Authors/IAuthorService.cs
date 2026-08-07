using Ee.Ebs.Application.Contracts.Authors.DTOs;

namespace Ee.Ebs.Application.Contracts.Authors;

public interface IAuthorService
{
    List<AuthorDto> GetAll();
    AuthorDto GetById(int id);
    void Insert(AuthorCreateDto dto);
    void Update(AuthorEditDto dto);
    void Delete(int id);
}
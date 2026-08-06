using Ee.Ebs.Application.Authors.DTOs;

namespace Ee.Ebs.Application.Authors;

public interface IAuthorService
{
    List<AuthorDto> GetAll();
    AuthorDto GetById(int id);
    void Insert(AuthorCreateDto dto);
    void Update(AuthorEditDto dto);
    void Delete(int id);
}
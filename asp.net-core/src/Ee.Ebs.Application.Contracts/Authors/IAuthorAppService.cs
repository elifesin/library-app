using System.Collections.Generic;
using Ee.Ebs.Application.Contracts.Authors.DTOs;

namespace Ee.Ebs.Application.Contracts.Authors;

public interface IAuthorAppService
{
    List<AuthorDto> GetAll();
    AuthorDto GetById(int id);
    void Insert(AuthorCreateDto dto);
    void Update(int id, AuthorEditDto dto);
    void Delete(int id);
}
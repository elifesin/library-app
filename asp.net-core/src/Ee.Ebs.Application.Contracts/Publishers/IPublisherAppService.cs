using System.Collections.Generic;
using Ee.Ebs.Application.Contracts.Publishers.DTOs;

namespace Ee.Ebs.Application.Contracts.Publishers;

public interface IPublisherAppService
{
    public List<PublisherDto> GetAll();
    PublisherDto GetById(int id);
    void Insert(PublisherDto dto);
    void Update(int id, PublisherDto dto);  
    void Delete(int id);
}
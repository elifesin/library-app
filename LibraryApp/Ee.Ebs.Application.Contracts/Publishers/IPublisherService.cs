using Ee.Ebs.Application.Contracts.Publishers.DTOs;

namespace Ee.Ebs.Application.Contracts.Publishers;

public interface IPublisherService
{
    public List<PublisherDto> GetAll();
    PublisherDto GetById(int id);
    void Insert(PublisherDto dto);
    void Update(PublisherDto dto);  
    void Delete(int id);
}
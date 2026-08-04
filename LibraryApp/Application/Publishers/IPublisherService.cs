using Application.Publishers.DTOs;

namespace Application.Publishers;

public interface IPublisherService
{
    public List<PublisherDto> GetAll();
    PublisherDto GetById(int id);
    void Insert(PublisherDto dto);
    void Update(PublisherDto dto);  
    void Delete(int id);
}
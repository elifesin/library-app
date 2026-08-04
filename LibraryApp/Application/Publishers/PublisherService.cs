using Application.Publishers.DTOs;
using AutoMapper;
using Domain.Publishers;

namespace Application.Publishers;

public class PublisherService : IPublisherService
{
    private readonly IMapper _mapper;
    private readonly IPublisherRepository _publisherRepository;
    
    public PublisherService(IMapper mapper, IPublisherRepository publisherRepository)
    {
        _mapper = mapper;
        _publisherRepository = publisherRepository;
    }

    public List<PublisherDto> GetAll()
    {
        var publishers = _publisherRepository.GetAll();
        return _mapper.Map<List<PublisherDto>>(publishers);
    }

    public PublisherDto GetById(int id)
    {
        var publisher = _publisherRepository.GetById(id);
        return _mapper.Map<PublisherDto>(publisher);
    }

    public void Insert(PublisherDto publisher)
    {
        var publisherEntity = _mapper.Map<Publisher>(publisher);
        publisherEntity.IsActive = true;
        _publisherRepository.Insert(publisherEntity);
    }

    public void Update(PublisherDto publisher)
    {
        var publisherEntity = _mapper.Map<Publisher>(publisher);
        publisherEntity.IsActive = true;
        _publisherRepository.Update(publisherEntity);
    }

    public void Delete(int id)
    {
        _publisherRepository.Delete(id);
    }
}
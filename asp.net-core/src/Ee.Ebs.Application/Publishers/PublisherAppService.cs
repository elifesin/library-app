using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Ee.Ebs.Application.Contracts.Publishers;
using Ee.Ebs.Application.Contracts.Publishers.DTOs;
using Ee.Ebs.Domain.Publishers;

namespace Ee.Ebs.Application.Publishers;

public class PublisherAppService : IPublisherAppService
{
    private readonly IMapper _mapper;
    private readonly IPublisherRepository _publisherRepository;
    
    public PublisherAppService(IMapper mapper, IPublisherRepository publisherRepository)
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
        bool ifExists = _publisherRepository.GetAll().Any(p => 
            p.Name.ToLower() == publisher.Name.ToLower());

        if (ifExists)
        {
            throw new InvalidOperationException("Kayıtlı bir yayınevini tekrar kaydedemezsiniz");
        }
        
        var publisherEntity = _mapper.Map<Publisher>(publisher);
        publisherEntity.IsActive = true;
        _publisherRepository.Insert(publisherEntity);
    }

    public void Update(int id, PublisherDto dto)
    {
        var publisher = _publisherRepository.GetById(id);
        
        _mapper.Map(dto, publisher);
       
        _publisherRepository.Update(publisher);
    }

    public void Delete(int id)
    {
        _publisherRepository.Delete(id);
    }
}
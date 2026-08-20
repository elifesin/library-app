using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Ee.Ebs.Application.Contracts.Categories;
using Ee.Ebs.Application.Contracts.Categories.DTOs;
using Ee.Ebs.Domain.Categories;

namespace Ee.Ebs.Application.Categories;

public class CategoryAppService : ICategoryAppService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryAppService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public List<CategoryDto> GetAll()
    {
        var  categories = _categoryRepository.GetAll();
        return _mapper.Map<List<CategoryDto>>(categories);
    }

    public CategoryDto GetById(int id)
    {
        var category = _categoryRepository.GetById(id);
        return _mapper.Map<CategoryDto>(category);
    }

    public void Insert(CategoryDto category)
    {
        bool ifExists = _categoryRepository.GetAll().Any(c => 
            c.CategoryName.ToLower() == category.CategoryName.ToLower());

        if (ifExists)
        {
            throw new InvalidOperationException("Kayıtlı bir kategoriyi tekrar kaydedemezsiniz!");
        }
        
        var categoryDto = _mapper.Map<Category>(category);
        categoryDto.IsActive = true;
        _categoryRepository.Insert(categoryDto);
    }

    public void Update(int id, CategoryDto dto)
    {
        var category =  _categoryRepository.GetById(id);
        
        _mapper.Map(dto, category);
        
        _categoryRepository.Update(category);
    }

    public void Delete(int id)
    {
        _categoryRepository.Delete(id);
    }
}
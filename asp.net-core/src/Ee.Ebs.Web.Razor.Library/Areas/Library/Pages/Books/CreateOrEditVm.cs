using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ee.Ebs.Application.Contracts.Authors.DTOs;
using Ee.Ebs.Application.Contracts.Categories.DTOs;
using Ee.Ebs.Application.Contracts.Publishers.DTOs;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Books;

public class BookCreateOrEditVm
{
    [Required]
    public int? AuthorID { get; set; }
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    [Required]
    public int PublishYear { get; set; }
    public int CategoryID { get; set; } 
    public int PublisherId { get; set; }
    public bool IsBorrowed { get; set; }
    
    public List<AuthorDto> Authors { get; set; }  
    public List<CategoryDto> Categories { get; set; } 
    public List<PublisherDto> Publishers { get; set; } 
}
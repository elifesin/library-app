using System.ComponentModel.DataAnnotations;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Publishers;

public class PublisherCreateOrEditVm
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
}
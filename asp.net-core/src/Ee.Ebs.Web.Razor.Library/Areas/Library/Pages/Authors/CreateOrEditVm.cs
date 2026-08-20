using System.ComponentModel.DataAnnotations;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Authors;

public class AuthorCreateOrEditVm
{
    [Required] [MaxLength(50)] public string FirstName { get; set; }

    [Required] [MaxLength(50)] public string LastName { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Ee.Ebs.Domain.Shared.Authors;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Authors;

public class AuthorCreateOrEditVm
{
    [Required] 
    [MaxLength(AuthorConsts.FirstNameMaxLength)] 
    public string FirstName { get; set; }

    [Required] [MaxLength(50)] public string LastName { get; set; }
}
// Constant features are declared here. These kinds of properties don't have to be changed much and can be used
// multiple times. So we write once in here and use it by injection.

namespace Ee.Ebs.Domain.Shared.Authors;

public class AuthorConsts
{
    public const int FirstNameMaxLength = 50;
    public const int LastNameMaxlength = 50;
}
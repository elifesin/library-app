using Ee.Ebs.Application.Contracts.Authors.DTOs;
using FluentValidation;

namespace Ee.Ebs.Application.Authors.Validators;

public class AuthorCreateValidator :  AbstractValidator<AuthorCreateDto>
{
    public AuthorCreateValidator()
    {
        RuleFor(author => author.FirstName).NotEmpty()
            .WithMessage("Yazar Adı boş geçilemez")
            .MaximumLength(50)
            .WithMessage("Yazar Adı en fazla 50 karakter olabilir.");
        RuleFor(author => author.LastName).NotEmpty()
            .WithMessage("Yazar Soyadı boş geçilemez")
            .MaximumLength(50)
            .WithMessage("Yazar Soyadı en fazla 50 karakter olabilir.");
    }
}
using CarListApp.Api.Core.Dto;
using FluentValidation;

namespace CarListApp.Api.DtoValidators;

/// <summary>
/// Used to create custom validation for models
/// https://docs.fluentvalidation.net/en/latest/
/// </summary>
public class IdentityUserValidator : AbstractValidator<IdentityUserDto>
{
	public IdentityUserValidator()
	{
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        
        RuleFor(x => x.UserName).NotNull();
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .Length(1, 10);

        RuleFor(x => x.ConfirmPassword)
           .NotEmpty()
           .Equal(x => x.Password);

        RuleFor(x => x.Role).NotNull();
    }
}

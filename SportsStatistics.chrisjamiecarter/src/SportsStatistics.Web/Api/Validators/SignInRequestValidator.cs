using FluentValidation;
using SportsStatistics.SharedKernel;
using SportsStatistics.Web.Contracts.Requests;
using SportsStatistics.Web.Models;

namespace SportsStatistics.Web.Api.Validators;

internal sealed class SigninRequestValidator : AbstractValidator<SigninRequest>
{
    public SigninRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithError(SignInErrors.EmailRequired)
            .EmailAddress().WithError(SignInErrors.EmailInvalid);

        RuleFor(x => x.Password)
            .NotEmpty().WithError(SignInErrors.PasswordRequired);
    }
}

using FluentValidation;
using SportsStatistics.SharedKernel;
using SportsStatistics.Web.Contracts.Requests;
using SportsStatistics.Web.Models;

namespace SportsStatistics.Web.Api.Validators;

internal sealed class DemoSigninRequestValidator : AbstractValidator<DemoSigninRequest>
{
    public DemoSigninRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithError(DemoSignInErrors.EmailRequired)
            .EmailAddress().WithError(DemoSignInErrors.EmailInvalid);
    }
}

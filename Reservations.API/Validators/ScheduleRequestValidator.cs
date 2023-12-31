using FluentValidation;
using Reservations.API.Models.Requests;

namespace Reservations.API.Validators;

public class ScheduleRequestValidator : AbstractValidator<ScheduleRequest>
{
    public ScheduleRequestValidator()
    {
        RuleFor(x => x.StartDateTime)
            .Must(DateValidator.HaveTimeComponent).WithMessage("StartDateTime must have a time component.")
            .Must(DateValidator.BeInFifteenMinuteInterval)
            .WithMessage("StartDateTime time must be in 15 minute intervals.");

        RuleFor(x => x.EndDateTime)
            .Must(DateValidator.HaveTimeComponent).WithMessage("EndDateTime must have a time component.")
            .Must(DateValidator.BeInFifteenMinuteInterval)
            .WithMessage("EndDateTime time must be in 15 minute intervals.");

        RuleFor(x => x)
            .Must(x => x.StartDateTime.Date == x.EndDateTime.Date).WithMessage("Both dates must be on the same day.")
            .Must(x => x.StartDateTime < x.EndDateTime).WithMessage("StartDateTime must be less than EndDateTime.");
    }
}
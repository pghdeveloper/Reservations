using FluentValidation;
using Reservations.API.Models.Requests;

namespace Reservations.API.Validators;

public class AppointmentRequestValidator : AbstractValidator<AppointmentRequest>
{
    public AppointmentRequestValidator()
    {
        RuleFor(x => x.AppointmentDateTime)
            .Must(DateValidator.HaveTimeComponent).WithMessage("AppointmentDateTime must have a time component.")
            .Must(DateValidator.BeInFifteenMinuteInterval).WithMessage("AppointmentDateTime time must be in 15 minute intervals.");
    }
}
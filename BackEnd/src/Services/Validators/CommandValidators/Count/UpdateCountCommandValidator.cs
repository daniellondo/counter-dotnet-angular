namespace Services.Validators.CommandValidators.Count
{
    using Domain.Dtos.Count;
    using FluentValidation;

    public class UpdateCountCommandValidator : AbstractValidator<UpdateCountCommand>
    {
        public UpdateCountCommandValidator()
        {
            RuleFor(payload => payload.Id).NotNull();
            RuleFor(payload => payload.Count).NotNull();
        }
    }
}

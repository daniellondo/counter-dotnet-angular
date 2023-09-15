namespace Services.Validators.CommandValidators.Count
{
    using Domain.Dtos.Count;
    using FluentValidation;

    public class AddCountCommandValidator : AbstractValidator<AddCountCommand>
    {
        public AddCountCommandValidator()
        {
            RuleFor(payload => payload.Count).NotNull();
        }
    }
}

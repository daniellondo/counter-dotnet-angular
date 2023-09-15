namespace Services.Validators.QueryValidators
{
    using Domain.Dtos.Count;
    using FluentValidation;

    public class GetCountQueryValidator : AbstractValidator<GetCountQuery>
    {
        public GetCountQueryValidator()
        { }
    }
}

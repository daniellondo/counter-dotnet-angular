namespace Tests
{
    using AutoMapper;
    using Domain.Dtos.Count;
    using FluentValidation.TestHelper;
    using Services.MapperConfiguration;
    using Services.Validators.CommandValidators.Count;
    using Services.Validators.QueryValidators;
    using Tests.Utils;
    using Xunit;
    using static Services.CommandHandlers.CountCommandHandlers;
    using static Services.QueryHandlers.CountQueryHandlers;

    [Collection("Database collection")]
    public class CountTests
    {
        private readonly InMemoryDbContextFixture _database;
        private readonly IMapper _mapper;
        public CountTests(InMemoryDbContextFixture database)
        {
            _database = database;
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<CountProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Adding_Count()
        {
            // Arrange

            var command = new AddCountCommandValidator();
            var request = new AddCountCommand
            {
                Count = 1
            };

            // Act
            var result = await command.TestValidateAsync(request);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task Update_Count()
        {
            // Arrange

            var commandValidator = new UpdateCountCommandValidator();
            var _request = new UpdateCountCommand
            {
                Id = 1000,
                Count = 2
            };
            var previous = _database.Context.Counts.First(x => x.Id == _request.Id).Count;

            // Act
            var result = await commandValidator.TestValidateAsync(_request);
            var handler = new UpdateCountCommandHandler(_database.Context, _mapper);
            var response = await handler.Handle(_request, new CancellationToken());
            var actual = _database.Context.Counts.First(x => x.Id == _request.Id).Count;

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
            Assert.NotNull(response.Result);
            Assert.NotEqual(actual, previous);
        }

        [Fact]
        public async Task Get_Count()
        {
            // Arrange

            var validator = new GetCountQueryValidator();
            var _request = new GetCountQuery();

            // Act
            var result = await validator.TestValidateAsync(_request);
            var handler = new GetCountQueryHandler(_database.Context, _mapper);
            var response = await handler.Handle(_request, new CancellationToken());

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
            Assert.NotNull(response.Result);
        }

    }
}

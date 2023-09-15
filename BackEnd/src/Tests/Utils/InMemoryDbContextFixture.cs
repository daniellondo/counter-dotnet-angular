namespace Tests.Utils
{
    using AutoFixture;
    using Data;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using NSubstitute;
    using System;

    public class InMemoryDbContextFixture : IDisposable
    {
        public IContext Context { get; private set; }

        public readonly IContext _mockContext;
        public static readonly Fixture _fixture = new();

        public InMemoryDbContextFixture()
        {
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            Context = GetInMemoryDbContext();
            _mockContext = Substitute.For<IContext>();
            InitializeData(Context, _mockContext);
        }

        private void InitializeData(IContext context, IContext mockContext)
        {
            context.Counts.AddRange(GetCountsArranged());
            context.SaveChanges();

            mockContext.Counts.Returns(context.Counts);
        }

        private List<CountEntity> GetCountsArranged()
        {
            return new List<CountEntity> {
                _fixture.Build<CountEntity>()
                    .With(p => p.Id, 1000)
                    .With(p => p.Count, 1)
                    .Create(),
            };
        }

        public static Context GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<Context>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString())
              .LogTo(Console.WriteLine)
              .EnableDetailedErrors(true)
              .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
              .EnableSensitiveDataLogging(true)
              .Options;

            return new Context(options);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Context.Dispose();
        }

        ~InMemoryDbContextFixture()
        {
            Dispose(false);
        }

    }
}

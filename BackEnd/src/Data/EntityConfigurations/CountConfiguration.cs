namespace Data.EntityConfigurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CountConfiguration : IEntityTypeConfiguration<CountEntity>
    {
        public void Configure(EntityTypeBuilder<CountEntity> builder)
        {
            builder.HasKey(entity => new { entity.Id });
            builder.Property(f => f.Id).ValueGeneratedOnAdd();
        }
    }
}

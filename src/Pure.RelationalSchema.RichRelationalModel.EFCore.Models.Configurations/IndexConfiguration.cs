using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pure.Primitives.Abstractions.EFCore.Converters;
using Pure.Primitives.Abstractions.EFCore.ValueComparers;

namespace Pure.RelationalSchema.RichRelationalModel.EFCore.Models.Configurations;

public sealed record IndexConfiguration : IEntityTypeConfiguration<IndexEFCoreModel>
{
    public void Configure(EntityTypeBuilder<IndexEFCoreModel> builder)
    {
        _ = builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired()
            .HasConversion(new GuidTypeConverter())
            .Metadata.SetValueComparer(new GuidValueComparer());

        builder
            .Property(x => x.IsUnique)
            .IsRequired()
            .HasConversion(new BoolTypeConverter())
            .Metadata.SetValueComparer(new BoolValueComparer());

        builder
            .Property(x => x.TableId)
            .IsRequired()
            .HasConversion(new GuidTypeConverter())
            .Metadata.SetValueComparer(new GuidValueComparer());

        _ = builder.HasMany(x => x.ColumnsNavigation).WithMany();
    }
}

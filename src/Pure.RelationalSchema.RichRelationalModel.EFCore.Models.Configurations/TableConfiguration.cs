using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pure.Primitives.Abstractions.EFCore.Converters;
using Pure.Primitives.Abstractions.EFCore.ValueComparers;

namespace Pure.RelationalSchema.RichRelationalModel.EFCore.Models.Configurations;

public sealed record TableConfiguration : IEntityTypeConfiguration<TableEFCoreModel>
{
    public void Configure(EntityTypeBuilder<TableEFCoreModel> builder)
    {
        _ = builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired()
            .HasConversion(new GuidTypeConverter())
            .Metadata.SetValueComparer(new GuidValueComparer());

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasConversion(new StringTypeConverter())
            .HasMaxLength(64)
            .Metadata.SetValueComparer(new StringValueComparer());

        builder
            .Property(x => x.SchemaId)
            .IsRequired()
            .HasConversion(new GuidTypeConverter())
            .Metadata.SetValueComparer(new GuidValueComparer());

        _ = builder
            .HasMany(x => x.ColumnsNavigation)
            .WithOne()
            .HasForeignKey(x => x.TableId);

        _ = builder
            .HasMany(x => x.IndexesNavigation)
            .WithOne()
            .HasForeignKey(x => x.TableId);
    }
}

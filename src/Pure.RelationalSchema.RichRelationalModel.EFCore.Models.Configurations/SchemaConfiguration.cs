using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pure.Primitives.Abstractions.EFCore.Converters;
using Pure.Primitives.Abstractions.EFCore.ValueComparers;

namespace Pure.RelationalSchema.RichRelationalModel.EFCore.Models.Configurations;

public sealed record SchemaConfiguration : IEntityTypeConfiguration<SchemaEFCoreModel>
{
    public void Configure(EntityTypeBuilder<SchemaEFCoreModel> builder)
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

        _ = builder
            .HasMany(x => x.TablesNavigation)
            .WithOne()
            .HasForeignKey(x => x.SchemaId);

        _ = builder
            .HasMany(x => x.ForeignKeysNavigation)
            .WithOne()
            .HasForeignKey(x => x.SchemaId);
    }
}

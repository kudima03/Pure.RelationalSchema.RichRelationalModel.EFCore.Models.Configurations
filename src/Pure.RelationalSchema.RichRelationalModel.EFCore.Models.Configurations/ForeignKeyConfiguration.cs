using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pure.Primitives.Abstractions.EFCore.Converters;
using Pure.Primitives.Abstractions.EFCore.ValueComparers;

namespace Pure.RelationalSchema.RichRelationalModel.EFCore.Models.Configurations;

public sealed record ForeignKeyConfiguration
    : IEntityTypeConfiguration<ForeignKeyEFCoreModel>
{
    public void Configure(EntityTypeBuilder<ForeignKeyEFCoreModel> builder)
    {
        _ = builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired()
            .HasConversion(new GuidTypeConverter())
            .Metadata.SetValueComparer(new GuidValueComparer());

        builder
            .Property(x => x.SchemaId)
            .IsRequired()
            .HasConversion(new GuidTypeConverter())
            .Metadata.SetValueComparer(new GuidValueComparer());

        builder
            .Property(x => x.ReferencingTableId)
            .IsRequired()
            .HasConversion(new GuidTypeConverter())
            .Metadata.SetValueComparer(new GuidValueComparer());

        _ = builder
            .HasOne(x => x.ReferencingTableNavigation)
            .WithMany()
            .HasForeignKey(x => x.ReferencingTableId);

        builder
            .Property(x => x.ReferencedTableId)
            .IsRequired()
            .HasConversion(new GuidTypeConverter())
            .Metadata.SetValueComparer(new GuidValueComparer());

        _ = builder
            .HasOne(x => x.ReferencedTableNavigation)
            .WithMany()
            .HasForeignKey(x => x.ReferencedTableId);

        _ = builder.HasMany(x => x.ReferencingColumnsNavigation).WithMany();

        _ = builder.HasMany(x => x.ReferencedColumnsNavigation).WithMany();
    }
}

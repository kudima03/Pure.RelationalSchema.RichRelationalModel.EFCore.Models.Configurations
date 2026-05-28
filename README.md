# Pure.RelationalSchema.RichRelationalModel.EFCore.Models.Configurations

EF Core `IEntityTypeConfiguration<T>` implementations for the **Pure** rich relational schema model entities.

[![.NET build & test](https://github.com/kudima03/Pure.RelationalSchema.RichRelationalModel.EFCore.Models.Configurations/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.RelationalSchema.RichRelationalModel.EFCore.Models.Configurations/actions/workflows/build-and-test.yml)
[![Build and Deploy](https://github.com/kudima03/Pure.RelationalSchema.RichRelationalModel.EFCore.Models.Configurations/actions/workflows/publish-nuget.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.RelationalSchema.RichRelationalModel.EFCore.Models.Configurations/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/Pure.RelationalSchema.RichRelationalModel.EFCore.Models.Configurations)](https://www.nuget.org/packages/Pure.RelationalSchema.RichRelationalModel.EFCore.Models.Configurations)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Overview

`Pure.RelationalSchema.RichRelationalModel.EFCore.Models.Configurations` provides a set of EF Core type configurations that wire up the relational schema model entities — schemas, tables, columns, column types, indexes, and foreign keys — with their column mappings, key constraints, max lengths, foreign key relationships, and Pure primitive converters/comparers.

Register all six configurations in your `DbContext` to persist the rich relational schema model via EF Core.

## Configurations

| Type | Entity | Description |
|---|---|---|
| `SchemaConfiguration` | `SchemaEFCoreModel` | GUID PK (never generated), Name (max 64), one-to-many Tables and ForeignKeys navigations |
| `TableConfiguration` | `TableEFCoreModel` | GUID PK, Name (max 64), SchemaId FK, one-to-many Columns and Indexes navigations |
| `ColumnConfiguration` | `ColumnEFCoreModel` | GUID PK, Name (max 64), TableId and TypeId FKs, many-to-one ColumnType navigation |
| `ColumnTypeConfiguration` | `ColumnTypeEFCoreModel` | GUID PK, Name (max 64, unique index) |
| `IndexConfiguration` | `IndexEFCoreModel` | GUID PK, IsUnique bool, TableId FK, many-to-many Columns navigation |
| `ForeignKeyConfiguration` | `ForeignKeyEFCoreModel` | GUID PK, SchemaId/ReferencingTableId/ReferencedTableId FKs, many-to-many referencing and referenced column navigations |

All GUID and String properties use converters and value comparers from the Pure primitives ecosystem. `IsUnique` on `IndexConfiguration` uses `BoolTypeConverter` and `BoolValueComparer`.

## Dependencies

- [`Pure.RelationalSchema.RichRelationalModel.EFCore.Models` v0.1.0-preview.0.1.0](https://github.com/kudima03/Pure.RelationalSchema.RichRelationalModel.EFCore.Models/tree/0.1.0-preview.0.1.0) — EF Core entity model classes for the rich relational schema (SchemaEFCoreModel, TableEFCoreModel, ColumnEFCoreModel, ColumnTypeEFCoreModel, IndexEFCoreModel, ForeignKeyEFCoreModel)
- [`Pure.Primitives.Abstractions.EFCore.Converters` v0.1.0-preview.0.1.0](https://github.com/kudima03/Pure.Primitives.Abstractions.EFCore.Converters/tree/0.1.0-preview.0.1.0) — EF Core `ValueConverter` implementations for Pure primitive types (Guid, String, Bool, and more)
- [`Pure.Primitives.Abstractions.EFCore.ValueComparers` v0.1.0-preview.0.1.0](https://github.com/kudima03/Pure.Primitives.Abstractions.EFCore.ValueComparers/tree/0.1.0-preview.0.1.0) — EF Core `ValueComparer` implementations for Pure primitive types

## Target Frameworks

- .NET 7
- .NET 8
- .NET 9
- .NET 10

## Installation

```
dotnet add package Pure.RelationalSchema.RichRelationalModel.EFCore.Models.Configurations
```

## Usage

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.ApplyConfigurationsFromAssembly(
        typeof(SchemaConfiguration).Assembly);
}
```

Or register each configuration individually:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.ApplyConfiguration(new SchemaConfiguration());
    modelBuilder.ApplyConfiguration(new TableConfiguration());
    modelBuilder.ApplyConfiguration(new ColumnConfiguration());
    modelBuilder.ApplyConfiguration(new ColumnTypeConfiguration());
    modelBuilder.ApplyConfiguration(new IndexConfiguration());
    modelBuilder.ApplyConfiguration(new ForeignKeyConfiguration());
}
```

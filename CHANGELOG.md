# Changelog

All notable changes to Pure.RelationalSchema.RichRelationalModel.EFCore.Models.Configurations are documented here.

Format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

---

## [0.1.0-preview.0.1.2] — 2026-06-25

- Maintenance release: dependency and build updates.

## [0.1.0-preview.0.1.1] — 2026-06-07

- Maintenance release: dependency and build updates.

## [0.1.0-preview.0.1.0] — 2026-04-30

### Added

- **`SchemaConfiguration`** — `IEntityTypeConfiguration<SchemaEFCoreModel>` with a
  never-generated GUID key, a `Name` property (max length 64), and one-to-many
  navigations to `Tables` and `ForeignKeys`.
- **`TableConfiguration`** — `IEntityTypeConfiguration<TableEFCoreModel>` with a GUID
  key, `Name` (max length 64), a `SchemaId` foreign key, and one-to-many navigations
  to `Columns` and `Indexes`.
- **`ColumnConfiguration`** — `IEntityTypeConfiguration<ColumnEFCoreModel>` with a GUID
  key, `Name` (max length 64), `TableId` and `TypeId` foreign keys, and a many-to-one
  navigation to `ColumnType`.
- **`ColumnTypeConfiguration`** — `IEntityTypeConfiguration<ColumnTypeEFCoreModel>` with
  a GUID key and a `Name` property (max length 64, unique index).
- **`IndexConfiguration`** — `IEntityTypeConfiguration<IndexEFCoreModel>` with a GUID
  key, an `IsUnique` flag, a `TableId` foreign key, and a many-to-many navigation to
  `Columns`.
- **`ForeignKeyConfiguration`** — `IEntityTypeConfiguration<ForeignKeyEFCoreModel>` with
  a GUID key, `SchemaId`/`ReferencingTableId`/`ReferencedTableId` foreign keys, and
  many-to-many navigations to the referencing and referenced columns.

All GUID, string, and bool properties use converters and value comparers from the Pure
primitives ecosystem.

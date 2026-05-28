# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

All `dotnet` commands must be run from the `./src` directory.

```bash
dotnet restore
dotnet build --no-restore -warnaserror
dotnet format --verify-no-changes             # check code style (CI enforces this)
dotnet format && csharpier format .           # auto-fix code style
dotnet test --no-build --verbosity normal     # run xUnit tests
dotnet pack --configuration Release -p:PackageVersion=<version> --output .
```

## Architecture

This is a **configurations-only NuGet library** — no domain logic, no data access, just EF Core `IEntityTypeConfiguration<T>` implementations for the rich relational schema model.

**Six sealed record configurations, one per entity:**

- `SchemaConfiguration` → `SchemaEFCoreModel` — configures the root schema entity with its tables and foreign keys navigations
- `TableConfiguration` → `TableEFCoreModel` — configures table entities belonging to a schema, with columns and indexes navigations
- `ColumnConfiguration` → `ColumnEFCoreModel` — configures column entities belonging to a table, with a many-to-one type navigation
- `ColumnTypeConfiguration` → `ColumnTypeEFCoreModel` — configures column type entities with a unique name index
- `IndexConfiguration` → `IndexEFCoreModel` — configures index entities with a many-to-many columns navigation
- `ForeignKeyConfiguration` → `ForeignKeyEFCoreModel` — configures foreign key entities with referencing/referenced table navigations and many-to-many column navigations

All GUID and String properties go through `GuidTypeConverter`/`GuidValueComparer` and `StringTypeConverter`/`StringValueComparer` from `Pure.Primitives.Abstractions.EFCore.Converters` and `Pure.Primitives.Abstractions.EFCore.ValueComparers`. The `IsUnique` bool on `IndexConfiguration` uses the corresponding Bool converter and comparer.

**Multi-targeting:** net7.0, net8.0, net9.0, net10.0. `IsAotCompatible = true` — no reflection-based patterns.

**Package validation:** `EnablePackageValidation = true` with `PackageValidationBaselineVersion = 0.1.0-preview.0.1.0`. Breaking API changes fail the build.

**Publishing:** triggered by pushing a semver tag (pattern `*.*.*`). The tag name becomes the `PackageVersion`. Packages are published to both GitHub Packages and NuGet.org.

**Tests:** one xUnit test project targeting net10.0 under `./src/Tests/`.

## Code Style

Enforced via `./src/.editorconfig` and checked in CI with `dotnet format --verify-no-changes` and `csharpier check .`:

- No `var` — always use explicit types
- No expression-bodied methods or constructors; expression-bodied properties and accessors are required
- File-scoped namespace declarations (`namespace Foo.Bar;`)
- `using` directives outside the namespace
- Discard unused expression results with `_ =` rather than calling as a statement
- Private fields: `_camelCase` prefix
- No implicit `new()` when the type is not apparent from context
- Max line length: 90 characters
- `using System.*` sorted before other usings, no blank line between groups

## Commit Messages

Do not mention Claude or AI assistance in commit messages.

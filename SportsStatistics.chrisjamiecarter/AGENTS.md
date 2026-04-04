# AGENTS.md

This file provides guidance for AI coding agents working in this repository.

## Project Overview

Sports Statistics is a .NET 9 Blazor Server application for tracking football team statistics. It uses Clean Architecture with Domain-Driven Design patterns, CQRS with MediatR, Entity Framework Core, and .NET Aspire for orchestration.

## Build Commands

```bash
# Build entire solution
dotnet build

# Build specific project
dotnet build src/SportsStatistics.Domain/SportsStatistics.Domain.csproj

# Restore packages
dotnet restore

# Clean build artifacts
dotnet clean
```

## Test Commands

```bash
# Run all tests
dotnet test

# Run tests with verbose output
dotnet test --verbosity normal

# Run tests for specific project
dotnet test tests/SportsStatistics.Domain.Tests/SportsStatistics.Domain.Tests.csproj

# Run a single test by name
dotnet test --filter "FullyQualifiedName~SeasonTests"

# Run a specific test class
dotnet test --filter "ClassName=SportsStatistics.Domain.Tests.Seasons.SeasonTests"

# Run a specific test method
dotnet test --filter "Name=Create_ShouldCreateSeason_WhenParametersAreValid"
```

## Run Commands

```bash
# Run Aspire AppHost (starts all services)
dotnet run --project src/SportsStatistics.Aspire.AppHost/SportsStatistics.Aspire.AppHost.csproj

# Run Web project only
dotnet run --project src/SportsStatistics.Web/SportsStatistics.Web.csproj
```

## Database Migration Commands

```bash
# Add migration for Application DbContext
Add-Migration MigrationName -Project SportsStatistics.Infrastructure -StartupProject SportsStatistics.Web -OutputDir Persistence\Migrations -Context SportsStatisticsDbContext

# Add migration for Identity DbContext
Add-Migration MigrationName -Project SportsStatistics.Authorization -StartupProject SportsStatistics.Web -OutputDir Database\Migrations -Context IdentityDbContext
```

## Code Style Guidelines

### General

- **Target Framework**: .NET 9 (C# 12+)
- **Nullable**: Enabled with `TreatWarningsAsErrors` - all warnings are errors
- **Analysis Level**: `latest-recommended`
- **Enforce Code Style**: Enabled in build

### Formatting (from .editorconfig)

- **Indent**: 4 spaces (no tabs)
- **Line endings**: CRLF
- **Final newline**: Not required (`insert_final_newline = false`)
- **Namespaces**: File-scoped preferred
- **Using placement**: Outside namespace

### Naming Conventions

- **Interfaces**: Prefix with `I` (e.g., `IApplicationDbContext`)
- **Private fields**: Prefix with `_` (e.g., `_dbContext`)
- **Static readonly fields**: PascalCase
- **Constants**: PascalCase
- **Types**: PascalCase
- **Non-field members**: PascalCase (properties, events, methods)

### Language Preferences

- **Var**: Use explicit types except when type is apparent
- **Expression-bodied members**: Allow for accessors, indexers, lambdas
- **Pattern matching**: Preferred over casts and null checks
- **Primary constructors**: Preferred
- **File-scoped namespaces**: Preferred
- **Top-level statements**: Not used

### Code Patterns

#### Domain Layer

```csharp
// Entities use private constructors for EF Core
public sealed class Season : Entity, ISoftDeletableEntity
{
    private Season(DateRange dateRange) : base(Guid.CreateVersion7())
    {
        DateRange = dateRange;
    }

    private Season() { } // EF Core required

    public DateRange DateRange { get; private set; } = default!;
    
    public static Season Create(DateRange dateRange)
    {
        var season = new Season(dateRange);
        season.Raise(new SeasonCreatedDomainEvent(season.Id));
        return season;
    }
}
```

#### Value Objects

```csharp
public sealed record DateRange
{
    private DateRange(DateOnly startDate, DateOnly endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public DateOnly StartDate { get; }
    public DateOnly EndDate { get; }

    public static Result<DateRange> Create(DateOnly? startDate, DateOnly? endDate)
    {
        // Validation logic
        return new DateRange(startDate.Value, endDate.Value);
    }
}
```

#### CQRS Handlers

```csharp
internal sealed class GetSeasonsQueryHandler(IApplicationDbContext dbContext) 
    : IQueryHandler<GetSeasonsQuery, List<SeasonResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<SeasonResponse>>> Handle(
        GetSeasonsQuery request, 
        CancellationToken cancellationToken)
    {
        var seasons = await _dbContext.Seasons.AsNoTracking()
            .OrderBy(season => season.DateRange.StartDate)
            .ToListAsync(cancellationToken);
        return seasons.ToResponse();
    }
}
```

### Testing Conventions

- **Framework**: xUnit with Shouldly assertions
- **Mocking**: Moq with MockQueryable.Moq for EF Core
- **Test class naming**: `{ClassUnderTest}Tests`
- **Test method naming**: `{Method}_{Scenario}_{ExpectedResult}`
- **Test data**: Use `TheoryData<T>` classes in `TestCases` folder
- **Test structure**: Arrange, Act, Assert comments

```csharp
public class SeasonTests
{
    [Theory]
    [ClassData(typeof(CreateSeasonTestCase))]
    public void Create_ShouldCreateSeason_WhenParametersAreValid(DateRange dateRange)
    {
        // Arrange.
        // Act.
        var season = Season.Create(dateRange);

        // Assert.
        season.ShouldNotBeNull();
        season.Id.ShouldNotBe(default);
    }
}
```

### Error Handling

- Use `Result<T>` pattern from SharedKernel
- Errors are defined in `{Feature}Errors.cs` static classes
- Domain validation returns `Result<T>` with specific error

### Import Organization

- System directives first (sorted)
- No separation between import groups
- No `this.` qualification for members

### Project Structure

```
src/
  SportsStatistics.Domain/           # Entities, value objects, domain events
  SportsStatistics.Application/      # CQRS handlers, interfaces, validators
  SportsStatistics.Infrastructure/   # EF Core, repositories, external services
  SportsStatistics.Web/              # Blazor Server UI
  SportsStatistics.Authorization/    # Identity services
  SportsStatistics.SharedKernel/     # Result, Error, Entity base classes
  SportsStatistics.Aspire.AppHost/   # Aspire orchestration
  SportsStatistics.Aspire.ServiceDefaults/
  SportsStatistics.Tools.DatabaseMigrator/
tests/
  SportsStatistics.Domain.Tests/
  SportsStatistics.Application.Tests/
  SportsStatistics.Infrastructure.Tests/
  SportsStatistics.ArchitectureTests/
```

## Key Architectural Decisions

1. **Clean Architecture**: Domain → Application → Infrastructure → Web
2. **CQRS**: Separate commands and queries using MediatR
3. **Result Pattern**: All operations return `Result<T>` or `Result`
4. **Domain Events**: Entities raise events for cross-cutting concerns
5. **Soft Deletes**: Entities implement `ISoftDeletableEntity`
6. **UUID v7**: Use `Guid.CreateVersion7()` for entity IDs
7. **AsNoTracking**: Use for all query operations
8. **Nullable Reference Types**: Fully enabled across all projects

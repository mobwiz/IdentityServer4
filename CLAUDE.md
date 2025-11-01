# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is the **IdentityServer4** codebase - a free, open source OpenID Connect and OAuth 2.0 framework for ASP.NET Core.

**IMPORTANT**: This project is unmaintained and will be archived. All new development has moved to Duende Software. The current branch is `feature/dotnet-8`, upgrading from .NET 6.0 to .NET 8.0.

## Build and Development Commands

### Main Build Commands
```bash
# Build entire solution (Storage + IdentityServer4)
./build.ps1

# Build specific components
./src/Storage/build.ps1
./src/IdentityServer4/build.ps1

# Development build (no signing)
./build.ps1 quick

# Full build with tests
./build.ps1 default

# Build with binary and package signing (requires SignClientSecret env var)
./build.ps1 sign
```

### Development Commands
```bash
# Restore tools and dependencies
dotnet tool restore
dotnet restore

# Build individual projects
dotnet build src/IdentityServer4/src/IdentityServer4.csproj
dotnet build src/Storage/src/IdentityServer4.Storage.csproj

# Run tests
dotnet test src/IdentityServer4/
dotnet test src/Storage/

# Run test host application
dotnet run --project src/IdentityServer4/host
```

## Architecture Overview

### Core Components

1. **IdentityServer4** (`src/IdentityServer4/src/`) - Main framework library
   - Target: .NET 6.0 (upgrading to .NET 8.0)
   - Contains all OAuth2/OIDC protocol implementations
   - Modular design with separate concerns for endpoints, services, stores, validation

2. **IdentityServer4.Storage** (`src/Storage/src/`) - Storage abstraction layer
   - Target: .NET Standard 2.1
   - Interfaces and models for database persistence
   - Separate storage abstraction for pluggable data stores

3. **Host Application** (`src/IdentityServer4/host/`) - Test/sample host
   - Complete working IdentityServer implementation
   - MVC-based UI for login, consent, error pages
   - Test configuration for clients and resources

### Key Architectural Patterns

- **Dependency Injection**: Extensive use of ASP.NET Core DI with extension methods
- **Storage Abstraction**: Interface-based design allowing pluggable storage providers
- **Event-Driven Architecture**: Comprehensive event logging system with Serilog integration
- **Endpoint-Centric Design**: Separate endpoint classes for each OAuth2/OIDC flow

### Project Structure

```
src/
├── IdentityServer4/
│   ├── src/              # Core framework
│   ├── host/             # Test host application
│   ├── build/            # Build system
│   └── test/             # Unit and integration tests
├── Storage/
│   ├── src/              # Storage interfaces
│   └── build/            # Build system
└── build/                # Shared build logic
```

## Sample Applications

The `samples/` directory contains comprehensive examples:

- **Legacy samples** (`old/`): MVC-based implementations of different OAuth2/OIDC flows
- **Modern samples** (`src/`): Console clients, API implementations, cross-service examples

Key flows demonstrated: Client Credentials, Hybrid, Implicit, Authorization Code flows.

## Development Environment

- **Required SDK**: .NET 6.0.408 (specified in global.json)
- **Build Tools**: Bullseye + SimpleExec for cross-platform builds
- **Strong Naming**: All assemblies signed with key.snk
- **Package Output**: ./nuget directory for built packages

## Testing

- **Framework**: xUnit with Fluent Assertions
- **Coverage**: High coverage across all components
- **Test Organization**: Unit tests, integration tests, conformance tests
- **Host Application**: Use for manual testing and API exploration

## Security Considerations

- All assemblies are strongly named
- Comprehensive input validation throughout
- Protection against common web vulnerabilities
- Certificate-based authentication support

## Migration Status

Currently upgrading from .NET 6.0 to .NET 8.0 on the `feature/dotnet-8` branch. Main development targets are in `src/IdentityServer4/src/IdentityServer4.csproj` and related build files.

# Quickstart Guide: IdentityServer4 .NET 8.0 Upgrade

**Generated**: 2025-11-01
**Purpose**: Quick reference for developers executing the upgrade

## Prerequisites

### Development Environment Setup
1. **Install .NET 8.0 SDK**
   ```bash
   # Download and install .NET 8.0 SDK from Microsoft
   # Verify installation
   dotnet --version  # Should show 8.0.100 or later
   ```

2. **Update IDE**
   - Visual Studio 2022 17.8+ or VS Code with C# extension
   - Ensure all extensions are compatible with .NET 8.0

3. **Verify Current State**
   ```bash
   # Ensure current branch is clean
   git status
   # Run current build to establish baseline
   ./build.ps1 default
   ```

## Quick Start Instructions

### Phase 1: Core Framework Upgrade (2-3 days)

#### 1.1 Update Target Frameworks
```bash
# Update IdentityServer4.csproj
# Change: <TargetFramework>net6.0</TargetFramework>
# To:     <TargetFramework>net8.0</TargetFramework>

# Update IdentityServer4.Storage.csproj
# Change: <TargetFramework>netstandard2.1</TargetFramework>
# To:     <TargetFramework>net8.0</TargetFramework>

# Update test projects
# Change: <TargetFramework>net6.0</TargetFramework>
# To:     <TargetFramework>net8.0</TargetFramework>
```

#### 1.2 Update Package References
```xml
<!-- Core packages -->
<PackageReference Include="Microsoft.AspNetCore.Authentication.OpenIdConnect" Version="8.0.0" />
<PackageReference Include="IdentityModel" Version="7.0.0" />
<PackageReference Include="Microsoft.IdentityModel.Protocols.OpenIdConnect" Version="8.0.0" />

<!-- Testing packages -->
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
<PackageReference Include="xunit" Version="2.4.2" />
<PackageReference Include="FluentAssertions" Version="6.12.0" />
```

#### 1.3 First Build and Fix Compilation
```bash
# Build core framework
dotnet build src/IdentityServer4/src/IdentityServer4.csproj
dotnet build src/Storage/src/IdentityServer4.Storage.csproj

# Fix compilation errors:
# - Update using statements for namespace changes
# - Fix method calls that have breaking changes
# - Update async/await patterns if needed
```

#### 1.4 Run Tests and Fix Issues
```bash
# Run unit tests
dotnet test src/IdentityServer4/
dotnet test src/Storage/

# Expected: Some tests may fail due to:
# - FluentAssertions API changes (v5 → v6)
# - Test framework updates
# - Mocking library compatibility issues

# Fix failing tests by updating assertion syntax:
// Old (FluentAssertions 5.x)
result.Should().Be(expectedValue);

// New (FluentAssertions 6.x)
result.Should().Be(expectedValue);
```

#### 1.5 Validate Core Functionality
```bash
# Run the host application
dotnet run --project src/IdentityServer4/host

# Test basic scenarios:
# - Discovery endpoint: https://localhost:5001/.well-known/openid-configuration
# - Authorization flow
# - Token endpoint
# - User info endpoint
```

### Phase 2: Entity Framework Migration (3-5 days)

#### 2.1 Update EF Core Packages
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.0" />
```

#### 2.2 Fix Breaking Changes
```csharp
// Method name changes
// Old: FromSql()
// New: FromSqlRaw()

// Configuration changes
// Old: UseSqlServer(connectionString, options => ...)
// New: UseSqlServer(connectionString, options => ...)

// Check all database contexts and update accordingly
```

#### 2.3 Update Migrations
```bash
# Backup existing migrations
cp -r src/IdentityServer4/host/Data/Migrations src/IdentityServer4/host/Data/Migrations.backup

# Create new migrations
dotnet ef migrations add InitialCreateForNet8 --project src/IdentityServer4/host

# Test database operations
dotnet run --project src/IdentityServer4/host
# Verify database schema and operations
```

### Phase 3: Sample Applications (5-7 days)

#### 3.1 Quickstart Solutions Priority Order
1. **Basic Quickstart** - Simplest, validates basic functionality
2. **Client Credentials Quickstart** - Tests service-to-service
3. **Hybrid Flow Quickstart** - Tests authentication flows
4. **JavaScript Client Quickstart** - Tests SPA integration
5. **API Resource Quickstart** - Tests API protection

#### 3.2 Update Each Quickstart
```bash
# For each quickstart solution:
cd samples/quickstarts/[NAME]/

# Update all .csproj files to target net8.0
# Update package references to .NET 8.0 compatible versions
# Build and test each project
dotnet build
dotnet test
```

#### 3.3 Test Each Sample Scenario
```bash
# For each sample:
1. Build successfully ✓
2. Run without errors ✓
3. Execute primary user journey ✓
4. Test OAuth2/OIDC flows ✓
5. Verify no configuration issues ✓
```

### Phase 4: Build System (1-2 days)

#### 4.1 Update Build Project
```xml
<!-- build/build.csproj -->
<TargetFramework>net8.0</TargetFramework>
<PackageReference Include="Bullseye" Version="5.0.0" />
<PackageReference Include="SimpleExec" Version="12.0.0" />
```

#### 4.2 Test All Build Variants
```bash
# Test all build commands
./build.ps1 quick      # Development build
./build.ps1 default    # Full build with tests
./build.ps1 sign       # Signed build (if configured)
```

## Validation Checklist

### Core Framework Validation
- [ ] IdentityServer4 builds without errors
- [ ] IdentityServer4.Storage builds without errors
- [ ] All unit tests pass (≥95% pass rate)
- [ ] Host application starts successfully
- [ ] Discovery endpoint accessible
- [ ] Basic OAuth2 flows work
- [ ] No assembly loading issues

### Sample Applications Validation
- [ ] At least 80% of samples build and run
- [ ] All quickstart solutions work
- [ ] Modern client samples work
- [ ] Legacy client samples work (if possible)
- [ ] Authentication flows complete successfully
- [ ] No configuration errors in samples

### Build System Validation
- [ ] All build scripts work with .NET 8.0
- [ ] CI/CD pipeline functions correctly
- [ ] Package generation works
- [ ] Code signing works (if configured)
- [ ] Build times are acceptable (<10 minutes)

### Performance Validation
- [ ] Startup time ≤2 minutes
- [ ] Memory usage acceptable
- [ ] Request throughput ≥90% of baseline
- [ ] No significant performance regressions

### Security Validation
- [ ] Strong naming preserved
- [ ] No security vulnerabilities introduced
- [ ] OAuth2/OIDC compliance maintained
- [ ] Input validation works correctly
- [ ] Cryptographic functions work

## Common Issues and Solutions

### Issue: Compilation Errors After Framework Upgrade
**Symptoms**: Missing types, namespace changes, method signature changes
**Solution**:
1. Check breaking changes documentation for .NET 8.0
2. Update using statements for namespace changes
3. Update method calls for API changes
4. Verify all package references are compatible

### Issue: Test Failures After FluentAssertions Upgrade
**Symptoms**: Test assertion syntax errors, method not found errors
**Solution**:
1. Update from FluentAssertions 5.x to 6.x syntax
2. Most assertions remain the same, but some extension methods changed
3. Update test projects to use consistent assertion patterns

### Issue: Entity Framework Migration Problems
**Symptoms**: Database context errors, migration failures
**Solution**:
1. Backup existing migrations
2. Update EF Core package references
3. Update method calls (FromSql → FromSqlRaw)
4. Regenerate migrations with EF Core 8.0
5. Test database operations thoroughly

### Issue: Sample Application Runtime Errors
**Symptoms**: Configuration errors, dependency injection issues
**Solution**:
1. Verify all package references in sample projects
2. Check ASP.NET Core configuration changes
3. Update startup code for .NET 8.0 patterns
4. Test with minimal configuration first

### Issue: Build System Failures
**Symptoms**: Build script errors, tooling failures
**Solution**:
1. Update build project to .NET 8.0
2. Update Bullseye and SimpleExec packages
3. Verify build script compatibility
4. Test all build variants

## Rollback Procedures

### Emergency Rollback
```bash
# If critical issues arise:
git checkout main
git checkout -b rollback-001-dotnet8-upgrade
git tag rollback-$(date +%Y%m%d-%H%M%S)

# Verify everything works as before
./build.ps1 default
```

### Partial Rollback
```bash
# If specific component fails:
git revert [commit-hash] --no-edit
dotnet build
dotnet test
```

## Getting Help

### Resources
- [IdentityServer4 Documentation](https://identityserver4.readthedocs.io/)
- [.NET 8.0 Migration Guide](https://learn.microsoft.com/en-us/dotnet/core/compatibility/8.0)
- [Entity Framework Core 8.0 Breaking Changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/breaking-changes)

### Troubleshooting Steps
1. Check build logs for specific error messages
2. Verify package versions are compatible
3. Test with minimal changes first
4. Roll back problematic changes and re-apply incrementally
5. Consult documentation for specific components

## Success Metrics

Your upgrade is successful when:
- ✅ Core framework builds and all tests pass
- ✅ Host application runs without errors
- ✅ OAuth2/OIDC flows work correctly
- ✅ At least 80% of sample applications work
- ✅ Build system functions correctly
- ✅ Performance is within acceptable limits
- ✅ No security regressions introduced
- ✅ Documentation is updated

## Next Steps After Upgrade

1. **Test Thoroughly**: Run comprehensive test suites
2. **Update Documentation**: Update README files, guides, and API docs
3. **Performance Validation**: Run performance benchmarks
4. **Security Review**: Conduct security assessment
5. **Deploy to Staging**: Test in staging environment
6. **Plan Release**: Prepare release notes and deployment plan
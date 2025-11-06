# IdentityServer4 .NET 8.0 Upgrade Plan

## Executive Summary

**Complexity**: HIGH - Multiple project types, extensive sample infrastructure, major dependency upgrades
**Estimated Timeline**: 4-6 weeks for full migration
**Risk Level**: MEDIUM-HIGH - Primarily due to Entity Framework migration and extensive sample updates

**Current State**:
- Core libraries on .NET 6.0 ✅ (good foundation)
- Storage library on .NET Standard 2.1 ⚠️ (needs upgrade)
- Sample projects on .NET Core 3.1 🔴 (major issue)
- Build infrastructure on .NET Core 3.1 🔴 (needs upgrade)

## Current Project Analysis

### Core Components
- **IdentityServer4**: .NET 6.0 class library (OAuth2/OIDC framework)
- **IdentityServer4.Storage**: .NET Standard 2.1 (storage abstraction)
- **Host Application**: .NET 6.0 web application (test/sample host)
- **Test Projects**: .NET 6.0 (unit & integration tests)
- **Build Infrastructure**: .NET Core 3.1 (legacy build system)

### Sample Infrastructure (30+ Projects)
- **Quickstart Solutions**: 6 main solutions (18 projects total)
- **Client Samples**: 22 modern + 5 legacy client implementations
- **Key Management**: File-based and database key management samples
- **Target Framework**: All currently on .NET Core 3.1

### Critical Dependencies
- **IdentityModel**: v6.1.0 → v7.0.0+ (compatible)
- **ASP.NET Core Auth**: v6.0.16 → v8.0.0+ (compatible)
- **Serilog**: v3.2.0 → v8.0.0+ (breaking changes)
- **Entity Framework Core**: v3.1.x → v8.0.x+ (major breaking changes)
- **FluentAssertions**: v5.10.2 → v6.12.0+ (API changes)

## Phase 1: Foundation Upgrade (Week 1)
**Risk**: LOW | **Effort**: MEDIUM

### 1.1 Development Environment Setup
```powershell
# Update global.json to .NET 8.0
{
  "sdk": {
    "version": "8.0.100"
  }
}
```

### 1.2 Core Framework Updates
- **IdentityServer4.csproj**: `net6.0` → `net8.0`
- **IdentityServer4.Storage.csproj**: `netstandard2.1` → `net8.0`
- **Host.csproj**: `net6.0` → `net8.0`
- **Test projects**: `net6.0` → `net8.0`

### 1.3 Critical Package Updates
```xml
<!-- Core packages -->
<PackageReference Include="IdentityModel" Version="7.0.0" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.OpenIdConnect" Version="8.0.0" />
<PackageReference Include="Microsoft.IdentityModel.Protocols.OpenIdConnect" Version="8.0.0" />

<!-- Build infrastructure -->
<PackageReference Include="Bullseye" Version="5.0.0" />
<PackageReference Include="SimpleExec" Version="12.0.0" />
```

### 1.4 Build System Modernization
- **build.csproj**: `netcoreapp3.1` → `net8.0`
- Update build scripts for .NET 8.0 compatibility

## Phase 2: Testing Framework Migration (Week 2)
**Risk**: MEDIUM | **Effort**: MEDIUM

### 2.1 Test Package Updates
```xml
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
<PackageReference Include="xunit" Version="2.4.2" />
<PackageReference Include="xunit.runner.visualstudio" Version="2.4.5" />
<PackageReference Include="FluentAssertions" Version="6.12.0" />
<PackageReference Include="Microsoft.AspNetCore.TestHost" Version="8.0.0" />
```

### 2.2 API Migration for FluentAssertions
**Affected files**: All test projects
**Breaking changes**: v5.x → v6.x assertion syntax changes
**Effort**: Update assertion patterns in ~50+ test files

### 2.3 Test Validation
- Run unit tests with updated framework
- Fix any compilation errors
- Validate test coverage remains intact

## Phase 3: Major Dependencies Migration (Week 3)
**Risk**: HIGH | **Effort**: HIGH

### 3.1 Serilog Migration (CRITICAL)
**Current**: v3.2.0 → **Target**: v8.0.0
```xml
<PackageReference Include="Serilog.AspNetCore" Version="8.0.0" />
```

**Breaking Changes**:
- Logger configuration syntax changes
- Enricher API updates
- Sink configuration changes

**Affected Projects**:
- Host project
- EF Core samples
- Various client samples

### 3.2 Entity Framework Core Migration (CRITICAL 🔴)
**Current**: v3.1.x → **Target**: v8.0.x
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.0" />
```

**Major Breaking Changes**:
- **Query syntax changes**: `FromSqlRaw` vs `FromSql`
- **Configuration API**: `UseSqlServer` parameter changes
- **Migration infrastructure**: Complete redesign
- **Change tracking API**: Significant updates

**Affected Areas**:
- Key Management samples
- Identity quickstarts
- EF-based storage samples

**Effort**: HIGH - Requires code rewrite for data access layers

## Phase 4: Sample Projects Migration (Weeks 4-5)
**Risk**: MEDIUM | **Effort**: VERY HIGH

### 4.1 Framework Updates (30+ Projects)
**Current**: `netcoreapp3.1` → **Target**: `net8.0`

**Affected Areas**:
- 6 Quickstart solutions (18 projects)
- 22 Modern client samples
- 5 Legacy client samples
- Key Management samples
- Constants library

### 4.2 Package Reference Updates
```xml
<!-- Authentication packages -->
<PackageReference Include="Microsoft.AspNetCore.Authentication.OpenIdConnect" Version="8.0.0" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.Cookies" Version="2.2.0" />

<!-- JSON serialization -->
<PackageReference Include="System.Text.Json" Version="8.0.0" />

<!-- HTTP clients -->
<PackageReference Include="IdentityModel" Version="7.0.0" />
```

### 4.3 API Compatibility Fixes
- **JSON serialization**: Verify `JsonSerializerOptions` behavior
- **HTTP client patterns**: Ensure DI patterns work
- **Authentication middleware**: Update configuration syntax
- **Configuration binding**: Verify with new framework

### 4.4 Sample-Specific Issues
- **MVC samples**: Controller and view compatibility
- **Console clients**: HTTP client usage patterns
- **JavaScript clients**: No changes needed
- **API samples**: JWT validation patterns

## Phase 5: Validation & Testing (Week 6)
**Risk**: LOW | **Effort**: MEDIUM

### 5.1 Comprehensive Testing
- **Unit tests**: All pass with new framework
- **Integration tests**: OAuth2/OIDC flows work correctly
- **Sample applications**: All 30+ samples compile and run
- **End-to-end scenarios**: Complete authentication flows

### 5.2 Performance Validation
- Memory usage comparison
- Startup time benchmarks
- Token processing performance
- Database operation performance (EF upgrade impact)

### 5.3 Documentation Updates
- Update README files with new prerequisites
- Update sample documentation
- Update build instructions
- Update deployment guides

## Detailed Implementation Strategy

### Critical Path Dependencies
```
1. Core Framework (IdentityServer4) → 2 days
2. Storage Layer → 1 day
3. Build Infrastructure → 1 day
4. Test Framework Migration → 3 days
5. Serilog Migration → 2 days
6. Entity Framework Migration → 5 days
7. Sample Projects Migration → 8 days
8. Integration Testing → 3 days
```

### Risk Mitigation Strategies

#### High-Risk Areas:
1. **Entity Framework Migration**:
   - Create separate branch for EF work
   - Backup existing migrations
   - Test thoroughly before merging

2. **Sample Project Volume**:
   - Prioritize core quickstarts first
   - Batch similar projects together
   - Automated validation scripts

3. **Breaking Changes Cascade**:
   - Update packages incrementally
   - Test after each major package update
   - Maintain compatibility matrices

#### Rollback Strategy:
- Feature branch for each phase
- Tag working states frequently
- Automated build verification
- Performance benchmarks for comparison

## Pre-Upgrade Validation Checklist

### Environment Readiness
- [ ] .NET 8.0 SDK installed globally
- [ ] Visual Studio 2022 or VS Code updated
- [ ] CI/CD pipeline updated for .NET 8.0
- [ ] All NuGet sources accessible

### Code Quality
- [ ] All tests passing on current version
- [ ] No outstanding warnings
- [ ] Code coverage baseline established
- [ ] Performance benchmarks recorded

### Dependencies
- [ ] List all custom packages to update
- [ ] Verify third-party package .NET 8.0 support
- [ ] Check for any deprecated package usage
- [ ] Validate licensing for updated packages

## Post-Upgrade Validation Checklist

### Functionality
- [ ] All OAuth2/OIDC flows working
- [ ] Token issuance and validation working
- [ ] Client authentication working
- [ ] All sample applications running
- [ ] Database migrations applying correctly

### Quality Assurance
- [ ] All unit tests passing
- [ ] All integration tests passing
- [ ] Code coverage maintained
- [ ] Performance benchmarks met
- [ ] Security scanning passes

### Deployment Readiness
- [ ] Documentation updated
- [ ] Build scripts working
- [ ] CI/CD pipeline validated
- [ ] Deployment packages generated
- [ ] Release notes prepared

## Estimated Resource Requirements

### Development Team
- **1 Senior .NET Developer** (full-time, 6 weeks)
- **1 QA Engineer** (part-time, weeks 4-6)
- **1 DevOps Engineer** (part-time, weeks 1, 6)

### Infrastructure
- **Development Environment**: .NET 8.0 SDK, updated IDEs
- **CI/CD Pipeline**: Updated for .NET 8.0
- **Test Environments**: Multiple test setups for sample validation
- **Performance Testing**: Benchmarking infrastructure

### Timeline Buffer
- **Unforeseen Issues**: +1 week buffer
- **EF Migration Complexity**: May extend by 1-2 weeks
- **Sample Validation**: Additional week if issues found

## Success Criteria

### Must Have
1. All core IdentityServer4 functionality working on .NET 8.0
2. All tests passing with new framework
3. At least 80% of sample applications working
4. Performance comparable to .NET 6.0 version
5. CI/CD pipeline building and deploying successfully

### Should Have
1. All sample applications working
2. Documentation completely updated
3. Performance improvements from .NET 8.0
4. Zero security regressions
5. All legacy migration paths documented

### Could Have
1. Enhanced security features from .NET 8.0
2. Improved performance characteristics
3. Updated project templates
4. Modernized sample applications
5. Additional diagnostic capabilities

## Final Recommendation

**PROCEED WITH UPGRADE** - The project is well-structured and primarily needs framework and dependency updates. The main challenges are the volume of sample projects and the Entity Framework migration, but these are manageable with proper planning and phased approach.

### Key Success Factors:
1. Incremental migration approach
2. Comprehensive testing at each phase
3. Focus on core functionality first
4. Automated validation for sample projects
5. Performance monitoring throughout

This upgrade will modernize the codebase, provide access to .NET 8.0 improvements, and ensure long-term maintainability despite the project being unmaintained.

---

*This plan was created on 2025-11-01 and should be reviewed and updated as the migration progresses.*
# Build Baseline Metrics

## IdentityServer4 .NET 6.0 Build Baseline
**Date**: 2025-11-01
**SDK Version**: 9.0.306 (active)
**Target Framework**: .NET 6.0 (current)

### Quick Build Results (`build.ps1 quick`)
- **Total Duration**: 45.26 seconds
- **Storage Component**: 5.33 seconds
  - Build: 3.13s
  - Pack: 2.17s
- **IdentityServer4 Component**: 24.0 seconds
  - Build: 21.94s
  - Pack: 2.06s
- **Status**: ✅ SUCCESS

### Performance Targets for .NET 8.0 Upgrade
- **SC-001**: Core framework libraries build within 2 minutes each
- **SC-005**: Full build pipeline under 10 minutes
- **SC-007**: Maintain ≥90% of current throughput

### Baseline Status
- ✅ Build system working correctly
- ✅ All components compiling successfully
- ✅ Package generation working
- ✅ Baseline established for comparison

**Note**: Current build uses .NET 9.0.306 SDK but targets .NET 6.0 framework.
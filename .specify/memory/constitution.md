<!--
Sync Impact Report:
Version change: 0.0.0 → 1.0.0 (initial constitution)
Modified principles: None (initial creation)
Added sections: Core Principles, Security Requirements, Development Workflow, Governance
Removed sections: None
Templates requiring updates: ✅ plan-template.md (validated), ✅ spec-template.md (validated), ✅ tasks-template.md (validated)
Follow-up TODOs: None
-->

# IdentityServer4 Constitution

## Core Principles

### I. Security-First Design
Every component MUST prioritize security, implementing defense-in-depth principles. All assemblies are strongly named, input validation is comprehensive, and protection against common web vulnerabilities is mandatory. OAuth2/OIDC protocol compliance is non-negotiable - any deviation from RFC specifications requires explicit security review and justification.

### II. Storage Abstraction Layer
All data persistence MUST use interface-based storage abstraction enabling pluggable storage providers. The storage layer (IdentityServer4.Storage) provides clean separation between framework logic and data persistence. Direct database access is prohibited - all storage operations must go through defined interfaces (IClientStore, IResourceStore, IPersistedGrantStore, etc.).

### III. Modular Architecture with Dependency Injection
The framework MUST maintain modular design with separate concerns for endpoints, services, stores, and validation. Extensive use of ASP.NET Core dependency injection with extension methods is required. Each endpoint class handles a specific OAuth2/OIDC flow, promoting maintainability and testability.

### IV. Comprehensive Testing Discipline
Test-First Development is mandatory. xUnit with Fluent Assertions provides the testing framework. High coverage across all components is required, including unit tests, integration tests, and conformance tests. The host application serves as both test implementation and manual testing environment. All protocol flows must be verified by automated tests.

### V. Protocol Compliance and Interoperability
Strict adherence to OAuth2 and OpenID Connect specifications is required. All implementations must be interoperable with other compliant implementations. Protocol validation includes comprehensive test suites covering all grant types (Authorization Code, Client Credentials, Hybrid, Implicit). Any protocol extension must be optional and backward compatible.

### VI. Event-Driven Observability
Comprehensive event logging system with Serilog integration is mandatory. All security-relevant events MUST be logged with appropriate detail levels. Structured logging enables debugging and audit capabilities. Event infrastructure supports diagnostic tracing without compromising security or performance.

## Security Requirements

### Cryptographic Standards
All cryptographic operations MUST use .NET's built-in cryptographic providers or approved libraries. Certificate-based authentication support is mandatory. Key management includes both file-based and database key storage options with proper rotation capabilities.

### Validation and Input Sanitization
Every input MUST be validated and sanitized according to OWASP guidelines. URL validation, redirect URI validation, and parameter validation are critical security gates. All validation failures must be properly logged and handled without leaking sensitive information.

### Transport Security
All production deployments MUST use HTTPS with proper certificate validation. Transport security requirements extend to all client communications, token endpoints, and discovery endpoints. Insecure transports are only permitted for development environments with explicit configuration.

## Development Workflow

### Build System Requirements
Cross-platform build system using Bullseye + SimpleExec is mandatory. All assemblies must be strongly named with key.snk. Build variants include development builds (quick), full builds with tests (default), and signed builds (sign). Package output is standardized to ./nuget directory.

### Code Quality Gates
All changes MUST pass:
1. Successful compilation across all target frameworks
2. All unit tests passing with maintained coverage
3. All integration tests passing
4. Protocol conformance tests passing
5. Security scanning passing
6. Performance benchmarks meeting baseline requirements

### Migration and Upgrade Path
Framework migrations (e.g., .NET 6.0 → .NET 8.0) must maintain backward compatibility. Breaking changes require version bump according to semantic versioning. Sample applications must be updated alongside core framework changes. Migration plans must be documented and tested.

## Governance

This constitution supersedes all other development practices and guidelines. All code reviews, pull requests, and implementations must verify compliance with these principles. Complexity must be justified against these core principles.

### Amendment Process
Constitution amendments require:
1. Written proposal documenting the change and its rationale
2. Impact analysis on existing code and samples
3. Community review period (minimum 14 days for major changes)
4. Formal approval and documentation update
5. Migration plan for existing non-compliant code

### Versioning Policy
Constitution versions follow semantic versioning:
- **MAJOR**: Backward incompatible governance changes or principle removals
- **MINOR**: New principles or sections added, materially expanded guidance
- **PATCH**: Clarifications, wording improvements, non-semantic refinements

### Compliance Review
All changes must be reviewed against constitution principles. Non-compliance requires explicit waiver documentation with technical justification and remediation timeline. Runtime development guidance is provided in CLAUDE.md for agent-specific instructions.

**Version**: 1.0.0 | **Ratified**: 2025-11-01 | **Last Amended**: 2025-11-01
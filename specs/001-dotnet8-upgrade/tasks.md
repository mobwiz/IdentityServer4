---

description: "Task list for IdentityServer4 .NET 8.0 upgrade implementation"
---

# Tasks: IdentityServer4 .NET 8.0 Upgrade

**Input**: Design documents from `/specs/001-dotnet8-upgrade/`
**Prerequisites**: plan.md (required), spec.md (required for user stories), research.md, data-model.md, contracts/

**Tests**: Tests are included based on the comprehensive testing requirements in the specification (95%+ coverage requirement).

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2, US3, US4)
- Include exact file paths in descriptions

## Path Conventions

- **Core Framework**: `src/IdentityServer4/src/`, `src/Storage/src/`
- **Host Application**: `src/IdentityServer4/host/`
- **Tests**: `src/IdentityServer4/test/`, `src/Storage/test/`
- **Samples**: `samples/`
- **Build**: `src/IdentityServer4/build/`, `src/Storage/build/`, `build/`

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Environment preparation and baseline establishment

- [ ] T001 Backup current working state and create development baseline
- [ ] T002 [P] Verify .NET 8.0 SDK installation and update global.json
- [ ] T003 [P] Update development environment (Visual Studio 2022, VS Code extensions)
- [ ] T004 Run current build to establish baseline metrics and performance

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

- [ ] T005 Create upgrade project tracking and validation infrastructure
- [ ] T006 [P] Set up comprehensive test validation framework
- [ ] T007 [P] Configure performance monitoring and baseline measurement
- [ ] T008 [P] Set up security validation and protocol compliance checking
- [ ] T009 Configure build system monitoring and validation
- [ ] T010 [P] Set up dependency compatibility validation framework

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - Core Framework Upgrade (Priority: P1) 🎯 MVP

**Goal**: Upgrade IdentityServer4 and Storage libraries to .NET 8.0 while maintaining functionality and test coverage

**Independent Test**: Build core libraries and run unit tests to ensure ≥95% pass rate without functionality regression

### Tests for User Story 1 (Mandatory - 95%+ coverage requirement) ⚠️

> **NOTE: Tests run continuously throughout implementation to validate no regressions**

- [ ] T011 [P] [US1] Establish unit test baseline for IdentityServer4 in src/IdentityServer4/test/
- [ ] T012 [P] [US1] Establish unit test baseline for Storage in src/Storage/test/
- [ ] T013 [P] [US1] Set up integration test framework for protocol conformance validation
- [ ] T014 [P] [US1] Configure performance baseline tests for core framework components

### Implementation for User Story 1

- [ ] T015 [US1] Update IdentityServer4.csproj target framework to net8.0 in src/IdentityServer4/src/IdentityServer4.csproj
- [ ] T016 [US1] Update IdentityServer4.Storage.csproj target framework to net8.0 in src/Storage/src/IdentityServer4.Storage.csproj
- [ ] T017 [US1] Update test project target frameworks to net8.0 in src/IdentityServer4/test/ and src/Storage/test/
- [ ] T018 [P] [US1] Update core package references in IdentityServer4 project
- [ ] T019 [P] [US1] Update core package references in Storage project
- [ ] T020 [US1] Update test package references (xUnit, FluentAssertions) in test projects
- [ ] T021 [US1] Build and resolve compilation errors in core framework
- [ ] T022 [US1] Fix FluentAssertions API changes from v5.x to v6.x in test projects
- [ ] T023 [US1] Run unit tests and address any test failures
- [ ] T024 [US1] Validate strong naming and assembly signing in IdentityServer4 project
- [ ] T025 [US1] Validate storage interface compatibility and test with different providers, ensuring all breaking changes are properly abstracted
- [ ] T026 [US1] Run comprehensive test suite and verify ≥95% pass rate
- [ ] T026a [US1] Validate test coverage meets constitutional 95%+ requirement using coverage tools
- [ ] T027 [US1] Validate OAuth2/OIDC protocol compliance with conformance tests
- [ ] T028 [US1] Performance validation - ensure ≥90% of baseline throughput maintained

**Checkpoint**: User Story 1 should be fully functional and testable independently - Core MVP ready for validation

---

## Phase 4: User Story 2 - Sample Applications Migration (Priority: P2)

**Goal**: Upgrade 30+ sample applications to .NET 8.0 with ≥80% success rate

**Independent Test**: Build and run sample applications to verify they compile and execute correctly

### Tests for User Story 2 (Functional validation required)

- [ ] T029 [P] [US2] Set up sample application validation framework
- [ ] T030 [P] [US2] Configure OAuth2/OIDC flow testing for sample validation

### Implementation for User Story 2

- [ ] T031 [P] [US2] Update quickstart solution projects to net8.0 in samples/quickstarts/
- [ ] T032 [P] [US2] Update modern client sample projects to net8.0 in samples/src/
- [ ] T033 [P] [US2] Update legacy client sample projects to net8.0 in samples/old/
- [ ] T034 [P] [US2] Update package references in all sample projects
- [ ] T035 [US2] Build all quickstart solutions and resolve compilation errors
- [ ] T036 [US2] Build all modern client samples and resolve compilation errors
- [ ] T037 [US2] Build all legacy client samples and resolve compilation errors
- [ ] T038 [US2] Test quickstart authentication flows (Authorization Code, Hybrid, Implicit)
- [ ] T039 [US2] Test client authentication scenarios (confidential, public, device flows)
- [ ] T040 [US2] Validate sample application success rate meets ≥80% target
- [ ] T041 [US2] Document any samples that cannot be upgraded and provide migration guidance

**Checkpoint**: User Stories 1 AND 2 should both work independently and provide comprehensive upgrade validation

---

## Phase 5: User Story 3 - Build System Modernization (Priority: P2)

**Goal**: Upgrade build infrastructure to .NET 8.0 for improved CI/CD reliability

**Independent Test**: Run all build scripts and verify they produce expected output packages

### Tests for User Story 3 (Build validation required)

- [ ] T042 [P] [US3] Set up build system validation framework
- [ ] T043 [P] [US3] Configure package generation and signing validation

### Implementation for User Story 3

- [ ] T044 [US3] Update build.csproj target framework to net8.0 in build/build.csproj
- [ ] T045 [US3] Update Bullseye and SimpleExec package references in build project
- [ ] T046 [US3] Update build scripts for .NET 8.0 compatibility in src/IdentityServer4/build/
- [ ] T047 [US3] Update build scripts for .NET 8.0 compatibility in src/Storage/build/
- [ ] T048 [US3] Test build.ps1 quick (development build) execution
- [ ] T049 [US3] Test build.ps1 default (full build with tests) execution
- [ ] T050 [US3] Validate package generation works correctly in ./nuget directory
- [ ] T051 [US3] Test build.ps1 sign (signed build) if signing is configured
- [ ] T052 [US3] Validate build system performance meets <10 minute target

**Checkpoint**: Build system modernization complete - all build variants working correctly

---

## Phase 6: User Story 4 - Dependency Updates (Priority: P3)

**Goal**: Update all NuGet package references to .NET 8.0 compatible versions

**Independent Test**: Run application with updated dependencies and verify all features work correctly

### Tests for User Story 4 (Compatibility validation required)

- [ ] T053 [P] [US4] Set up dependency compatibility testing framework
- [ ] T054 [P] [US4] Configure logging validation for Serilog upgrade testing

### Implementation for User Story 4

- [ ] T055 [US4] Update Entity Framework Core packages to v8.0.0 in host project
- [ ] T056 [US4] Update Serilog packages to v8.0.0 in host project
- [ ] T057 [US4] Fix Entity Framework breaking changes (FromSql → FromSqlRaw)
- [ ] T058 [US4] Update Serilog configuration syntax for v8.0.0
- [ ] T059 [US4] Regenerate EF Core migrations for .NET 8.0 compatibility
- [ ] T060 [US4] Update remaining NuGet package references across all projects
- [ ] T061 [US4] Test Entity Framework database operations with new version
- [ ] T062 [US4] Test Serilog logging functionality with new syntax
- [ ] T063 [US4] Validate all dependency updates work without conflicts
- [ ] T064 [US4] Run comprehensive application testing with all dependencies updated

**Checkpoint**: All user stories should now be independently functional with fully updated dependencies

---

## Phase 7: Polish & Cross-Cutting Concerns

**Purpose**: Integration testing, performance optimization, and final validation

- [ ] T065 [P] Run comprehensive end-to-end integration tests across all components
- [ ] T066 [P] Performance benchmarking and optimization (target ≥90% baseline)
- [ ] T067 [P] Security validation and vulnerability scanning
- [ ] T068 [P] Documentation updates for .NET 8.0 upgrade changes
- [ ] T069 Update README files with new prerequisites and build instructions
- [ ] T070 [P] Create migration guide documentation for users upgrading
- [ ] T071 Final validation of all success criteria from specification
- [ ] T072 Prepare release notes and deployment documentation

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
- **User Stories (Phase 3-6)**: All depend on Foundational phase completion
  - User Stories can proceed in parallel (if staffed) or sequentially in priority order
  - US1 (P1) must complete before US2, US3, US4 can provide full value
  - US2 and US3 can proceed in parallel after US1
  - US4 (P3) can proceed independently after US1
- **Polish (Phase 7)**: Depends on all desired user stories being complete

### User Story Dependencies

- **User Story 1 (P1)**: Can start after Foundational - No dependencies on other stories
- **User Story 2 (P2)**: Can start after Foundational and US1 - Core framework required
- **User Story 3 (P2)**: Can start after Foundational - Independent of other stories
- **User Story 4 (P3)**: Can start after Foundational and US1 - Framework required

### Within Each User Story

- Tests must run continuously throughout implementation
- Build and compile tasks before functionality testing
- Core framework before samples and infrastructure
- Dependency updates before final validation

### Parallel Opportunities

- All Setup tasks marked [P] can run in parallel
- All Foundational tasks marked [P] can run in parallel
- Within US1: Package updates [P] can run in parallel
- Within US2: Sample project updates [P] can run in parallel
- Within US3: Build script updates [P] can run in parallel
- Within US4: Dependency updates [P] can run in parallel
- Polish phase tasks [P] can run in parallel

---

## Parallel Example: User Story 1 (Core Framework)

```bash
# Launch all package updates for User Story 1 together:
Task: "Update core package references in IdentityServer4 project"
Task: "Update core package references in Storage project"
Task: "Update test package references (xUnit, FluentAssertions) in test projects"

# Launch build tasks in parallel after packages complete:
Task: "Build and resolve compilation errors in core framework"
Task: "Validate strong naming and assembly signing in IdentityServer4 project"
Task: "Validate storage interface compatibility and test with different providers"
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup
2. Complete Phase 2: Foundational (CRITICAL - blocks all stories)
3. Complete Phase 3: User Story 1 - Core Framework Upgrade
4. **STOP and VALIDATE**: Test User Story 1 independently
5. Deploy/demo core framework upgrade if ready

### Incremental Delivery

1. Complete Setup + Foundational → Foundation ready
2. Add User Story 1 → Test independently → Core framework MVP
3. Add User Story 2 + User Story 3 (parallel) → Test independently → Samples + Build
4. Add User Story 4 → Test independently → Dependencies updated
5. Each story adds value without breaking previous stories

### Parallel Team Strategy

With multiple developers:

1. Team completes Setup + Foundational together
2. Once Foundational is done:
   - Developer A: User Story 1 (Core Framework)
   - Developer B: User Story 3 (Build System)
   - Developer C: User Story 4 (Dependencies)
3. After US1 complete:
   - Developer A: User Story 2 (Samples)
4. Stories complete and integrate independently

---

## Success Criteria Validation

Each phase and user story includes validation tasks to ensure success criteria are met:

- **SC-001**: Build time validation in T052 (build system)
- **SC-002**: Test pass rate validation in T026 (core framework)
- **SC-003**: Protocol compliance validation in T027 (core framework)
- **SC-004**: Sample success rate validation in T040 (samples)
- **SC-005**: Build pipeline validation in T050-T052 (build system)
- **SC-006**: Dependency upgrade validation in T063 (dependencies)
- **SC-007**: Performance benchmarking in T066 (polish phase)
- **SC-008**: Security validation in T067 (polish phase)

---

## Notes

- [P] tasks = different files, no dependencies
- [Story] label maps task to specific user story for traceability
- Each user story should be independently completable and testable
- Tests run continuously throughout implementation, not just at the end
- Success criteria validation is built into each phase
- 95%+ test coverage requirement drives continuous test execution
- Stop at any checkpoint to validate story independently
- Avoid: vague tasks, missing file paths, cross-story dependencies that break independence
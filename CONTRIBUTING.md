# Contributing to SimuNEX

We are thrilled that you're interested in contributing to SimuNEX! To ensure a smooth contribution process for everyone, we've established some guidelines.

## How to Contribute

If you're not a member of the organization and wish to contribute:

1. Fork the repository.
2. Create a new branch in your fork for your changes.
3. Make your changes following the commit message and coding guidelines below.
4. Open a pull request using the guidelines mentioned in the [PR template](/.github/pull_request_template.md).

## Commit Message Guidelines

We follow Semantic Versioning (SemVer) and our commit messages reflect this. Each commit message consists of a header, a body, and a footer. The header has a special format that includes a type, an optional scope, and a subject:

```
<type>(<scope>): <subject>
```

The `<type>` and `<subject>` are mandatory, while `<scope>` is optional.

### Type

Must be one of the following:

- **feat**: A new feature
- **fix**: A bug fix
- **docs**: Documentation only changes
- **style**: Changes that do not affect the meaning of the code (white-space, formatting, missing semi-colons, etc)
- **refactor**: A code change that neither fixes a bug nor adds a feature
- **perf**: A code change that improves performance
- **test**: Adding missing tests or correcting existing tests

### Scope

The scope could be anything specifying the place of the commit change. For example:

- **SimuNEX**: Types or Unity Editor changes, or areas that cannot be classified elsewhere
- **dynamics**: Physics-related changes (including loads)
- **faults**: Fault functionality
- **legal**: Legal changes like LICENSE (restricted to the owner)
- **communication**: Communication features and protocols
- **environment**: Environment creation tooling and visualization
- **actuator** / **<actuator_name>**: Actuator-related code
- **sensor** / **<sensor_name>**: Sensor-related code
- **alarm**: Alarm-based code
- **system** / **<system_name>**: System-related code such as quadcopter, AUV
- **<dependency_name>**: Dependency-specific changes, such as Eigen3, ROS2, etc.

### Subject

A concise summary of the changes. Follow these rules for clarity:
  - If the subject is too long, skip a line after the first line to add more details. The first line should summarize the change well, while the additional lines can describe more changes or modifications.
  - Capitalization is optional.
  - End with a period if you prefer.

### Examples

```
feat(environment): Implement wind resistance calculation.
```

```
fix(sensor): Correct temperature reading offset.
Fixes the issue where the sensor readout was 5 degrees too high.
```

### Documentation Changes

For documentation changes, use `docs` as the type, and the file name as the scope:

```
docs(README): update installation instructions
```

## Coding Style

To maintain the consistency and quality of our codebase, please adhere to the coding styles defined in `.editorconfig` and the Roslyn analyzer. We recommend using Visual Studio with its code cleanup tools to manage coding style automatically. This will help ensure that your contributions match the project's coding standards.

## Code of Conduct

Please note that this project is released with a [Contributor Code of Conduct](CODE_OF_CONDUCT.md). By participating in this project you agree to abide by its terms.
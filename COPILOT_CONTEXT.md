# TaskManager - Development Guidelines

## Project Purpose

This is an educational WPF .NET 8 project built to:

- Practice clean MVVM architecture
- Apply SOLID principles
- Follow clean code guidelines
- Maintain proper separation of concerns
- Use Git with professional commit messages
- Write well-documented and maintainable code

All code suggestions should include explanations.

---

## Architecture

We follow a clean MVVM structure:

- Models → Domain entities only. No UI logic.
- ViewModels → Presentation logic only.
- Views → Pure XAML, no business logic.
- Services → Persistence and external operations.
- Commands → ICommand implementations.
- Infrastructure → Shared utilities.

---

## Design Principles

When suggesting code:

- Follow SOLID principles.
- Avoid code-behind logic in Views.
- Prefer immutability when possible.
- Use dependency injection where appropriate.
- Keep classes focused on a single responsibility.
- Avoid static classes unless justified.

---

## Code Style

- Follow .editorconfig rules.
- Use explicit types instead of `var` when clarity improves readability.
- Add XML documentation to public classes and methods.
- Prefer UTC dates.
- Avoid magic strings.

---

## Educational Mode

When generating code:

- Always explain what the code does.
- Explain why the design choice is correct.
- Mention which SOLID principle applies.
- Suggest possible improvements if relevant.

---

## Technical Stack

- WPF (.NET 8)
- MVVM without external frameworks
- JSON persistence (System.Text.Json)
- No third-party libraries initially


# Service Desk Mini — No MCP

A small Blazor WebAssembly support-ticket dashboard generated for the same Ignite UI for Blazor task, but **without Ignite UI MCP guidance** or library-specific agent tooling.

This repo is part of a comparison between an AI-generated Ignite UI app built **with MCP support** and the same task generated **without MCP support**.

## Purpose

This version shows the baseline result when the AI assistant is asked to build an Ignite UI for Blazor app without access to Ignite UI MCP servers, skills, agents, or additional library-specific guidance.

## Built With

- Blazor WebAssembly
- Ignite UI for Blazor requirement
- No MCP guidance
- No Ignite UI-specific agent tooling
- LocalStorage-based mock data

## Ignite UI Setup

This project is a minimal Blazor WebAssembly app with Ignite UI for Blazor wired in.

- `IgniteUI.Blazor` is referenced at version `25.2.32`.
- The Bootstrap light theme is loaded from `_content/IgniteUI.Blazor/themes/light/bootstrap.css`.
- Shared component imports live in `IgApplication/_Imports.razor`.
- Ignite UI component modules are registered in `IgApplication/Program.cs`.
- `NuGet.config` includes the Infragistics licensed feed required for current Ignite UI packages.

When adding a new Ignite UI component, add its module to `AddIgniteUIBlazor(...)` and the component will use the loaded theme styles.

## Features

- Dashboard page with KPI cards
- Ticket status and priority views
- Tickets page with ticket management functionality
- Add, edit, delete, assign, and update ticket status
- Mock data persisted in LocalStorage
- Demo-ready UI generated from the same task requirements

## Comparison Context

This repository represents the **no-MCP** result from the benchmark.

The goal was to evaluate how well the AI assistant handles Ignite UI-specific requirements without extra tooling, including:

- Correct component selection
- Correct API usage
- Theming consistency
- Feature completeness
- Build/runtime correctness
- Required correction effort

## Notes

This is a demo/benchmark application. It uses mock data only and does not include a backend.

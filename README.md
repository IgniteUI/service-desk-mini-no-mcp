# Ignite UI Blazor Starter

This is a minimal Blazor WebAssembly starter with Ignite UI for Blazor wired in.

- `IgniteUI.Blazor` is referenced at version `25.2.32`.
- The Bootstrap light theme is loaded from `_content/IgniteUI.Blazor/themes/light/bootstrap.css`.
- Shared component imports live in `IgApplication/_Imports.razor`.
- Ignite UI component modules are registered in `IgApplication/Program.cs`.
- `NuGet.config` includes the Infragistics licensed feed required for current Ignite UI packages.

When adding a new Ignite UI component, add its module to `AddIgniteUIBlazor(...)` and the component will use the loaded theme styles.

# FastUI

FastUI is a lightweight, modern UI toolkit for WinForms, designed to bring clean visuals, reusable styling, and structured theming to traditional desktop applications — without external dependencies or designer complexity.

FastUI focuses on productivity, clarity, and consistency, allowing developers to build elegant WinForms tools faster while keeping full control over behavior and rendering.

---

## Key Goals

- Modernize WinForms visuals without abandoning its performance
- Provide reusable, preset-based styling across components
- Keep APIs simple, explicit, and developer-friendly
- Avoid external UI frameworks or heavy dependencies

---

## Core Concepts

### 1. Custom Rendering Engine

FastUI uses its own lightweight rendering engine to draw:

- Backgrounds
- Borders
- Rounded corners
- Hover / focus / press states

This ensures full visual control and consistent behavior across all components.

---

### Clear & Human-Friendly Properties

FastUI replaces confusing or scattered WinForms properties with clear, readable, and well-organized alternatives.

Examples:

- `TextColor` instead of `ForeColor`
- `MoveTextHorizontal` instead of `TextOffsetX`
- `RowHeight` instead of `RowTemplate.Height`
- `TableColor` instead of managing:

  - `ColumnHeadersDefaultCellStyle.BackColor`
  - `SelectionBackColor`
  - `RowsDefaultCellStyle.BackColor`
  - `BackgroundColor`
  - `GridColor`

All properties are grouped into logical categories, clearly named, and easy to discover in the designer.

---

### 2. Smart Input System

FastUI includes a set of smart input controls designed to eliminate repetitive validation logic.

Features include:

- Required field handling
- Auto-validation
- Formatting rules
- Custom masking and input restrictions

This saves development time and avoids rewriting the same validation logic across projects.

Examples:

- Email input with built-in validation
- Date input with strict formats (YYYY-MM-DD)
- Time input with automatic colon insertion (HH:mm)
- Password input with internal policies exposed through simple properties:

  - Forbidden passwords list (`string[]`)
  - Minimum length enforcement
  - Optional complexity mode (upper, lower, digit, symbol)

---

### 3. Preset-Based Theming System

FastUI introduces a preset-based theming architecture.

Instead of styling controls individually, themes provide presets that describe how each component should look.

Each theme can define presets for:

- Buttons
- TextBoxes
- ComboBoxes
- Tables

This approach makes styling:

- Consistent
- Reusable
- Easy to extend

---

### Built-in Themes

FastUI ships with several ready-to-use themes:

- Windows11 — clean and native-like
- Google Material — bold colors and rounded surfaces
- Apple — minimal and soft UI
- Mayora — custom design example

Themes can be applied per control or across the entire application.

---

## Example: Applying a Theme

```csharp
fuiButton1.Theme = "Windows11";
fuiTextBox1.Theme = "GoogleMaterial";
fuiTable1.Theme = "Apple";
```

Themes are resolved by name using the internal theme registry.

---

## Available Components

Core Components:

- **FuiButton** — modern button with hover, press, and animation support
- **FuiTextBox** — custom single-line input with placeholder, caret, and validation rules
- **FuiComboBox** — styled dropdown with hover and focus behavior
- **FuiTable** — DataGridView wrapper with modern styling, row effects, and unified alignment
- **FuiPanel** — panel with rounded corners and custom rendering

Smart Input Components:

- **FuiEmail** — email validation
- **FuiPhoneDz** — Algerian phone number validation
- **FuiPhoneEgypt** — Egyptian phone number validation
- **FuiPhoneUSA** — US phone number validation
- **FuiDate** — strict date format (YYYY-MM-DD)
- **FuiTime** — smart time input (HH:mm)
- **FuiCreditCardNumber** — strict 16-digit card input
- **FuiCreditCardCVV** — strict 3-digit CVV
- **FuiCreditCardDate** — expiry date (MM/YY)
- **FuiPassword** — advanced password component with policy support

All native WinForms functionality remains accessible.

---

## Extending FastUI

### Creating a Custom Theme

To create your own theme, implement the theme interface and provide presets:

```csharp
public class MyCustomTheme : IFuiTheme
{
    public ButtonPreset GetButtonPreset() => new ButtonPreset { /* values */ };
    public TextBoxPreset GetTextBoxPreset() => new TextBoxPreset { /* values */ };
    public ComboBoxPreset GetComboBoxPreset() => new ComboBoxPreset { /* values */ };
    public TablePreset GetTablePreset() => new TablePreset { /* values */ };
}
```

Then register it once:

```csharp
FuiThemeRegistry.Register("MyTheme", new MyCustomTheme());
```

Your theme will automatically appear in the designer theme list.

---

## Open Source

FastUI is fully open source and free to use.

- No licensing fees
- Can be extended or modified freely
- Designed to be built upon

The codebase is fully documented, and each component includes clear inline documentation to help developers understand behavior quickly.

---

## Philosophy

FastUI does not try to replace WinForms.

It enhances it.

The goal is to keep WinForms:

- Fast
- Explicit
- Predictable

while offering a modern UI experience and a clean architectural foundation.

---

## NuGet Package

FastUI is available on NuGet and can be installed directly into any WinForms project.

**Package ID:** `FastUI.WinForms`

Using Package Manager:

```powershell
Install-Package FastUI.WinForms
```

Using .NET CLI:

```bash
dotnet add package FastUI.WinForms
```

- No external dependencies
- Works with .NET Framework and modern .NET
- Fully designer-friendly

---

### Framework Support

FastUI.WinForms supports both **.NET Framework 4.8** and **modern .NET (8.0+)**, making it suitable for legacy WinForms applications as well as new projects.

---

## License

MIT License

---

FastUI.WinForms — Write less UI code. Reuse more logic.
Build cleaner WinForms apps.

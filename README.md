![FastUI Model](Assets/FastUIModel3.png)

---

# FastUI — Modern UI Toolkit for WinForms

FastUI is a lightweight, modern toolkit that brings clean, reusable, and elegant components to WinForms — powered by its own rendering engine and a growing library of smart input controls.

Born from a backend developer’s need for clean testing tools, FastUI evolved into a flexible mini-framework that makes WinForms development faster, cleaner, and far more modern.

---

## Why FastUI?

WinForms is fast — but visually outdated. FastUI bridges that gap by providing:

- Ready-made modern controls
- Built-in input validation
- Reusable styling and rendering
- Simple, readable, human-friendly API
- Zero designer complications
- No external dependencies

FastUI focuses on productivity and clarity:

"Write less UI code. Reuse more logic. Build faster tools."

---

## Core Features

### Custom Rendering Engine

A rendering engine designed for full control over shapes, borders, animations, and modern UI behavior.

### Smart Input System

Input controls that support:

- Required fields
- Auto-validation
- Formatting rules
- Custom masking and input restrictions

### Extended Component Library

A complete collection of ready-to-use building blocks.

### Clean API

Readable, direct, and easy to integrate.

---

## Available Components (15+)

### Core Controls

| Component   | Description                                     |
| ----------- | ----------------------------------------------- |
| FuiButton   | Modern button with hover and press animations   |
| FuiPanel    | Panel with rounded corners and custom rendering |
| FuiComboBox | Styled dropdown with custom popup               |
| FuiTable    | Data table with clean rows, columns, and APIs   |
| FuiTextBox  | Base modern text input with full control        |

---

### Validated Inputs (Smart Controls)

All components below are built on top of **FuiValidatedTextBox** and include their own validation logic.

| Component           | Description                                             |
| ------------------- | ------------------------------------------------------- |
| FuiEmail            | Email validation                                        |
| FuiPhoneDz          | Algerian phone number validation                        |
| FuiPhoneEgypt       | Egyptian phone number validation                        |
| FuiPhoneUSA         | US phone number validation                              |
| FuiDate             | Strict date format (YYYY-MM-DD)                         |
| FuiTime             | Smart time input (HH:mm) with automatic colon insertion |
| FuiCreditCardNumber | Strict 16-digit card input                              |
| FuiCreditCardCVV    | Strict 3-digit CVV                                      |
| FuiCreditCardDate   | Expiry date (MM/YY)                                     |
| FuiAddress          | General purpose address field                           |
| FuiPassword         | Advanced password component with policy support         |

---

## Password Component Enhancements

FuiPassword now includes:

- Real text masking
- Forbidden passwords list (string[])
- Minimum length enforcement
- Optional complexity mode (upper, lower, digit, symbol)
- Secure input handling
- Structured validation methods for clarity

---

## Validation Framework

All validated components support:

- Required field handling
- Custom error messages
- Automatic invalid styling
- Organized and override-ready validation methods

This enables consistent and reliable forms in WinForms applications.

---

## Goals

- Modernize WinForms with minimal complexity
- Provide reusable UI components
- Reduce repetitive form logic
- Enable fast prototyping and testing
- Keep the library clean, extensible, and open-source

---

## Project Status

FastUI is stable for:

- Internal tools
- Testing utilities
- Admin dashboards
- Lightweight business applications

More components and improvements are planned as the rendering engine evolves.

---

## Contribute

FastUI is open-source. Developers are encouraged to fork, extend, and contribute to its growth.

More documentation and examples will be added as the project progresses.

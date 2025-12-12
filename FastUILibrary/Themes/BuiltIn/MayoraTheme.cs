using FastUI.FastUILibrary.Themes.Interfaces;
using FastUI.FastUILibrary.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastUI.FastUILibrary.Themes.Presets;

namespace FastUI.FastUILibrary.Themes.BuiltIn
{
    /// <summary>
    /// Built-in FastUI theme inspired by Mayora brand identity.
    /// 
    /// This theme focuses on:
    /// - Strong brand colors
    /// - Rounded, friendly UI elements
    /// - High contrast for call-to-action components
    /// - Clean and modern enterprise look
    /// </summary>
    public class MayoraTheme : IFuiTheme
    {
        /// <summary>
        /// Returns the button visual preset styled according to
        /// Mayora branding guidelines.
        /// </summary>
        public ButtonPreset GetButtonPreset() => new ButtonPreset
        {
            // ========================================================
            // TEXT
            // ========================================================

            FontColor = Color.White,
            FontSize = 12f,
            MoreFontSettings = new Font("Segoe UI", 12f, FontStyle.Bold),
            TextPosition = FastTextAlign.Center,

            // ========================================================
            // LAYOUT
            // ========================================================

            ControlWidth = 125,
            ControlHeight = 36,

            // ========================================================
            // NORMAL COLORS
            // ========================================================

            FillColor = Color.FromArgb(255, 188, 1),
            BorderColor = Color.FromArgb(255, 188, 1),

            // ========================================================
            // HOVER COLORS
            // ========================================================

            HoverFillColor = Color.FromArgb(255, 200, 40),
            HoverBorder = Color.FromArgb(255, 200, 40),
            HoverTextColor = Color.White,

            // ========================================================
            // PRESS COLORS
            // ========================================================

            PressFillColor = Color.Orange,
            PressBorderColor = Color.Orange,
            PressDepth = 1,

            // ========================================================
            // STYLE
            // ========================================================

            BorderWidth = 0f,
            CornerRadius = 16f
        };

        /// <summary>
        /// Returns the textbox visual preset styled according to
        /// Mayora clean and modern input design.
        /// </summary>
        public TextBoxPreset GetTextBoxPreset() => new TextBoxPreset
        {
            // ========================================================
            // A) TEXT
            // ========================================================

            FontSize = 12f,
            TextAlignment = FastTextAlign.Left,
            MoveTextHorizontal = 8,
            MoveTextVertical = 0,
            TextColor = Color.Black,

            // ========================================================
            // B) PLACEHOLDER
            // ========================================================

            PlaceholderColor = Color.White,
            PlaceholderTextColor = Color.FromArgb(183, 183, 184),

            // ========================================================
            // C) COLORS – NORMAL
            // ========================================================

            FillColor = Color.White,
            BorderColor = Color.FromArgb(218, 221, 221),

            // ========================================================
            // D) COLORS – HOVER
            // ========================================================

            HoverFillColor = Color.FromArgb(250, 250, 251),
            HoverBorderColor = Color.FromArgb(181, 188, 188),

            // ========================================================
            // E) COLORS – FOCUS
            // ========================================================

            FocusFillColor = Color.White,
            FocusBorderColor = Color.FromArgb(0, 138, 128),

            // ========================================================
            // F) STYLE
            // ========================================================

            CornerRadius = 12f,
            BorderWidth = 1f
        };

        /// <summary>
        /// Returns the ComboBox visual preset styled according to
        /// Mayora dropdown and selection controls.
        /// </summary>
        public ComboBoxPreset GetComboBoxPreset() => new ComboBoxPreset
        {
            // ========================================================
            // TEXT
            // ========================================================

            Placeholder = "Select",
            PlaceholderColor = Color.FromArgb(183, 183, 184),
            TextColor = Color.Black,

            // ========================================================
            // NORMAL COLORS
            // ========================================================

            FillColor = Color.White,
            BorderColor = Color.FromArgb(218, 221, 221),

            // ========================================================
            // FOCUS COLORS (Brand Accent)
            // ========================================================

            FocusFillColor = Color.White,
            FocusBorderColor = Color.FromArgb(0, 138, 128),

            // ========================================================
            // HOVER COLORS
            // ========================================================

            HoverFillColor = Color.FromArgb(250, 250, 251),
            HoverBorderColor = Color.FromArgb(181, 188, 188),

            // ========================================================
            // STYLE
            // ========================================================

            CornerRadius = 12f,
            BorderWidth = 1f
        };

        /// <summary>
        /// Returns the table visual preset styled according to
        /// Mayora enterprise data presentation guidelines.
        /// </summary>
        public TablePreset GetTablePreset() => new TablePreset
        {
            // ========================================================
            // Layout: compact and dense
            // ========================================================

            HeaderHeight = 32,
            RowHeight = 30,
            TopPadding = 2,

            // ========================================================
            // Base table color
            // ========================================================

            TableColor = Color.White,

            // ========================================================
            // Row states
            // ========================================================

            RowHoverColor = Color.FromArgb(240, 245, 244),
            RowSelectedColor = Color.FromArgb(200, 225, 220),

            // ========================================================
            // Grid lines
            // ========================================================

            HorizontalLineColor = Color.FromArgb(200, 210, 210),

            // ========================================================
            // Text styling
            // ========================================================

            HeaderTextColor = Color.FromArgb(0, 90, 85),
            RowTextColor = Color.Black,
            RowHoverTextColor = Color.Black,
            RowSelectedTextColor = Color.Black,
            TextAlign = FastTextAlign.Left,

            // ========================================================
            // Fonts hierarchy
            // ========================================================

            HeaderTextFont = new Font("Segoe UI", 11f, FontStyle.Bold),
            RowTextFont = new Font("Segoe UI", 11f, FontStyle.Regular),
            RowHoverTextFont = new Font("Segoe UI", 11f, FontStyle.Regular),
            RowSelectedTextFont = new Font("Segoe UI", 11f, FontStyle.Bold),

            // ========================================================
            // Border styling
            // ========================================================

            BorderColor = Color.FromArgb(180, 190, 190),
            BorderRadius = 8f,
            BorderWidth = 1.4f
        };
    }
}

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
    /// Built-in FastUI theme inspired by Google Material Design principles.
    /// 
    /// This theme emphasizes:
    /// - Friendly rounded shapes
    /// - Clear hierarchy and spacing
    /// - Google Blue accent for focus and interaction
    /// - Clean, modern, and accessible UI visuals
    /// </summary>
    public class GoogleMaterialTheme : IFuiTheme
    {
        /// <summary>
        /// Returns the button visual preset styled according to
        /// Google Material Design guidelines.
        /// </summary>
        public ButtonPreset GetButtonPreset() => new ButtonPreset
        {
            // ========================================================
            // TEXT
            // ========================================================

            FontColor = Color.White,
            FontSize = 10.5f,
            MoreFontSettings = new Font("Segoe UI", 10.5f, FontStyle.Bold),
            TextPosition = FastTextAlign.Center,

            // ========================================================
            // LAYOUT
            // ========================================================

            ControlWidth = 125,
            ControlHeight = 36,

            // ========================================================
            // NORMAL COLORS
            // ========================================================

            FillColor = Color.FromArgb(66, 133, 244),
            BorderColor = Color.FromArgb(66, 133, 244),

            // ========================================================
            // HOVER COLORS
            // ========================================================

            HoverFillColor = Color.FromArgb(52, 115, 220),
            HoverBorder = Color.FromArgb(52, 115, 220),
            HoverTextColor = Color.White,

            // ========================================================
            // PRESS COLORS
            // ========================================================

            PressFillColor = Color.FromArgb(41, 96, 185),
            PressBorderColor = Color.FromArgb(41, 96, 185),
            PressDepth = 2,

            // ========================================================
            // STYLE
            // ========================================================

            BorderWidth = 0.8f,
            CornerRadius = 14f
        };

        /// <summary>
        /// Returns the textbox visual preset styled according to
        /// Google Material input field behavior.
        /// </summary>
        public TextBoxPreset GetTextBoxPreset() => new TextBoxPreset
        {
            // ========================================================
            // TEXT
            // ========================================================

            TextColor = Color.FromArgb(30, 30, 30),
            FontSize = 10.5f,
            MoveTextHorizontal = 8,
            MoveTextVertical = 0,
            TextAlignment = FastTextAlign.Left,

            // ========================================================
            // PLACEHOLDER
            // ========================================================

            PlaceholderColor = Color.FromArgb(255, 255, 255),
            PlaceholderTextColor = Color.FromArgb(160, 160, 160),

            // ========================================================
            // NORMAL COLORS
            // ========================================================

            FillColor = Color.FromArgb(255, 255, 255),
            BorderColor = Color.FromArgb(200, 200, 200),

            // ========================================================
            // HOVER COLORS
            // ========================================================

            HoverFillColor = Color.FromArgb(250, 250, 250),
            HoverBorderColor = Color.FromArgb(170, 170, 170),

            // ========================================================
            // FOCUS COLORS (Google Accent)
            // ========================================================

            FocusFillColor = Color.FromArgb(255, 255, 255),
            FocusBorderColor = Color.FromArgb(66, 133, 244),

            // ========================================================
            // STYLE
            // ========================================================

            CornerRadius = 12f,
            BorderWidth = 1f
        };

        /// <summary>
        /// Returns the ComboBox visual preset styled according to
        /// Google Material dropdown interaction rules.
        /// </summary>
        public ComboBoxPreset GetComboBoxPreset() => new ComboBoxPreset
        {
            // ========================================================
            // TEXT
            // ========================================================

            Placeholder = "Select",
            PlaceholderColor = Color.FromArgb(160, 160, 160),
            TextColor = Color.FromArgb(30, 30, 30),

            // ========================================================
            // NORMAL COLORS
            // ========================================================

            FillColor = Color.White,
            BorderColor = Color.FromArgb(200, 200, 200),

            // ========================================================
            // FOCUS COLORS (Google Blue Accent)
            // ========================================================

            FocusFillColor = Color.White,
            FocusBorderColor = Color.FromArgb(66, 133, 244),

            // ========================================================
            // HOVER COLORS
            // ========================================================

            HoverFillColor = Color.FromArgb(250, 250, 250),
            HoverBorderColor = Color.FromArgb(170, 170, 170),

            // ========================================================
            // STYLE
            // ========================================================

            CornerRadius = 12f,
            BorderWidth = 1f
        };

        /// <summary>
        /// Returns the table visual preset styled according to
        /// Google Material data presentation patterns.
        /// </summary>
        public TablePreset GetTablePreset() => new TablePreset
        {
            // ========================================================
            // Layout: balanced and interactive
            // ========================================================

            HeaderHeight = 36,
            RowHeight = 36,
            TopPadding = 6,

            // ========================================================
            // Base table color
            // ========================================================

            TableColor = Color.White,

            // ========================================================
            // Row interaction states
            // ========================================================

            RowHoverColor = Color.FromArgb(232, 240, 254),
            RowSelectedColor = Color.FromArgb(208, 224, 249),

            // ========================================================
            // Grid lines
            // ========================================================

            HorizontalLineColor = Color.FromArgb(220, 220, 220),

            // ========================================================
            // Text styling
            // ========================================================

            HeaderTextColor = Color.FromArgb(66, 66, 66),
            RowTextColor = Color.FromArgb(32, 32, 32),
            RowHoverTextColor = Color.FromArgb(32, 32, 32),
            RowSelectedTextColor = Color.FromArgb(26, 115, 232),
            TextAlign = FastTextAlign.Left,

            // ========================================================
            // Fonts hierarchy
            // ========================================================

            HeaderTextFont = new Font("Segoe UI", 10.5f, FontStyle.Bold),
            RowTextFont = new Font("Segoe UI", 10.5f, FontStyle.Regular),
            RowHoverTextFont = new Font("Segoe UI", 10.5f, FontStyle.Regular),
            RowSelectedTextFont = new Font("Segoe UI", 10.5f, FontStyle.Bold),

            // ========================================================
            // Border styling
            // ========================================================

            BorderColor = Color.FromArgb(200, 200, 200),
            BorderRadius = 10f,
            BorderWidth = 1f
        };
    }
}

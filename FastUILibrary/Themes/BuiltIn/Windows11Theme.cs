using FastUI.FastUILibrary.Core;
using FastUI.FastUILibrary.Themes.Interfaces;
using System;
using FastUI.FastUILibrary.Core;
using FastUI.FastUILibrary.Themes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastUI.FastUILibrary.Themes.Presets;

namespace FastUI.FastUILibrary.Themes.BuiltIn
{
    /// <summary>
    /// Built-in FastUI theme inspired by Windows 11 design language.
    /// 
    /// This theme provides clean, neutral colors with subtle borders
    /// and moderate corner radius, aiming for clarity and usability
    /// rather than heavy visual effects.
    /// </summary>
    public class Windows11Theme : IFuiTheme
    {
        /// <summary>
        /// Returns the button visual preset styled according to
        /// Windows 11 UI guidelines.
        /// </summary>
        public ButtonPreset GetButtonPreset() => new ButtonPreset
        {
            // ========================================================
            // TEXT
            // ========================================================

            FontColor = Color.FromArgb(30, 30, 30),
            FontSize = 10.5f,
            MoreFontSettings = new Font("Segoe UI", 10.5f, FontStyle.Regular),
            TextPosition = FastTextAlign.Center,

            // ========================================================
            // LAYOUT
            // ========================================================

            ControlWidth = 125,
            ControlHeight = 36,

            // ========================================================
            // NORMAL COLORS
            // ========================================================

            FillColor = Color.FromArgb(245, 245, 245),
            BorderColor = Color.FromArgb(180, 180, 180),

            // ========================================================
            // HOVER COLORS
            // ========================================================

            HoverFillColor = Color.FromArgb(235, 235, 235),
            HoverBorder = Color.FromArgb(150, 150, 150),
            HoverTextColor = Color.FromArgb(20, 20, 20),

            // ========================================================
            // PRESS COLORS
            // ========================================================

            PressFillColor = Color.FromArgb(215, 215, 215),
            PressBorderColor = Color.FromArgb(130, 130, 130),
            PressDepth = 2,

            // ========================================================
            // STYLE
            // ========================================================

            BorderWidth = 1.2f,
            CornerRadius = 8f
        };

        /// <summary>
        /// Returns the textbox visual preset styled according to
        /// Windows 11 input field appearance.
        /// </summary>
        public TextBoxPreset GetTextBoxPreset() => new TextBoxPreset
        {
            // ========================================================
            // TEXT
            // ========================================================

            TextColor = Color.FromArgb(30, 30, 30),
            FontSize = 10.5f,
            MoveTextHorizontal = 6,
            MoveTextVertical = 0,
            TextAlignment = FastTextAlign.Left,

            // ========================================================
            // PLACEHOLDER
            // ========================================================

            PlaceholderColor = Color.FromArgb(240, 240, 240),
            PlaceholderTextColor = Color.FromArgb(120, 120, 120),

            // ========================================================
            // NORMAL COLORS
            // ========================================================

            FillColor = Color.FromArgb(245, 245, 245),
            BorderColor = Color.FromArgb(180, 180, 180),

            // ========================================================
            // HOVER COLORS
            // ========================================================

            HoverFillColor = Color.FromArgb(238, 238, 238),
            HoverBorderColor = Color.FromArgb(150, 150, 150),

            // ========================================================
            // FOCUS COLORS
            // ========================================================

            FocusFillColor = Color.FromArgb(255, 255, 255),
            FocusBorderColor = Color.FromArgb(120, 120, 120),

            // ========================================================
            // STYLE
            // ========================================================

            CornerRadius = 6f,
            BorderWidth = 1.2f
        };

        /// <summary>
        /// Returns the ComboBox visual preset styled according to
        /// Windows 11 dropdown controls.
        /// </summary>
        public ComboBoxPreset GetComboBoxPreset() => new ComboBoxPreset
        {
            // ========================================================
            // TEXT
            // ========================================================

            Placeholder = "Select",
            PlaceholderColor = Color.FromArgb(130, 130, 130),
            TextColor = Color.FromArgb(30, 30, 30),

            // ========================================================
            // NORMAL COLORS
            // ========================================================

            FillColor = Color.FromArgb(245, 245, 245),
            BorderColor = Color.FromArgb(180, 180, 180),

            // ========================================================
            // FOCUS COLORS
            // ========================================================

            FocusFillColor = Color.White,
            FocusBorderColor = Color.FromArgb(120, 120, 120),

            // ========================================================
            // HOVER COLORS
            // ========================================================

            HoverFillColor = Color.FromArgb(238, 238, 238),
            HoverBorderColor = Color.FromArgb(150, 150, 150),

            // ========================================================
            // STYLE
            // ========================================================

            CornerRadius = 6f,
            BorderWidth = 1.2f
        };

        /// <summary>
        /// Returns the table visual preset styled according to
        /// Windows 11 list and grid appearance.
        /// </summary>
        public TablePreset GetTablePreset() => new TablePreset
        {
            // ========================================================
            // Fast B - Layout
            // ========================================================

            HeaderHeight = 34,
            RowHeight = 32,
            TopPadding = 4,

            // ========================================================
            // Fast C - Table Base
            // ========================================================

            TableColor = Color.FromArgb(240, 240, 240),

            // ========================================================
            // Fast D - Rows
            // ========================================================

            RowHoverColor = Color.FromArgb(235, 235, 235),
            RowSelectedColor = Color.FromArgb(215, 215, 215),

            // ========================================================
            // Fast E - Grid Lines
            // ========================================================

            HorizontalLineColor = Color.FromArgb(200, 200, 200),

            // ========================================================
            // Fast F - Text
            // ========================================================

            HeaderTextColor = Color.FromArgb(30, 30, 30),
            RowTextColor = Color.FromArgb(30, 30, 30),
            RowHoverTextColor = Color.FromArgb(20, 20, 20),
            RowSelectedTextColor = Color.FromArgb(20, 20, 20),
            TextAlign = FastTextAlign.Left,

            // ========================================================
            // Fast G - Fonts
            // ========================================================

            HeaderTextFont = new Font("Segoe UI", 10f, FontStyle.Bold),
            RowTextFont = new Font("Segoe UI", 10f, FontStyle.Regular),
            RowHoverTextFont = new Font("Segoe UI", 10f, FontStyle.Regular),
            RowSelectedTextFont = new Font("Segoe UI", 10f, FontStyle.Bold),

            // ========================================================
            // Fast H - Border
            // ========================================================

            BorderColor = Color.FromArgb(180, 180, 180),
            BorderRadius = 6f,
            BorderWidth = 1.2f
        };
    }
}

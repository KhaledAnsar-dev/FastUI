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
    /// Built-in FastUI theme inspired by Apple Human Interface Guidelines.
    /// 
    /// This theme focuses on:
    /// - Soft rounded corners
    /// - Minimal borders
    /// - Calm spacing and typography
    /// - Subtle hover and selection feedback
    /// - A clean, distraction-free UI appearance
    /// </summary>
    public class AppleTheme : IFuiTheme
    {
        /// <summary>
        /// Returns the button preset styled with Apple-like softness
        /// and minimal visual noise.
        /// </summary>
        public ButtonPreset GetButtonPreset() => new ButtonPreset
        {
            FontColor = Color.FromArgb(45, 45, 45),
            FontSize = 11f,
            MoreFontSettings = new Font("Segoe UI", 11f, FontStyle.Regular),
            TextPosition = FastTextAlign.Center,

            ControlWidth = 125,
            ControlHeight = 36,

            FillColor = Color.FromArgb(255, 255, 255),
            BorderColor = Color.FromArgb(220, 220, 220), // nearly invisible border

            HoverFillColor = Color.FromArgb(245, 245, 245),
            HoverBorder = Color.FromArgb(200, 200, 200),
            HoverTextColor = Color.FromArgb(30, 30, 30),

            PressFillColor = Color.FromArgb(230, 230, 230),
            PressBorderColor = Color.FromArgb(170, 170, 170),
            PressDepth = 1,

            BorderWidth = 1f,
            CornerRadius = 12f
        };

        /// <summary>
        /// Returns the textbox preset designed for a soft,
        /// calm, and minimal Apple-like input experience.
        /// </summary>
        public TextBoxPreset GetTextBoxPreset() => new TextBoxPreset
        {
            // TEXT
            TextColor = Color.FromArgb(40, 40, 40),
            FontSize = 11f,
            MoveTextHorizontal = 6,
            MoveTextVertical = 0,
            TextAlignment = FastTextAlign.Left,

            // PLACEHOLDER
            PlaceholderColor = Color.FromArgb(245, 245, 245),
            PlaceholderTextColor = Color.FromArgb(150, 150, 150),

            // NORMAL
            FillColor = Color.FromArgb(255, 255, 255),
            BorderColor = Color.FromArgb(220, 220, 220),

            // HOVER
            HoverFillColor = Color.FromArgb(252, 252, 252),
            HoverBorderColor = Color.FromArgb(200, 200, 200),

            // FOCUS
            FocusFillColor = Color.FromArgb(255, 255, 255),
            FocusBorderColor = Color.FromArgb(170, 170, 170),

            // STYLE
            CornerRadius = 10f,
            BorderWidth = 1f
        };

        /// <summary>
        /// Returns the ComboBox preset with subtle borders
        /// and smooth interaction states inspired by Apple UI.
        /// </summary>
        public ComboBoxPreset GetComboBoxPreset() => new ComboBoxPreset
        {
            // TEXT
            Placeholder = "Select",
            PlaceholderColor = Color.FromArgb(160, 160, 160),
            TextColor = Color.FromArgb(45, 45, 45),

            // NORMAL
            FillColor = Color.White,
            BorderColor = Color.FromArgb(220, 220, 220),

            // FOCUS
            FocusFillColor = Color.White,
            FocusBorderColor = Color.FromArgb(170, 170, 170),

            // HOVER
            HoverFillColor = Color.FromArgb(248, 248, 248),
            HoverBorderColor = Color.FromArgb(200, 200, 200),

            // STYLE
            CornerRadius = 12f,
            BorderWidth = 1f
        };

        /// <summary>
        /// Returns the table preset designed for an airy,
        /// calm, and almost invisible data presentation style.
        /// </summary>
        public TablePreset GetTablePreset() => new TablePreset
        {
            // Layout: airy and calm
            HeaderHeight = 42,
            RowHeight = 40,
            TopPadding = 10,

            // Base
            TableColor = Color.White,

            // Rows: very subtle interaction
            RowHoverColor = Color.FromArgb(250, 250, 250),
            RowSelectedColor = Color.FromArgb(235, 235, 235),

            // Lines: barely visible
            HorizontalLineColor = Color.FromArgb(245, 245, 245),

            // Text hierarchy
            HeaderTextColor = Color.FromArgb(90, 90, 90),
            RowTextColor = Color.FromArgb(50, 50, 50),
            RowHoverTextColor = Color.FromArgb(40, 40, 40),
            RowSelectedTextColor = Color.Black,
            TextAlign = FastTextAlign.Left,

            // Fonts: calm and consistent
            HeaderTextFont = new Font("Segoe UI", 10.5f, FontStyle.Regular),
            RowTextFont = new Font("Segoe UI", 10.5f, FontStyle.Regular),
            RowHoverTextFont = new Font("Segoe UI", 10.5f, FontStyle.Regular),
            RowSelectedTextFont = new Font("Segoe UI", 10.5f, FontStyle.Regular),

            // Border: UI should visually disappear
            BorderColor = Color.FromArgb(230, 230, 230),
            BorderRadius = 14f,
            BorderWidth = 0.8f
        };
    }
}


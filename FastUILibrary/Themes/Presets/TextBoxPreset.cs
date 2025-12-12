using FastUI.FastUILibrary.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.FastUILibrary.Themes.Presets
{
    /// <summary>
    /// Defines a complete visual preset for FastUI text-based input controls.
    /// 
    /// This class represents a style snapshot that can be applied to
    /// text boxes via the FastUI theming system.
    /// 
    /// Presets are designed to:
    /// - Group related visual properties together
    /// - Enable ready-made styles
    /// - Avoid repetitive manual UI configuration
    /// </summary>
    public class TextBoxPreset
    {
        // ============================================================
        // A) TEXT
        // ============================================================

        /// <summary>
        /// Color used to render the input text.
        /// </summary>
        public Color TextColor { get; set; }

        /// <summary>
        /// Font size of the text inside the control.
        /// </summary>
        public float FontSize { get; set; }

        /// <summary>
        /// Horizontal offset applied to the text rendering position.
        /// </summary>
        public int MoveTextHorizontal { get; set; }

        /// <summary>
        /// Vertical offset applied to the text rendering position.
        /// </summary>
        public int MoveTextVertical { get; set; }

        /// <summary>
        /// Determines how text is aligned within the control.
        /// </summary>
        public FastTextAlign TextAlignment { get; set; }


        // ============================================================
        // B) PLACEHOLDER
        // ============================================================

        /// <summary>
        /// Color used to render the placeholder background or accent.
        /// </summary>
        public Color PlaceholderColor { get; set; }

        /// <summary>
        /// Color of the placeholder text when no value is entered.
        /// </summary>
        public Color PlaceholderTextColor { get; set; }


        // ============================================================
        // C) COLORS – NORMAL
        // ============================================================

        /// <summary>
        /// Background fill color in the normal (idle) state.
        /// </summary>
        public Color FillColor { get; set; }

        /// <summary>
        /// Border color in the normal (idle) state.
        /// </summary>
        public Color BorderColor { get; set; }


        // ============================================================
        // D) COLORS – HOVER
        // ============================================================

        /// <summary>
        /// Background fill color when the control is hovered.
        /// </summary>
        public Color HoverFillColor { get; set; }

        /// <summary>
        /// Border color when the control is hovered.
        /// </summary>
        public Color HoverBorderColor { get; set; }


        // ============================================================
        // E) COLORS – FOCUS
        // ============================================================

        /// <summary>
        /// Background fill color when the control is focused.
        /// </summary>
        public Color FocusFillColor { get; set; }

        /// <summary>
        /// Border color when the control is focused.
        /// </summary>
        public Color FocusBorderColor { get; set; }


        // ============================================================
        // F) STYLE
        // ============================================================

        /// <summary>
        /// Radius used for rounding control corners.
        /// </summary>
        public float CornerRadius { get; set; }

        /// <summary>
        /// Thickness of the control border.
        /// </summary>
        public float BorderWidth { get; set; }
    }
}

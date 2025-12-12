using FastUI.FastUILibrary.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.FastUILibrary.Themes.Presets
{
    /// <summary>
    /// Defines a visual style preset for FastUI button controls.
    /// 
    /// This preset groups all button-related appearance properties
    /// into a single reusable configuration that can be applied
    /// by themes or style systems.
    /// </summary>
    public class ButtonPreset
    {
        // ============================================================
        // A) TEXT
        // ============================================================

        /// <summary>
        /// Color of the button text.
        /// </summary>
        public Color FontColor { get; set; }

        /// <summary>
        /// Font size of the button text.
        /// </summary>
        public float FontSize { get; set; }

        /// <summary>
        /// Full font configuration for advanced customization.
        /// </summary>
        public Font MoreFontSettings { get; set; }

        /// <summary>
        /// Horizontal offset applied to the text position.
        /// </summary>
        public int MoveTextHorizontal { get; set; }

        /// <summary>
        /// Vertical offset applied to the text position.
        /// </summary>
        public int MoveTextVertical { get; set; }

        /// <summary>
        /// Determines text alignment inside the button.
        /// </summary>
        public FastTextAlign TextPosition { get; set; }


        // ============================================================
        // B) LAYOUT
        // ============================================================

        /// <summary>
        /// Explicit width of the button control.
        /// </summary>
        public int ControlWidth { get; set; }

        /// <summary>
        /// Explicit height of the button control.
        /// </summary>
        public int ControlHeight { get; set; }


        // ============================================================
        // C) NORMAL COLORS
        // ============================================================

        /// <summary>
        /// Background fill color in the normal state.
        /// </summary>
        public Color FillColor { get; set; }

        /// <summary>
        /// Border color in the normal state.
        /// </summary>
        public Color BorderColor { get; set; }


        // ============================================================
        // D) HOVER COLORS
        // ============================================================

        /// <summary>
        /// Background fill color when the button is hovered.
        /// </summary>
        public Color HoverFillColor { get; set; }

        /// <summary>
        /// Border color when the button is hovered.
        /// </summary>
        public Color HoverBorder { get; set; }

        /// <summary>
        /// Text color when the button is hovered.
        /// </summary>
        public Color HoverTextColor { get; set; }


        // ============================================================
        // E) PRESS COLORS
        // ============================================================

        /// <summary>
        /// Background fill color when the button is pressed.
        /// </summary>
        public Color PressFillColor { get; set; }

        /// <summary>
        /// Border color when the button is pressed.
        /// </summary>
        public Color PressBorderColor { get; set; }

        /// <summary>
        /// Vertical offset applied to simulate press depth.
        /// </summary>
        public int PressDepth { get; set; }


        // ============================================================
        // F) STYLE
        // ============================================================

        /// <summary>
        /// Thickness of the button border.
        /// </summary>
        public float BorderWidth { get; set; }

        /// <summary>
        /// Radius used for rounding button corners.
        /// </summary>
        public float CornerRadius { get; set; }
    }
}

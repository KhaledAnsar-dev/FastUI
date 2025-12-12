using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.FastUILibrary.Themes.Presets
{
    /// <summary>
    /// Defines a visual preset for FastUI ComboBox controls.
    /// 
    /// This preset encapsulates all appearance-related properties
    /// required to style a ComboBox consistently across themes.
    /// </summary>
    public class ComboBoxPreset
    {
        // ============================================================
        // B) TEXT
        // ============================================================

        /// <summary>
        /// Placeholder text displayed when no item is selected.
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// Color of the placeholder text.
        /// </summary>
        public Color PlaceholderColor { get; set; }

        /// <summary>
        /// Color of the selected text.
        /// </summary>
        public Color TextColor { get; set; }


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
        // D) COLORS – FOCUS
        // ============================================================

        /// <summary>
        /// Background fill color when the ComboBox is focused.
        /// </summary>
        public Color FocusFillColor { get; set; }

        /// <summary>
        /// Border color when the ComboBox is focused.
        /// </summary>
        public Color FocusBorderColor { get; set; }


        // ============================================================
        // E) COLORS – HOVER
        // ============================================================

        /// <summary>
        /// Background fill color when the ComboBox is hovered.
        /// </summary>
        public Color HoverFillColor { get; set; }

        /// <summary>
        /// Border color when the ComboBox is hovered.
        /// </summary>
        public Color HoverBorderColor { get; set; }


        // ============================================================
        // F) STYLE
        // ============================================================

        /// <summary>
        /// Radius used for rounding ComboBox corners.
        /// </summary>
        public float CornerRadius { get; set; }

        /// <summary>
        /// Thickness of the ComboBox border.
        /// </summary>
        public float BorderWidth { get; set; }
    }
}

using FastUI.FastUILibrary.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.FastUILibrary.Themes.Presets
{
    /// <summary>
    /// Defines a complete visual preset for FastUI table controls.
    /// 
    /// This preset encapsulates layout, color, text, font, and border
    /// settings required to style a table consistently across themes.
    /// </summary>
    public class TablePreset
    {
        // ============================================================
        // Fast B - Layout
        // ============================================================

        /// <summary>
        /// Height of the table header row.
        /// </summary>
        public int HeaderHeight { get; set; }

        /// <summary>
        /// Height of each data row.
        /// </summary>
        public int RowHeight { get; set; }

        /// <summary>
        /// Top padding space above the table content.
        /// </summary>
        public int TopPadding { get; set; }


        // ============================================================
        // Fast C - Colors (Table Base)
        // ============================================================

        /// <summary>
        /// Base background color of the table.
        /// </summary>
        public Color TableColor { get; set; }


        // ============================================================
        // Fast D - Colors (Rows)
        // ============================================================

        /// <summary>
        /// Background color applied when a row is hovered.
        /// </summary>
        public Color RowHoverColor { get; set; }

        /// <summary>
        /// Background color applied when a row is selected.
        /// </summary>
        public Color RowSelectedColor { get; set; }


        // ============================================================
        // Fast E - Colors (Grid Lines)
        // ============================================================

        /// <summary>
        /// Color of horizontal grid separator lines.
        /// </summary>
        public Color HorizontalLineColor { get; set; }


        // ============================================================
        // Fast F - Text
        // ============================================================

        /// <summary>
        /// Text color of the table header.
        /// </summary>
        public Color HeaderTextColor { get; set; }

        /// <summary>
        /// Text color of normal rows.
        /// </summary>
        public Color RowTextColor { get; set; }

        /// <summary>
        /// Text color of a hovered row.
        /// </summary>
        public Color RowHoverTextColor { get; set; }

        /// <summary>
        /// Text color of a selected row.
        /// </summary>
        public Color RowSelectedTextColor { get; set; }

        /// <summary>
        /// Text alignment used for both headers and rows.
        /// </summary>
        public FastTextAlign TextAlign { get; set; }


        // ============================================================
        // Fast G - Fonts
        // ============================================================

        /// <summary>
        /// Font used for header text.
        /// </summary>
        public Font HeaderTextFont { get; set; }

        /// <summary>
        /// Font used for normal row text.
        /// </summary>
        public Font RowTextFont { get; set; }

        /// <summary>
        /// Font used for hovered row text.
        /// </summary>
        public Font RowHoverTextFont { get; set; }

        /// <summary>
        /// Font used for selected row text.
        /// </summary>
        public Font RowSelectedTextFont { get; set; }


        // ============================================================
        // Fast H - Border
        // ============================================================

        /// <summary>
        /// Border color of the table container.
        /// </summary>
        public Color BorderColor { get; set; }

        /// <summary>
        /// Radius used to round table corners.
        /// </summary>
        public float BorderRadius { get; set; }

        /// <summary>
        /// Thickness of the table border.
        /// </summary>
        public float BorderWidth { get; set; }
    }
}

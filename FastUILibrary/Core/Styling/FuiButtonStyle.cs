using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.FastUILibrary.Core.Styling
{
    public class FuiButtonStyle
    {
        // A) TEXT
        public Color FontColor { get; set; }
        public float FontSize { get; set; }
        public Font MoreFontSettings { get; set; }
        public int MoveTextHorizontal { get; set; }
        public int MoveTextVertical { get; set; }
        public FastTextAlign TextPosition { get; set; }

        // B) LAYOUT
        public int ControlWidth { get; set; }
        public int ControlHeight { get; set; }

        // C) NORMAL COLORS
        public Color FillColor { get; set; }
        public Color BorderColor { get; set; }

        // D) HOVER COLORS
        public Color HoverFillColor { get; set; }
        public Color HoverBorder { get; set; }
        public Color HoverTextColor { get; set; }

        // E) PRESS COLORS
        public Color PressFillColor { get; set; }
        public Color PressBorderColor { get; set; }
        public int PressDepth { get; set; }

        // F) STYLE
        public float BorderWidth { get; set; }
        public float CornerRadius { get; set; }
    }

}

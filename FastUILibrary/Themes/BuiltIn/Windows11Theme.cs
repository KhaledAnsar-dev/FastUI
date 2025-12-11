using FastUI.FastUILibrary.Core;
using FastUI.FastUILibrary.Core.Interfaces;
using FastUI.FastUILibrary.Core.Styling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.FastUILibrary.Themes.BuiltIn
{
    public class Windows11Theme : IFuiTheme
    {
        public FuiButtonStyle GetButtonStyle() => new FuiButtonStyle
        {
            FontColor = Color.FromArgb(30, 30, 30),
            FontSize = 10.5f,
            MoreFontSettings = new Font("Segoe UI", 10.5f, FontStyle.Regular),
            TextPosition = FastTextAlign.Center,

            ControlWidth = 125,
            ControlHeight = 36,

            FillColor = Color.FromArgb(245, 245, 245),
            BorderColor = Color.FromArgb(180, 180, 180),

            HoverFillColor = Color.FromArgb(235, 235, 235),
            HoverBorder = Color.FromArgb(150, 150, 150),
            HoverTextColor = Color.FromArgb(20, 20, 20),

            PressFillColor = Color.FromArgb(215, 215, 215),
            PressBorderColor = Color.FromArgb(130, 130, 130),
            PressDepth = 2,

            BorderWidth = 1.2f,
            CornerRadius = 6f   // Windows = sharper, not rounded like Apple
        };
    }
}

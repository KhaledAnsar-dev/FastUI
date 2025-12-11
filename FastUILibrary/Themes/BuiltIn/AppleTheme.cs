using FastUI.FastUILibrary.Core.Interfaces;
using FastUI.FastUILibrary.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastUI.FastUILibrary.Core.Styling;

namespace FastUI.FastUILibrary.Themes.BuiltIn
{
    public class AppleTheme : IFuiTheme
    {
        public FuiButtonStyle GetButtonStyle() => new FuiButtonStyle
        {
            FontColor = Color.FromArgb(45, 45, 45),
            FontSize = 11f,
            MoreFontSettings = new Font("Segoe UI", 11f, FontStyle.Regular),
            TextPosition = FastTextAlign.Center,

            ControlWidth = 125,
            ControlHeight = 36,

            FillColor = Color.FromArgb(255, 255, 255),
            BorderColor = Color.FromArgb(220, 220, 220), // شبه مخفي

            HoverFillColor = Color.FromArgb(245, 245, 245),
            HoverBorder = Color.FromArgb(200, 200, 200),
            HoverTextColor = Color.FromArgb(30, 30, 30),

            PressFillColor = Color.FromArgb(230, 230, 230),
            PressBorderColor = Color.FromArgb(170, 170, 170),
            PressDepth = 1,

            BorderWidth = 1f,
            CornerRadius = 12f // Apple = Rounded & Soft
        };
    }
}

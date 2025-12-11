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
    internal class MayoraTheme : IFuiTheme
    {
        public FuiButtonStyle GetButtonStyle() => new FuiButtonStyle
        {
            FontColor = Color.White,
            FontSize = 12f,
            MoreFontSettings = new Font("Segoe UI", 12f, FontStyle.Bold),
            TextPosition = FastTextAlign.Center,

            ControlWidth = 125,
            ControlHeight = 36,

            FillColor = Color.FromArgb(255, 188, 1),
            BorderColor = Color.FromArgb(255, 188, 1),

            HoverFillColor = Color.FromArgb(255, 200, 40),
            HoverBorder = Color.FromArgb(255, 200, 40),
            HoverTextColor = Color.White,

            PressFillColor = Color.Orange,
            PressBorderColor = Color.Orange,
            PressDepth = 1,

            BorderWidth = 0f,
            CornerRadius = 16f
        };
    }
}

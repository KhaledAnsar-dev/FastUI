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
    public class GoogleMaterialTheme : IFuiTheme
    {
        public FuiButtonStyle GetButtonStyle() => new FuiButtonStyle
        {
            FontColor = Color.White,
            FontSize = 10.5f,
            MoreFontSettings = new Font("Segoe UI", 10.5f, FontStyle.Bold),
            TextPosition = FastTextAlign.Center,

            ControlWidth = 125,
            ControlHeight = 36,

            FillColor = Color.FromArgb(66, 133, 244),  // Google Blue
            BorderColor = Color.FromArgb(66, 133, 244),

            HoverFillColor = Color.FromArgb(52, 115, 220),
            HoverBorder = Color.FromArgb(52, 115, 220),
            HoverTextColor = Color.White,

            PressFillColor = Color.FromArgb(41, 96, 185),
            PressBorderColor = Color.FromArgb(41, 96, 185),
            PressDepth = 2,

            BorderWidth = 0.8f,
            CornerRadius = 14f // Google = friendly, rounded
        };
    }
}

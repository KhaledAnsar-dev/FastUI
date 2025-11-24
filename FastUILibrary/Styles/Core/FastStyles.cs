using FastUI.Modules.Input.FastTextBoxUI;
using FastUI.Modules.Buttons.FastButtonUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastUI.FastUILibrary.Core;

namespace FastUI.FastUILibrary.Styles.Core
{
    public static class FastStyles
    {
        public static class Windows11
        {
            public static void Apply(FastTextBox txt)
            {
                // Base colors
                txt.FillColor = Color.White;
                txt.BorderColor = Color.FromArgb(209, 209, 209); // #D1D1D1
                txt.BorderWidth = 1;
                txt.CornerRadius = 4;

                // Text
                txt.FontSize = 10;
                txt.FontColor = Color.Black;
                txt.TextPosition = FastEnumPosition.Left;

                // Placeholder
                txt.EmptyTextColor = Color.FromArgb(138, 138, 138); // #8A8A8A

                // Hover (official Windows 11 hover border)
                txt.HoverBorderColor = Color.FromArgb(168, 168, 168); // #A8A8A8

                // Focus (Windows 11 blue focus ring)
                txt.FocusBorderColor = Color.FromArgb(74, 144, 226); // #4A90E2

                // For Windows 11 "focus glow"
                txt.BorderWidth = 1;          // idle
                                              // في حال أردت تقليد InnerGlow يمكن جعله 2 أو 3 أثناء التركيز عبر event داخلي

                // Text movement defaults
                txt.MoveTextHorizontal = 0;
                txt.MoveTextVertical = 0;
            }


            public static void Apply(FastButton btn)
            {
                // Base Normal
                btn.FillColor = Color.White;
                btn.BorderColor = Color.FromArgb(209, 209, 209);   // #D1D1D1
                btn.BorderWidth = 1;
                btn.CornerRadius = 8;

                // Text
                btn.FontSize = 10.5f;
                btn.FontColor = Color.Black;
                btn.TextPosition = FastEnumPosition.Center;

                // Hover (real Win11 hover)
                btn.HoverFillColor = Color.FromArgb(243, 243, 243);      // #F3F3F3
                btn.HoverBorderColor = Color.FromArgb(175, 175, 175);     // #AFAFAF
                btn.HoverTextColor = Color.Black;

                // Image position (left by default)
                btn.ImagePosition = FastEnumPosition.Left;
                btn.MoveImageHorizontal = -4;

                // Text movement
                btn.MoveTextHorizontal = 0;
                btn.MoveTextVertical = 0;

            }

        }
    }
}

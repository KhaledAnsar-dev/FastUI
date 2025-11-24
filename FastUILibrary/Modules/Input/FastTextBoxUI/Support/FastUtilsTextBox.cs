using FastUI.FastUILibrary.Core;
using FastUI.FastUILibrary.Styles.Core;
using FastUI.Modules.Input.FastTextBoxUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.FastUILibrary.Modules.Input.FastTextBoxUI.Support
{
    public static class FastUtilsTextBox
    {
        // --------------------------------------------------------------
        // GET PLACEHOLDER BASED ON INPUT TYPE
        // --------------------------------------------------------------
        public static string GetPlaceholder(FastEnumInputType type)
        {
            return type switch
            {
                FastEnumInputType.Email => "example@mail.com",
                FastEnumInputType.PhoneDZ => "0XXXXXXXXX",
                FastEnumInputType.Integer => "0",
                FastEnumInputType.Decimal => "0,00",
                _ => "Text"
            };
        }


        public static void ChangeStyle(FastTextBox fastTextBox)
        {
            FastStyles.Windows11.Apply(fastTextBox);
        }
    }

}

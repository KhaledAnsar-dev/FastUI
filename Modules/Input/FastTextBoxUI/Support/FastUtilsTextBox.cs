using FastUI.Core;
using FastUI.Styles.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.Modules.Input.FastTextBoxUI.Support
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

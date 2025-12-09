using FastUI.FastUILibrary.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FastUI.FastUILibrary.Components
{
    /// <summary>
    /// A FastUI validated textbox for credit card expiry dates.
    /// Supports:
    /// - Required field validation
    /// - Format: MM/YY
    /// - Automatic red border styling when invalid
    /// </summary>
    public class FuiCreditCardDate : FuiValidatedTextBox
    {
        // ============================================================
        //  Constructor
        // ============================================================

        public FuiCreditCardDate()
        {
            // Expiry date format is not purely numeric (“/” allowed)
            InputType = FastInputType.Any;

            // Default UI hint
            Placeholder = "MM/YY";
        }


        // ============================================================
        //  Validation Override
        // ============================================================

        /// <summary>
        /// Validates required state + credit card expiry format.
        /// Pattern:
        /// - MM between 01 and 12
        /// - YY is any 2 digits
        /// </summary>
        public override bool Validate()
        {
            string txt = FastText.Trim();

            // Required field enforcement
            if (Required && txt == "")
            {
                ErrorMessage = "Expiry date required.";
                ApplyInvalidStyle();
                return false;
            }

            // Matches MM/YY
            bool ok = Regex.IsMatch(txt, @"^(0[1-9]|1[0-2])\/\d{2}$");

            if (!ok)
            {
                ErrorMessage = "Invalid expiry date.";
                ApplyInvalidStyle();
                return false;
            }

            // Valid → restore original styling
            ApplyValidStyle();
            return true;
        }
    }
}

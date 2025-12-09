using FastUI.FastUILibrary.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FastUI.FastUILibrary.Components
{
    /// <summary>
    /// A FastUI validated textbox specialized for US phone numbers.
    /// Supports:
    /// - Required field validation
    /// - North American Numbering Plan rules:
    ///     Must be 10 digits, cannot start with 0 or 1
    /// - Automatic red border feedback via FuiValidatedTextBox
    /// </summary>
    public class FuiPhoneUSA : FuiValidatedTextBox
    {
        // ============================================================
        //  Constructor
        // ============================================================

        public FuiPhoneUSA()
        {
            // Only numeric characters are permitted for US phone numbers
            InputType = FastInputType.IntegerOnly;

            // Placeholder for standard 10-digit US format
            Placeholder = "1234567890";
        }


        // ============================================================
        //  Validation Override
        // ============================================================

        /// <summary>
        /// Validates required state + US phone number format.
        /// NANP rules:
        /// - Must be 10 digits
        /// - First digit must be 2–9
        /// </summary>
        public override bool Validate()
        {
            string txt = FastText.Trim();

            // Required field check
            if (Required && txt == "")
            {
                ErrorMessage = "Phone required.";
                ApplyInvalidStyle();
                return false;
            }

            // US phone validation: 10 digits, cannot start with 0 or 1
            bool ok = Regex.IsMatch(txt, @"^[2-9][0-9]{9}$");

            if (!ok)
            {
                ErrorMessage = "Invalid US phone number.";
                ApplyInvalidStyle();
                return false;
            }

            // Valid input → restore normal style
            ApplyValidStyle();
            return true;
        }
    }
}

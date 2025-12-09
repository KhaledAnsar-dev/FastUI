using FastUI.FastUILibrary.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FastUI.FastUILibrary.Components
{
    /// <summary>
    /// A FastUI validated textbox specialized for Algerian phone numbers.
    /// Supports:
    /// - Required validation
    /// - Algerian mobile format: 05 / 06 / 07 + 8 digits
    /// - Automatic visual feedback via FuiValidatedTextBox
    /// </summary>
    public class FuiPhoneDz : FuiValidatedTextBox
    {
        // ============================================================
        //  Constructor
        // ============================================================

        public FuiPhoneDz()
        {
            // Only digits should be allowed for DZ phone numbers
            InputType = FastInputType.IntegerOnly;

            // Default placeholder to guide user input
            Placeholder = "05xxxxxxxx";
        }


        // ============================================================
        //  Validation Override
        // ============================================================

        /// <summary>
        /// Validates required state + Algerian phone number format.
        /// </summary>
        public override bool Validate()
        {
            string txt = FastText.Trim();

            // Required field check
            if (Required && txt == "")
            {
                ErrorMessage = "Phone number required.";
                ApplyInvalidStyle();
                return false;
            }

            // Algerian mobile format:
            // 05 / 06 / 07 followed by 8 digits
            bool ok = Regex.IsMatch(txt, @"^(05|06|07)[0-9]{8}$");

            if (!ok)
            {
                ErrorMessage = "Invalid Algerian phone number.";
                ApplyInvalidStyle();
                return false;
            }

            // If valid → restore original styling
            ApplyValidStyle();
            return true;
        }
    }
}

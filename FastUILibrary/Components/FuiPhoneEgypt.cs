using FastUI.FastUILibrary.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FastUI.FastUILibrary.Components
{
    /// <summary>
    /// A FastUI validated textbox specialized for Egyptian phone numbers.
    /// Supports:
    /// - Required validation
    /// - Egyptian mobile prefixes: 010 / 011 / 012 / 015
    /// - Automatic visual feedback via FuiValidatedTextBox
    /// </summary>
    public class FuiPhoneEgypt : FuiValidatedTextBox
    {
        // ============================================================
        //  Constructor
        // ============================================================

        public FuiPhoneEgypt()
        {
            // Only digits should be allowed for EG phone numbers
            InputType = FastInputType.IntegerOnly;

            // Default placeholder to guide user input
            Placeholder = "01xxxxxxxxx";
        }


        // ============================================================
        //  Validation Override
        // ============================================================

        /// <summary>
        /// Validates required state + Egyptian phone number format.
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

            // Egyptian mobile number format:
            // 010 / 011 / 012 / 015 + 8 digits
            bool ok = Regex.IsMatch(txt, @"^(010|011|012|015)[0-9]{8}$");

            if (!ok)
            {
                ErrorMessage = "Invalid Egyptian phone number.";
                ApplyInvalidStyle();
                return false;
            }

            // If valid → restore original styling
            ApplyValidStyle();
            return true;
        }
    }
}

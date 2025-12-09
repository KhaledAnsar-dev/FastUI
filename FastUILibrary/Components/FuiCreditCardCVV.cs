using FastUI.FastUILibrary.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FastUI.FastUILibrary.Components
{
    /// <summary>
    /// A FastUI validated textbox for credit card CVV codes.
    /// Features:
    /// - Enforces numeric-only input
    /// - Limit to 3 characters
    /// - Required field support
    /// - Automatic styling feedback (red border when invalid)
    /// </summary>
    public class FuiCreditCardCVV : FuiValidatedTextBox
    {
        // ============================================================
        //  Constructor
        // ============================================================

        public FuiCreditCardCVV()
        {
            // CVV uses digits only
            InputType = FastInputType.IntegerOnly;

            // Placeholder for standard CVV
            Placeholder = "CVV";
        }


        // ============================================================
        //  KeyPress Behavior (Limit to 3 digits)
        // ============================================================

        /// <summary>
        /// Ensures CVV cannot exceed 3 digits.
        /// </summary>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // Allow control keys (Backspace, Delete...)
            if (char.IsControl(e.KeyChar))
            {
                base.OnKeyPress(e);
                return;
            }

            // Prevent typing more than 3 digits
            if (FastText.Length >= 3)
            {
                e.Handled = true;
                return;
            }

            base.OnKeyPress(e);
        }


        // ============================================================
        //  Validation Override
        // ============================================================

        /// <summary>
        /// Validates required state + CVV format (exactly 3 digits).
        /// </summary>
        public override bool Validate()
        {
            string txt = FastText.Trim();

            // Required field
            if (Required && txt == "")
            {
                ErrorMessage = "CVV required.";
                ApplyInvalidStyle();
                return false;
            }

            // Must be exactly 3 digits
            bool ok = Regex.IsMatch(txt, @"^[0-9]{3}$");

            if (!ok)
            {
                ErrorMessage = "Invalid CVV.";
                ApplyInvalidStyle();
                return false;
            }

            ApplyValidStyle();
            return true;
        }
    }
}

using FastUI.FastUILibrary.Core;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FastUI.FastUILibrary.Components
{
    /// <summary>
    /// A FastUI validated textbox for credit card numbers.
    /// Features:
    /// - Numeric-only input
    /// - Maximum length: 16 digits
    /// - Required field validation
    /// - Styling feedback for invalid values
    /// </summary>
    public class FuiCreditCardNumber : FuiValidatedTextBox
    {
        private const int MaxLength = 16;

        // ============================================================
        //  Constructor
        // ============================================================

        public FuiCreditCardNumber()
        {
            InputType = FastInputType.IntegerOnly;   // Digits only
            Placeholder = "Card Number";
        }


        // ============================================================
        //  Input Restriction (16 digits max)
        // ============================================================

        /// <summary>
        /// Prevents typing more than 16 digits.
        /// </summary>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // Allow control keys (Backspace etc.)
            if (char.IsControl(e.KeyChar))
            {
                base.OnKeyPress(e);
                return;
            }

            // Block input if already at max length
            if (FastText.Length >= MaxLength)
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
        /// Validates required state + credit card number format.
        /// Must be exactly 16 digits.
        /// </summary>
        public override bool Validate()
        {
            string digits = FastText.Trim();

            // Required field check
            if (Required && digits == "")
            {
                ErrorMessage = "Card number required.";
                ApplyInvalidStyle();
                return false;
            }

            // Must be exactly 16 digits
            if (digits.Length != MaxLength)
            {
                ErrorMessage = "Card number must be 16 digits.";
                ApplyInvalidStyle();
                return false;
            }

            // Ensure all are digits (InputType already enforces this, but validation covers extra safety)
            if (!Regex.IsMatch(digits, @"^[0-9]{16}$"))
            {
                ErrorMessage = "Invalid card number.";
                ApplyInvalidStyle();
                return false;
            }

            ApplyValidStyle();
            return true;
        }
    }
}

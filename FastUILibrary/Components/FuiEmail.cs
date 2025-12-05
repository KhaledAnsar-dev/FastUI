using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FastUI.FastUILibrary.Components
{
    /// <summary>
    /// A specialized FastUI TextBox designed for email validation.
    /// Provides:
    /// - Required field enforcement
    /// - Regex-based email validation
    /// - Automatic visual feedback (red border on invalid input)
    /// </summary>
    public class FuiEmail : FuiTextBox
    {
        // ============================================================
        //  Fields
        // ============================================================

        // Stores original border colors to restore them after validation
        private Color _originalBorderColor;
        private Color _originalFocusBorderColor;

        /// <summary>
        /// Indicates whether the current email value is valid.
        /// </summary>
        public bool IsValid { get; private set; } = false;


        // ============================================================
        //  Properties
        // ============================================================

        /// <summary>
        /// If true, the field cannot lose focus until a valid email is entered.
        /// </summary>
        [Category("Fast Validation")]
        public bool Required { get; set; } = false;


        // ============================================================
        //  Constructor
        // ============================================================

        public FuiEmail()
        {
            Placeholder = "example@mail.com";

            // Save original styling
            _originalBorderColor = BorderColor;
            _originalFocusBorderColor = FocusBorderColor;

            CausesValidation = true;
        }


        // ============================================================
        //  Public API
        // ============================================================

        /// <summary>
        /// Performs validation and applies visual feedback.
        /// Returns true if the email is valid.
        /// </summary>
        public bool Validate()
        {
            bool result = ValidateInternal();
            ApplyValidationStyle(result);
            return result;
        }


        // ============================================================
        //  Internal Logic
        // ============================================================

        /// <summary>
        /// Internal email validation logic using regex.
        /// </summary>
        private bool ValidateInternal()
        {
            if (string.IsNullOrWhiteSpace(FastText))
                return false;

            return Regex.IsMatch(FastText, @"^[\w\.-]+@[\w\.-]+\.\w+$");
        }

        /// <summary>
        /// Applies visual feedback depending on validation result.
        /// Keeps red color when invalid, restores original styling when valid.
        /// </summary>
        private void ApplyValidationStyle(bool ok)
        {
            IsValid = ok;

            if (ok)
            {
                BorderColor = _originalBorderColor;
                FocusBorderColor = _originalFocusBorderColor;
            }
            else
            {
                BorderColor = Color.Red;
                FocusBorderColor = Color.Red;
            }

            Invalidate();
        }


        // ============================================================
        //  Required Field Behavior
        // ============================================================

        /// <summary>
        /// Prevents losing focus when Required = true and input is invalid.
        /// </summary>
        protected override void OnValidating(CancelEventArgs e)
        {
            bool result = ValidateInternal();
            ApplyValidationStyle(result);

            if (Required && !result)
            {
                e.Cancel = true;
            }

            base.OnValidating(e);
        }


        // ============================================================
        //  Focus Behavior
        // ============================================================

        /// <summary>
        /// Ensures the red border persists when regaining focus if still invalid.
        /// </summary>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            if (!IsValid && Required)
            {
                BorderColor = Color.Red;
                FocusBorderColor = Color.Red;
                Invalidate();
            }
        }
    }
}

using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FastUI.FastUILibrary.Components
{
    /// <summary>
    /// A specialized FastUI TextBox designed for Algerian phone numbers.
    /// Supports:
    /// - Required field enforcement
    /// - Regex-based phone validation (05/06/07 + 8 digits)
    /// - Automatic visual feedback (red border on invalid input)
    /// </summary>
    public class FuiPhoneDz : FuiTextBox
    {
        // ============================================================
        //  Fields
        // ============================================================

        private Color _originalBorderColor;
        private Color _originalFocusBorderColor;

        /// <summary>
        /// Indicates whether the current phone value is valid.
        /// </summary>
        public bool IsValid { get; private set; } = false;


        // ============================================================
        //  Properties
        // ============================================================

        /// <summary>
        /// If true, the field cannot lose focus until the value is valid.
        /// </summary>
        [Category("Fast Validation")]
        public bool Required { get; set; } = false;


        // ============================================================
        //  Constructor
        // ============================================================

        public FuiPhoneDz()
        {
            Placeholder = "06 12 34 56 78";

            _originalBorderColor = BorderColor;
            _originalFocusBorderColor = FocusBorderColor;

            CausesValidation = true;
        }


        // ============================================================
        //  Public API
        // ============================================================

        /// <summary>
        /// Performs validation and applies visual feedback.
        /// Returns true if the phone number is valid.
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
        /// Validates Algerian phone numbers (05/06/07 + 8 digits).
        /// </summary>
        private bool ValidateInternal()
        {
            if (string.IsNullOrWhiteSpace(FastText))
                return false;

            // Remove spaces to accept user formatting
            string raw = FastText.Replace(" ", "");

            return Regex.IsMatch(raw, @"^(05|06|07)[0-9]{8}$");
        }

        /// <summary>
        /// Applies validation styling (red when invalid, original when valid).
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
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            // نزيل أي حرف غير رقم
            string filtered = new string(FastText.Where(char.IsDigit).ToArray());

            if (filtered != FastText)
            {
                FastText = filtered;
                Invalidate();
            }
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            bool result = ValidateInternal();
            ApplyValidationStyle(result);

            if (Required && !result)
                e.Cancel = true;

            base.OnValidating(e);
        }


        // ============================================================
        //  Focus Behavior
        // ============================================================

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

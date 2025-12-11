using FastUI.FastUILibrary.Core;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FastUI.FastUILibrary.Components
{
    /// <summary>
    /// A secured FastUI textbox specialized for password handling.
    /// Features:
    /// - Masked input (•)
    /// - Internal RealText storage
    /// - Required validation
    /// - Minimum length rule
    /// - Forbidden password list
    /// - Optional complexity validation
    /// </summary>
    public class FuiPassword : FuiValidatedTextBox
    {
        // ============================================================
        // Internal State
        // ============================================================



        // ============================================================
        // Properties (Categorized)
        // ============================================================

        // -----------------------------
        // Fast A - Text
        // -----------------------------

        /// <summary>
        /// Stores the actual unmasked password.
        /// This value is never shown to the user; only '•' is displayed.
        /// </summary>
        [Category("Fast A - Text")]
        public string RealText { get; private set; } = "";


        // -----------------------------
        // Fast H - Password Rules
        // -----------------------------

        /// <summary>
        /// A list of password strings that are not allowed.
        /// Example: "123456", "password", etc.
        /// </summary>
        [Category("Fast H - Password Rules")]
        public string[] ForbiddenPasswords { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Minimum number of characters required to accept the password.
        /// </summary>
        [Category("Fast H - Password Rules")]
        public int MinimumLength { get; set; } = 6;

        /// <summary>
        /// If enabled, password must include:
        /// - Lowercase letter
        /// - Uppercase letter
        /// - Digit
        /// - Symbol
        /// </summary>
        [Category("Fast H - Password Rules")]
        public bool RequireComplexity { get; set; } = false;


        // ============================================================
        // Constructor
        // ============================================================

        /// <summary>
        /// Initializes the password control with default placeholder and settings.
        /// </summary>
        public FuiPassword()
        {
            InputType = FastInputType.Any; // custom handling is done manually
            Placeholder = "••••••";
        }


        // ============================================================
        // Key Input Handling
        // ============================================================

        /// <summary>
        /// Intercepts and manually processes all keystrokes to apply masking
        /// and maintain an internal unmasked password buffer.
        /// </summary>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // Allow control keys like Backspace
            if (char.IsControl(e.KeyChar))
            {
                HandleControlKey(e);
                return;
            }

            // Prevent spaces in passwords
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
                return;
            }

            // Append actual key to hidden RealText
            RealText += e.KeyChar;

            // Display masked characters
            FastText = new string('•', RealText.Length);

            // Completely take over key handling
            e.Handled = true;
        }

        /// <summary>
        /// Handles special control keys such as Backspace.
        /// Updates both RealText and the masked display version.
        /// </summary>
        private void HandleControlKey(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                if (RealText.Length > 0)
                    RealText = RealText.Substring(0, RealText.Length - 1);

                FastText = new string('•', RealText.Length);
                e.Handled = true;
            }
        }


        // ============================================================
        // Validation Pipeline
        // ============================================================

        /// <summary>
        /// Runs the full password validation pipeline in order:
        /// Required → MinimumLength → Forbidden → Complexity.
        /// </summary>
        public override bool Validate()
        {
            if (!ValidateRequired()) return false;
            if (!ValidateMinimumLength()) return false;
            if (!ValidateForbidden()) return false;
            if (!ValidateComplexity()) return false;

            ApplyValidStyle();
            return true;
        }

        /// <summary>
        /// Validates that a password is present when Required is true.
        /// </summary>
        private bool ValidateRequired()
        {
            if (Required && RealText.Trim() == "")
            {
                ErrorMessage = "Password required.";
                ApplyInvalidStyle();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Ensures the password meets the minimum character length.
        /// </summary>
        private bool ValidateMinimumLength()
        {
            if (RealText.Length < MinimumLength)
            {
                ErrorMessage = $"Password must be at least {MinimumLength} characters.";
                ApplyInvalidStyle();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Rejects passwords that match any value in ForbiddenPasswords.
        /// Case insensitive check.
        /// </summary>
        private bool ValidateForbidden()
        {
            foreach (var bad in ForbiddenPasswords)
            {
                if (RealText.Equals(bad, StringComparison.OrdinalIgnoreCase))
                {
                    ErrorMessage = "This password is not allowed.";
                    ApplyInvalidStyle();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Validates password complexity (upper, lower, digit, symbol) if enabled.
        /// </summary>
        private bool ValidateComplexity()
        {
            if (!RequireComplexity)
                return true;

            bool hasLower = Regex.IsMatch(RealText, "[a-z]");
            bool hasUpper = Regex.IsMatch(RealText, "[A-Z]");
            bool hasDigit = Regex.IsMatch(RealText, "[0-9]");
            bool hasSymbol = Regex.IsMatch(RealText, "[^a-zA-Z0-9]");

            if (!(hasLower && hasUpper && hasDigit && hasSymbol))
            {
                ErrorMessage = "Password must contain upper, lower, number, and symbol.";
                ApplyInvalidStyle();
                return false;
            }

            return true;
        }


        // ============================================================
        // Events
        // ============================================================

        /// <summary>
        /// Fires whenever the password value changes.
        /// Not used internally but available for external consumers.
        /// </summary>
        public event EventHandler PasswordChanged;

        /// <summary>
        /// Raises PasswordChanged event.
        /// </summary>
        private void RaisePasswordChanged()
        {
            PasswordChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

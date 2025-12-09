using FastUI.FastUILibrary.Core;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FastUI.FastUILibrary.Components
{
    /// <summary>
    /// A FastUI validated textbox for 24-hour time input in HH:mm format.
    /// Features:
    /// - Auto-inserts ":" after 2 digits
    /// - Blocks invalid characters
    /// - Smart backspace handling
    /// - Required validation
    /// - HH:mm strict format validation
    /// </summary>
    public class FuiTime : FuiValidatedTextBox
    {
        // ============================================================
        //  Constructor
        // ============================================================

        public FuiTime()
        {
            Placeholder = "HH:mm";
            InputType = FastInputType.Any; // We handle restrictions manually
        }


        // ============================================================
        //  Input Handling (KeyPress)
        // ============================================================

        /// <summary>
        /// Enforces HH:mm typing rules:
        /// - Allows only digits
        /// - Auto-inserts colon ":"
        /// - Prevents exceeding 5 characters
        /// - Custom backspace behavior for removing ":" correctly
        /// </summary>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // Always allow Backspace but handle it manually
            if (e.KeyChar == (char)Keys.Back)
            {
                HandleBackspace();
                e.Handled = true;
                return;
            }

            // Only allow digits
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            // Prevent typing past HH:mm length
            if (FastText.Length >= 5)
            {
                e.Handled = true;
                return;
            }

            // Let base insert the digit
            base.OnKeyPress(e);

            // After typing 2 digits, insert colon automatically
            if (FastText.Length == 2)
            {
                FastText += ":";
                CaretToEnd();
            }
        }


        // ============================================================
        //  Backspace Logic
        // ============================================================

        /// <summary>
        /// Custom delete logic to properly handle the ":" separator.
        /// </summary>
        private void HandleBackspace()
        {
            string t = FastText;

            // If deleting colon → clear everything (HH:)
            if (t.EndsWith(":"))
            {
                FastText = "";
                CaretToEnd();
                return;
            }

            // Normal deletion
            if (t.Length > 0)
                FastText = t.Substring(0, t.Length - 1);

            CaretToEnd();
        }

        /// <summary>
        /// Forces the caret position to the end since FuiTextBox manages caret manually.
        /// </summary>
        private void CaretToEnd()
        {
            this.Focus();

            typeof(FuiTextBox)
                .GetField("_caretIndex",
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance)
                .SetValue(this, FastText.Length);

            Invalidate();
        }


        // ============================================================
        //  Validation (HH:mm strict format)
        // ============================================================

        /// <summary>
        /// Validates required state + strict 24-hour time format (HH:mm).
        /// </summary>
        public override bool Validate()
        {
            string txt = FastText.Trim();

            // Required field
            if (Required && txt == "")
            {
                ErrorMessage = "Time is required.";
                ApplyInvalidStyle();
                return false;
            }

            // Valid 24-hour format
            bool ok = Regex.IsMatch(txt, @"^([01][0-9]|2[0-3]):[0-5][0-9]$");

            if (!ok)
            {
                ErrorMessage = "Invalid time format. Use HH:mm.";
                ApplyInvalidStyle();
                return false;
            }

            ApplyValidStyle();
            return true;
        }
    }
}

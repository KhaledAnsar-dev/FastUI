using FastUI.FastUILibrary.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FastUI.FastUILibrary.Components
{
    /// <summary>
    /// A FastUI validated textbox for date values.
    /// Supports:
    /// - Required field validation
    /// - Any valid system-parsable date (via DateTime.TryParse)
    /// - Automatic styling feedback when invalid
    /// </summary>
    public class FuiDate : FuiValidatedTextBox
    {
        // ============================================================
        //  Constructor
        // ============================================================

        public FuiDate()
        {
            // Allows digits + dashes (free-form input validated by TryParse)
            InputType = FastInputType.Any;

            // User-friendly placeholder
            Placeholder = "YYYY-MM-DD";
        }


        // ============================================================
        //  Validation Override
        // ============================================================

        /// <summary>
        /// Validates required state + whether the input is a valid date.
        /// Uses DateTime.TryParse for flexible parsing.
        /// </summary>
        public override bool Validate()
        {
            string txt = FastText.Trim();

            // Required check
            if (Required && txt == "")
            {
                ErrorMessage = "Date is required.";
                ApplyInvalidStyle();
                return false;
            }

            // Validate with TryParse
            bool ok = DateTime.TryParse(txt, out _);

            if (!ok)
            {
                ErrorMessage = "Invalid date format.";
                ApplyInvalidStyle();
                return false;
            }

            // Restore default styling
            ApplyValidStyle();
            return true;
        }
    }
}

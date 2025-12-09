using FastUI.FastUILibrary.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FastUI.FastUILibrary.Components
{
    /// <summary>
    /// Base class for all FastUI validated textboxes.
    /// Adds:
    /// - Required field enforcement
    /// - Error messaging
    /// - Automatic visual feedback (red border when invalid)
    /// 
    /// InputType and AllowSpace are locked (read-only) to ensure
    /// the derived specialized controls define them internally.
    /// </summary>
    public class FuiValidatedTextBox : FuiTextBox
    {
        // ============================================================
        //  Locked Properties (ReadOnly in Designer)
        // ============================================================

        /// <summary>
        /// Validation controls define InputType internally.
        /// Designer cannot modify this value.
        /// </summary>
        [Browsable(true)]
        [ReadOnly(true)]
        public new FastInputType InputType
        {
            get => base.InputType;
            protected set => base.InputType = value;
        }

        /// <summary>
        /// Prevents designers from toggling space input.
        /// Validation rules control this internally.
        /// </summary>
        [Browsable(true)]
        [ReadOnly(true)]
        public new bool AllowSpace
        {
            get => base.AllowSpace;
            protected set => base.AllowSpace = value;
        }


        // ============================================================
        //  Validation Properties
        // ============================================================

        /// <summary>
        /// When true, the field cannot lose focus unless valid.
        /// </summary>
        [Category("Fast Validation")]
        public bool Required { get; set; } = false;

        /// <summary>
        /// Contains the validation error message, if any.
        /// </summary>
        [Category("Fast Validation")]
        public string ErrorMessage { get; protected set; } = "";


        // ============================================================
        //  Internal Styling State
        // ============================================================

        private Color _originalBorderColor;
        private Color _originalFocusBorderColor;
        private bool _originalSaved = false;


        // ============================================================
        //  Constructor
        // ============================================================

        public FuiValidatedTextBox()
        {
            // Important for OnValidating to fire
            CausesValidation = true;
        }


        // ============================================================
        //  Initialization
        // ============================================================

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            // Save initial colors once
            if (!_originalSaved)
            {
                _originalBorderColor = BorderColor;
                _originalFocusBorderColor = FocusBorderColor;
                _originalSaved = true;
            }
        }


        // ============================================================
        //  Validation Logic
        // ============================================================

        /// <summary>
        /// Performs validation for the current field.
        /// Derived classes override this for specific rules.
        /// </summary>
        public virtual bool Validate()
        {
            string txt = FastText.Trim();

            if (Required && txt.Length == 0)
            {
                ErrorMessage = "This field is required.";
                ApplyInvalidStyle();
                return false;
            }

            ApplyValidStyle();
            return true;
        }


        // ============================================================
        //  Styling Feedback
        // ============================================================

        /// <summary>
        /// Applies red borders to indicate invalid input.
        /// </summary>
        protected void ApplyInvalidStyle()
        {
            BorderColor = Color.Red;
            FocusBorderColor = Color.Red;
        }

        /// <summary>
        /// Restores original border colors when valid.
        /// </summary>
        protected void ApplyValidStyle()
        {
            if (_originalSaved)
            {
                BorderColor = _originalBorderColor;
                FocusBorderColor = _originalFocusBorderColor;
            }
        }


        // ============================================================
        //  Focus Behavior
        // ============================================================

        /// <summary>
        /// Prevents losing focus if Required = true and the user input is invalid.
        /// </summary>
        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);

            bool isValid = Validate();

            if (!isValid && Required)
                e.Cancel = true;
        }
    }
}

using FastUI.FastUILibrary.Core;
using FastUI.FastUILibrary.Core.Interfaces;
using FastUI.FastUILibrary.Core.Shadow;
using FastUI.FastUILibrary.Core.Shadow.Adapters;
using FastUI.FastUILibrary.Modules.Input.FastTextBoxUI.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastUI.Modules.Input.FastTextBoxUI
{
    public partial class FastTextBox : UserControl
    {
        // =====================================================================
        //  Fields
        // =====================================================================

        // Original border color (used to restore normal style)
        private Color _defaultBorderColor;

        private FastEnumInputType _inputType = FastEnumInputType.Text;


        // Handles all shadow layout logic (padding, resizing, positioning)
        private FastShadowEngine _shadowEngine;

        // Adapter that unifies how the inner control exposes shadow properties
        private IFastShadowTarget _shadowAdapter;


        // =====================================================================
        //  Constructors
        // =====================================================================
        public FastTextBox()
        {
            InitializeComponent();

            _shadowAdapter = new GunaShadowAdapter(textBox);
            _shadowEngine = new FastShadowEngine(this, _shadowAdapter);     
        }

        // =====================================================================
        //  Public Properties
        // =====================================================================

        #region Fast General

        [Browsable(true)]
        [Category("FastGeneral")]
        [Description("The actual text entered by the user.")]
        public string Value
        {
            get => textBox.Text;
            set => textBox.Text = value;
        }

        [Browsable(true)]
        [Category("FastGeneral")]
        [Description("The color used to display the text entered by the user.")]
        public Color ValueColor
        {
            get => textBox.ForeColor;
            set => textBox.ForeColor = value;
        }


        [Browsable(true)]
        [Category("FastGeneral")]
        [Description("Enable or disable multi-line text input.")]
        public bool AllowMultiLine
        {
            get => textBox.Multiline;
            set
            {
                textBox.Multiline = value;

                // optional: adjusting control height for multiline
                if (value)
                    textBox.Height = this.Height - 2;

                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("FastGeneral")]
        [Description("Enable vertical scroll.")]
        public bool EnableVerticalScroll
        {
            get => textBox.ScrollBars == ScrollBars.Vertical;
            set
            {
                if (value)
                {
                    textBox.Multiline = true;
                    textBox.ScrollBars = ScrollBars.Vertical;
                }
                else
                {
                    textBox.Multiline = false;
                    textBox.ScrollBars = ScrollBars.None;
                }
            }
        }


        [Browsable(true)]
        [Category("FastGeneral")]
        [Description("The text displayed when the input field is empty.")]
        public string EmptyText
        {
            get => textBox.PlaceholderText;
            set => textBox.PlaceholderText = value;
        }

        [Browsable(true)]
        [Category("FastGeneral")]
        [Description("The color used to display the empty placeholder text.")]
        public Color EmptyTextColor
        {
            get => textBox.PlaceholderForeColor;
            set => textBox.PlaceholderForeColor = value;
        }

        [Browsable(true)]
        [Category("FastGeneral")]
        [Description("The width of the input component.")]
        public int ControlWidth
        {
            get => _shadowAdapter.ShadowEnabled? textBox.Width : this.Width;
            set
            {
                if (_shadowAdapter.ShadowEnabled)
                {
                    // When shadow is enabled: resize only the inner textbox and reapply padding
                    textBox.Width = value;
                    _shadowEngine.Apply();
                }
                else
                {
                    // Without shadow: resize the container directly
                    this.Width = value;
                }
            }
        }

        [Browsable(true)]
        [Category("FastGeneral")]
        [Description("The height of the input component.")]
        public int ControlHeight
        {
            get => _shadowAdapter.ShadowEnabled ? textBox.Height : this.Height;
            set
            {
                if (_shadowAdapter.ShadowEnabled)
                {
                    // When shadow is enabled: resize inner textbox and update shadow layout
                    textBox.Height = value;
                    _shadowEngine.Apply();
                }
                else
                {
                    // Without shadow: resize the container itself
                    this.Height = value;
                }
            }
        }

        #endregion

        // ---------------------------------------------------------------------

        #region Fast Style

        [Browsable(true)]
        [Category("FastStyle")]
        [Description("Background color of the input field.")]
        public Color FillColor
        {
            get => textBox.FillColor;
            set => textBox.FillColor = value;
        }

        [Browsable(true)]
        [Category("FastStyle")]
        [Description("Determines how rounded the corners of the input field are.")]
        public int CornerRadius
        {
            get => textBox.BorderRadius;
            set => textBox.BorderRadius = value;
        }

        [Browsable(true)]
        [Category("FastStyle")]
        [Description("Thickness of the field border.")]
        public int BorderWidth
        {
            get => textBox.BorderThickness;
            set => textBox.BorderThickness = value;
        }


        [Browsable(true)]
        [Category("FastStyle")]
        [Description("Color of the field border.")]
        public Color BorderColor
        {
            get => textBox.BorderColor;
            set
            {
                textBox.BorderColor = value;
                _defaultBorderColor = textBox.BorderColor; // store original color
            }
        }


        #endregion

        // ---------------------------------------------------------------------

        #region Fast Text

        [Browsable(true)]
        [Category("FastText")]
        [Description("The size of the text typed by the user.")]
        public float FontSize
        {
            get => textBox.Font.Size;
            set
            {
                textBox.Font = new Font(textBox.Font.FontFamily, value);

                // Ensure font-size changes do NOT alter the control's dimensions.
                textBox.Height = this.Height;
                textBox.Width = this.Width;
            }
        }

        [Browsable(true)]
        [Category("FastText")]
        [Description("The color of the main input text.")]
        public Color FontColor
        {
            get => textBox.ForeColor;
            set => textBox.ForeColor = value;
        }

        [Browsable(true)]
        [Category("FastText")]
        [Description("Full font settings for the input text.")]
        public Font MoreFontSettings
        {
            get => textBox.Font;
            set => textBox.Font = value;
        }


        [Browsable(true)]
        [Category("FastText")]
        [Description("Moves the displayed text horizontally inside the textbox.")]
        public int MoveTextHorizontal
        {
            get => textBox.TextOffset.X;
            set
            {
                textBox.TextOffset = new Point(value, textBox.TextOffset.Y);
            }
        }

        [Browsable(true)]
        [Category("FastText")]
        [Description("Moves the displayed text vertically inside the textbox.")]
        public int MoveTextVertical
        {
            // The framework interprets vertical offset in the opposite direction,
            // so we invert the value to match what the user naturally expects.
            get => -textBox.TextOffset.Y;
            set
            {
                int correctedValue = -value;
                textBox.TextOffset = new Point(textBox.TextOffset.X, correctedValue);
            }
        }


        [Browsable(true)]
        [Category("FastText")]
        [Description("Defines the text alignment inside the control.")]
        public FastEnumPosition TextPosition
        {
            get
            {
                return textBox.TextAlign switch
                {
                    HorizontalAlignment.Center => FastEnumPosition.Center,
                    HorizontalAlignment.Right => FastEnumPosition.Right,
                    _ => FastEnumPosition.Left
                };
            }
            set
            {
                textBox.TextAlign = value switch
                {
                    FastEnumPosition.Center => HorizontalAlignment.Center,
                    FastEnumPosition.Right => HorizontalAlignment.Right,
                    _ => HorizontalAlignment.Left
                };
            }
        }

        #endregion

        // ---------------------------------------------------------------------

        #region Fast Interaction

        [Browsable(true)]
        [Category("FastInteraction")]
        [Description("Background color when the mouse is hovering over the field.")]
        public Color HoverFillColor
        {
            get => textBox.HoverState.FillColor;
            set => textBox.HoverState.FillColor = value;
        }

        [Browsable(true)]
        [Category("FastInteraction")]
        [Description("Text color when the mouse is hovering over the field.")]
        public Color HoverTextColor
        {
            get => textBox.HoverState.ForeColor;
            set => textBox.HoverState.ForeColor = value;
        }

        [Browsable(true)]
        [Category("FastInteraction")]
        [Description("Border color when the mouse is hovering over the field.")]
        public Color HoverBorderColor
        {
            get => textBox.HoverState.BorderColor;
            set => textBox.HoverState.BorderColor = value;
        }

        [Browsable(true)]
        [Category("FastInteraction")]
        [Description("Background color when the field is focused.")]
        public Color FocusFillColor
        {
            get => textBox.FocusedState.FillColor;
            set => textBox.FocusedState.FillColor = value;
        }

        [Browsable(true)]
        [Category("FastInteraction")]
        [Description("Text color when the field is focused.")]
        public Color FocusTextColor
        {
            get => textBox.FocusedState.ForeColor;
            set => textBox.FocusedState.ForeColor = value;
        }

        [Browsable(true)]
        [Category("FastInteraction")]
        [Description("Border color when the field is focused.")]
        public Color FocusBorderColor
        {
            get => textBox.FocusedState.BorderColor;
            set => textBox.FocusedState.BorderColor = value;
        }
        #endregion

        // ---------------------------------------------------------------------

        #region FastShadow  // ---- GENERAL SHADOW SETTINGS ----

        [Browsable(true)]
        [Category("FastShadow")]
        [Description("Enables or disables shadow around the control.")]
        public bool ShadowEnabled
        {
            get => _shadowAdapter.ShadowEnabled;
            set
            {
                _shadowAdapter.ShadowEnabled = value;
                if (value)
                {
                    // Enable shadow: remove docking and sync inner control size with the container
                    _shadowAdapter.Dock = DockStyle.None;
                    textBox.Size = this.Size;
                }
                else
                {
                    // Disable shadow: reset layout and dock the control to fill the container
                    _shadowEngine.Disable();
                    _shadowAdapter.Dock = DockStyle.Fill;

                }
            }
        }

        [Browsable(true)]
        [Category("FastShadow")]
        [Description("Color of the shadow around the control.")]
        public Color ShadowColor
        {
            get => textBox.ShadowDecoration.Color;
            set => textBox.ShadowDecoration.Color = value;
        }

        [Browsable(true)]
        [Category("FastShadow")]
        [Description("Shadow blur radius (how soft the shadow looks).")]
        public int ShadowBlur
        {
            get => textBox.ShadowDecoration.Depth;
            set => textBox.ShadowDecoration.Depth = value;
        }

        #endregion


        #region FastShadowEdges  // ---- PER-SIDE SHADOW VALUES ----

        [Browsable(true)]
        [Category("FastShadowEdges")]
        [Description("Shadow size on the top side.")]
        public int ShadowTop
        {
            get => textBox.ShadowDecoration.Shadow.Top;
            set
            {
                // Apply top shadow padding only when shadow is enabled (or when removing it using zero).
                if (_shadowAdapter.ShadowEnabled || value == 0)
                    _shadowEngine.SetTop(value);
            }
        }

        [Browsable(true)]
        [Category("FastShadowEdges")]
        [Description("Shadow size on the bottom side.")]
        public int ShadowBottom
        {
            get => textBox.ShadowDecoration.Shadow.Bottom;
            set
            {
                if (_shadowAdapter.ShadowEnabled || value == 0)
                    _shadowEngine.SetBottom(value);
            }
        }

        [Browsable(true)]
        [Category("FastShadowEdges")]
        [Description("Shadow size on the left side.")]
        public int ShadowLeft
        {
            get => textBox.ShadowDecoration.Shadow.Left;
            set
            {
                if (_shadowAdapter.ShadowEnabled || value == 0)
                    _shadowEngine.SetLeft(value);
            }
        }

        [Browsable(true)]
        [Category("FastShadowEdges")]
        [Description("Shadow size on the right side.")]
        public int ShadowRight
        {
            get => textBox.ShadowDecoration.Shadow.Right;
            set
            {
                if (_shadowAdapter.ShadowEnabled || value == 0)
                    _shadowEngine.SetRight(value);
            }
        }

        #endregion


        // =====================================================================
        //  INTERNAL EVENTS
        // =====================================================================

        // Removes the placeholder on first click so the user
        // can start typing immediately.
        private void textBox_Click(object sender, EventArgs e)
        {
            textBox.PlaceholderText = string.Empty;
        }

        // Restores placeholder and applies validation rules when leaving the field.
        private void textBox_MouseLeave(object sender, EventArgs e)
        {
            // 1) Empty → reset placeholder and restore normal border
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "";
                textBox.PlaceholderText = FastUtilsTextBox.GetPlaceholder(_inputType);
                fakeFocus.Focus();
                return;
            }

            // 2) Validate content based on input type
            bool valid = FastValidation.IsValid(_inputType, textBox.Text);


            // 3) Invalid → show error border
            if (!valid)
                textBox.BorderColor = Color.Red;
            // 4) Valid → restore original border color
            else
                textBox.BorderColor = _defaultBorderColor;

            fakeFocus.Focus();

        }
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool allowed = FastValidation.IsKeyAllowed(_inputType, e, textBox.Text);

            if (!allowed)
                e.Handled = true;
        }


        #region forDelete

        private FastEnumStyle _savedStyle = FastEnumStyle.normal;

        [Browsable(true)]
        [Category("FastForDelete")]
        public FastEnumStyle SetStyle
        {
            get => _savedStyle; set
            {
                if (value == FastEnumStyle.Windows11)
                {
                    _savedStyle = value;
                    FastUtilsTextBox.ChangeStyle(this);
                }
            }
        }

        #endregion

    }

}

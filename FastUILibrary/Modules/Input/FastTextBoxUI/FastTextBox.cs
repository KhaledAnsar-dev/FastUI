using FastUI.FastUILibrary.Core;
using FastUI.FastUILibrary.Core.Shadow;
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

        // Stores the original border color selected by the user or designer.
        // Used to restore the normal styling after showing validation errors.
        private Color _defaultBorderColor;
        private FastEnumInputType _inputType = FastEnumInputType.Text;


        // Preserve original user-defined size
        private Size _originalSize;

        // Shadow values (kept for layout recalculation)
        private int _shadowTop = 0;
        private int _shadowBottom = 0;
        private int _shadowLeft = 0;
        private int _shadowRight = 0;


        // =====================================================================
        //  Constructors
        // =====================================================================
        public FastTextBox()
        {
            InitializeComponent();

            // Save initial size as the base size for shadow operations
            _originalSize = new Size(this.Width, this.Height);

            // Reset Guna default shadow values to zero
            var s = textBox.ShadowDecoration.Shadow;
            s.Top = 0;
            s.Bottom = 0;
            s.Right = 0;
            s.Left = 0;
            textBox.ShadowDecoration.Shadow = s;

            // Reset Guna default shadow depth
            textBox.ShadowDecoration.Depth = 5;
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
            get => textBox.Width;
            set
            {
                this.Width = value;
                textBox.Width = value;
            }
        }

        [Browsable(true)]
        [Category("FastGeneral")]
        [Description("The height of the input component.")]
        public int ControlHeight
        {
            get => textBox.Height;
            set
            {
                this.Height = value;
                textBox.Height = value;
            }
        }



        [Browsable(true)]
        [Category("FastGeneral")]
        [Description("Defines the type of data allowed in this input.")]
        public FastEnumInputType InputType
        {
            get => _inputType;
            set
            {
                _inputType = value;

                textBox.PlaceholderText = FastUtilsTextBox.GetPlaceholder(value);
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

        #region FastShadow

        private void ApplyShadowLayout()
        {
            // --- 1) Reset inner control size before applying new shadow settings ---
            // Ensures the TextBox always returns to its base/original dimensions.
            textBox.Size = _originalSize;

            // --- 2) Apply shadow padding to the inner control ---
            // Guna2 shadow uses Padding (Left, Top, Right, Bottom).
            textBox.ShadowDecoration.Shadow = new Padding(
                _shadowLeft, _shadowTop, _shadowRight, _shadowBottom
            );

            // --- 3) Move the inner control to reveal top/left shadow areas ---
            // The textbox is shifted by the shadow values so shadow appears outside.


            int x = _shadowLeft;
            int y = _shadowTop;

            textBox.Location = new Point(x, y);


            // --- 4) Resize the UserControl to include the entire shadow region ---
            // New size = original textbox size + shadow padding on all sides.
            int width = _originalSize.Width + _shadowLeft + _shadowRight;
            int height = _originalSize.Height + _shadowTop + _shadowBottom;

            // Temporarily detach SizeChanged to avoid recursive resizing loops.
            this.SizeChanged -= FastTextBox_SizeChanged;
            this.Size = new Size(width, height);
            this.SizeChanged += FastTextBox_SizeChanged;

            // --- 5) Refresh layout and visuals ---

            this.Invalidate();
        }

        [Browsable(true)]
        [Category("FastShadow")]
        [Description("Enables or disables shadow around the control.")]
        public bool ShadowEnabled
        {
            get => textBox.ShadowDecoration.Enabled;
            set
            {
                textBox.ShadowDecoration.Enabled = value;
                if (value)
                {
                    // Add extra space to render the shadow; 
                    // this area won't be visible but allows the shadow to appear fully.

                    textBox.Dock = DockStyle.None;
                }
                else
                {
                    // because adding shadow will make the cntainer control
                    // bigger then the inner control , so resize will fix thing 
                    // the inner control will be always in the original size
                    // so it will help to retrive the original size for all the control
                    this.Size = textBox.Size;

                    // if we cancel the shadow no need for the old values
                    // because it will apply directly when enbaled the shadow again and 
                    // it can cost errors
                    this.ShadowTop = 0;
                    this.ShadowBottom = 0;
                    this.ShadowLeft = 0;
                    this.ShadowRight = 0;

                    // no shadow so no need for more space this way the inner 
                    // control is the same as the container controls
                    textBox.Dock = DockStyle.Fill;
                }

                // Always match shadow radius to control radius
                textBox.ShadowDecoration.BorderRadius = textBox.BorderRadius;
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


        // relevent to shadow : 

        [Browsable(true)]
        [Category("FastShadow")]
        [Description("Shadow size on the top side.")]
        public int ShadowTop
        {


            get => textBox.ShadowDecoration.Shadow.Top;
            set
            {
                // can not set the shadow values while it is not enabaled 
                // it can work only when the value is zero because when the shadow is set as unenabaled 
                // the previuos values need to set to zeros to avaoid unxpected errors
                if (textBox.ShadowDecoration.Enabled || value == 0)
                {
                    _shadowTop = value;
                    ApplyShadowLayout();
                }
            }
        }

        [Browsable(true)]
        [Category("FastShadow")]
        [Description("Shadow size on the bottom side.")]
        public int ShadowBottom
        {
            get => textBox.ShadowDecoration.Shadow.Bottom;
            set
            {
                if (textBox.ShadowDecoration.Enabled || value == 0)
                {
                    _shadowBottom = value;
                    ApplyShadowLayout();
                }
            }
        }

        [Browsable(true)]
        [Category("FastShadow")]
        [Description("Shadow size on the left side.")]
        public int ShadowLeft
        {
            get => textBox.ShadowDecoration.Shadow.Left;
            set
            {
                if (textBox.ShadowDecoration.Enabled || value == 0)
                {
                    _shadowLeft = value;
                    ApplyShadowLayout();
                }
            }
        }

        [Browsable(true)]
        [Category("FastShadow")]
        [Description("Shadow size on the right side.")]
        public int ShadowRight
        {
            get => textBox.ShadowDecoration.Shadow.Right;
            set
            {
                if (textBox.ShadowDecoration.Enabled || value == 0)
                {
                    _shadowRight = value;
                    ApplyShadowLayout();
                }
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
        private void FastTextBox_SizeChanged(object sender, EventArgs e)
        {
            // relevent to shadow : 
            // because the inner control need space so it can not be always the same as 
            // the container (Dock = Fill), this event handller allow as to truck the original size only when the user changed it 
            // and we can stop this event when we need to change the container control 
            // size when adding shadow space which means the inner control will not chamge its size 
            // at this case
            _originalSize = new Size(this.Width, this.Height);

            // when the shadow doesnt work the inner control and the container
            // control have the same size
            if (!textBox.ShadowDecoration.Enabled)
                textBox.Size = this.Size;
            // when there is a shadow the inner container will get the rest size
            else
            {
                int width = this.Width - textBox.ShadowDecoration.Shadow.Left - textBox.ShadowDecoration.Shadow.Right;
                int height = this.Height - textBox.ShadowDecoration.Shadow.Top - textBox.ShadowDecoration.Shadow.Bottom;
                textBox.Width = width;
                textBox.Height = height;
            }
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

        [Browsable(true)]
        [Category("FastShadow")]
        [Description("Inner Text box size.")]
        public string innerTextBoxSize
        {
            get => $"{textBox.Width} , {textBox.Height}";
        }
        #endregion

    }

}

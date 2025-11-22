using FastUI.Enums;
using FastUI.Helpers;
using FastUI.Helpers.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastUI.Controls.Input
{
    public partial class FastTextBox : UserControl
    {
        public FastTextBox()
        {
            InitializeComponent();

        }
        // Stores the original border color selected by the user or designer.
        // Used to restore the normal styling after showing validation errors.
        private Color _defaultBorderColor;
        private FastInputType _inputType = FastInputType.Text;

        // ----------------------------------------------------------
        // FAST GENERAL
        // ----------------------------------------------------------

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
        public FastInputType InputType
        {
            get => _inputType;
            set
            {
                _inputType = value;

                textBox.PlaceholderText = FastInputValidator.GetPlaceholder(value);
            }
        }


        // ----------------------------------------------------------
        // FAST STYLE
        // ----------------------------------------------------------

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



        // ----------------------------------------------------------
        // FAST TEXT
        // ----------------------------------------------------------

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
        
   
        // ----------------------------------------------------------
        // FAST INTERACTION
        // ----------------------------------------------------------

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
                textBox.PlaceholderText = FastInputValidator.GetPlaceholder(_inputType);
                fakeFocus.Focus();
                return;
            }

            // 2) Validate content based on input type
            bool valid = FastInputValidator.IsValid(_inputType, textBox.Text);


            // 3) Invalid → show error border
            if (!valid)
                textBox.BorderColor = Color.Red;
            // 4) Valid → restore original border color
            else
                textBox.BorderColor = _defaultBorderColor;
        }
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool allowed = FastInputValidator.IsKeyAllowed(_inputType, e, textBox.Text);

            if (!allowed)
                e.Handled = true;
        }

    }
}


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
            set => textBox.BorderColor = value;
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
            set => textBox.Font = new Font(textBox.Font.FontFamily, value);
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
        [Description("Border color when the field is focused.")]
        public Color FocusBorderColor
        {
            get => textBox.FocusedState.BorderColor;
            set => textBox.FocusedState.BorderColor = value;
        }

        // =====================================================================
        //  INTERNAL EVENTS
        // =====================================================================

        // This event ensures the inner textBox always matches 
        // the UserControl's size when the control is resized.
        private void FastTextBox_SizeChanged(object sender, EventArgs e)
        {
            textBox.Width = this.Width;
            textBox.Height = this.Height;
        }


        // Removes the placeholder on first click so the user
        // can start typing immediately.

        private string _placeHolderText = string.Empty;
        private void textBox_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox.PlaceholderText))
                _placeHolderText = textBox.PlaceholderText;

            textBox.PlaceholderText = string.Empty;
        }

        // Restores the placeholder when leaving the field if no text was entered.
        private void textBox_MouseLeave(object sender, EventArgs e)
        {
            if (textBox.Text == string.Empty)
                textBox.PlaceholderText = _placeHolderText;

            fakeFocus.Focus();
        }


    }
}

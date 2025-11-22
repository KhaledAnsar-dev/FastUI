using FastUI.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastUI.Controls.Buttons
{
    public partial class FastButton : UserControl
    {
        public FastButton()
        {
            InitializeComponent();
        }
        // ----------------------------------------------------------
        // FAST GENERAL
        // ----------------------------------------------------------

        [Browsable(true)]
        [Category("FastGeneral")]
        [Description("The actual text entered by the user.")]
        public string ButtonLabel
        {
            get => button.Text;
            set => button.Text = value;
        }


        [Browsable(true)]
        [Category("FastGeneral")]
        [Description("The width of the input component.")]
        public int ControlWidth
        {
            get => button.Width;
            set
            {
                this.Width = value;
                button.Width = value;
            }
        }

        [Browsable(true)]
        [Category("FastGeneral")]
        [Description("The height of the input component.")]
        public int ControlHeight
        {
            get => button.Height;
            set
            {
                this.Height = value;
                button.Height = value;
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
            get => button.FillColor;
            set => button.FillColor = value;
        }

        [Browsable(true)]
        [Category("FastStyle")]
        [Description("Determines how rounded the corners of the input field are.")]
        public int CornerRadius
        {
            get => button.BorderRadius;
            set => button.BorderRadius = value;
        }

        [Browsable(true)]
        [Category("FastStyle")]
        [Description("Thickness of the field border.")]
        public int BorderWidth
        {
            get => button.BorderThickness;
            set => button.BorderThickness = value;
        }

        [Browsable(true)]
        [Category("FastStyle")]
        [Description("Color of the field border.")]
        public Color BorderColor
        {
            get => button.BorderColor;
            set
            {
                button.BorderColor = value;
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
            get => button.Font.Size;
            set
            {
                button.Font = new Font(button.Font.FontFamily, value);

                // Ensure font-size changes do NOT alter the control's dimensions.
                button.Height = this.Height;
                button.Width = this.Width;
            }
        }

        [Browsable(true)]
        [Category("FastText")]
        [Description("The color of the main input text.")]
        public Color FontColor
        {
            get => button.ForeColor;
            set => button.ForeColor = value;
        }

        [Browsable(true)]
        [Category("FastText")]
        [Description("Full font settings for the input text.")]
        public Font MoreFontSettings
        {
            get => button.Font;
            set => button.Font = value;
        }

        [Browsable(true)]
        [Category("FastText")]
        [Description("Move the button text horizontally.")]
        public int MoveTextHorizontal
        {
            get => button.TextOffset.X;
            set
            {
                button.TextOffset = new Point(value, button.TextOffset.Y);
            }
        }

        [Browsable(true)]
        [Category("FastText")]
        [Description("Move the button text vertically.")]
        public int MoveTextVertical
        {
            // The framework interprets vertical offset in the opposite direction,
            // so we invert the value to match what the user naturally expects.
            get => -button.TextOffset.Y;
            set
            {
                int correctedValue = -value;
                button.TextOffset = new Point(button.TextOffset.X, correctedValue);
            }
        }

        [Browsable(true)]
        [Category("FastText")]
        [Description("Defines the text alignment inside the button.")]
        public FastPosition TextPosition
        {
            get
            {
                return button.TextAlign switch
                {
                    HorizontalAlignment.Center => FastPosition.Center,
                    HorizontalAlignment.Right => FastPosition.Right,
                    _ => FastPosition.Left
                };
            }
            set
            {
                button.TextAlign = value switch
                {
                    FastPosition.Center => HorizontalAlignment.Center,
                    FastPosition.Right => HorizontalAlignment.Right,
                    _ => HorizontalAlignment.Left
                };
            }
        }

        // ----------------------------------------------------------
        // FAST INTERACTION
        // ----------------------------------------------------------

        [Browsable(true)]
        [Category("FastInteraction")]
        [Description("Background color when the mouse is hovering over the field.")]
        public Color HoverFillColor
        {
            get => button.HoverState.FillColor;
            set => button.HoverState.FillColor = value;
        }

        [Browsable(true)]
        [Category("FastInteraction")]
        [Description("Text color when the mouse is hovering over the field.")]
        public Color HoverTextColor
        {
            get => button.HoverState.ForeColor;
            set => button.HoverState.ForeColor = value;
        }

        [Browsable(true)]
        [Category("FastInteraction")]
        [Description("Border color when the mouse is hovering over the field.")]
        public Color HoverBorderColor
        {
            get => button.HoverState.BorderColor;
            set => button.HoverState.BorderColor = value;
        }

        // ----------------------------------------------------------
        // FAST IMAGE
        // ----------------------------------------------------------

        [Browsable(true)]
        [Category("FastImage")]
        [Description("The image displayed inside the button.")]
        public Image ButtonImage
        {
            get => button.Image;
            set => button.Image = value;
            
        }

        [Browsable(true)]
        [Category("FastImage")]
        [Description("Defines the horizontal position of the button image.")]
        public FastPosition ImagePosition
        {
            get
            {
                return button.ImageAlign switch
                {
                    
                    HorizontalAlignment.Center => FastPosition.Center,
                    HorizontalAlignment.Right => FastPosition.Right,
                    _ => FastPosition.Left
                };
            }
            set
            {
                button.ImageAlign = value switch
                {
                    FastPosition.Center => HorizontalAlignment.Center,
                    FastPosition.Right => HorizontalAlignment.Right,
                    _ => HorizontalAlignment.Left
                };
            }
        }

        [Browsable(true)]
        [Category("FastImage")]
        [Description("Moves the button image horizontally.")]
        public int MoveImageHorizontal
        {
            get => button.ImageOffset.X;
            set => button.ImageOffset = new Point(value, button.ImageOffset.Y);
        }

        [Browsable(true)]
        [Category("FastImage")]
        [Description("Moves the button image vertically.")]
        public int MoveImageVertical
        {
            get => -button.ImageOffset.Y;
            set
            {
                int correctedValue = -value;
                button.ImageOffset = new Point(button.ImageOffset.X, correctedValue);
            }
        }

        [Browsable(true)]
        [Category("FastImage")]
        [Description("Defines the width of the button image.")]
        public int ImageWidth
        {
            get => button.ImageSize.Width;
            set => button.ImageSize = new Size(value, button.ImageSize.Height);
        }

        [Browsable(true)]
        [Category("FastImage")]
        [Description("Defines the height of the button image.")]
        public int ImageHeight
        {
            get => button.ImageSize.Height;
            set => button.ImageSize = new Size(button.ImageSize.Width, value);
        }

    }
}

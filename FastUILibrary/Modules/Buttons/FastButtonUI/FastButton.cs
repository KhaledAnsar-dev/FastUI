using FastUI.FastUILibrary.Core;
using FastUI.FastUILibrary.Core.Interfaces;
using FastUI.FastUILibrary.Core.Shadow;
using FastUI.FastUILibrary.Core.Shadow.Adapters;
using FastUI.FastUILibrary.Modules.Buttons.FastButtonUI.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FastUI.Modules.Buttons.FastButtonUI
{
    /// <summary>
    /// A customizable FastUI button that supports styling, text and image control,
    /// interaction states, and optional shadow rendering through FastShadowEngine.
    /// </summary>
    public partial class FastButton : UserControl
    {

        // =====================================================================
        //  Fields
        // =====================================================================

        // Handles all shadow layout logic (padding, resizing)
        private FastShadowEngine _shadowEngine;

        // Adapter exposing shadow-related properties of the inner button
        private IFastShadowTarget _shadowAdapter;


        // =====================================================================
        //  Constructors
        // =====================================================================
        public FastButton()
        {
            InitializeComponent();

            // Adapter mapping shadow APIs of the Guna2 button
            _shadowAdapter = new GunaShadowButtonAdapter(button);

            // Core engine that manages shadow padding & layout
            _shadowEngine = new FastShadowEngine(this, _shadowAdapter);
        }


        // =====================================================================
        //  Public Properties
        // =====================================================================

        #region Fast General

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
        [Description("The width of the button component.")]
        public int ControlWidth
        {
            get => _shadowAdapter.ShadowEnabled ? button.Width : this.Width;
            set
            {
                if (_shadowAdapter.ShadowEnabled)
                {
                    // Resize inner button and refresh shadow layout
                    button.Width = value;
                    _shadowEngine.Apply();
                }
                else
                {
                    // Resize container directly
                    this.Width = value;
                }
            }
        }

        [Browsable(true)]
        [Category("FastGeneral")]
        [Description("The height of the button component.")]
        public int ControlHeight
        {
            get => _shadowAdapter.ShadowEnabled ? button.Height : this.Height;
            set
            {
                if (_shadowAdapter.ShadowEnabled)
                {
                    // Resize inner button then update shadow
                    button.Height = value;
                    _shadowEngine.Apply();
                }
                else
                {
                    // Resize container directly
                    this.Height = value;
                }
            }
        }

        #endregion


        // ---------------------------------------------------------------------

        #region Fast Style

        [Browsable(true)]
        [Category("FastStyle")]
        [Description("Background color of the button.")]
        public Color FillColor
        {
            get => button.FillColor;
            set => button.FillColor = value;
        }

        [Browsable(true)]
        [Category("FastStyle")]
        [Description("Determines how rounded the corners of the button are.")]
        public int CornerRadius
        {
            get => button.BorderRadius;
            set => button.BorderRadius = value;
        }

        [Browsable(true)]
        [Category("FastStyle")]
        [Description("Thickness of the button border.")]
        public int BorderWidth
        {
            get => button.BorderThickness;
            set => button.BorderThickness = value;
        }

        [Browsable(true)]
        [Category("FastStyle")]
        [Description("Color of the button border.")]
        public Color BorderColor
        {
            get => button.BorderColor;
            set => button.BorderColor = value;
        }

        #endregion


        // ---------------------------------------------------------------------

        #region Fast Text

        [Browsable(true)]
        [Category("FastText")]
        [Description("The size of the text inside the button.")]
        public float FontSize
        {
            get => button.Font.Size;
            set
            {
                button.Font = new Font(button.Font.FontFamily, value);

                // Keep size unchanged when modifying font
                button.Height = this.Height;
                button.Width = this.Width;
            }
        }

        [Browsable(true)]
        [Category("FastText")]
        [Description("The color of the button text.")]
        public Color FontColor
        {
            get => button.ForeColor;
            set => button.ForeColor = value;
        }

        [Browsable(true)]
        [Category("FastText")]
        [Description("Full font configuration.")]
        public Font MoreFontSettings
        {
            get => button.Font;
            set => button.Font = value;
        }

        [Browsable(true)]
        [Category("FastText")]
        [Description("Moves the button text horizontally.")]
        public int MoveTextHorizontal
        {
            get => button.TextOffset.X;
            set => button.TextOffset = new Point(value, button.TextOffset.Y);
        }

        [Browsable(true)]
        [Category("FastText")]
        [Description("Moves the button text vertically.")]
        public int MoveTextVertical
        {
            get => -button.TextOffset.Y;
            set => button.TextOffset = new Point(button.TextOffset.X, -value);
        }

        [Browsable(true)]
        [Category("FastText")]
        [Description("Defines text alignment inside the button.")]
        public FastEnumPosition TextPosition
        {
            get
            {
                return button.TextAlign switch
                {
                    HorizontalAlignment.Center => FastEnumPosition.Center,
                    HorizontalAlignment.Right => FastEnumPosition.Right,
                    _ => FastEnumPosition.Left
                };
            }
            set
            {
                button.TextAlign = value switch
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
        [Description("Background color on hover.")]
        public Color HoverFillColor
        {
            get => button.HoverState.FillColor;
            set => button.HoverState.FillColor = value;
        }

        [Browsable(true)]
        [Category("FastInteraction")]
        [Description("Text color on hover.")]
        public Color HoverTextColor
        {
            get => button.HoverState.ForeColor;
            set => button.HoverState.ForeColor = value;
        }

        [Browsable(true)]
        [Category("FastInteraction")]
        [Description("Border color on hover.")]
        public Color HoverBorderColor
        {
            get => button.HoverState.BorderColor;
            set => button.HoverState.BorderColor = value;
        }

        #endregion


        // ---------------------------------------------------------------------

        #region Fast Image

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
        [Description("Horizontal alignment of the button image.")]
        public FastEnumPosition ImagePosition
        {
            get
            {
                return button.ImageAlign switch
                {
                    HorizontalAlignment.Center => FastEnumPosition.Center,
                    HorizontalAlignment.Right => FastEnumPosition.Right,
                    _ => FastEnumPosition.Left
                };
            }
            set
            {
                button.ImageAlign = value switch
                {
                    FastEnumPosition.Center => HorizontalAlignment.Center,
                    FastEnumPosition.Right => HorizontalAlignment.Right,
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
            set => button.ImageOffset = new Point(button.ImageOffset.X, -value);
        }

        [Browsable(true)]
        [Category("FastImage")]
        [Description("Image width inside the button.")]
        public int ImageWidth
        {
            get => button.ImageSize.Width;
            set => button.ImageSize = new Size(value, button.ImageSize.Height);
        }

        [Browsable(true)]
        [Category("FastImage")]
        [Description("Image height inside the button.")]
        public int ImageHeight
        {
            get => button.ImageSize.Height;
            set => button.ImageSize = new Size(button.ImageSize.Width, value);
        }

        #endregion


        // ---------------------------------------------------------------------

        #region FastShadow

        [Browsable(true)]
        [Category("FastShadow")]
        [Description("Enables or disables shadow around the button.")]
        public bool ShadowEnabled
        {
            get => _shadowAdapter.ShadowEnabled;
            set
            {
                _shadowAdapter.ShadowEnabled = value;

                if (value)
                {
                    // Enable shadow: undock and sync size
                    _shadowAdapter.Dock = DockStyle.None;
                    button.Size = this.Size;
                    _shadowEngine.Apply();
                }
                else
                {
                    // Disable shadow and restore layout
                    _shadowEngine.Disable();
                    _shadowAdapter.Dock = DockStyle.Fill;
                }
            }
        }

        [Browsable(true)]
        [Category("FastShadow")]
        public Color ShadowColor
        {
            get => _shadowAdapter.ShadowColor;
            set => _shadowAdapter.ShadowColor = value;
        }

        [Browsable(true)]
        [Category("FastShadow")]
        public int ShadowBlur
        {
            get => _shadowAdapter.ShadowBlur;
            set => _shadowAdapter.ShadowBlur = value;
        }

        #endregion


        // ---------------------------------------------------------------------

        #region FastShadowEdges

        [Browsable(true)]
        [Category("FastShadowEdges")]
        public int ShadowTop
        {
            get => _shadowAdapter.ShadowPadding.Top;
            set
            {
                // Apply only if shadow is enabled or being cleared
                if (_shadowAdapter.ShadowEnabled || value == 0)
                    _shadowEngine.SetTop(value);
            }
        }

        [Browsable(true)]
        [Category("FastShadowEdges")]
        public int ShadowBottom
        {
            get => _shadowAdapter.ShadowPadding.Bottom;
            set
            {
                if (_shadowAdapter.ShadowEnabled || value == 0)
                    _shadowEngine.SetBottom(value);
            }
        }

        [Browsable(true)]
        [Category("FastShadowEdges")]
        public int ShadowLeft
        {
            get => _shadowAdapter.ShadowPadding.Left;
            set
            {
                if (_shadowAdapter.ShadowEnabled || value == 0)
                    _shadowEngine.SetLeft(value);
            }
        }

        [Browsable(true)]
        [Category("FastShadowEdges")]
        public int ShadowRight
        {
            get => _shadowAdapter.ShadowPadding.Right;
            set
            {
                if (_shadowAdapter.ShadowEnabled || value == 0)
                    _shadowEngine.SetRight(value);
            }
        }

        #endregion


        // ---------------------------------------------------------------------

        private FastEnumStyle _savedStyle = FastEnumStyle.normal;

        [Browsable(true)]
        [Category("FastForDelete")]
        public FastEnumStyle SetStyle
        {
            get => _savedStyle;
            set
            {
                if (value == FastEnumStyle.Windows11)
                {
                    _savedStyle = value;
                    FastUtilsButton.ChangeStyle(this);
                }
            }
        }
    }

}

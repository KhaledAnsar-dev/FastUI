using FastUI.FastUILibrary.Core.Rendering;
using System;
using FastUI.FastUILibrary.Core;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastUI.FastUILibrary.Core.Styling;
using FastUI.FastUILibrary.Core.Interfaces;
using FastUI.FastUILibrary.Themes;
using FastUI.FastUILibrary.Themes.Infrastructure;

namespace FastUI.FastUILibrary.Components
{
    public class FuiButton : Control
    {
        // ============================================================
        //  Fields
        // ============================================================

        // Renderer used to draw button visuals (background, border, text)
        private FastShapeRenderer _renderer = new FastShapeRenderer();

        // Hover animation state
        private bool _isHovered = false;
        private float _hoverLerp = 0f;
        private readonly float _hoverSpeed = 0.5f;

        // Press animation state
        private bool _isPressed = false;
        private float _pressLerp = 0f;
        private readonly float _pressSpeed = 0.25f;

        // Offset for fine-tuning text position
        private Point _textOffset = Point.Empty;

        private Color _normalColor;
        private Color _borderNormalColor;
        private Color _hoverTextColor;


        // Animation timer (hover + press transitions)
        private System.Windows.Forms.Timer _animTimer;


        // ============================================================
        //  Constructor
        // ============================================================

        public FuiButton()
        {
            Size = new Size(120, 40);
            Font = new Font("Segoe UI", 10f);

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor,
                true
            );

            // Cursor
            this.Cursor = Cursors.Hand;

            BackColor = Color.Transparent;
            UpdateStyles();

            _renderer.BackgroundColor = _normalColor;
            _renderer.BorderColor = _borderNormalColor;

            // Initialize animation timer
            if (!IsInDesigner)
            {
                _animTimer = new System.Windows.Forms.Timer();
                _animTimer.Interval = 15;
                _animTimer.Tick += (s, e) => UpdateAnimation();
                _animTimer.Start();
            }

            ApplyTheme();
        }

        // Determines whether control is inside Visual Studio Designer
        private bool IsInDesigner =>
            DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;



        // ============================================================
        //  Properties (Organized A → G)
        // ============================================================


        // ----------------------------
        // A) TEXT
        // ----------------------------

        [Browsable(true)]
        [Category("Fast A - Text")]
        public override string Text
        {
            get => base.Text;
            set { base.Text = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("Fast A - Text")]
        public Color FontColor
        {
            get => ForeColor;
            set { ForeColor = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("Fast A - Text")]
        public float FontSize
        {
            get => Font.Size;
            set { Font = new Font(Font.FontFamily, value); Invalidate(); }
        }

        [Browsable(true)]
        [Category("Fast A - Text")]
        public Font MoreFontSettings
        {
            get => Font;
            set { Font = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("Fast A - Text")]
        public int MoveTextHorizontal
        {
            get => _textOffset.X;
            set { _textOffset.X = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("Fast A - Text")]
        public int MoveTextVertical
        {
            get => _textOffset.Y;
            set { _textOffset.Y = value; Invalidate(); }
        }


        [Browsable(true)]
        [Category("Fast A - Text")]
        public FastTextAlign TextPosition { get; set; }



        // ----------------------------
        // B) LAYOUT
        // ----------------------------

        [Browsable(true)]
        [Category("Fast B - Layout")]
        public int ControlWidth
        {
            get => Width;
            set { Width = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("Fast B - Layout")]
        public int ControlHeight
        {
            get => Height;
            set { Height = value; Invalidate(); }
        }



        // ----------------------------
        // C) COLORS – NORMAL
        // ----------------------------

        [Browsable(true)]
        [Category("Fast C - Colors Normal")]
        public Color FillColor
        {
            get => _normalColor;
            set { _normalColor = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("Fast C - Colors Normal")]
        public Color BorderColor
        {
            get => _borderNormalColor;
            set { _borderNormalColor = value; Invalidate(); }
        }



        // ----------------------------
        // D) COLORS – HOVER
        // ----------------------------

        [Browsable(true)]
        [Category("Fast D - Colors Hover")]
        public Color HoverFillColor { get; set; }

        [Browsable(true)]
        [Category("Fast D - Colors Hover")]
        public Color HoverBorder { get; set; }

        [Browsable(true)]
        [Category("Fast D - Colors Hover")]
        public Color HoverTextColor
        {
            get => _hoverTextColor;
            set { _hoverTextColor = value; Invalidate(); }
        }



        // ----------------------------
        // E) COLORS – PRESS
        // ----------------------------

        [Browsable(true)]
        [Category("Fast E - Colors Press")]
        public Color PressFillColor { get; set; }

        [Browsable(true)]
        [Category("Fast E - Colors Press")]
        public Color PressBorderColor { get; set; }

        [Browsable(true)]
        [Category("Fast E - Colors Press")]
        public int PressDepth { get; set; } = 1;



        // ----------------------------
        // F) STYLE
        // ----------------------------

        [Browsable(true)]
        [Category("Fast F - Style")]
        public float BorderWidth
        {
            get => _renderer.BorderThickness;
            set { _renderer.BorderThickness = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("Fast F - Style")]
        public float CornerRadius
        {
            get => _renderer.Radius;
            set { _renderer.Radius = value; Invalidate(); }
        }


        private string _themeName = "Windows11";

        [Category("FastUI - Theme")]
        [TypeConverter(typeof(FuiThemeConverter))]
        public string Theme
        {
            get => _themeName;
            set
            {
                _themeName = value;
                ApplyTheme();
            }
        }


        // ============================================================
        //  Animation Logic
        // ============================================================

        private void UpdateAnimation()
        {
            bool needUpdate = false;

            // Hover animation
            if (_isHovered && _hoverLerp < 1f)
            {
                _hoverLerp += _hoverSpeed;
                if (_hoverLerp > 1f) _hoverLerp = 1f;
                needUpdate = true;
            }
            else if (!_isHovered && _hoverLerp > 0f)
            {
                _hoverLerp -= _hoverSpeed;
                if (_hoverLerp < 0f) _hoverLerp = 0f;
                needUpdate = true;
            }

            // Press animation
            if (_isPressed && _pressLerp < 1f)
            {
                _pressLerp += _pressSpeed;
                if (_pressLerp > 1f) _pressLerp = 1f;
                needUpdate = true;
            }
            else if (!_isPressed && _pressLerp > 0f)
            {
                _pressLerp -= _pressSpeed;
                if (_pressLerp < 0f) _pressLerp = 0f;
                needUpdate = true;
            }

            if (needUpdate)
                Invalidate();
        }



        // ============================================================
        //  Color Interpolation
        // ============================================================

        private Color LerpColor(Color a, Color b, float t)
        {
            return Color.FromArgb(
                (int)(a.A + (b.A - a.A) * t),
                (int)(a.R + (b.R - a.R) * t),
                (int)(a.G + (b.G - a.G) * t),
                (int)(a.B + (b.B - a.B) * t)
            );
        }



        // ============================================================
        //  Painting
        // ============================================================

        protected override void OnPaint(PaintEventArgs e)
        {
            bool designer = IsInDesigner;

            Color bg = LerpColor(_normalColor, HoverFillColor, _hoverLerp);
            bg = LerpColor(bg, PressFillColor, _pressLerp);

            Color border = LerpColor(_borderNormalColor, HoverBorder, _hoverLerp);
            border = LerpColor(border, PressBorderColor, _pressLerp);

            Point finalOffset = new Point(
                _textOffset.X,
                _textOffset.Y + (int)(_pressLerp * PressDepth)
            );

            _renderer.BackgroundColor = bg;
            _renderer.BorderColor = border;

            base.OnPaint(e);

            _renderer.Render(
                e.Graphics,
                ClientRectangle,
                Text,
                Font,
                _hoverLerp > 0 ? HoverTextColor : ForeColor,
                designer,
                TextPosition,
                finalOffset
            );
        }



        // ============================================================
        //  Mouse Events
        // ============================================================

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!IsInDesigner) _isHovered = true;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!IsInDesigner) _isHovered = false;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!IsInDesigner) _isPressed = true;
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!IsInDesigner) _isPressed = false;
            base.OnMouseUp(e);
        }

        // ============================================================
        //  Styling
        // ============================================================
       

        private void ApplyStyle(FuiButtonStyle s)
        {
            // A) TEXT
            FontColor = s.FontColor;
            FontSize = s.FontSize;
            MoreFontSettings = s.MoreFontSettings;
            MoveTextHorizontal = s.MoveTextHorizontal;
            MoveTextVertical = s.MoveTextVertical;
            TextPosition = s.TextPosition;

            // B) LAYOUT
            ControlWidth = s.ControlWidth;
            ControlHeight = s.ControlHeight;

            // C) NORMAL
            _normalColor = s.FillColor;
            _borderNormalColor = s.BorderColor;

            // D) HOVER
            HoverFillColor = s.HoverFillColor;
            HoverBorder = s.HoverBorder;
            _hoverTextColor = s.HoverTextColor;

            // E) PRESS
            PressFillColor = s.PressFillColor;
            PressBorderColor = s.PressBorderColor;
            PressDepth = s.PressDepth;

            // F) STYLE
            BorderWidth = s.BorderWidth;
            CornerRadius = s.CornerRadius;

            Invalidate();
        }
        private void ApplyTheme()
        {
            var theme = FuiThemeRegistry.Get(_themeName);
            if (theme != null)
                ApplyStyle(theme.GetButtonStyle());
        }


    }
}

using FastUI.FastUILibrary.Core.Rendering;
using System;
using FastUI.FastUILibrary.Core;
using System.ComponentModel;
using FastUI.FastUILibrary.Themes.Infrastructure;
using FastUI.FastUILibrary.Themes.Presets;

namespace FastUI.FastUILibrary.Components
{
    /// <summary>
    /// A modern animated button control for FastUI.
    /// 
    /// Features:
    /// - Custom rendering engine
    /// - Hover and press animations
    /// - Theme and preset support
    /// - Text alignment and offset control
    /// - Fully styleable colors and shape
    /// </summary>
    public class FuiButton : Control
    {
        // ============================================================
        //  Rendering & Animation Fields
        // ============================================================

        private FastShapeRenderer _renderer = new FastShapeRenderer();

        private bool _isHovered = false;
        private float _hoverLerp = 0f;
        private readonly float _hoverSpeed = 0.5f;

        private bool _isPressed = false;
        private float _pressLerp = 0f;
        private readonly float _pressSpeed = 0.25f;

        private System.Windows.Forms.Timer _animTimer;

        // ============================================================
        //  Text & Layout State
        // ============================================================

        private Point _textOffset = Point.Empty;

        // ============================================================
        //  Colors
        // ============================================================

        private Color _normalColor;
        private Color _borderNormalColor;
        private Color _hoverTextColor;

        // ============================================================
        //  Theme
        // ============================================================

        private string _themeName = "Windows11";

        // ============================================================
        //  Constructor
        // ============================================================

        /// <summary>
        /// Initializes a new instance of the FuiButton control.
        /// </summary>
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

            Cursor = Cursors.Hand;
            BackColor = Color.Transparent;
            UpdateStyles();

            _renderer.BackgroundColor = _normalColor;
            _renderer.BorderColor = _borderNormalColor;

            // Initialize animation timer (disabled in designer)
            if (!IsInDesigner)
            {
                _animTimer = new System.Windows.Forms.Timer();
                _animTimer.Interval = 15;
                _animTimer.Tick += (s, e) => UpdateAnimation();
                _animTimer.Start();
            }

            // APPLY DEFAULT THEME ON CREATION
            ApplyTheme();
        }

        /// <summary>
        /// Determines whether the control is running inside Visual Studio Designer.
        /// </summary>
        private bool IsInDesigner =>
            DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;

        // ============================================================
        //  Properties
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

        // ----------------------------
        // G) THEME
        // ----------------------------

        [Category("Fast G - Theme")]
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

        /// <summary>
        /// Updates hover and press animation interpolation values.
        /// </summary>
        private void UpdateAnimation()
        {
            bool needUpdate = false;

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
        //  Utility
        // ============================================================

        /// <summary>
        /// Linearly interpolates between two colors.
        /// </summary>
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
        //  Presets & Themes
        // ============================================================

        /// <summary>
        /// Applies a visual preset to the button.
        /// </summary>
        private void ApplyPreset(ButtonPreset p)
        {
            FontColor = p.FontColor;
            FontSize = p.FontSize;
            MoreFontSettings = p.MoreFontSettings;
            MoveTextHorizontal = p.MoveTextHorizontal;
            MoveTextVertical = p.MoveTextVertical;
            TextPosition = p.TextPosition;

            ControlWidth = p.ControlWidth;
            ControlHeight = p.ControlHeight;

            _normalColor = p.FillColor;
            _borderNormalColor = p.BorderColor;

            HoverFillColor = p.HoverFillColor;
            HoverBorder = p.HoverBorder;
            _hoverTextColor = p.HoverTextColor;

            PressFillColor = p.PressFillColor;
            PressBorderColor = p.PressBorderColor;
            PressDepth = p.PressDepth;

            BorderWidth = p.BorderWidth;
            CornerRadius = p.CornerRadius;

            Invalidate();
        }

        /// <summary>
        /// Applies the currently selected theme.
        /// </summary>
        private void ApplyTheme()
        {
            var theme = FuiThemeRegistry.Get(_themeName);
            if (theme != null)
                ApplyPreset(theme.GetButtonPreset());
        }
    }
}

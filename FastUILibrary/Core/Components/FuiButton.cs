using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FastUI.Core.Rendering;

namespace FastUI.Modules.Buttons
{
    public class FuiButton : Control
    {
        private FastButtonRenderer _renderer = new FastButtonRenderer();

        private bool _isHovered = false;
        private float _hoverLerp = 0f;
        private readonly float _hoverSpeed = 0.15f;

        private bool _isPressed = false;
        private float _pressLerp = 0f;
        private readonly float _pressSpeed = 0.25f;

        private Point _textOffset = Point.Empty;
        private Color _hoverTextColor = Color.Black;

        private Color _normalColor = Color.White;
        private Color _borderNormalColor = Color.Gray;

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

            BackColor = Color.Transparent;
            UpdateStyles();

            _renderer.BackgroundColor = _normalColor;
            _renderer.BorderColor = _borderNormalColor;
        }

        private bool IsInDesigner =>
            DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;

        // =====================================================================
        // FastUI – General
        // =====================================================================
        #region FastUI – General

        [Browsable(true)]
        [Category("FastUI – General")]
        public override string Text
        {
            get => base.Text;
            set { base.Text = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("FastUI – General")]
        public int ControlWidth
        {
            get => Width;
            set { Width = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("FastUI – General")]
        public int ControlHeight
        {
            get => Height;
            set { Height = value; Invalidate(); }
        }

        #endregion

        // =====================================================================
        // FastUI – Style
        // =====================================================================
        #region FastUI – Style

        [Browsable(true)]
        [Category("FastUI – Style")]
        public Color FillColor
        {
            get => _normalColor;
            set { _normalColor = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("FastUI – Style")]
        public Color BorderColor
        {
            get => _borderNormalColor;
            set { _borderNormalColor = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("FastUI – Style")]
        public float BorderWidth
        {
            get => _renderer.BorderThickness;
            set { _renderer.BorderThickness = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("FastUI – Style")]
        public float CornerRadius
        {
            get => _renderer.Radius;
            set { _renderer.Radius = Math.Max(0, value); Invalidate(); }
        }

        #endregion

        // =====================================================================
        // FastUI – Text
        // =====================================================================
        #region FastUI – Text

        [Browsable(true)]
        [Category("FastUI – Text")]
        public Color FontColor
        {
            get => ForeColor;
            set { ForeColor = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("FastUI – Text")]
        public float FontSize
        {
            get => Font.Size;
            set { Font = new Font(Font.FontFamily, value); Invalidate(); }
        }

        [Browsable(true)]
        [Category("FastUI – Text")]
        public Font MoreFontSettings
        {
            get => Font;
            set { Font = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("FastUI – Text")]
        public int MoveTextHorizontal
        {
            get => _textOffset.X;
            set { _textOffset.X = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("FastUI – Text")]
        public int MoveTextVertical
        {
            get => _textOffset.Y;
            set { _textOffset.Y = value; Invalidate(); }
        }

        public enum FastTextAlign { Left, Center, Right }

        [Browsable(true)]
        [Category("FastUI – Text")]
        public FastTextAlign TextPosition { get; set; } = FastTextAlign.Center;

        #endregion

        // =====================================================================
        // FastUI – Interaction
        // =====================================================================
        #region FastUI – Interaction

        [Browsable(true)]
        [Category("FastUI – Interaction")]
        public Color HoverFillColor { get; set; } = Color.FromArgb(240, 240, 240);

        [Browsable(true)]
        [Category("FastUI – Interaction")]
        public Color HoverBorder { get; set; } = Color.Black;

        [Browsable(true)]
        [Category("FastUI – Interaction")]
        public Color HoverTextColor
        {
            get => _hoverTextColor;
            set { _hoverTextColor = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("FastUI – Interaction")]
        public Color PressFillColor { get; set; } = Color.FromArgb(220, 220, 220);

        [Browsable(true)]
        [Category("FastUI – Interaction")]
        public Color PressBorderColor { get; set; } = Color.FromArgb(80, 80, 80);

        [Browsable(true)]
        [Category("FastUI – Interaction")]
        public int PressDepth { get; set; } = 1;

        #endregion

        // =====================================================================
        // Hover + Press Animation
        // =====================================================================
        private Color LerpColor(Color a, Color b, float t)
        {
            return Color.FromArgb(
                (int)(a.A + (b.A - a.A) * t),
                (int)(a.R + (b.R - a.R) * t),
                (int)(a.G + (b.G - a.G) * t),
                (int)(a.B + (b.B - a.B) * t)
            );
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            bool designer = IsInDesigner;

            if (!designer)
            {
                _hoverLerp = _isHovered
                    ? Math.Min(1f, _hoverLerp + _hoverSpeed)
                    : Math.Max(0f, _hoverLerp - _hoverSpeed);

                _pressLerp = _isPressed
                    ? Math.Min(1f, _pressLerp + _pressSpeed)
                    : Math.Max(0f, _pressLerp - _pressSpeed);
            }

            var bg = LerpColor(_normalColor, HoverFillColor, _hoverLerp);
            bg = LerpColor(bg, PressFillColor, _pressLerp);

            var border = LerpColor(_borderNormalColor, HoverBorder, _hoverLerp);
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

            if (!designer)
                Invalidate();
        }

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
    }
}

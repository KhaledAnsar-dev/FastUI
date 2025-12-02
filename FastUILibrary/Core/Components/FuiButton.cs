using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FastUI.Core.Rendering;

namespace FastUI.Modules.Buttons
{
    /// <summary>
    /// A lightweight custom button that supports smooth rounded rendering,
    /// hover color transitions, and high-quality drawing via FastButtonRenderer.
    /// </summary>
    public class FuiButton : Control
    {
        // Renders the button visuals
        private FastButtonRenderer _renderer = new FastButtonRenderer();

        // Hover animation state
        private bool _isHovered = false;
        private float _hoverLerp = 0f;
        private readonly float _hoverSpeed = 0.15f;

        private Color _normalColor = Color.White;
        public Color NormalColor
        {
            get => _normalColor;
            set { _normalColor = value; Invalidate(); }
        }

        public Color HoverColor { get; set; } = Color.FromArgb(240, 240, 240);

        private Color _borderNormalColor = Color.Gray;
        public Color BorderNormalColor
        {
            get => _borderNormalColor;
            set { _borderNormalColor = value; Invalidate(); }
        }

        public Color BorderHoverColor { get; set; } = Color.Black;

        // Detect design-mode safely (WinForms quirk)
        private bool IsInDesigner =>
            DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;

        public FuiButton()
        {
            Size = new Size(120, 40);
            Font = new Font("Segoe UI", 10f);

            // Enable smooth custom painting
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

            // Initial renderer setup
            _renderer.BackgroundColor = _normalColor;
            _renderer.BorderColor = _borderNormalColor;
        }

        public float BorderThickness
        {
            get => _renderer.BorderThickness;
            set { _renderer.BorderThickness = value; Invalidate(); }
        }

        public float Radius
        {
            get => _renderer.Radius / 5;
            set { _renderer.Radius = value * 5; Invalidate(); }
        }

        // Smooth color blending for hover animation
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
                // Animate hover transition
                _hoverLerp = _isHovered
                    ? Math.Min(1f, _hoverLerp + _hoverSpeed)
                    : Math.Max(0f, _hoverLerp - _hoverSpeed);
            }

            // Update renderer colors
            _renderer.BackgroundColor = LerpColor(_normalColor, HoverColor, _hoverLerp);
            _renderer.BorderColor = LerpColor(_borderNormalColor, BorderHoverColor, _hoverLerp);

            base.OnPaint(e);

            // Draw button
            _renderer.Render(e.Graphics, ClientRectangle, Text, Font, ForeColor, designer);

            // Redraw continuously for animation
            if (!designer)
                Invalidate();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;

                // Enable deep double-buffering to remove flicker
                cp.ExStyle |= 0x02000000;  // WS_EX_COMPOSITED

                return cp;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!IsInDesigner)
                _isHovered = true;

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!IsInDesigner)
                _isHovered = false;

            base.OnMouseLeave(e);
        }
    }
}

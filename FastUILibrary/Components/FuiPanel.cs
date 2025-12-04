using FastUI.Core.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastUI.FastUILibrary.Components
{
    public class FuiPanel : Panel
    {
        private FastPanelRenderer _renderer = new FastPanelRenderer();

        private bool _isHovered = false;
        private bool _isFocused = false;

        // Normal colors
        private Color _fillColor = Color.White;
        private Color _borderColor = Color.Gray;

        // Hover colors
        private Color _hoverFillColor = Color.White;
        private Color _hoverBorderColor = Color.Gray;

        // Focus color
        private Color _focusedBorderColor = Color.Black;

        public FuiPanel()
        {
            Size = new Size(200, 120);

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor,
                true
            );

            BackColor = Color.Transparent;

            // Mouse events for hover
            MouseEnter += (s, e) => { _isHovered = true; Invalidate(); };
            MouseLeave += (s, e) => { _isHovered = false; Invalidate(); };

            // Track focus from children
            ControlAdded += ChildAdded;
            ControlRemoved += ChildRemoved;

            UpdateStyles();
        }

        private void ChildAdded(object sender, ControlEventArgs e)
        {
            e.Control.GotFocus += ChildFocused;
            e.Control.LostFocus += ChildLostFocus;
        }

        private void ChildRemoved(object sender, ControlEventArgs e)
        {
            e.Control.GotFocus -= ChildFocused;
            e.Control.LostFocus -= ChildLostFocus;
        }

        private void ChildFocused(object sender, EventArgs e)
        {
            _isFocused = true;
            Invalidate();
        }

        private void ChildLostFocus(object sender, EventArgs e)
        {
            // Check if no child controls have focus
            foreach (Control c in Controls)
            {
                if (c.Focused)
                    return;
            }

            _isFocused = false;
            Invalidate();
        }

        // ============================================================
        // FastUI – General
        // ============================================================

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

        // ============================================================
        // FastUI – Style (Normal)
        // ============================================================

        [Browsable(true)]
        [Category("FastUI – Style")]
        public Color FillColor
        {
            get => _fillColor;
            set { _fillColor = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("FastUI – Style")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        // ============================================================
        // Hover Style
        // ============================================================

        [Browsable(true)]
        [Category("FastUI – Hover")]
        public Color HoverFillColor
        {
            get => _hoverFillColor;
            set { _hoverFillColor = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("FastUI – Hover")]
        public Color HoverBorderColor
        {
            get => _hoverBorderColor;
            set { _hoverBorderColor = value; Invalidate(); }
        }

        // ============================================================
        // Focus Style
        // ============================================================

        [Browsable(true)]
        [Category("FastUI – Focus")]
        public Color FocusedBorderColor
        {
            get => _focusedBorderColor;
            set { _focusedBorderColor = value; Invalidate(); }
        }

        // ============================================================
        // Shape
        // ============================================================

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

        // ============================================================
        // Rendering
        // ============================================================

        protected override void OnPaint(PaintEventArgs e)
        {
            Color finalFill;
            Color finalBorder;

            if (_isFocused)
            {
                finalFill = _fillColor;
                finalBorder = _focusedBorderColor;
            }
            else if (_isHovered)
            {
                finalFill = _hoverFillColor;
                finalBorder = _hoverBorderColor;
            }
            else
            {
                finalFill = _fillColor;
                finalBorder = _borderColor;
            }

            _renderer.BackgroundColor = finalFill;
            _renderer.BorderColor = finalBorder;

            _renderer.Render(e.Graphics, ClientRectangle);

            base.OnPaint(e);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED (no flicker)
                return cp;
            }
        }
    }

}

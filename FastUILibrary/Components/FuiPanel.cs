using FastUI.Core.Rendering;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FastUI.FastUILibrary.Components
{
    /// <summary>
    /// A modern FastUI panel container with custom rendering.
    /// 
    /// Features:
    /// - Custom background and border rendering
    /// - Hover state styling
    /// - Focus tracking from child controls
    /// - Rounded corners support
    /// - Flicker-free rendering
    /// 
    /// Designed to act as a styled container for FastUI layouts.
    /// </summary>
    public class FuiPanel : Panel
    {
        // ============================================================
        //  Rendering
        // ============================================================

        /// <summary>
        /// Renderer responsible for drawing panel background and border.
        /// </summary>
        private FastPanelRenderer _renderer = new FastPanelRenderer();


        // ============================================================
        //  State
        // ============================================================

        private bool _isHovered = false;
        private bool _isFocused = false;


        // ============================================================
        //  Colors – Normal
        // ============================================================

        private Color _fillColor = Color.White;
        private Color _borderColor = Color.Gray;


        // ============================================================
        //  Colors – Hover
        // ============================================================

        private Color _hoverFillColor = Color.White;
        private Color _hoverBorderColor = Color.Gray;


        // ============================================================
        //  Colors – Focus
        // ============================================================

        private Color _focusedBorderColor = Color.Black;


        // ============================================================
        //  Constructor
        // ============================================================

        /// <summary>
        /// Initializes a new instance of the FuiPanel.
        /// </summary>
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

            // Hover tracking
            MouseEnter += (s, e) => { _isHovered = true; Invalidate(); };
            MouseLeave += (s, e) => { _isHovered = false; Invalidate(); };

            // Track focus from child controls
            ControlAdded += ChildAdded;
            ControlRemoved += ChildRemoved;

            UpdateStyles();
        }


        // ============================================================
        //  Child Focus Tracking
        // ============================================================

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
            // Check if any child control is still focused
            foreach (Control c in Controls)
            {
                if (c.Focused)
                    return;
            }

            _isFocused = false;
            Invalidate();
        }


        // ============================================================
        //  FastUI – General
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
        //  FastUI – Style (Normal)
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
        //  FastUI – Hover
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
        //  FastUI – Focus
        // ============================================================

        [Browsable(true)]
        [Category("FastUI – Focus")]
        public Color FocusedBorderColor
        {
            get => _focusedBorderColor;
            set { _focusedBorderColor = value; Invalidate(); }
        }


        // ============================================================
        //  Shape
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
        //  Rendering
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


        // ============================================================
        //  Flicker-Free Rendering
        // ============================================================

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }
    }
}

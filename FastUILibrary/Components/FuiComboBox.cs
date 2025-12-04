using FastUI.FastUILibrary.Core.Rendering;
using System;
using FastUI.FastUILibrary.Core;
using FastUI.FastUILibrary.Components.Internal;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastUI.FastUILibrary.Components
{
    public class FuiComboBox : Control
    {
        // ============================================================
        //  Fields
        // ============================================================

        // Renderer for background and border visuals
        private FastShapeRenderer _renderer = new FastShapeRenderer();

        // Animation states
        private bool _isHovered = false;
        private bool _isFocused = false;
        private bool _popupOpen = false;

        // Animation interpolation values
        private float _hoverLerp = 0f;   // kept for compatibility (not used for colors)
        private float _focusLerp = 0f;

        // Animation speeds
        private readonly float _hoverSpeed = 0.8f;
        private readonly float _focusSpeed = 0.28f;

        // Timer that drives animations
        private System.Windows.Forms.Timer _animTimer;

        // Text + Placeholder
        private string _placeholder = "Select...";
        private string _selectedValue = "";
        private bool _showingPlaceholder = true;

        // Item list
        private string[] _items = Array.Empty<string>();

        // Popup window instance
        private FuiComboPopup _popup;

        // Arrow drawing rectangle
        private Rectangle _arrowRect;


        // ============================================================
        //  Properties (Organized A → F)
        // ============================================================

        // ----------------------------
        // A) DATA
        // ----------------------------

        [Category("Fast A - Data")]
        public string[] Items
        {
            get => _items;
            set
            {
                _items = value ?? Array.Empty<string>();
                Invalidate();
            }
        }

        [Category("Fast A - Data")]
        public string SelectedItem => _selectedValue;

        [Category("Fast A - Data")]
        public int SelectedIndex { get; private set; } = -1;

        public event EventHandler SelectedIndexChanged;



        // ----------------------------
        // B) TEXT / PLACEHOLDER
        // ----------------------------

        [Category("Fast B - Text")]
        public string Placeholder
        {
            get => _placeholder;
            set { _placeholder = value; Invalidate(); }
        }

        [Category("Fast B - Text")]
        public Color PlaceholderColor { get; set; } = Color.Gray;

        [Category("Fast B - Text")]
        public Color TextColor { get; set; } = Color.Black;



        // ----------------------------
        // C) COLORS – NORMAL
        // ----------------------------

        [Category("Fast C - Colors Normal")]
        public Color FillColor { get; set; } = Color.White;

        [Category("Fast C - Colors Normal")]
        public Color BorderColor { get; set; } = Color.Gray;



        // ----------------------------
        // D) COLORS – FOCUS
        // ----------------------------

        [Category("Fast D - Colors Focus")]
        public Color FocusFillColor { get; set; } = Color.White;

        [Category("Fast D - Colors Focus")]
        public Color FocusBorderColor { get; set; } = Color.DodgerBlue;



        // ----------------------------
        // E) STYLE
        // ----------------------------

        [Category("Fast E - Style")]
        public float CornerRadius
        {
            get => _renderer.Radius;
            set { _renderer.Radius = value; Invalidate(); }
        }

        [Category("Fast E - Style")]
        public float BorderWidth
        {
            get => _renderer.BorderThickness;
            set { _renderer.BorderThickness = value; Invalidate(); }
        }



        // ============================================================
        //  Constructor
        // ============================================================

        public FuiComboBox()
        {
            Size = new Size(200, 40);
            Font = new Font("Segoe UI", 10f);

            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.Selectable,
                     true);

            _animTimer = new System.Windows.Forms.Timer
            {
                Interval = 16
            };
            _animTimer.Tick += (s, e) => UpdateAnimation();
            _animTimer.Start();
        }



        // ============================================================
        //  Animation
        // ============================================================

        /// <summary>
        /// Updates focus and hover interpolation values.
        /// This drives the smooth animations for border/fill transitions.
        /// </summary>
        private void UpdateAnimation()
        {
            bool changed = false;

            float focusTarget = _isFocused ? 1 : 0;
            float hoverTarget = _isHovered ? 1 : 0;

            // Hover lerp (still active for consistency)
            if (Math.Abs(_hoverLerp - hoverTarget) > 0.01f)
            {
                _hoverLerp += (_isHovered ? _hoverSpeed : -_hoverSpeed);
                _hoverLerp = Math.Clamp(_hoverLerp, 0, 1);
                changed = true;
            }

            // Focus lerp
            if (Math.Abs(_focusLerp - focusTarget) > 0.01f)
            {
                _focusLerp += (_isFocused ? _focusSpeed : -_focusSpeed);
                _focusLerp = Math.Clamp(_focusLerp, 0, 1);
                changed = true;
            }

            if (changed)
                Invalidate();
        }



        // ============================================================
        //  Popup
        // ============================================================

        /// <summary>
        /// Opens the dropdown popup and registers its event callbacks.
        /// </summary>
        private void ShowPopup()
        {
            if (_items.Length == 0)
                return;

            int offsetY = 5; // spacing between combo and popup

            _popup = new FuiComboPopup(_items)
            {
                Width = Width,
                Height = Math.Min(200, _items.Length * 28),
                Location = Parent.PointToScreen(new Point(Left, Bottom + offsetY))
            };

            _popupOpen = true;
            _isFocused = true;

            _popup.ItemSelected += (value, index) =>
            {
                _selectedValue = value;
                SelectedIndex = index;
                _showingPlaceholder = false;

                SelectedIndexChanged?.Invoke(this, EventArgs.Empty);

                _popupOpen = false;
                Invalidate();
            };

            _popup.FormClosed += (s, e) =>
            {
                _popupOpen = false;
                _isFocused = false;
                Invalidate();
            };

            _popup.Show();
        }



        // ============================================================
        //  Mouse Events
        // ============================================================

        protected override void OnMouseEnter(EventArgs e)
        {
            _isHovered = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!_isHovered)
            {
                _isHovered = true;
                Invalidate();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!_popupOpen)
            {
                _isHovered = false;
                Invalidate();
            }
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            _isFocused = true;

            ShowPopup();
            Invalidate();

            base.OnMouseDown(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (!_popupOpen)
            {
                _isFocused = false;
                Invalidate();
            }
            base.OnLostFocus(e);
        }



        // ============================================================
        //  Painting
        // ============================================================

        protected override void OnPaint(PaintEventArgs e)
        {
            // Combine normal and focus states
            Color bg = Lerp(FillColor, FocusFillColor, _focusLerp);
            Color border = Lerp(BorderColor, FocusBorderColor, _focusLerp);

            _renderer.BackgroundColor = bg;
            _renderer.BorderColor = border;

            base.OnPaint(e);

            _renderer.Render(e.Graphics, ClientRectangle, "",
                Font, TextColor, false,
                FastTextAlign.Left, Point.Empty);

            // Text
            string txt = _showingPlaceholder ? _placeholder : _selectedValue;
            Color txtColor = _showingPlaceholder ? PlaceholderColor : TextColor;

            TextRenderer.DrawText(
                e.Graphics,
                txt,
                Font,
                new Rectangle(8, 0, Width - 30, Height),
                txtColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

            // Arrow ▼
            _arrowRect = new Rectangle(Width - 25, 0, 25, Height);
            DrawArrow(e.Graphics, _arrowRect);
        }


        /// <summary>
        /// Draws arrow ▼ centered inside its rectangle.
        /// </summary>
        private void DrawArrow(Graphics g, Rectangle r)
        {
            int arrowOffset = -4; // shift arrow slightly left

            int centerX = r.Left + r.Width / 2 + arrowOffset;
            int centerY = r.Top + r.Height / 2;

            Point p1 = new Point(centerX - 5, centerY - 2);
            Point p2 = new Point(centerX, centerY + 3);
            Point p3 = new Point(centerX + 5, centerY - 2);

            using (Pen pen = new Pen(BorderColor, 1.6f))
            {
                pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.DrawLines(pen, new[] { p1, p2, p3 });
            }
        }



        // ============================================================
        //  Utility
        // ============================================================

        private Color Lerp(Color a, Color b, float t)
        {
            return Color.FromArgb(
                (int)(a.A + (b.A - a.A) * t),
                (int)(a.R + (b.R - a.R) * t),
                (int)(a.G + (b.G - a.G) * t),
                (int)(a.B + (b.B - a.B) * t));
        }
    }

}

using FastUI.FastUILibrary.Core.Rendering;
using System;
using FastUI.FastUILibrary.Core;
using FastUI.FastUILibrary.Components.Internal;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FastUI.FastUILibrary.Themes.Infrastructure;
using FastUI.FastUILibrary.Themes.Presets;

namespace FastUI.FastUILibrary.Components
{
    /// <summary>
    /// A modern dropdown selection control for FastUI.
    /// 
    /// Features:
    /// - Custom rendered combo box
    /// - Animated hover and focus states
    /// - Placeholder support
    /// - Popup-based item selection
    /// - Theme and preset integration
    /// </summary>
    public class FuiComboBox : Control
    {
        // ============================================================
        //  Rendering & Animation Fields
        // ============================================================

        private FastShapeRenderer _renderer = new FastShapeRenderer();

        private bool _isHovered = false;
        private bool _isFocused = false;
        private bool _popupOpen = false;

        private float _hoverLerp = 0f;   // kept for compatibility (not used for colors)
        private float _focusLerp = 0f;

        private readonly float _hoverSpeed = 0.8f;
        private readonly float _focusSpeed = 0.28f;

        private System.Windows.Forms.Timer _animTimer;

        // ============================================================
        //  Text & Data State
        // ============================================================

        private string _placeholder = "Select...";
        private string _selectedValue = "";
        private bool _showingPlaceholder = true;

        private string[] _items = Array.Empty<string>();

        // ============================================================
        //  Popup
        // ============================================================

        private FuiComboPopup _popup;

        // ============================================================
        //  Visual Helpers
        // ============================================================

        private Rectangle _arrowRect;

        // ============================================================
        //  Theme
        // ============================================================

        private string _themeName = "Windows11";

        // ============================================================
        //  Properties
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
        public Color PlaceholderColor { get; set; }

        [Category("Fast B - Text")]
        public Color TextColor { get; set; }

        // ----------------------------
        // C) COLORS – NORMAL
        // ----------------------------

        [Category("Fast C - Colors Normal")]
        public Color FillColor { get; set; }

        [Category("Fast C - Colors Normal")]
        public Color BorderColor { get; set; }

        // ----------------------------
        // D) COLORS – FOCUS
        // ----------------------------

        [Category("Fast D - Colors Focus")]
        public Color FocusFillColor { get; set; }

        [Category("Fast D - Colors Focus")]
        public Color FocusBorderColor { get; set; }

        // ----------------------------
        // E) COLORS – HOVER
        // ----------------------------

        [Category("Fast E - Colors Focus")]
        public Color HoverFillColor { get; set; }

        [Category("Fast E - Colors Focus")]
        public Color HoverBorderColor { get; set; }

        // ----------------------------
        // F) STYLE
        // ----------------------------

        [Category("Fast F - Style")]
        public float CornerRadius
        {
            get => _renderer.Radius;
            set { _renderer.Radius = value; Invalidate(); }
        }

        [Category("Fast F - Style")]
        public float BorderWidth
        {
            get => _renderer.BorderThickness;
            set { _renderer.BorderThickness = value; Invalidate(); }
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
        //  Constructor
        // ============================================================

        /// <summary>
        /// Initializes a new instance of the FuiComboBox control.
        /// </summary>
        public FuiComboBox()
        {
            Size = new Size(200, 40);
            Font = new Font("Segoe UI", 10f);

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable,
                true);

            Cursor = Cursors.Hand;

            _animTimer = new System.Windows.Forms.Timer
            {
                Interval = 16
            };
            _animTimer.Tick += (s, e) => UpdateAnimation();
            _animTimer.Start();

            // APPLY DEFAULT THEME ON CREATION
            ApplyTheme();
        }

        // ============================================================
        //  Animation Logic
        // ============================================================

        /// <summary>
        /// Updates hover and focus interpolation values.
        /// Drives smooth visual transitions.
        /// </summary>
        private void UpdateAnimation()
        {
            bool changed = false;

            float focusTarget = _isFocused ? 1 : 0;
            float hoverTarget = _isHovered ? 1 : 0;

            if (Math.Abs(_hoverLerp - hoverTarget) > 0.01f)
            {
                _hoverLerp += (_isHovered ? _hoverSpeed : -_hoverSpeed);
                _hoverLerp = MathUtils.Clamp(_hoverLerp, 0, 1);
                changed = true;
            }

            if (Math.Abs(_focusLerp - focusTarget) > 0.01f)
            {
                _focusLerp += (_isFocused ? _focusSpeed : -_focusSpeed);
                _focusLerp = MathUtils.Clamp(_focusLerp, 0, 1);
                changed = true;
            }

            if (changed)
                Invalidate();
        }

        // ============================================================
        //  Popup Logic
        // ============================================================

        /// <summary>
        /// Opens the dropdown popup and handles item selection.
        /// </summary>
        private void ShowPopup()
        {
            if (_items.Length == 0)
                return;

            int offsetY = 5;

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
        //  Theme Lifecycle
        // ============================================================

        /// <summary>
        /// Applies the selected theme after the control handle
        /// has been fully created to ensure safe styling.
        /// </summary>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Apply theme after WinForms finishes control initialization
            BeginInvoke((Action)(() => ApplyTheme()));
        }


        // ============================================================
        //  Painting
        // ============================================================

        protected override void OnPaint(PaintEventArgs e)
        {
            Color bgHover = Lerp(FillColor, HoverFillColor, _hoverLerp);
            Color borderHover = Lerp(BorderColor, HoverBorderColor, _hoverLerp);

            Color bgFinal = Lerp(bgHover, FocusFillColor, _focusLerp);
            Color borderFinal = Lerp(borderHover, FocusBorderColor, _focusLerp);

            _renderer.BackgroundColor = bgFinal;
            _renderer.BorderColor = borderFinal;

            base.OnPaint(e);

            _renderer.Render(
                e.Graphics,
                ClientRectangle,
                "",
                Font,
                TextColor,
                false,
                FastTextAlign.Left,
                Point.Empty);

            string txt = _showingPlaceholder ? _placeholder : _selectedValue;
            Color txtColor = _showingPlaceholder ? PlaceholderColor : TextColor;

            TextRenderer.DrawText(
                e.Graphics,
                txt,
                Font,
                new Rectangle(8, 0, Width - 30, Height),
                txtColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

            _arrowRect = new Rectangle(Width - 25, 0, 25, Height);
            DrawArrow(e.Graphics, _arrowRect);
        }

        /// <summary>
        /// Draws the dropdown arrow icon.
        /// </summary>
        private void DrawArrow(Graphics g, Rectangle r)
        {
            int arrowOffset = -4;

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

        /// <summary>
        /// Linearly interpolates between two colors.
        /// </summary>
        private Color Lerp(Color a, Color b, float t)
        {
            return Color.FromArgb(
                (int)(a.A + (b.A - a.A) * t),
                (int)(a.R + (b.R - a.R) * t),
                (int)(a.G + (b.G - a.G) * t),
                (int)(a.B + (b.B - a.B) * t));
        }

        // ============================================================
        //  Presets & Themes
        // ============================================================

        /// <summary>
        /// Applies a visual preset to the combo box.
        /// </summary>
        private void ApplyPreset(ComboBoxPreset p)
        {
            Placeholder = p.Placeholder;
            PlaceholderColor = p.PlaceholderColor;
            TextColor = p.TextColor;

            FillColor = p.FillColor;
            BorderColor = p.BorderColor;

            FocusFillColor = p.FocusFillColor;
            FocusBorderColor = p.FocusBorderColor;

            HoverFillColor = p.HoverFillColor;
            HoverBorderColor = p.HoverBorderColor;

            CornerRadius = p.CornerRadius;
            BorderWidth = p.BorderWidth;

            Invalidate();
        }

        /// <summary>
        /// Applies the currently selected theme.
        /// </summary>
        private void ApplyTheme()
        {
            var theme = FuiThemeRegistry.Get(_themeName);
            if (theme != null)
                ApplyPreset(theme.GetComboBoxPreset());
        }
    }
}

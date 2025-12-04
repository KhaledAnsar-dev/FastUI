using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FastUI.Core.Rendering;

namespace FastUI.Modules.Input
{
    public class FuiTextBox2 : Control
    {
        // ============================================================
        //  Fields
        // ============================================================

        // Renderer responsible for drawing the background & borders
        private FastButtonRenderer _renderer = new FastButtonRenderer();

        // Tracks if mouse truly left the control (supports delayed focus removal)
        private bool _leftControl = false;

        // Timer used to delay focus removal after mouse leave
        private System.Windows.Forms.Timer _leaveTimer;

        // State: hover & focus flags
        private bool _isHovered = false;
        private bool _isFocused = false;

        // Animation interpolation values
        private float _hoverLerp = 0f;
        private float _focusLerp = 0f;

        // Animation speeds
        private readonly float _hoverSpeed = 0.5f;
        private readonly float _focusSpeed = 0.32f;

        // Text + Placeholder
        private string _textValue = "";
        private string _placeholder = "Placeholder";

        // Whether placeholder should be shown
        private bool _showingPlaceholder = true;

        // Text offset inside control
        private Point _textOffset = new Point(8, 0);

        // Normal state colors
        private Color _normalFill = Color.White;
        private Color _borderNormal = Color.Gray;

        // Hover state colors
        private Color _hoverFill = Color.FromArgb(245, 245, 245);
        private Color _hoverBorder = Color.Black;

        // Focus state colors
        private Color _focusFill = Color.White;
        private Color _focusBorder = Color.DodgerBlue;

        // Text alignment enum
        public enum FastTextAlign { Left, Center, Right }
        private FastTextAlign _textAlign = FastTextAlign.Left;

        // Caret positions
        private int _caretIndex = 0;
        private int _selectionStart = 0;
        private int _selectionLength = 0;

        // Caret blinking visibility
        private bool _caretVisible = true;

        // Caret blinking timer
        private System.Windows.Forms.Timer _caretTimer;

        // Animation update timer
        private System.Windows.Forms.Timer _animTimer;

        // Mouse selection variables
        private bool _mouseDown = false;
        private int _mouseDownIndex = 0;

        // Text drawing colors
        [Category("Fast A - Text")]
        public Color TextColor { get; set; } = Color.Black;

        [Category("Fast B - Placeholder")]
        public Color PlaceholderColor { get; set; } = Color.Gray;



        // ============================================================
        //  Properties (Organized A → F)
        // ============================================================

        // ----------------------------
        // A) TEXT
        // ----------------------------

        [Category("Fast A - Text")]
        [Description("The main text placeholder shown when the user has entered no value.")]
        public string Placeholder
        {
            get => _placeholder;
            set { _placeholder = value; Invalidate(); }
        }

        [Category("Fast A - Text")]
        [Description("Horizontal offset applied when drawing text.")]
        public int MoveTextHorizontal
        {
            get => _textOffset.X;
            set { _textOffset.X = value; Invalidate(); }
        }

        [Category("Fast A - Text")]
        [Description("Vertical offset applied when drawing text.")]
        public int MoveTextVertical
        {
            get => _textOffset.Y;
            set { _textOffset.Y = value; Invalidate(); }
        }

        [Category("Fast A - Text")]
        [Description("Font size of the textbox text.")]
        public float FontSize
        {
            get => Font.Size;
            set { Font = new Font(Font.FontFamily, value); Invalidate(); }
        }

        [Category("Fast A - Text")]
        [Description("Defines how text inside the control is aligned.")]
        public FastTextAlign TextAlignment
        {
            get => _textAlign;
            set { _textAlign = value; Invalidate(); }
        }


        // ----------------------------
        // B) PLACEHOLDER
        // ----------------------------

        [Category("Fast B - Placeholder")]
        [Description("Color of the placeholder text.")]
        public Color PlaceholderTextColor
        {
            get => PlaceholderColor;
            set { PlaceholderColor = value; Invalidate(); }
        }


        // ----------------------------
        // C) COLORS – NORMAL
        // ----------------------------

        [Category("Fast C - Colors Normal")]
        [Description("Background color when control is in normal state.")]
        public Color FillColor
        {
            get => _normalFill;
            set { _normalFill = value; Invalidate(); }
        }

        [Category("Fast C - Colors Normal")]
        [Description("Border color when control is in normal state.")]
        public Color BorderColor
        {
            get => _borderNormal;
            set { _borderNormal = value; Invalidate(); }
        }


        // ----------------------------
        // D) COLORS – HOVER
        // ----------------------------

        [Category("Fast D - Colors Hover")]
        [Description("Background color when mouse hovers over the control.")]
        public Color HoverFillColor
        {
            get => _hoverFill;
            set { _hoverFill = value; Invalidate(); }
        }

        [Category("Fast D - Colors Hover")]
        [Description("Border color when mouse hovers over the control.")]
        public Color HoverBorderColor
        {
            get => _hoverBorder;
            set { _hoverBorder = value; Invalidate(); }
        }


        // ----------------------------
        // E) COLORS – FOCUS
        // ----------------------------

        [Category("Fast E - Colors Focus")]
        [Description("Background color when the control is focused.")]
        public Color FocusFillColor
        {
            get => _focusFill;
            set { _focusFill = value; Invalidate(); }
        }

        [Category("Fast E - Colors Focus")]
        [Description("Border color when the control is focused.")]
        public Color FocusBorderColor
        {
            get => _focusBorder;
            set { _focusBorder = value; Invalidate(); }
        }


        // ----------------------------
        // F) STYLE
        // ----------------------------

        [Category("Fast F - Style")]
        [Description("Corner radius of the textbox border.")]
        public float CornerRadius
        {
            get => _renderer.Radius;
            set { _renderer.Radius = value; Invalidate(); }
        }

        [Category("Fast F - Style")]
        [Description("Border thickness used when drawing the control.")]
        public float BorderWidth
        {
            get => _renderer.BorderThickness;
            set { _renderer.BorderThickness = value; Invalidate(); }
        }



        // ============================================================
        //  Constructor
        // ============================================================

        public FuiTextBox2()
        {
            Size = new Size(200, 40);
            Font = new Font("Segoe UI", 10f);

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable |
                ControlStyles.SupportsTransparentBackColor,
                true
            );

            BackColor = Color.Transparent;
            TabStop = true;

            InitializeCaretTimer();
            InitializeAnimationTimer();
            InitializeLeaveTimer();
        }


        // ============================================================
        //  Initialization Helpers
        // ============================================================

        private void InitializeCaretTimer()
        {
            // Controls caret blinking animation
            _caretTimer = new System.Windows.Forms.Timer { Interval = 500 };
            _caretTimer.Tick += (s, e) =>
            {
                if (_isFocused)
                {
                    _caretVisible = !_caretVisible;
                    Invalidate();
                }
            };
            _caretTimer.Start();
        }

        private void InitializeAnimationTimer()
        {
            // Handles hover/focus lerp animation
            _animTimer = new System.Windows.Forms.Timer { Interval = 15 };
            _animTimer.Tick += (s, e) => UpdateAnimation();
            _animTimer.Start();
        }

        private void InitializeLeaveTimer()
        {
            // Delays focus removal when cursor exits control
            _leaveTimer = new System.Windows.Forms.Timer();
            _leaveTimer.Interval = 1000;

            _leaveTimer.Tick += (s, e) =>
            {
                if (_leftControl)
                {
                    _isFocused = false;
                    _caretVisible = false;
                    _selectionLength = 0;

                    if (FindForm() != null)
                        FindForm().ActiveControl = null;

                    Invalidate();
                }

                _leaveTimer.Stop();
            };
        }



        // ============================================================
        //  Animation Logic
        // ============================================================

        private void UpdateAnimation()
        {
            bool changed = false;

            float hoverTarget = _isHovered ? 1 : 0;
            float focusTarget = _isFocused ? 1 : 0;

            // Hover animation
            if (Math.Abs(_hoverLerp - hoverTarget) > 0.01f)
            {
                _hoverLerp += (_isHovered ? _hoverSpeed : -_hoverSpeed);
                _hoverLerp = Math.Clamp(_hoverLerp, 0f, 1f);
                changed = true;
            }

            // Focus animation
            if (Math.Abs(_focusLerp - focusTarget) > 0.01f)
            {
                _focusLerp += (_isFocused ? _focusSpeed : -_focusSpeed);
                _focusLerp = Math.Clamp(_focusLerp, 0f, 1f);
                changed = true;
            }

            if (changed)
                Invalidate();
        }



        // ============================================================
        //  Mouse Events
        // ============================================================

        protected override void OnMouseEnter(EventArgs e)
        {
            _leftControl = false;
            _leaveTimer.Stop();
            _isHovered = true;

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _leftControl = true;
            _leaveTimer.Start();
            _isHovered = false;

            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();

            _mouseDown = true;

            int index = GetCaretIndexFromPoint(e.X);
            _caretIndex = index;
            _mouseDownIndex = index;
            _selectionLength = 0;

            if (_showingPlaceholder && string.IsNullOrEmpty(_textValue))
                _showingPlaceholder = false;

            Invalidate();
            base.OnMouseDown(e);
        }



        // ============================================================
        //  Keyboard Events
        // ============================================================

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Left || keyData == Keys.Right ||
                keyData == Keys.Up || keyData == Keys.Down)
                return true;

            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                if (_selectionLength > 0)
                {
                    _textValue = _textValue.Remove(_selectionStart, _selectionLength);
                    _caretIndex = _selectionStart;
                    _selectionLength = 0;
                }
                else if (_caretIndex > 0)
                {
                    _textValue = _textValue.Remove(_caretIndex - 1, 1);
                    _caretIndex--;
                }

                _showingPlaceholder = string.IsNullOrEmpty(_textValue);
                Invalidate();
                return;
            }

            if (e.KeyCode == Keys.Left)
            {
                if (_caretIndex > 0) _caretIndex--;
                Invalidate();
                return;
            }

            if (e.KeyCode == Keys.Right)
            {
                if (_caretIndex < _textValue.Length) _caretIndex++;
                Invalidate();
                return;
            }

            base.OnKeyDown(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                if (_selectionLength > 0)
                {
                    _textValue = _textValue.Remove(_selectionStart, _selectionLength);
                    _caretIndex = _selectionStart;
                    _selectionLength = 0;
                }

                _textValue = _textValue.Insert(_caretIndex, e.KeyChar.ToString());
                _caretIndex++;
            }

            _showingPlaceholder = string.IsNullOrEmpty(_textValue);

            Invalidate();
            base.OnKeyPress(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            _isFocused = true;
            _caretVisible = true;

            Invalidate();
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            _isFocused = false;
            _caretVisible = false;

            _selectionLength = 0;
            _showingPlaceholder = string.IsNullOrEmpty(_textValue);

            Invalidate();
            base.OnLostFocus(e);
        }



        // ============================================================
        //  Rendering
        // ============================================================

        protected override void OnPaint(PaintEventArgs e)
        {
            Color bg = Lerp(Lerp(_normalFill, _hoverFill, _hoverLerp), _focusFill, _focusLerp);
            Color border = Lerp(Lerp(_borderNormal, _hoverBorder, _hoverLerp), _focusBorder, _focusLerp);

            _renderer.BackgroundColor = bg;
            _renderer.BorderColor = border;

            base.OnPaint(e);

            _renderer.Render(
                e.Graphics,
                ClientRectangle,
                "",
                Font,
                ForeColor,
                false,
                FastUI.Modules.Buttons.FuiButton.FastTextAlign.Left,
                Point.Empty
            );

            // -------------------
            // Draw text or placeholder
            // -------------------

            string txt = _showingPlaceholder ? _placeholder : _textValue;
            Color txtColor = _showingPlaceholder ? PlaceholderColor : TextColor;

            TextRenderer.DrawText(
                e.Graphics,
                txt,
                Font,
                new Rectangle(_textOffset.X, 0, Width, Height),
                txtColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left
            );

            // -------------------
            // Draw caret
            // -------------------

            if (_isFocused && !_showingPlaceholder && _caretVisible)
            {
                int caretX = _textOffset.X +
                             TextRenderer.MeasureText(_textValue.Substring(0, _caretIndex), Font).Width - 2;

                using (Pen p = new Pen(ForeColor, 1))
                    e.Graphics.DrawLine(p, caretX, 8, caretX, Height - 8);
            }
        }



        // ============================================================
        //  Utility Methods
        // ============================================================

        private int GetCaretIndexFromPoint(int mouseX)
        {
            for (int i = 1; i <= _textValue.Length; i++)
            {
                int width = TextRenderer.MeasureText(_textValue.Substring(0, i), Font).Width;
                if (mouseX < width + _textOffset.X)
                    return i - 1;
            }
            return _textValue.Length;
        }

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

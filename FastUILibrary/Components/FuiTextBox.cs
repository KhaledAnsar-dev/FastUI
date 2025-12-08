using FastUI.FastUILibrary.Core.Rendering;
using FastUI.FastUILibrary.Core.Rendering;
using System;
using FastUI.FastUILibrary.Core;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FastUI.FastUILibrary.Components
{
    public class FuiTextBox : Control
    {

        // ============================================================
        //  Fields
        // ============================================================

        private FastShapeRenderer _renderer = new FastShapeRenderer();

        private bool _isHovered = false;
        private bool _isFocused = false;

        private float _hoverLerp = 0f;
        private float _focusLerp = 0f;

        private readonly float _hoverSpeed = 0.5f;
        private readonly float _focusSpeed = 0.32f;

        private string _textValue = "";
        private string _placeholder = "Enter text...";
        private bool _showingPlaceholder = true;

        private Point _textOffset = new Point(8, 0);

        private Color _normalFill = Color.White;
        private Color _borderNormal = Color.Gray;

        private Color _hoverFill = Color.FromArgb(245, 245, 245);
        private Color _hoverBorder = Color.Black;

        private Color _focusFill = Color.White;
        private Color _focusBorder = Color.DodgerBlue;

        private FastTextAlign _textAlign = FastTextAlign.Left;

        private int _caretIndex = 0;
        private int _selectionStart = 0;
        private int _selectionLength = 0;

        private bool _caretVisible = true;
        private System.Windows.Forms.Timer _caretTimer;
        private System.Windows.Forms.Timer _animTimer;

        private bool _mouseDown = false;
        private int _mouseDownIndex = 0;

        [Category("Fast A - Text")]
        public Color TextColor { get; set; } = Color.Black;

        [Category("Fast B - Placeholder")]
        public Color PlaceholderColor { get; set; } = Color.Gray;

        // ============================================================
        //  NEW: Input Type Property + AllowSpace + Auto Placeholder
        // ============================================================

        private FastInputType _inputType = FastInputType.Any;

        [Category("Fast G - Input Rules")]
        [Description("Defines what type of characters are allowed.")]
        public FastInputType InputType
        {
            get => _inputType;
            set
            {
                _inputType = value;
                UpdatePlaceholderByInputType();
                Invalidate();
            }
        }

        private bool _allowSpace = true;

        [Category("Fast G - Input Rules")]
        [Description("Determines whether the user can type space.")]
        public bool AllowSpace
        {
            get => _allowSpace;
            set => _allowSpace = value;
        }

        // ============================================================
        //  Properties
        // ============================================================

        [Category("Fast A - Text")]
        public string FastText
        {
            get => _textValue;
            set
            {
                _textValue = value ?? "";
                _showingPlaceholder = string.IsNullOrEmpty(_textValue);
                _caretIndex = _textValue.Length;
                Invalidate();
            }
        }

        [Category("Fast A - Text")]
        public string Placeholder
        {
            get => _placeholder;
            set { _placeholder = value; Invalidate(); }
        }

        [Category("Fast A - Text")]
        public int MoveTextHorizontal
        {
            get => _textOffset.X;
            set { _textOffset.X = value; Invalidate(); }
        }

        [Category("Fast A - Text")]
        public int MoveTextVertical
        {
            get => _textOffset.Y;
            set { _textOffset.Y = value; Invalidate(); }
        }

        [Category("Fast A - Text")]
        public float FontSize
        {
            get => Font.Size;
            set { Font = new Font(Font.FontFamily, value); Invalidate(); }
        }

        [Category("Fast A - Text")]
        public FastTextAlign TextAlignment
        {
            get => _textAlign;
            set { _textAlign = value; Invalidate(); }
        }

        [Category("Fast B - Placeholder")]
        public Color PlaceholderTextColor
        {
            get => PlaceholderColor;
            set { PlaceholderColor = value; Invalidate(); }
        }

        [Category("Fast C - Colors Normal")]
        public Color FillColor
        {
            get => _normalFill;
            set { _normalFill = value; Invalidate(); }
        }

        [Category("Fast C - Colors Normal")]
        public Color BorderColor
        {
            get => _borderNormal;
            set { _borderNormal = value; Invalidate(); }
        }

        [Category("Fast D - Colors Hover")]
        public Color HoverFillColor
        {
            get => _hoverFill;
            set { _hoverFill = value; Invalidate(); }
        }

        [Category("Fast D - Colors Hover")]
        public Color HoverBorderColor
        {
            get => _hoverBorder;
            set { _hoverBorder = value; Invalidate(); }
        }

        [Category("Fast E - Colors Focus")]
        public Color FocusFillColor
        {
            get => _focusFill;
            set { _focusFill = value; Invalidate(); }
        }

        [Category("Fast E - Colors Focus")]
        public Color FocusBorderColor
        {
            get => _focusBorder;
            set { _focusBorder = value; Invalidate(); }
        }

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

        // ============================================================
        //  Constructor
        // ============================================================

        public FuiTextBox()
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

            Cursor = Cursors.IBeam;
            BackColor = Color.Transparent;
            TabStop = true;

            InitializeCaretTimer();
            InitializeAnimationTimer();
        }

        // ============================================================
        //  Initialization
        // ============================================================

        private void InitializeCaretTimer()
        {
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
            _animTimer = new System.Windows.Forms.Timer { Interval = 15 };
            _animTimer.Tick += (s, e) => UpdateAnimation();
            _animTimer.Start();
        }

        // ============================================================
        //  Animation Logic
        // ============================================================

        private void UpdateAnimation()
        {
            bool changed = false;

            float hoverTarget = _isHovered ? 1 : 0;
            float focusTarget = _isFocused ? 1 : 0;

            if (Math.Abs(_hoverLerp - hoverTarget) > 0.01f)
            {
                _hoverLerp += (_isHovered ? _hoverSpeed : -_hoverSpeed);
                _hoverLerp = Math.Clamp(_hoverLerp, 0f, 1f);
                changed = true;
            }

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
            _isHovered = true;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
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
            char c = e.KeyChar;

            if (char.IsControl(c))
            {
                base.OnKeyPress(e);
                return;
            }

            if (c == ' ')
            {
                if (_allowSpace)
                {
                    // allowed
                }
                else
                {
                    e.Handled = true;
                    return;
                }
            }

            switch (_inputType)
            {
                case FastInputType.IntegerOnly:
                    if (!char.IsDigit(c)) { e.Handled = true; return; }
                    break;

                case FastInputType.DecimalOnly:
                    if (!char.IsDigit(c) && c != '.') { e.Handled = true; return; }
                    if (c == '.' && _textValue.Contains(".")) { e.Handled = true; return; }
                    break;

                case FastInputType.LettersOnly:
                    if (!char.IsLetter(c) && c != ' ') { e.Handled = true; return; }
                    break;
            }

            // ============================================================
            //  ⭐ NEW: Prevent overflow (no multiline) by width
            // ============================================================
            if (!CanInsertChar(c))
            {
                e.Handled = true;
                return;
            }

            if (_selectionLength > 0)
            {
                _textValue = _textValue.Remove(_selectionStart, _selectionLength);
                _caretIndex = _selectionStart;
                _selectionLength = 0;
            }

            _textValue = _textValue.Insert(_caretIndex, c.ToString());
            _caretIndex++;

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
                FastTextAlign.Left,
                Point.Empty
            );

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;

            if (_textAlign == FastTextAlign.Left) sf.Alignment = StringAlignment.Near;
            else if (_textAlign == FastTextAlign.Center) sf.Alignment = StringAlignment.Center;
            else if (_textAlign == FastTextAlign.Right) sf.Alignment = StringAlignment.Far;

            Rectangle textArea =
                new Rectangle(_textOffset.X, _textOffset.Y, Width - (_textOffset.X * 2), Height);

            string txt = _showingPlaceholder ? _placeholder : _textValue;
            Color txtColor = _showingPlaceholder ? PlaceholderColor : TextColor;

            e.Graphics.DrawString(
                txt,
                Font,
                new SolidBrush(txtColor),
                textArea,
                sf
            );

            // ============================================================
            //  ⭐ FIXED CARET POSITION — Accurate pixel-perfect caret + RTL Support
            // ============================================================
            if (_isFocused && !_showingPlaceholder && _caretVisible)
            {
                string beforeCaret = _textValue.Substring(0, _caretIndex);

                float textWidth = MeasureStringWidth(e.Graphics, beforeCaret, Font);
                float totalWidth = MeasureStringWidth(e.Graphics, _textValue, Font);

                int caretX = _textOffset.X; // default for LEFT

                if (_textAlign == FastTextAlign.Left)
                {
                    caretX = _textOffset.X + (int)textWidth;
                }
                else if (_textAlign == FastTextAlign.Center)
                {
                    // center alignment calculation
                    caretX = (Width / 2) - ((int)totalWidth / 2) + (int)textWidth;
                }
                else if (_textAlign == FastTextAlign.Right)
                {
                    // right alignment calculation
                    caretX = Width - _textOffset.X - (int)totalWidth + (int)textWidth;
                }

                using (Pen p = new Pen(ForeColor, 1))
                    e.Graphics.DrawLine(p, caretX, 8, caretX, Height - 8);
            }

        }

        // ============================================================
        //  Utility
        // ============================================================

        private int GetCaretIndexFromPoint(int mouseX)
        {
            for (int i = 1; i <= _textValue.Length; i++)
            {
                int width =
                    (int)MeasureStringWidth(CreateGraphics(), _textValue.Substring(0, i), Font);

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

        // ============================================================
        //  ⭐ NEW: Accurate Text Width Calculation
        // ============================================================

        private float MeasureStringWidth(Graphics g, string text, Font font)
        {
            if (string.IsNullOrEmpty(text))
                return 0f;

            SizeF size = g.MeasureString(text, font, int.MaxValue,
                StringFormat.GenericTypographic);

            return size.Width;
        }

        // ============================================================
        //  ⭐ NEW: Check if char fits in available width
        // ============================================================
        private bool CanInsertChar(char c)
        {
            string temp = _textValue;

            if (_selectionLength > 0)
                temp = temp.Remove(_selectionStart, _selectionLength);

            if (_caretIndex < 0) _caretIndex = 0;
            if (_caretIndex > temp.Length) _caretIndex = temp.Length;

            temp = temp.Insert(_caretIndex, c.ToString());

            using (Graphics g = CreateGraphics())
            {
                float textWidth = MeasureStringWidth(g, temp, Font);

                int maxWidth = Width - (_textOffset.X * 2) - 4; // small padding

                return textWidth <= maxWidth;
            }
        }

        // ============================================================
        //  AUTO PLACEHOLDER LOGIC
        // ============================================================

        private void UpdatePlaceholderByInputType()
        {
            _placeholder = GetDefaultPlaceholder();
        }

        private string GetDefaultPlaceholder()
        {
            return _inputType switch
            {
                FastInputType.Any => "Enter text...",
                FastInputType.IntegerOnly => "123",
                FastInputType.DecimalOnly => "3.14",
                FastInputType.LettersOnly => "Hello",
                _ => "Enter text..."
            };
        }
    }
}

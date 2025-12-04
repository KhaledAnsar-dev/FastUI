using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FastUI.Modules.Panels;

namespace FastUI.FastUILibrary.Core.Components
{
    public partial class FuiTextBox : UserControl
    {
        private string _placeholder = "Placeholder";
        private bool _isPlaceholderActive = true;

        private Color _placeholderColor = Color.Silver;
        private Color _textColor = Color.Black;
        private Color _fillColor = Color.White;

        private bool _ignoreSystemChanges = false;
        private bool _isInitialized = false;

        private bool _isHovered = false;
        private bool _isFocused = false;

        public FuiTextBox()
        {
            InitializeComponent();

            innerControl.Multiline = false;
            innerControl.WordWrap = false;
            innerControl.ScrollBars = ScrollBars.None;
            innerControl.BorderStyle = BorderStyle.None;

            innerControl.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Return)
                    e.Handled = true;
            };

            innerControl.GotFocus += Inner_GotFocus;
            innerControl.LostFocus += Inner_LostFocus;

            innerControl.MouseEnter += Inner_MouseEnter;
            innerControl.MouseLeave += Inner_MouseLeave;

            innerControl.TextChanged += InnerControl_TextChanged;

            myPanel.MouseEnter += Panel_MouseEnter;
            myPanel.MouseLeave += Panel_MouseLeave;
            myPanel.Click += (s, e) => innerControl.Focus();

            this.Resize += (s, e) => AdjustLayout();
            this.Load += OnLoad;

            ApplyFillColor();
        }

        // ============================================================
        // INITIALIZATION
        // ============================================================

        private void OnLoad(object sender, EventArgs e)
        {
            _isInitialized = true;

            if (string.IsNullOrWhiteSpace(innerControl.Text) || innerControl.Text == _placeholder)
                ShowPlaceholder();
            else
            {
                _isPlaceholderActive = false;
                innerControl.ForeColor = _textColor;
            }

            AdjustLayout();
            UpdateVisualState();
        }

        // ============================================================
        // PLACEHOLDER SYSTEM
        // ============================================================

        private void ShowPlaceholder()
        {
            _ignoreSystemChanges = true;

            _isPlaceholderActive = true;
            innerControl.Text = _placeholder;
            innerControl.ForeColor = _placeholderColor;

            _ignoreSystemChanges = false;
        }

        private void RemovePlaceholder()
        {
            if (_isPlaceholderActive)
            {
                _ignoreSystemChanges = true;

                innerControl.Text = "";
                innerControl.ForeColor = _textColor;
                _isPlaceholderActive = false;

                _ignoreSystemChanges = false;
            }
        }

        private void ApplyPlaceholder()
        {
            if (innerControl.Focused) return;

            if (string.IsNullOrWhiteSpace(innerControl.Text))
                ShowPlaceholder();
        }

        private void InnerControl_TextChanged(object sender, EventArgs e)
        {
            if (_ignoreSystemChanges || !_isInitialized) return;

            if (!_isPlaceholderActive && innerControl.Focused)
            {
                innerControl.ForeColor = _textColor;
                return;
            }

            if (!innerControl.Focused && string.IsNullOrWhiteSpace(innerControl.Text))
                ShowPlaceholder();
        }

        private string GetRealText() =>
            _isPlaceholderActive ? "" : innerControl.Text;

        // ============================================================
        // COLOR SYNC SYSTEM
        // ============================================================

        private void ApplyFillColor()
        {
            myPanel.FillColor = _fillColor;
            innerControl.BackColor = _fillColor;
        }

        private void ApplyHoverColors()
        {
            myPanel.FillColor = HoverFillColor;
            innerControl.BackColor = HoverFillColor;

            myPanel.BorderColor = HoverBorderColor;
        }

        private void ApplyNormalColors()
        {
            // ALWAYS restore original colors
            myPanel.FillColor = _fillColor;
            innerControl.BackColor = _fillColor;

            myPanel.BorderColor = _borderColor;
        }

        // ============================================================
        // THE MASTER CONTROLLER
        // ============================================================

        private void UpdateVisualState()
        {
            if (_isFocused)
            {
                ApplyNormalColors();
                return;
            }

            if (_isHovered)
            {
                ApplyHoverColors();
                return;
            }

            ApplyNormalColors();
        }

        // ============================================================
        // HOVER & FOCUS EVENTS
        // ============================================================

        private void Inner_GotFocus(object sender, EventArgs e)
        {
            _isFocused = true;
            RemovePlaceholder();
            UpdateVisualState();
        }

        private void Inner_LostFocus(object sender, EventArgs e)
        {
            _isFocused = false;
            ApplyPlaceholder();
            UpdateVisualState();
        }

        private void Inner_MouseEnter(object sender, EventArgs e)
        {
            _isHovered = true;
            UpdateVisualState();
        }

        private void Inner_MouseLeave(object sender, EventArgs e)
        {
            _isHovered = false;
            UpdateVisualState();
        }

        private void Panel_MouseEnter(object sender, EventArgs e)
        {
            _isHovered = true;
            UpdateVisualState();
        }

        private void Panel_MouseLeave(object sender, EventArgs e)
        {
            _isHovered = false;
            UpdateVisualState();
        }

        // ============================================================
        // LAYOUT
        // ============================================================

        private void AdjustLayout()
        {
            if (myPanel == null || innerControl == null)
                return;

            myPanel.Location = new Point(0, 0);
            myPanel.Size = this.ClientSize;

            int h = innerControl.PreferredHeight;
            innerControl.Width = this.Width - 16;

            int y = (this.Height - h) / 2;
            innerControl.Location = new Point(8, Math.Max(0, y));
            innerControl.Height = h;
        }

        // ============================================================
        // PUBLIC PROPERTIES
        // ============================================================

        [Browsable(true)]
        [Category("FastUI – Text")]
        public string TextValue
        {
            get => GetRealText();
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    ShowPlaceholder();
                    UpdateVisualState();
                    return;
                }

                _isPlaceholderActive = false;

                _ignoreSystemChanges = true;
                innerControl.Text = value;
                innerControl.ForeColor = _textColor;
                _ignoreSystemChanges = false;

                AdjustLayout();
                UpdateVisualState();
            }
        }

        [Browsable(true)]
        [Category("FastUI – Text")]
        public string Placeholder
        {
            get => _placeholder;
            set
            {
                _placeholder = value;
                ApplyPlaceholder();
                UpdateVisualState();
            }
        }

        [Browsable(true)]
        [Category("FastUI – Appearance")]
        public Color PlaceholderColor
        {
            get => _placeholderColor;
            set
            {
                _placeholderColor = value;
                if (_isPlaceholderActive)
                    innerControl.ForeColor = value;
            }
        }

        [Browsable(true)]
        [Category("FastUI – Appearance")]
        public Color TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                if (!_isPlaceholderActive)
                    innerControl.ForeColor = value;
            }
        }

        [Browsable(true)]
        [Category("FastUI – Appearance")]
        public Color FillColor
        {
            get => _fillColor;
            set
            {
                _fillColor = value;
                ApplyFillColor();
                UpdateVisualState();
            }
        }

        [Browsable(true)]
        [Category("FastUI – Text")]
        public int FontSize
        {
            get => (int)Math.Round(innerControl.Font.Size);
            set
            {
                innerControl.Font = new Font(
                    innerControl.Font.FontFamily,
                    value,
                    innerControl.Font.Style,
                    GraphicsUnit.Point
                );
                AdjustLayout();
            }
        }

        public override Font Font
        {
            get => innerControl.Font;
            set
            {
                innerControl.Font = value;
                base.Font = value;
                AdjustLayout();
            }
        }

        // ============================================================
        // BORDER / HOVER PROPERTIES
        // ============================================================

        private Color _borderColor = Color.Gray;

        [Browsable(true)]
        [Category("FastUI – Style")]
        public float BorderThickness
        {
            get => myPanel.BorderWidth;
            set => myPanel.BorderWidth = value;
        }

        [Browsable(true)]
        [Category("FastUI – Style")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                myPanel.BorderColor = value;
            }
        }

        [Browsable(true)]
        [Category("FastUI – Style")]
        public float CornerRadius
        {
            get => myPanel.CornerRadius;
            set => myPanel.CornerRadius = value;
        }

        [Browsable(true)]
        [Category("FastUI – Hover")]
        public Color HoverBorderColor
        {
            get => myPanel.HoverBorderColor;
            set => myPanel.HoverBorderColor = value;
        }

        [Browsable(true)]
        [Category("FastUI – Hover")]
        public Color HoverFillColor
        {
            get => myPanel.HoverFillColor;
            set => myPanel.HoverFillColor = value;
        }
    }
}

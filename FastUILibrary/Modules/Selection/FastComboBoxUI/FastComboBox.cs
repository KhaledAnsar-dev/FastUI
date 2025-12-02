using FastUI.FastUILibrary.Core.Interfaces;
using FastUI.FastUILibrary.Core.Shadow;
using FastUI.FastUILibrary.Core;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Guna.UI2.WinForms;
using FastUI.FastUILibrary.Core.Shadow.Adapters;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using System.Runtime;
using System.Xml.Linq;

namespace FastUI.FastUILibrary.Modules.Selection.FastComboBoxUI
{
    /// <summary>
    /// A fully customizable FastUI combo box component that supports
    /// styling, text control, shadow rendering, and optional "None" mode.
    /// </summary>
    public partial class FastComboBox : UserControl
    {
        // =====================================================================
        //  Fields
        // =====================================================================

        // Shadow behavior engine
        private FastShadowEngine _shadowEngine;

        // Adapter exposing Guna2ComboBox to the engine
        private IFastShadowTarget _shadowAdapter;

        // Internal field for border radius
        private int _borderRadius = 12;


        // =====================================================================
        //  Constructor
        // =====================================================================

        public FastComboBox()
        {
            InitializeComponent();

            // Create shadow adapter
            _shadowAdapter = new GunaShadowComboBoxAdapter(comboBox);

            // Create shadow engine
            _shadowEngine = new FastShadowEngine(this, _shadowAdapter);

            comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox.Dock = DockStyle.Fill;

        }


        // =====================================================================
        //  None Option Logic
        // =====================================================================
        private void ApplyNoneOption()
        {
            const string noneText = "None";

            if (_noneMode == FastNoneMode.Allowed)
            {
                if (!comboBox.Items.Contains(noneText))
                    comboBox.Items.Insert(0, noneText);
            }
            else
            {
                if (comboBox.Items.Contains(noneText))
                    comboBox.Items.Remove(noneText);
            }
        }


        // =====================================================================
        //  Public Properties
        // =====================================================================

        #region FastGeneral

        [Browsable(true)]
        [Category("FastGeneral")]
        [Description("Text displayed inside the combo box.")]
        public string ComboText
        {
            get => comboBox.Text;
            set => comboBox.Text = value;
        }

        [Browsable(true)]
        [Category("FastGeneral")]
        [Description("The width of the combo box component.")]
        public int ControlWidth
        {
            get => _shadowAdapter.ShadowEnabled ? comboBox.Width : this.Width;
            set
            {
                if (_shadowAdapter.ShadowEnabled)
                {
                    // Resize inner button and refresh shadow layout
                    comboBox.Width = value;
                    _shadowEngine.Apply();
                }
                else
                {
                    // Resize container directly
                    this.Width = value;
                }
            }
        }

        [Browsable(true)]
        [Category("FastGeneral")]
        [Description("The height of the combo box component.")]
        public int ControlHeight
        {
            get => _shadowAdapter.ShadowEnabled ? comboBox.Height : this.Height;
            set
            {
                if (_shadowAdapter.ShadowEnabled)
                {
                    // Resize inner button then update shadow
                    comboBox.ItemHeight = value - 6;
                    _shadowEngine.Apply();
                }
                else
                {
                    // Resize container directly
                    this.Height = value;
                }
            }
        }
        [Browsable(true)]
        [Category("FastGeneral")]
        [Description("The border radius of the combo box.")]
        public int CornerRadius
        {
            get => _borderRadius;
            set
            {
                _borderRadius = value;
                comboBox.BorderRadius = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("FastGeneral")]
        [Description("Collection of items inside the combo box.")]
        public Guna2ComboBox.ObjectCollection Items => comboBox.Items;



        private FastNoneMode _noneMode = FastNoneMode.NotAllowed;

        [Browsable(true)]
        [Category("FastGeneral")]
        [Description("Allow 'None' as a selectable option.")]
        public FastNoneMode NoneOption
        {
            get => _noneMode;
            set
            {
                _noneMode = value;
                ApplyNoneOption();
            }
        }

        #endregion


        // ---------------------------------------------------------------------

        #region FastStyle

        [Browsable(true)]
        [Category("FastStyle")]
        [Description("Background color of the combo box.")]
        public Color FillColor
        {
            get => comboBox.FillColor;
            set => comboBox.FillColor = value;
        }

        [Browsable(true)]
        [Category("FastStyle")]
        [Description("Combo box border color.")]
        public Color BorderColor
        {
            get => comboBox.BorderColor;
            set => comboBox.BorderColor = value;
        }

        [Browsable(true)]
        [Category("FastStyle")]
        [Description("Border thickness.")]
        public int BorderWidth
        {
            get => comboBox.BorderThickness;
            set => comboBox.BorderThickness = value;
        }

        #endregion


        // ---------------------------------------------------------------------

        #region FastText

        [Browsable(true)]
        [Category("FastText")]
        [Description("Text color inside the combo box.")]
        public Color TextColor
        {
            get => comboBox.ForeColor;
            set => comboBox.ForeColor = value;
        }

        [Browsable(true)]
        [Category("FastText")]
        [Description("Font size of combo box text.")]
        public float FontSize
        {
            get => comboBox.Font.Size;
            set
            {
                comboBox.Font = new Font(comboBox.Font.FontFamily, value);
                comboBox.Height = this.Height;
                comboBox.Width = this.Width;
            }
        }

        [Browsable(true)]
        [Category("FastText")]
        [Description("Complete font settings.")]
        public Font MoreFontSettings
        {
            get => comboBox.Font;
            set => comboBox.Font = value;
        }

        #endregion


        // ---------------------------------------------------------------------

        #region FastInteraction

        [Browsable(true)]
        [Category("FastInteraction")]
        [Description("Text color when the combo box is focused.")]
        public Color FocusTextColor
        {
            get => comboBox.FocusedState.ForeColor;
            set => comboBox.FocusedState.ForeColor = value;
        }

        [Browsable(true)]
        [Category("FastInteraction")]
        [Description("Border color when the combo box is focused.")]
        public Color FocusBorderColor
        {
            get => comboBox.FocusedState.BorderColor;
            set => comboBox.FocusedState.BorderColor = value;
        }

        [Browsable(true)]
        [Category("FastInteraction")]
        [Description("Background color when focused.")]
        public Color FocusFillColor
        {
            get => comboBox.FocusedState.FillColor;
            set => comboBox.FocusedState.FillColor = value;
        }

        #endregion


        // ---------------------------------------------------------------------

        #region FastShadow

        [Browsable(true)]
        [Category("FastShadow")]
        [Description("Enable or disable shadow around the combo box.")]
        public bool ShadowEnabled
        {
            get => _shadowAdapter.ShadowEnabled;
            set
            {
                _shadowAdapter.ShadowEnabled = value;

                if (value)
                {
                    _shadowAdapter.Dock = DockStyle.None;
                    comboBox.Size = this.Size;
                    _shadowEngine.Apply();
                }
                else
                {
                    _shadowEngine.Disable();
                    _shadowAdapter.Dock = DockStyle.Fill;
                }
            }
        }

        [Browsable(true)]
        [Category("FastShadow")]
        public Color ShadowColor
        {
            get => _shadowAdapter.ShadowColor;
            set => _shadowAdapter.ShadowColor = value;
        }

        [Browsable(true)]
        [Category("FastShadow")]
        public int ShadowBlur
        {
            get => _shadowAdapter.ShadowBlur;
            set => _shadowAdapter.ShadowBlur = value;
        }

        #endregion


        // ---------------------------------------------------------------------

        #region FastShadowEdges

        [Browsable(true)]
        [Category("FastShadowEdges")]
        public int ShadowTop
        {
            get => _shadowAdapter.ShadowPadding.Top;
            set
            {
                if (_shadowAdapter.ShadowEnabled || value == 0)
                    _shadowEngine.SetTop(value);
            }
        }

        [Browsable(true)]
        [Category("FastShadowEdges")]
        public int ShadowBottom
        {
            get => _shadowAdapter.ShadowPadding.Bottom;
            set
            {
                if (_shadowAdapter.ShadowEnabled || value == 0)
                    _shadowEngine.SetBottom(value);
            }
        }

        [Browsable(true)]
        [Category("FastShadowEdges")]
        public int ShadowLeft
        {
            get => _shadowAdapter.ShadowPadding.Left;
            set
            {
                if (_shadowAdapter.ShadowEnabled || value == 0)
                    _shadowEngine.SetLeft(value);
            }
        }

        [Browsable(true)]
        [Category("FastShadowEdges")]
        public int ShadowRight
        {
            get => _shadowAdapter.ShadowPadding.Right;
            set
            {
                if (_shadowAdapter.ShadowEnabled || value == 0)
                    _shadowEngine.SetRight(value);
            }
        }

        #endregion

        
        
        [Browsable(true)]
        [Category("Fast For Delete")]
        public string ContainerSize
        {
            get => $"Width: {this.Width} | Height: {this.Height}";            
        }
        // =====================================================================
        //  Internal Events
        // =====================================================================

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_noneMode == FastNoneMode.Allowed && comboBox.Text == "None")
            {
                comboBox.SelectedIndex = -1;
                label.Visible = true;
                return;
            }

            label.Visible = comboBox.SelectedIndex == -1;
            fakeFocus.Focus();
        }

        private void comboBox_MouseLeave(object sender, EventArgs e)
        {
            fakeFocus.Focus();

            if (comboBox.SelectedIndex == -1)
            {
                label.BackColor = Color.FromArgb(242, 242, 242);
                label.Visible = true;
            }
        }

        private void comboBox_MouseEnter(object sender, EventArgs e)
        {
            label.BackColor = Color.FromArgb(235, 235, 235);
        }

        private void label_Click(object sender, EventArgs e)
        {
            comboBox.Focus();
            comboBox.DroppedDown = true;
        }

        private void comboBox_DropDown(object sender, EventArgs e)
        {
            label.BackColor = Color.FromArgb(242, 242, 242);
        }

        private void comboBox_SizeChanged(object sender, EventArgs e)
        {

            //comboBox.ItemHeight = this.Height;
            //comboBox.Width = this.Width;
            //label.Location = new Point(comboBox.Location.X + 11, comboBox.Location.Y + comboBox.ItemHeight / 2);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            comboBox.DrawMode = DrawMode.OwnerDrawFixed;
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, height, specified);

            if (comboBox.ShadowDecoration.Enabled)
            {
                comboBox.ItemHeight = this.Height + comboBox.ShadowDecoration.Shadow.Top + comboBox.ShadowDecoration.Shadow.Bottom - 6;
            }
            else
                comboBox.ItemHeight = this.Height - 6;
        }


    }
}

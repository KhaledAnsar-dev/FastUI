using FastUI.FastUILibrary.Core.Rendering;
using FastUI.FastUILibrary.Core;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.FastUILibrary.Components.Internal
{
    /// <summary>
    /// Dropdown popup used by FuiComboBox. 
    /// Displays items inside a ListBox with custom FastUI border rendering.
    /// </summary>
    public class FuiComboPopup : Form
    {
        // ============================================================
        //  Fields
        // ============================================================

        // Internal ListBox displaying selectable items
        private ListBox _list;

        // FastUI renderer for rounded background & borders
        private FastShapeRenderer _renderer = new FastShapeRenderer();


        // ============================================================
        //  Public Properties
        // ============================================================

        /// <summary>Corner radius of the popup border.</summary>
        public float Radius { get; set; } = 9f;

        /// <summary>Border color used by the popup.</summary>
        public Color BorderColor { get; set; } = Color.Gray;

        /// <summary>Background fill color for the popup.</summary>
        public Color FillColor { get; set; } = Color.FromArgb(240,240,240);

        /// <summary>Thickness of the popup border.</summary>
        public float BorderThickness { get; set; } = 1f;


        // ============================================================
        //  Events
        // ============================================================

        /// <summary>
        /// Triggered when the user selects an item.
        /// Provides selected text and its index.
        /// </summary>
        public event Action<string, int> ItemSelected;


        // ============================================================
        //  Constructor
        // ============================================================

        public FuiComboPopup(string[] items)
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            ShowInTaskbar = false;
            TopMost = true;
            DoubleBuffered = true;

            BackColor = Color.White; // ← خلفية الفورم الطبيعية (أبيض)

            // Sync renderer initial values
            _renderer.Radius = Radius;
            _renderer.BorderColor = BorderColor;
            _renderer.BorderThickness = BorderThickness;
            _renderer.BackgroundColor = FillColor;

            _list = new ListBox()
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 10f),
                BackColor = FillColor
            };

            _list.Items.AddRange(items);

            _list.Click += (s, e) =>
            {
                if (_list.SelectedIndex >= 0)
                {
                    ItemSelected?.Invoke(
                        _list.SelectedItem.ToString(),
                        _list.SelectedIndex);
                    Close();
                }
            };

            Padding = new Padding(4);
            Controls.Add(_list);

            Deactivate += (s, e) => Close();
        }

        // ============================================================
        //  Painting
        // ============================================================

        /// <summary>
        /// Renders rounded border and background using FastUI renderer.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Sync renderer values (if properties changed externally)
            _renderer.Radius = Radius;
            _renderer.BackgroundColor = FillColor;
            _renderer.BorderColor = BorderColor;
            _renderer.BorderThickness = BorderThickness;

            _renderer.Render(
                e.Graphics,
                new Rectangle(0, 0, Width, Height),
                "",
                Font,
                Color.Black,
                false,
                FastTextAlign.Left,
                Point.Empty
            );
        }
    }

}

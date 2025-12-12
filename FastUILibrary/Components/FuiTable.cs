using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FastUI.FastUILibrary.Core;
using FastUI.FastUILibrary.Core.Rendering;
using FastUI.FastUILibrary.Themes.Infrastructure;
using FastUI.FastUILibrary.Themes.Presets;

namespace FastUI.FastUILibrary.Components
{
    /// <summary>
    /// A modern FastUI wrapper around DataGridView.
    /// 
    /// Features:
    /// - Custom container rendering (background + border)
    /// - Row hover and selection styling
    /// - Unified text alignment API
    /// - Scroll behavior control
    /// - Automatic or manual column sizing
    /// - Theme and preset integration
    /// 
    /// All native DataGridView functionality remains accessible
    /// through the InnerGrid property.
    /// </summary>
    public class FuiTable : UserControl
    {
        // ============================================================
        //  Rendering & Core Fields
        // ============================================================

        private readonly FastShapeRenderer _renderer = new FastShapeRenderer();

        public readonly DataGridView InnerGrid = new DataGridView();

        // ============================================================
        //  Interaction State
        // ============================================================

        private int _hoveredRow = -1;

        // ============================================================
        //  Layout State
        // ============================================================

        private bool _autoColumnWidth = true;

        private int _headerHeight = 32;
        private int _rowHeight = 28;
        private int _topPadding = 1;

        // ============================================================
        //  Colors State
        // ============================================================

        private Color _tableColor = Color.FromArgb(240, 240, 240);
        private Color _horizontalLineColor = Color.LightGray;

        // ============================================================
        //  Text & Alignment State
        // ============================================================

        private FastTextAlign _textAlign = FastTextAlign.Left;

        // ============================================================
        //  Behavior State
        // ============================================================

        private FastTableScroll _scrollMode = FastTableScroll.Vertical;

        // ============================================================
        //  Theme
        // ============================================================

        private string _themeName = "Windows11";

        // ============================================================
        //  Constructor
        // ============================================================

        /// <summary>
        /// Initializes a new instance of the FuiTable control
        /// and configures the internal DataGridView.
        /// </summary>
        public FuiTable()
        {
            DoubleBuffered = true;

            InnerGrid.Parent = this;
            InnerGrid.Dock = DockStyle.Fill;
            InnerGrid.BorderStyle = BorderStyle.None;
            InnerGrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            InnerGrid.EnableHeadersVisualStyles = false;
            InnerGrid.AllowUserToOrderColumns = false;
            InnerGrid.AllowUserToResizeColumns = false;
            InnerGrid.RowHeadersVisible = false;
            InnerGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            InnerGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            InnerGrid.MultiSelect = false;

            InnerGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            InnerGrid.ScrollBars = ScrollBars.Vertical;

            Padding = new Padding(10);
            ApplyTableColor(Color.FromArgb(240, 240, 240));

            // Hover tracking
            InnerGrid.MouseMove += (s, e) =>
            {
                var hit = InnerGrid.HitTest(e.X, e.Y);

                if (hit.RowIndex >= 0 && hit.RowIndex < InnerGrid.Rows.Count)
                {
                    if (_hoveredRow != -1 && _hoveredRow < InnerGrid.Rows.Count)
                        ApplyRowNormalStyle(_hoveredRow);

                    ApplyRowHoverStyle(hit.RowIndex);
                    _hoveredRow = hit.RowIndex;
                }
            };

            InnerGrid.MouseLeave += (s, e) =>
            {
                if (_hoveredRow != -1 && _hoveredRow < InnerGrid.Rows.Count)
                {
                    ApplyRowNormalStyle(_hoveredRow);
                    _hoveredRow = -1;
                }
            };

            InnerGrid.SelectionChanged += (s, e) =>
            {
                if (InnerGrid.CurrentCell != null && InnerGrid.CurrentCell.RowIndex == -1)
                {
                    InnerGrid.ClearSelection();
                    InnerGrid.CurrentCell = null;
                }
            };

            // APPLY DEFAULT THEME ON CREATION
            ApplyTheme();
        }

        // ============================================================
        //  Row Styling Helpers
        // ============================================================

        /// <summary>
        /// Restores the normal visual style for a row.
        /// </summary>
        private void ApplyRowNormalStyle(int index)
        {
            var row = InnerGrid.Rows[index];
            row.DefaultCellStyle.BackColor = _tableColor;
            row.DefaultCellStyle.ForeColor = RowTextColor;
            row.DefaultCellStyle.Font = RowTextFont;
            row.DefaultCellStyle.Alignment = ConvertAlignment(_textAlign);
        }

        /// <summary>
        /// Applies hover visual style to a row.
        /// </summary>
        private void ApplyRowHoverStyle(int index)
        {
            var row = InnerGrid.Rows[index];
            row.DefaultCellStyle.BackColor = RowHoverColor;
            row.DefaultCellStyle.ForeColor = RowHoverTextColor;
            row.DefaultCellStyle.Font = RowHoverTextFont;
            row.DefaultCellStyle.Alignment = ConvertAlignment(_textAlign);
        }

        /// <summary>
        /// Converts FastTextAlign to DataGridViewContentAlignment.
        /// </summary>
        private DataGridViewContentAlignment ConvertAlignment(FastTextAlign align)
        {
            return align switch
            {
                FastTextAlign.Left => DataGridViewContentAlignment.MiddleLeft,
                FastTextAlign.Center => DataGridViewContentAlignment.MiddleCenter,
                FastTextAlign.Right => DataGridViewContentAlignment.MiddleRight,
                _ => DataGridViewContentAlignment.MiddleLeft
            };
        }

        /// <summary>
        /// Applies text alignment to all rows, columns, and headers.
        /// </summary>
        private void ApplyTextAlignmentToAllCells()
        {
            var a = ConvertAlignment(_textAlign);

            InnerGrid.ColumnHeadersDefaultCellStyle.Alignment = a;

            foreach (DataGridViewRow row in InnerGrid.Rows)
                row.DefaultCellStyle.Alignment = a;

            foreach (DataGridViewColumn col in InnerGrid.Columns)
                col.DefaultCellStyle.Alignment = a;

            InnerGrid.Invalidate();
        }

        // ============================================================
        //  Table Color Logic
        // ============================================================

        /// <summary>
        /// Applies base table color to grid, rows, and headers.
        /// </summary>
        private void ApplyTableColor(Color c)
        {
            _tableColor = c;

            _renderer.BackgroundColor = c;
            InnerGrid.ColumnHeadersDefaultCellStyle.BackColor = c;
            InnerGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = c;
            InnerGrid.DefaultCellStyle.BackColor = c;
            InnerGrid.RowsDefaultCellStyle.BackColor = c;
            InnerGrid.BackgroundColor = c;

            InnerGrid.GridColor = _horizontalLineColor;

            InnerGrid.Invalidate();
        }

        // ============================================================
        //  Fast A - Behavior
        // ============================================================

        [Category("Fast A - Behavior")]
        public bool ReadOnly
        {
            get => InnerGrid.ReadOnly;
            set => InnerGrid.ReadOnly = value;
        }

        [Category("Fast A - Behavior")]
        public FastTableScroll ScrollMode
        {
            get => _scrollMode;
            set
            {
                _scrollMode = value;

                InnerGrid.ScrollBars = value switch
                {
                    FastTableScroll.None => ScrollBars.None,
                    FastTableScroll.Vertical => ScrollBars.Vertical,
                    FastTableScroll.Horizontal => ScrollBars.Horizontal,
                    FastTableScroll.Both => ScrollBars.Both,
                    _ => ScrollBars.Vertical
                };

                Invalidate();
            }
        }

        // ============================================================
        //  Fast B - Layout
        // ============================================================

        [Category("Fast B - Layout")]
        public bool AutoColumnWidth
        {
            get => _autoColumnWidth;
            set
            {
                _autoColumnWidth = value;
                InnerGrid.AutoSizeColumnsMode =
                    value ? DataGridViewAutoSizeColumnsMode.Fill
                          : DataGridViewAutoSizeColumnsMode.None;

                InnerGrid.Invalidate();
            }
        }

        [Category("Fast B - Layout")]
        public int HeaderHeight
        {
            get => _headerHeight;
            set { _headerHeight = value; InnerGrid.ColumnHeadersHeight = value; Invalidate(); }
        }

        [Category("Fast B - Layout")]
        public int RowHeight
        {
            get => _rowHeight;
            set
            {
                _rowHeight = value;
                InnerGrid.RowTemplate.Height = value;

                foreach (DataGridViewRow r in InnerGrid.Rows)
                    r.Height = value;

                Invalidate();
            }
        }

        [Category("Fast B - Layout")]
        public int TopPadding
        {
            get => _topPadding;
            set
            {
                _topPadding = Math.Max(1, value);
                Padding = new Padding(Padding.Left, _topPadding, Padding.Right, Padding.Bottom);
                Invalidate();
            }
        }

        // ============================================================
        //  Fast C - Colors (Table Base)
        // ============================================================

        [Category("Fast C - Colors (Table Base)")]
        public Color TableColor
        {
            get => _tableColor;
            set => ApplyTableColor(value);
        }

        // ============================================================
        //  Fast D - Colors (Rows)
        // ============================================================

        [Category("Fast D - Colors (Rows)")]
        public Color RowHoverColor { get; set; } = Color.FromArgb(250, 250, 250);

        [Category("Fast D - Colors (Rows)")]
        public Color RowSelectedColor { get; set; } = Color.FromArgb(220, 230, 255);

        // ============================================================
        //  Fast E - Colors (Grid Lines)
        // ============================================================

        [Category("Fast E - Colors (Grid Lines)")]
        public Color HorizontalLineColor
        {
            get => _horizontalLineColor;
            set { _horizontalLineColor = value; InnerGrid.GridColor = value; Invalidate(); }
        }

        // ============================================================
        //  Fast F - Text
        // ============================================================

        [Category("Fast F - Text")]
        public Color HeaderTextColor { get; set; } = Color.Black;

        [Category("Fast F - Text")]
        public Color RowTextColor { get; set; } = Color.Black;

        [Category("Fast F - Text")]
        public Color RowHoverTextColor { get; set; } = Color.Black;

        [Category("Fast F - Text")]
        public Color RowSelectedTextColor { get; set; } = Color.Black;

        [Category("Fast F - Text")]
        public FastTextAlign TextAlign
        {
            get => _textAlign;
            set
            {
                _textAlign = value;
                ApplyTextAlignmentToAllCells();
            }
        }

        // ============================================================
        //  Fast G - Fonts
        // ============================================================

        [Category("Fast G - Fonts")]
        public Font HeaderTextFont { get; set; } = new Font("Segoe UI", 10f);

        [Category("Fast G - Fonts")]
        public Font RowTextFont { get; set; } = new Font("Segoe UI", 10f);

        [Category("Fast G - Fonts")]
        public Font RowHoverTextFont { get; set; } = new Font("Segoe UI", 10f, FontStyle.Bold);

        [Category("Fast G - Fonts")]
        public Font RowSelectedTextFont { get; set; } = new Font("Segoe UI", 10f);

        // ============================================================
        //  Fast H - Border
        // ============================================================

        [Category("Fast H - Border")]
        public Color BorderColor
        {
            get => _renderer.BorderColor;
            set { _renderer.BorderColor = value; Invalidate(); }
        }

        [Category("Fast H - Border")]
        public float BorderRadius
        {
            get => _renderer.Radius;
            set { _renderer.Radius = value; Invalidate(); }
        }

        [Category("Fast H - Border")]
        public float BorderWidth
        {
            get => _renderer.BorderThickness;
            set { _renderer.BorderThickness = value; Invalidate(); }
        }

        // ============================================================
        //  Fast I - Theme
        // ============================================================

        [Category("Fast I - Theme")]
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
        //  Presets & Themes
        // ============================================================

        /// <summary>
        /// Applies a visual table preset.
        /// </summary>
        private void ApplyPreset(TablePreset p)
        {
            HeaderHeight = p.HeaderHeight;
            RowHeight = p.RowHeight;
            TopPadding = p.TopPadding;

            TableColor = p.TableColor;

            RowHoverColor = p.RowHoverColor;
            RowSelectedColor = p.RowSelectedColor;

            HorizontalLineColor = p.HorizontalLineColor;

            HeaderTextColor = p.HeaderTextColor;
            RowTextColor = p.RowTextColor;
            RowHoverTextColor = p.RowHoverTextColor;
            RowSelectedTextColor = p.RowSelectedTextColor;
            TextAlign = p.TextAlign;

            HeaderTextFont = p.HeaderTextFont;
            RowTextFont = p.RowTextFont;
            RowHoverTextFont = p.RowHoverTextFont;
            RowSelectedTextFont = p.RowSelectedTextFont;

            BorderColor = p.BorderColor;
            BorderRadius = p.BorderRadius;
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
                ApplyPreset(theme.GetTablePreset());
        }

        // ============================================================
        //  Painting
        // ============================================================

        /// <summary>
        /// Renders the table container background and border.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            _renderer.Render(
                e.Graphics,
                rect,
                "",
                Font,
                Color.Transparent,
                false,
                FastTextAlign.Left,
                Point.Empty
            );

            InnerGrid.ColumnHeadersDefaultCellStyle.ForeColor = HeaderTextColor;
            InnerGrid.ColumnHeadersDefaultCellStyle.Font = HeaderTextFont;

            InnerGrid.DefaultCellStyle.ForeColor = RowTextColor;
            InnerGrid.DefaultCellStyle.Font = RowTextFont;

            InnerGrid.DefaultCellStyle.SelectionBackColor = RowSelectedColor;
            InnerGrid.DefaultCellStyle.SelectionForeColor = RowSelectedTextColor;
        }

        // ============================================================
        //  Public API
        // ============================================================

        /// <summary>
        /// Adds a new column to the table.
        /// </summary>
        public void AddColumn(string name, int width = 100)
        {
            InnerGrid.Columns.Add(name, name);
            InnerGrid.Columns[name].Width = width;

            InnerGrid.Columns[name].DefaultCellStyle.Alignment = ConvertAlignment(_textAlign);
            InnerGrid.ColumnHeadersDefaultCellStyle.Alignment = ConvertAlignment(_textAlign);
        }

        /// <summary>
        /// Adds a new data row to the table.
        /// </summary>
        public void AddRow(params object[] values)
        {
            InnerGrid.Rows.Add(values);

            foreach (DataGridViewRow r in InnerGrid.Rows)
            {
                r.Height = _rowHeight;
                r.DefaultCellStyle.Alignment = ConvertAlignment(_textAlign);
            }
        }
    }
}

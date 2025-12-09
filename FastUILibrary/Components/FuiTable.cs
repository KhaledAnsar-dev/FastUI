using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FastUI.FastUILibrary.Core;
using FastUI.FastUILibrary.Core.Rendering;

namespace FastUI.FastUILibrary.Components
{
    /// <summary>
    /// A modern FastUI wrapper around DataGridView that provides:
    /// - Styled rows
    /// - Hover effects
    /// - Text alignment API
    /// - Scroll control
    /// - Auto column width
    /// - Custom border rendering
    /// 
    /// All DataGridView native features remain fully accessible via InnerGrid.
    /// </summary>
    public class FuiTable : UserControl
    {
        // ============================================================
        //  Fields
        // ============================================================

        /// <summary>
        /// Renderer responsible for drawing table background and border.
        /// </summary>
        private readonly FastShapeRenderer _renderer = new FastShapeRenderer();

        /// <summary>
        /// Underlying DataGridView that powers the table.
        /// Developers can still access all DataGridView features through this object.
        /// </summary>
        public readonly DataGridView InnerGrid = new DataGridView();

        /// <summary>
        /// Stores the index of the currently hovered row (used for hover highlight).
        /// </summary>
        private int _hoveredRow = -1;

        /// <summary>
        /// Padding for top spacing (e.g., room for buttons above the grid).
        /// </summary>
        private int _topPadding = 1;


        // ============================================================
        //  Constructor
        // ============================================================

        /// <summary>
        /// Initializes the FuiTable with default styles, layout,
        /// hover logic, scroll mode, and DataGridView configuration.
        /// </summary>
        public FuiTable()
        {
            DoubleBuffered = true;

            // Base DataGridView configuration
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


            // --------------------------
            // Hover logic
            // --------------------------
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

            // Prevent selecting header row
            InnerGrid.SelectionChanged += (s, e) =>
            {
                if (InnerGrid.CurrentCell != null && InnerGrid.CurrentCell.RowIndex == -1)
                {
                    InnerGrid.ClearSelection();
                    InnerGrid.CurrentCell = null;
                }
            };
        }


        // ============================================================
        //  Helpers
        // ============================================================

        /// <summary>
        /// Restores default row style (background, text, font, alignment).
        /// </summary>
        private void ApplyRowNormalStyle(int index)
        {
            var row = InnerGrid.Rows[index];
            row.DefaultCellStyle.BackColor = _tableColor;
            row.DefaultCellStyle.ForeColor = RowTextColor;
            row.DefaultCellStyle.Font = RowTextFont;
            row.DefaultCellStyle.Alignment = ConvertAlignment(TextAlign);
        }

        /// <summary>
        /// Applies hover style to a given row index.
        /// </summary>
        private void ApplyRowHoverStyle(int index)
        {
            var row = InnerGrid.Rows[index];
            row.DefaultCellStyle.BackColor = RowHoverColor;
            row.DefaultCellStyle.ForeColor = RowHoverTextColor;
            row.DefaultCellStyle.Font = RowHoverTextFont;
            row.DefaultCellStyle.Alignment = ConvertAlignment(TextAlign);
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
        /// Applies text alignment to all columns, rows, and headers.
        /// </summary>
        private void ApplyTextAlignmentToAllCells()
        {
            var a = ConvertAlignment(TextAlign);

            InnerGrid.ColumnHeadersDefaultCellStyle.Alignment = a;

            foreach (DataGridViewRow row in InnerGrid.Rows)
                row.DefaultCellStyle.Alignment = a;

            foreach (DataGridViewColumn col in InnerGrid.Columns)
                col.DefaultCellStyle.Alignment = a;

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

        private FastTableScroll _scrollMode = FastTableScroll.Vertical;

        /// <summary>
        /// Controls scrollbar visibility (None, Vertical, Horizontal, Both).
        /// </summary>
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

        private bool _autoColumnWidth = true;

        /// <summary>
        /// When enabled, columns stretch automatically using Fill mode.
        /// </summary>
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

        private int _headerHeight = 32;
        private int _rowHeight = 28;

        /// <summary>
        /// Height of header row.
        /// </summary>
        [Category("Fast B - Layout")]
        public int HeaderHeight
        {
            get => _headerHeight;
            set { _headerHeight = value; InnerGrid.ColumnHeadersHeight = value; Invalidate(); }
        }

        /// <summary>
        /// Height of each data row.
        /// </summary>
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

        /// <summary>
        /// Extra padding above the table (useful for placing buttons/toolbars).
        /// </summary>
        [Category("Fast B - Layout")]
        public int TopPadding
        {
            get => _topPadding;
            set
            {
                _topPadding = Math.Max(1, value);
                this.Padding = new Padding(
                    this.Padding.Left,
                    _topPadding,
                    this.Padding.Right,
                    this.Padding.Bottom
                );
                Invalidate();
            }
        }


        // ============================================================
        //  Fast C - Colors (Table Base)
        // ============================================================

        private Color _tableColor = Color.FromArgb(240, 240, 240);

        /// <summary>
        /// Background color of the table surface and default rows.
        /// </summary>
        [Category("Fast C - Colors (Table Base)")]
        public Color TableColor
        {
            get => _tableColor;
            set => ApplyTableColor(value);
        }

        /// <summary>
        /// Applies table background color to grid, headers, and cells.
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

            InnerGrid.GridColor = HorizontalLineColor;

            InnerGrid.Invalidate();
        }


        // ============================================================
        //  Fast D - Colors (Rows)
        // ============================================================

        /// <summary>
        /// Background color used when hovering over a row.
        /// </summary>
        [Category("Fast D - Colors (Rows)")]
        public Color RowHoverColor { get; set; } = Color.FromArgb(250, 250, 250);

        /// <summary>
        /// Color used for selected rows.
        /// </summary>
        [Category("Fast D - Colors (Rows)")]
        public Color RowSelectedColor { get; set; } = Color.FromArgb(220, 230, 255);


        // ============================================================
        //  Fast E - Colors (Grid Lines)
        // ============================================================

        private Color _horizontalLineColor = Color.LightGray;

        /// <summary>
        /// Color of horizontal separator lines between rows.
        /// </summary>
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

        private FastTextAlign _textAlign = FastTextAlign.Left;

        /// <summary>
        /// Controls horizontal text alignment across all table cells.
        /// </summary>
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

        /// <summary>
        /// Border color of the table container.
        /// </summary>
        [Category("Fast H - Border")]
        public Color BorderColor
        {
            get => _renderer.BorderColor;
            set { _renderer.BorderColor = value; Invalidate(); }
        }

        /// <summary>
        /// Border corner radius for rounded edges.
        /// </summary>
        [Category("Fast H - Border")]
        public float BorderRadius
        {
            get => _renderer.Radius;
            set { _renderer.Radius = value; Invalidate(); }
        }

        /// <summary>
        /// Border thickness around the table.
        /// </summary>
        [Category("Fast H - Border")]
        public float BorderWidth
        {
            get => _renderer.BorderThickness;
            set { _renderer.BorderThickness = value; Invalidate(); }
        }


        // ============================================================
        //  Painting
        // ============================================================

        /// <summary>
        /// Renders the table container background and border using FastUI renderer.
        /// Also applies all font & color settings to DataGridView cells.
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

            // Apply text colors/fonts dynamically
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
        /// Name is used for both column header and internal key.
        /// </summary>
        public void AddColumn(string name, int width = 100)
        {
            InnerGrid.Columns.Add(name, name);
            InnerGrid.Columns[name].Width = width;

            InnerGrid.Columns[name].DefaultCellStyle.Alignment = ConvertAlignment(TextAlign);
            InnerGrid.ColumnHeadersDefaultCellStyle.Alignment = ConvertAlignment(TextAlign);
        }

        /// <summary>
        /// Adds a new data row to the table with the specified values.
        /// </summary>
        public void AddRow(params object[] values)
        {
            InnerGrid.Rows.Add(values);

            foreach (DataGridViewRow r in InnerGrid.Rows)
            {
                r.Height = RowHeight;
                r.DefaultCellStyle.Alignment = ConvertAlignment(TextAlign);
            }
        }
    }
}

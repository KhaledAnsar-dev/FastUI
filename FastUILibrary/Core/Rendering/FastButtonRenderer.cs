using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FastUI.Core.Rendering
{
    /// <summary>
    /// Renders FastUI buttons using high-quality smoothing, rounded corners,
    /// border simulation, and supersampling for crisp edges.
    /// Now supports text alignment + text offset.
    /// </summary>
    public class FastButtonRenderer
    {
        public Color BackgroundColor { get; set; } = Color.White;
        public Color BorderColor { get; set; } = Color.Black;
        public float BorderThickness { get; set; } = 1f;
        public float Radius { get; set; } = 10f;

        // Supersampling factor for smoother curves
        private const int SSAA = 2;


        // =====================================================================
        //  MAIN RENDER FUNCTION (FuiButton calls this one)
        // =====================================================================
        public void Render(
            Graphics g,
            Rectangle bounds,
            string text,
            Font font,
            Color textColor,
            bool designerMode,
            FastUI.Modules.Buttons.FuiButton.FastTextAlign textAlign,
            Point textOffset)
        {
            int w = bounds.Width * SSAA;
            int h = bounds.Height * SSAA;

            using (Bitmap bmp = new Bitmap(w, h))
            using (Graphics sg = Graphics.FromImage(bmp))
            {
                sg.SmoothingMode = SmoothingMode.AntiAlias;
                sg.PixelOffsetMode = PixelOffsetMode.HighQuality;
                sg.InterpolationMode = InterpolationMode.HighQualityBicubic;
                sg.CompositingQuality = CompositingQuality.HighQuality;

                RectangleF r = new RectangleF(0, 0, w - 1, h - 1);
                float radius = Radius * SSAA;
                float thickness = BorderThickness * SSAA;


                // =============================================================
                //  BACKGROUND SHAPE
                // =============================================================
                using (GraphicsPath p = CreateRoundedRect(r, radius))
                using (SolidBrush b = new SolidBrush(BackgroundColor))
                    sg.FillPath(b, p);


                // =============================================================
                //  BORDER (FILL TECHNIQUE)
                // =============================================================
                if (BorderThickness > 0)
                {
                    using (GraphicsPath outerPath = CreateRoundedRect(r, radius))
                    using (SolidBrush borderBrush = new SolidBrush(BorderColor))
                        sg.FillPath(borderBrush, outerPath);

                    float shrink = thickness;

                    RectangleF innerRect = new RectangleF(
                        r.X + shrink,
                        r.Y + shrink,
                        r.Width - shrink * 2,
                        r.Height - shrink * 2
                    );

                    float innerRadius = Math.Max(radius - shrink, 1);

                    using (GraphicsPath innerPath = CreateRoundedRect(innerRect, innerRadius))
                    using (SolidBrush bgBrush = new SolidBrush(BackgroundColor))
                        sg.FillPath(bgBrush, innerPath);
                }


                // =============================================================
                //  DRAW TO CONTROL (Downscale)
                // =============================================================
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, bounds);
            }


            // =============================================================
            //  TEXT RENDERING WITH ALIGNMENT + OFFSET
            // =============================================================

            // base rectangle for text
            Rectangle textRect = new Rectangle(
                bounds.X + textOffset.X,
                bounds.Y + textOffset.Y,
                bounds.Width,
                bounds.Height
            );

            TextFormatFlags flags = TextFormatFlags.VerticalCenter;

            switch (textAlign)
            {
                case FastUI.Modules.Buttons.FuiButton.FastTextAlign.Left:
                    flags |= TextFormatFlags.Left;
                    break;

                case FastUI.Modules.Buttons.FuiButton.FastTextAlign.Right:
                    flags |= TextFormatFlags.Right;
                    break;

                default:
                case FastUI.Modules.Buttons.FuiButton.FastTextAlign.Center:
                    flags |= TextFormatFlags.HorizontalCenter;
                    break;
            }

            TextRenderer.DrawText(
                g,
                text,
                font,
                textRect,
                textColor,
                flags
            );
        }



        // =====================================================================
        //  Rounded Rectangle (supports radius = 0)
        // =====================================================================
        public static GraphicsPath CreateRoundedRect(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();

            if (radius <= 0f)
            {
                path.AddRectangle(rect);
                path.CloseFigure();
                return path;
            }

            float maxRadius = Math.Min(rect.Width, rect.Height) / 2f;
            float r = Math.Min(radius, maxRadius);
            float d = r * 2f;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}

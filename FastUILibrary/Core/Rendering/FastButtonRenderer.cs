using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FastUI.Core.Rendering
{
    /// <summary>
    /// Renders FastUI buttons using high-quality smoothing, rounded corners,
    /// border simulation, and supersampling for crisp edges.
    /// </summary>
    public class FastButtonRenderer
    {
        public Color BackgroundColor { get; set; } = Color.White;
        public Color BorderColor { get; set; } = Color.Black;
        public float BorderThickness { get; set; } = 1f;
        public float Radius { get; set; } = 10f;

        // Supersampling factor for smoother curves
        private const int SSAA = 2;

        public void Render(Graphics g, Rectangle bounds, string text, Font font, Color textColor, bool designerMode)
        {
            int w = bounds.Width * SSAA;
            int h = bounds.Height * SSAA;

            using (Bitmap bmp = new Bitmap(w, h))
            using (Graphics sg = Graphics.FromImage(bmp))
            {
                // High-quality rendering setup
                sg.SmoothingMode = SmoothingMode.AntiAlias;
                sg.PixelOffsetMode = PixelOffsetMode.HighQuality;
                sg.InterpolationMode = InterpolationMode.HighQualityBicubic;
                sg.CompositingQuality = CompositingQuality.HighQuality;

                RectangleF r = new RectangleF(0, 0, w - 1, h - 1);
                float radius = Radius * SSAA;
                float thickness = BorderThickness * SSAA;

                // ===== BACKGROUND SHAPE =====
                using (GraphicsPath p = CreateRoundedRect(r, radius))
                using (SolidBrush b = new SolidBrush(BackgroundColor))
                    sg.FillPath(b, p);

                // ===== BORDER USING FILL TECHNIQUE =====
                if (BorderThickness > 0)
                {
                    // Outer border area
                    using (GraphicsPath outerPath = CreateRoundedRect(r, radius))
                    using (SolidBrush borderBrush = new SolidBrush(BorderColor))
                        sg.FillPath(borderBrush, outerPath);

                    // Inner shape that restores background
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

                // ===== DRAW RESULT BACK TO MAIN GRAPHICS (downscaled) =====
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, bounds);
            }

            // ===== TEXT =====
            // Centered text rendering
            TextRenderer.DrawText(
                g,
                text,
                font,
                bounds,
                textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }

        // Creates a smooth rounded rectangle using arcs (optimized for supersampling)
        public static GraphicsPath CreateRoundedRectoff(RectangleF rect, float radius)
        {
            float d = radius * 2f;
            GraphicsPath path = new GraphicsPath();

            // Corner arcs
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);

            path.CloseFigure();
            return path;
        }
        public static GraphicsPath CreateRoundedRect(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();

            // 1) No radius → normal rectangle (fix for radius = 0)
            if (radius <= 0f)
            {
                path.AddRectangle(rect);
                path.CloseFigure();
                return path;
            }

            // 2) Clamp radius so it never exceeds half of width/height
            float maxRadius = Math.Min(rect.Width, rect.Height) / 2f;
            float r = Math.Min(radius, maxRadius);
            float d = r * 2f;

            // 3) Normal arc-based rounded rect
            path.AddArc(rect.X, rect.Y, d, d, 180, 90); // TL
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90); // TR
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90); // BR
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90); // BL

            path.CloseFigure();
            return path;
        }

    }
}

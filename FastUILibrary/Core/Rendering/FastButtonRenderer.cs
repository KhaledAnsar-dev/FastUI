using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FastUI.Core.Rendering
{
    public class FastButtonRenderer
    {
        public Color BackgroundColor { get; set; } = Color.White;
        public Color BorderColor { get; set; } = Color.Black;
        public float BorderThickness { get; set; } = 1f;
        public float Radius { get; set; } = 10f;

        // Anti-aliasing via supersampling
        private const int SSAA = 2; // 2x supersampling

        public void Render(Graphics g, Rectangle bounds, string text, Font font, Color textColor, bool designerMode)
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

                // ===== BACKGROUND =====
                using (GraphicsPath p = CreateSmoothRoundedRect(r, radius))
                using (SolidBrush b = new SolidBrush(BackgroundColor))
                    sg.FillPath(b, p);

                // ===== BORDER =====
                if (BorderThickness > 0)
                {
                    using (GraphicsPath p = CreateSmoothRoundedRect(r, radius))
                    using (Pen pen = new Pen(BorderColor, thickness))
                    {
                        pen.Alignment = PenAlignment.Center;
                        sg.DrawPath(pen, p);
                    }
                }

                // ===== DRAW TO REAL GRAPHICS (Downscale) =====
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, bounds);
            }

            // TEXT
            TextRenderer.DrawText(
                g,
                text,
                font,
                bounds,
                textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }

        // Same Bezier method, but now it's perfect with supersampling
        public static GraphicsPath CreateSmoothRoundedRect(RectangleF rect, float radius)
        {
            float r = radius;

            GraphicsPath path = new GraphicsPath();
            path.StartFigure();

            // TOP LEFT
            path.AddBezier(rect.X, rect.Y + r,
                           rect.X, rect.Y,
                           rect.X, rect.Y,
                           rect.X + r, rect.Y);

            // TOP RIGHT
            path.AddBezier(rect.Right - r, rect.Y,
                           rect.Right, rect.Y,
                           rect.Right, rect.Y,
                           rect.Right, rect.Y + r);

            // BOTTOM RIGHT
            path.AddBezier(rect.Right, rect.Bottom - r,
                           rect.Right, rect.Bottom,
                           rect.Right, rect.Bottom,
                           rect.Right - r, rect.Bottom);

            // BOTTOM LEFT
            path.AddBezier(rect.X + r, rect.Bottom,
                           rect.X, rect.Bottom,
                           rect.X, rect.Bottom,
                           rect.X, rect.Bottom - r);

            path.CloseFigure();
            return path;
        }
    }
}

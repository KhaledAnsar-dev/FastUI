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
                using (GraphicsPath p = CreateRoundedRect(r, radius))
                using (SolidBrush b = new SolidBrush(BackgroundColor))
                    sg.FillPath(b, p);

                // ===== BORDER (Fill Technique) =====
                if (BorderThickness > 0)
                {
                    float shrink = thickness;

                    // Outer shape (border color)
                    using (GraphicsPath outerPath = CreateRoundedRect(r, radius))
                    using (SolidBrush borderBrush = new SolidBrush(BorderColor))
                        sg.FillPath(borderBrush, outerPath);

                    // Inner shape (background color)
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
        public static GraphicsPath CreateRoundedRect(RectangleF rect, float radius)
        {
            float d = radius * 2f;
            GraphicsPath path = new GraphicsPath();

            // Top-left arc
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);

            // Top-right arc
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);

            // Bottom-right arc
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);

            // Bottom-left arc
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}

using System.Drawing;
using System.Drawing.Drawing2D;

namespace FastUI.Core.Rendering
{
    public class FastPanelRenderer
    {
        public Color BackgroundColor { get; set; } = Color.White;
        public Color BorderColor { get; set; } = Color.Black;
        public float BorderThickness { get; set; } = 1f;
        public float Radius { get; set; } = 10f;

        private const int SSAA = 2; // supersampling ×2

        public void Render(Graphics g, Rectangle bounds)
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
                    // Outer shape
                    using (GraphicsPath outer = CreateRoundedRect(r, radius))
                    using (SolidBrush borderBrush = new SolidBrush(BorderColor))
                        sg.FillPath(borderBrush, outer);

                    float shrink = thickness;

                    RectangleF innerRect = new RectangleF(
                        r.X + shrink,
                        r.Y + shrink,
                        r.Width - shrink * 2,
                        r.Height - shrink * 2
                    );

                    float innerRadius = Math.Max(radius - shrink, 0);

                    // Inner shape = background
                    using (GraphicsPath inner = CreateRoundedRect(innerRect, innerRadius))
                    using (SolidBrush bgBrush = new SolidBrush(BackgroundColor))
                        sg.FillPath(bgBrush, inner);
                }

                // ===== DRAW RESULT (Downscale) =====
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, bounds);
            }
        }

        // Smooth rounded rectangle generator
        public static GraphicsPath CreateRoundedRect(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();

            // If no radius → draw rectangle
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

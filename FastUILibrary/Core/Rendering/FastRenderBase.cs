
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FastUI.Core.Rendering
{
    /// <summary>
    /// Provides core rendering utilities for FastUI, including smooth rounded shapes
    /// and high-quality graphics settings.
    /// </summary>
    public static class FastRenderBase
    {
        public static GraphicsPath CreateSmoothRoundedRect(RectangleF rect, float radius)
        {
            float r = radius * 2f;

            GraphicsPath path = new GraphicsPath();
            path.StartFigure();

            // TOP LEFT corner curve
            path.AddBezier(
                rect.X,
                rect.Y + radius,
                rect.X,
                rect.Y,
                rect.X,
                rect.Y,
                rect.X + radius,
                rect.Y
            );

            // TOP RIGHT corner curve
            path.AddBezier(
                rect.Right - radius,
                rect.Y,
                rect.Right,
                rect.Y,
                rect.Right,
                rect.Y,
                rect.Right,
                rect.Y + radius
            );

            // BOTTOM RIGHT corner curve
            path.AddBezier(
                rect.Right,
                rect.Bottom - radius,
                rect.Right,
                rect.Bottom,
                rect.Right,
                rect.Bottom,
                rect.Right - radius,
                rect.Bottom
            );

            // BOTTOM LEFT corner curve
            path.AddBezier(
                rect.X + radius,
                rect.Bottom,
                rect.X,
                rect.Bottom,
                rect.X,
                rect.Bottom,
                rect.X,
                rect.Bottom - radius
            );

            path.CloseFigure();
            return path;
        }

        public static void SetHighQuality(Graphics g)
        {
            // Enable anti-aliasing and highest rendering quality
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
        }
    }
}

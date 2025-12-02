using System.Drawing;
using System.Drawing.Drawing2D;

namespace FastUI.Core.Rendering
{
    public static class FastRenderBase
    {
        public static GraphicsPath CreateSmoothRoundedRect(RectangleF rect, float radius)
{
    float r = radius * 2f;

    GraphicsPath path = new GraphicsPath();

    path.StartFigure();

    // TOP LEFT
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

    // TOP RIGHT
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

    // BOTTOM RIGHT
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

    // BOTTOM LEFT
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
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
        }
    }
}

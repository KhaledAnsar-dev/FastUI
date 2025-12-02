using System.Drawing;
using System.Drawing.Drawing2D;

namespace FastUI.Core.Rendering
{
    /// <summary>
    /// Provides basic drop-shadow rendering for UI elements.
    /// This is a simple non-blurred shadow for early versions of FastUI.
    /// Later it can be upgraded to soft blurred shadows.
    /// </summary>
    public static class FastShadowEngine
    {
        /// <summary>
        /// Draws a simple drop shadow behind a rectangle.
        /// </summary>
        public static void DrawShadow(Graphics g, Rectangle rect, int radius, int shadowSize, Color shadowColor)
        {
            // The shadow is simply an inflated rounded rectangle behind the main shape.
            Rectangle shadowRect = new Rectangle(
                rect.X + shadowSize,
                rect.Y + shadowSize,
                rect.Width,
                rect.Height
            );

            //using (GraphicsPath path = FastRenderBase.CreateRoundedRect(shadowRect, radius))
            //using (SolidBrush brush = new SolidBrush(shadowColor))
            //{
            //    g.FillPath(brush, path);
            //}
        }

        /// <summary>
        /// Returns a default soft shadow color (semi-transparent black).
        /// </summary>
        public static Color DefaultShadowColor = Color.FromArgb(60, 0, 0, 0);

        /// <summary>
        /// Recommended shadow size for simple UI elements.
        /// </summary>
        public static int DefaultShadowSize = 4;
    }
}

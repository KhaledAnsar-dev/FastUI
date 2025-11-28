using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.FastUILibrary.Core.Shadow
{
    /// <summary>
    /// Holds all shadow-related values for internal layout calculations.
    /// </summary>
    public class FastShadowSettings
    {
        // Shadow padding
        public int Top { get; set; } = 0;
        public int Bottom { get; set; } = 0;
        public int Left { get; set; } = 0;
        public int Right { get; set; } = 0;

        public FastShadowSettings()
        {
        }
    }
}

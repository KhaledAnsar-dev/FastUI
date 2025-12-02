using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.FastUILibrary.Core.Interfaces
{
    /// <summary>
    /// Defines the required shadow-related properties that any FastUI control 
    /// must expose to be managed by the FastShadowEngine.
    /// </summary>
    public interface IFastShadowTarget
    {
        Size Size { get; set;}
        Point Location { get; set;}
        Padding ShadowPadding { get; set; }
        bool ShadowEnabled { get; set; }
        int ShadowBlur { get; set; }
        Color ShadowColor { get; set; }
        int BorderRadius { get; }
        DockStyle Dock { get; set; }
        int ItemHeight { get; set; }
        bool IsCombo { get; set; }

    }
}

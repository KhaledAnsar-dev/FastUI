using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.FastUILibrary.Core.Interfaces
{
    public interface IFastShadowTarget
    {
        Size Size { get; set;}
        Point Location { get; set;}
        Padding ShadowPadding { get; set; }
        bool ShadowEnabled { get; set; }
        int ShadowBlur { get; set; }
        Color ShadowColor { get; set; }
        int BorderRadius { get; }

        event EventHandler SizeChanged;   

    }
}

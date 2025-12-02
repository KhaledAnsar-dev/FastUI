using FastUI.FastUILibrary.Core.Interfaces;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.FastUILibrary.Core.Shadow.Adapters
{

    /// <summary>
    /// Adapts a Guna2TextBox to the IFastShadowTarget interface,
    /// allowing FastShadowEngine to control its shadow behavior.
    /// </summary>
    public class GunaShadowAdapter : IFastShadowTarget
    {
        private readonly Guna.UI2.WinForms.Guna2TextBox _ctrl;

        public GunaShadowAdapter(Guna2TextBox ctrl)
        {
            _ctrl = ctrl;

            // Reset default Guna shadow size
            ctrl.ShadowDecoration.Shadow = new Padding(0);

            // Default blur level
            ctrl.ShadowDecoration.Depth = 5;

            // Sync shadow radius with control's corner radius
            ctrl.ShadowDecoration.BorderRadius = ctrl.BorderRadius;
        }
        public bool IsCombo { set; get; } = false;
        public int ItemHeight { set; get; }
        // Forward size read/write
        public Size Size
        {
            get => _ctrl.Size;
            set => _ctrl.Size = value;
        }

        // Forward location read/write
        public Point Location
        {
            get => _ctrl.Location;
            set => _ctrl.Location = value;
        }

        // Shadow padding edges
        public Padding ShadowPadding
        {
            get => _ctrl.ShadowDecoration.Shadow;
            set => _ctrl.ShadowDecoration.Shadow = value;
        }

        // Enable/disable shadow
        public bool ShadowEnabled
        {
            get => _ctrl.ShadowDecoration.Enabled;
            set => _ctrl.ShadowDecoration.Enabled = value;
        }

        // Blur intensity
        public int ShadowBlur
        {
            get => _ctrl.ShadowDecoration.Depth;
            set => _ctrl.ShadowDecoration.Depth = value;
        }

        // Shadow color
        public Color ShadowColor
        {
            get => _ctrl.ShadowDecoration.Color;
            set => _ctrl.ShadowDecoration.Color = value;
        }

        // Dock mode passthrough
        public DockStyle Dock
        {
            get => _ctrl.Dock;
            set => _ctrl.Dock = value;
        }

        // Corner radius (read-only)
        public int BorderRadius => _ctrl.BorderRadius;
    }

}

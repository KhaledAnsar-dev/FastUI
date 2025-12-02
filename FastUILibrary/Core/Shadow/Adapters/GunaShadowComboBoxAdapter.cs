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
    /// Adapter for binding Guna2ComboBox shadow and size properties
    /// to the FastShadowEngine through IFastShadowTarget.
    /// </summary>
    public class GunaShadowComboBoxAdapter : IFastShadowTarget
    {
        private readonly Guna.UI2.WinForms.Guna2ComboBox _ctrl;

        public GunaShadowComboBoxAdapter(Guna2ComboBox ctrl)
        {
            _ctrl = ctrl;

            // Reset and configure shadow defaults to avoid conflicts
            _ctrl.ShadowDecoration.Shadow = new Padding(0);
            _ctrl.ShadowDecoration.Depth = 5;
            _ctrl.ShadowDecoration.BorderRadius = _ctrl.BorderRadius;
        }

        public bool IsCombo { set; get; } = true;
        public int ItemHeight
        {
            get => _ctrl.ItemHeight;
            set => _ctrl.ItemHeight = value;
        }
        // -----------------------------------------------------------
        // Size passthrough
        // -----------------------------------------------------------
        public Size Size
        {
            get => _ctrl.Size;
            set => _ctrl.Size = value;
        }

        // -----------------------------------------------------------
        // Location passthrough
        // -----------------------------------------------------------
        public Point Location
        {
            get => _ctrl.Location;
            set => _ctrl.Location = value;
        }

        // -----------------------------------------------------------
        // Shadow padding (top, bottom, left, right)
        // -----------------------------------------------------------
        public Padding ShadowPadding
        {
            get => _ctrl.ShadowDecoration.Shadow;
            set => _ctrl.ShadowDecoration.Shadow = value;
        }

        // -----------------------------------------------------------
        // Enable/disable shadow rendering
        // -----------------------------------------------------------
        public bool ShadowEnabled
        {
            get => _ctrl.ShadowDecoration.Enabled;
            set => _ctrl.ShadowDecoration.Enabled = value;
        }

        // -----------------------------------------------------------
        // Shadow blur effect (softness)
        // -----------------------------------------------------------
        public int ShadowBlur
        {
            get => _ctrl.ShadowDecoration.Depth;
            set => _ctrl.ShadowDecoration.Depth = value;
        }

        // -----------------------------------------------------------
        // Shadow color mapping
        // -----------------------------------------------------------
        public Color ShadowColor
        {
            get => _ctrl.ShadowDecoration.Color;
            set => _ctrl.ShadowDecoration.Color = value;
        }

        // -----------------------------------------------------------
        // Border radius of the combo box
        // (Read-only — cannot be modified from the engine)
        // -----------------------------------------------------------
        public int BorderRadius => _ctrl.BorderRadius;

        // -----------------------------------------------------------
        // Dock passthrough (very important for shadow switching)
        // -----------------------------------------------------------
        public DockStyle Dock
        {
            get => _ctrl.Dock;
            set => _ctrl.Dock = value;
        }
    }
}

using FastUI.FastUILibrary.Core.Interfaces;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.FastUILibrary.Core.Shadow.Adapters
{
    public class GunaShadowButtonAdapter : IFastShadowTarget
    {
        private readonly Guna.UI2.WinForms.Guna2Button _ctrl;

        public GunaShadowButtonAdapter(Guna2Button ctrl)
        {
            _ctrl = ctrl;

            // reset default shadow to avoid conflicts
            ctrl.ShadowDecoration.Shadow = new Padding(0);
            ctrl.ShadowDecoration.Depth = 5;
            ctrl.ShadowDecoration.BorderRadius = ctrl.BorderRadius;
        }

        public Size Size
        {
            get => _ctrl.Size;
            set => _ctrl.Size = value;
        }

        public Point Location
        {
            get => _ctrl.Location;
            set => _ctrl.Location = value;
        }

        public Padding ShadowPadding
        {
            get => _ctrl.ShadowDecoration.Shadow;
            set => _ctrl.ShadowDecoration.Shadow = value;
        }

        public bool ShadowEnabled
        {
            get => _ctrl.ShadowDecoration.Enabled;
            set => _ctrl.ShadowDecoration.Enabled = value;
        }

        public int ShadowBlur
        {
            get => _ctrl.ShadowDecoration.Depth;
            set => _ctrl.ShadowDecoration.Depth = value;
        }

        public Color ShadowColor
        {
            get => _ctrl.ShadowDecoration.Color;
            set => _ctrl.ShadowDecoration.Color = value;
        }

        public int BorderRadius => _ctrl.BorderRadius;

        public DockStyle Dock
        {
            get => _ctrl.Dock;
            set => _ctrl.Dock = value;
        }
    }

}

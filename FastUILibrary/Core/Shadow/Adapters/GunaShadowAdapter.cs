using FastUI.FastUILibrary.Core.Interfaces;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.FastUILibrary.Core.Shadow.Adapters
{
    public class GunaShadowAdapter : IFastShadowTarget
    {
        private readonly Guna.UI2.WinForms.Guna2TextBox _ctrl;

        public GunaShadowAdapter(Guna2TextBox ctrl)
        {
            _ctrl = ctrl;

            // Reset Guna default shadow values to zero
            ctrl.ShadowDecoration.Shadow = new Padding(0);

            // Reset Guna default shadow depth
            ctrl.ShadowDecoration.Depth = 5;

            // Always match shadow radius to control radius
            ctrl.ShadowDecoration.BorderRadius = ctrl.BorderRadius;

            _ctrl.SizeChanged += Ctrl_SizeChanged;
        }

        private void Ctrl_SizeChanged(object? sender, EventArgs e)
        {
            if (SizeChanged != null)
                SizeChanged.Invoke(this, e);
        }
        public event EventHandler SizeChanged;

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
    }

}

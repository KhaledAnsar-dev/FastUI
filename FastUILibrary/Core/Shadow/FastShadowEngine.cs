using FastUI.FastUILibrary.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.FastUILibrary.Core.Shadow
{
    public class FastShadowEngine
    {
        private readonly UserControl _container;
        private readonly IFastShadowTarget _inner;
        public readonly FastShadowSettings _settings;

        private bool _internalResize = false;

        public FastShadowEngine(UserControl container, IFastShadowTarget innerShadow)
        {
            _container = container;
            _inner = innerShadow;

            _settings = new FastShadowSettings(innerShadow.Size);

            _container.SizeChanged += Container_SizeChanged;
        }

        private void Container_SizeChanged(object sender, EventArgs e)
        {
            if (_internalResize)
                return;

            _settings.OriginalSize = _container.Size;

            if (!_inner.ShadowEnabled)
                 _inner.Size = _container.Size;
            else
                _inner.Size = new Size(
                    _container.Width - _settings.Left - _settings.Right,
                    _container.Height - _settings.Top - _settings.Bottom
                );
        }

        public void Apply()
        {
            _inner.Size = _settings.OriginalSize;

            _inner.ShadowPadding = new Padding(
                _settings.Left, _settings.Top, _settings.Right, _settings.Bottom
            );


            _inner.Location = new Point(_settings.Left, _settings.Top);

            _internalResize = true;

            _container.Size = new Size(
                _settings.OriginalSize.Width + _settings.Left + _settings.Right,
                _settings.OriginalSize.Height + _settings.Top + _settings.Bottom
            );

            _internalResize = false;


            _container.Invalidate();
        }
        public void Disable()
        {
            // remove shadow padding
            _inner.ShadowPadding = new Padding(0);

            // return inner control to (0,0)
            _inner.Location = new Point(0, 0);

            _settings.Top = 0;
            _settings.Bottom = 0;
            _settings.Left = 0;
            _settings.Right = 0;

            // reset size to original
            _internalResize = true;
            _container.Size = _inner.Size;
            _internalResize = false;

            _container.Invalidate();
        }
        public void SetOriginalWidth(int w)
        {
            _settings.OriginalSize = new Size(w, _settings.OriginalSize.Height);
        }

        public void SetOriginalHeight(int h)
        {
            _settings.OriginalSize = new Size(_settings.OriginalSize.Width, h);
        }

        public void SetTop(int v) { _settings.Top = v; Apply(); }
        public void SetBottom(int v) { _settings.Bottom = v; Apply(); }
        public void SetLeft(int v) { _settings.Left = v; Apply(); }
        public void SetRight(int v) { _settings.Right = v; Apply(); }
    }

}

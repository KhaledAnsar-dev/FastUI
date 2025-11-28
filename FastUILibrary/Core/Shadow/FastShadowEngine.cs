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
        // ---------------------------------------------------------------------
        //  Fields
        // ---------------------------------------------------------------------

        // The outer UserControl that visually contains the inner control.
        // The shadow engine adjusts this container’s size and layout.
        private readonly UserControl _container;

        // The wrapped control that actually displays the shadow.
        // This must implement IFastShadowTarget to expose needed shadow-related APIs.
        private readonly IFastShadowTarget _innerControl;

        // Settings that define the shadow padding (Top/Bottom/Left/Right).
        // Public so external components can adjust it.
        public readonly FastShadowSettings _settings;

        // Flag used to prevent recursive events when engine adjusts container size.
        // (Because changing container size triggers SizeChanged again).
        private bool _internalResize = false;

        // ---------------------------------------------------------------------
        //  Constructor
        // ---------------------------------------------------------------------
        public FastShadowEngine(UserControl container, IFastShadowTarget innerShadow)
        {
            _container = container;
            _innerControl = innerShadow;

            // Initialize default padding settings
            _settings = new FastShadowSettings();

            // Listen for container resizing to update inner control dynamically
            _container.SizeChanged += Container_SizeChanged;
        }

        // ---------------------------------------------------------------------
        //  Automatically adjust inner control size when container is resized
        // ---------------------------------------------------------------------
        private void Container_SizeChanged(object sender, EventArgs e)
        {
            // Prevent re-entry if the resize was triggered internally by Apply()
            if (_internalResize)
                return;

            // Update the inner control only if shadow is enabled
            if (_innerControl.ShadowEnabled)
            {
                // Recalculate inner control size based on shadow padding margins
                _innerControl.Size = new Size(
                    _container.Width - _settings.Left - _settings.Right,
                    _container.Height - _settings.Top - _settings.Bottom
                );
            }
        }


        /// <summary>
        /// Updates the layout to apply the current shadow padding settings.
        /// </summary>
        public void Apply()
        {
            // Apply shadow padding to the inner control
            _innerControl.ShadowPadding = new Padding(
                _settings.Left, _settings.Top, _settings.Right, _settings.Bottom
            );

            // Position the inner control inside the shadow margin box
            _innerControl.Location = new Point(_settings.Left, _settings.Top);

            // Prevent recursive SizeChanged events while adjusting container size
            _internalResize = true;

            // Resize container so it matches (inner control size + padding)
            _container.Size = new Size(
                _innerControl.Size.Width + _settings.Left + _settings.Right,
                _innerControl.Size.Height + _settings.Top + _settings.Bottom
            );

            // Allow future resize events again
            _internalResize = false;

            // Refresh the layout
            _container.Invalidate();
        }

        /// <summary>
        /// Removes shadow padding and restores the original layout.
        /// </summary>
        public void Disable()
        {
            // Remove shadow padding around inner control
            _innerControl.ShadowPadding = new Padding(0);

            // Reset control position to the top-left corner
            _innerControl.Location = new Point(0, 0);

            // Reset all padding values in settings
            _settings.Top = 0;
            _settings.Bottom = 0;
            _settings.Left = 0;
            _settings.Right = 0;

            // Resize container to match inner control strictly (no padding)
            _internalResize = true;
            _container.Size = _innerControl.Size;
            _internalResize = false;

            // Redraw
            _container.Invalidate();
        }

        // ---------------------------------------------------------------------
        //  Helper methods to update each padding side and auto-Apply
        // ---------------------------------------------------------------------

        /// <summary>
        /// Removes shadow padding and restores the original layout.
        /// </summary>
        public void SetTop(int v) { _settings.Top = v; Apply(); }

        /// <summary>
        /// Applies a new bottom shadow padding value.
        /// </summary>
        public void SetBottom(int v) { _settings.Bottom = v; Apply(); }

        /// <summary>
        /// Applies a new left shadow padding value.
        /// </summary>
        public void SetLeft(int v) { _settings.Left = v; Apply(); }

        /// <summary>
        /// Applies a new right shadow padding value.
        /// </summary>
        public void SetRight(int v) { _settings.Right = v; Apply(); }
    }

}

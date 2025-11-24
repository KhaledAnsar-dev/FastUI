using FastUI.Styles.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.Modules.Buttons.FastButtonUI.Support
{
    public static class FastUtilsButton
    {
        public static void ChangeStyle(FastButton button)
        {
            FastStyles.Windows11.Apply(button);
        }
    }
}

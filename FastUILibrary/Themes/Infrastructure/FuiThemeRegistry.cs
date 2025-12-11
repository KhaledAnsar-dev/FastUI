using FastUI.FastUILibrary.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.FastUILibrary.Themes.Infrastructure
{
    public static class FuiThemeRegistry
    {
     
        private static Dictionary<string, IFuiTheme> _themes = new();

        public static void Register(string name, IFuiTheme theme)
            => _themes[name] = theme;

        public static IFuiTheme Get(string name)
            => _themes.TryGetValue(name, out var t) ? t : null;

        public static string[] GetNames()
            => _themes.Keys.ToArray();
    }

}

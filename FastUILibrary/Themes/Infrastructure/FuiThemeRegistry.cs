using FastUI.FastUILibrary.Themes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.FastUILibrary.Themes.Infrastructure
{
    /// <summary>
    /// Central registry responsible for storing and retrieving
    /// FastUI themes by name.
    /// 
    /// This registry acts as a global theme container that allows
    /// the framework to resolve themes dynamically at runtime.
    /// </summary>
    public static class FuiThemeRegistry
    {
        /// <summary>
        /// Internal storage for all registered themes.
        /// The key represents the theme name, while the value
        /// represents the theme implementation.
        /// </summary>
        private static Dictionary<string, IFuiTheme> _themes = new();

        /// <summary>
        /// Registers or replaces a theme in the registry.
        /// If a theme with the same name already exists,
        /// it will be overwritten.
        /// </summary>
        /// <param name="name">Unique name of the theme.</param>
        /// <param name="theme">Theme implementation instance.</param>
        public static void Register(string name, IFuiTheme theme)
            => _themes[name] = theme;

        /// <summary>
        /// Retrieves a theme by its registered name.
        /// Returns null if the theme does not exist.
        /// </summary>
        /// <param name="name">Name of the requested theme.</param>
        /// <returns>The corresponding theme instance or null.</returns>
        public static IFuiTheme Get(string name)
            => _themes.TryGetValue(name, out var t) ? t : null;

        /// <summary>
        /// Returns a list of all registered theme names.
        /// </summary>
        /// <returns>An array containing theme identifiers.</returns>
        public static string[] GetNames()
            => _themes.Keys.ToArray();
    }

}

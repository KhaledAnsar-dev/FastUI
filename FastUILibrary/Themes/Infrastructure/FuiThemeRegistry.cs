using FastUI.FastUILibrary.Themes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private static readonly Dictionary<string, IFuiTheme> _themes = new();

        /// <summary>
        /// Indicates whether built-in themes have already been loaded.
        /// This prevents multiple executions of theme registration.
        /// </summary>
        private static bool _initialized;

        /// <summary>
        /// Ensures that built-in themes are registered exactly once.
        /// This method forces ThemeRegistration to be loaded by the CLR.
        /// </summary>
        private static void EnsureInitialized()
        {
            if (_initialized)
                return;

            _initialized = true;

            // Force CLR to execute ThemeRegistration static constructor
            ThemeRegistration.EnsureLoaded();
        }

        /// <summary>
        /// Registers or replaces a theme in the registry.
        /// If a theme with the same name already exists,
        /// it will be overwritten.
        /// </summary>
        /// <param name="name">Unique name of the theme.</param>
        /// <param name="theme">Theme implementation instance.</param>
        public static void Register(string name, IFuiTheme theme)
        {
            EnsureInitialized();
            _themes[name] = theme;
        }

        /// <summary>
        /// Retrieves a theme by its registered name.
        /// Returns null if the theme does not exist.
        /// </summary>
        /// <param name="name">Name of the requested theme.</param>
        /// <returns>The corresponding theme instance or null.</returns>
        public static IFuiTheme Get(string name)
        {
            EnsureInitialized();
            return _themes.TryGetValue(name, out var t) ? t : null;
        }

        /// <summary>
        /// Returns a list of all registered theme names.
        /// Used mainly by the designer and property editors.
        /// </summary>
        /// <returns>An array containing theme identifiers.</returns>
        public static string[] GetNames()
        {
            EnsureInitialized();
            return _themes.Keys.ToArray();
        }
    }
}

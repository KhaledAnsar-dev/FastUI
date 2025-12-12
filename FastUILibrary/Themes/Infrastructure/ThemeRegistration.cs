using FastUI.FastUILibrary.Themes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastUI.FastUILibrary.Themes.BuiltIn;

namespace FastUI.FastUILibrary.Themes.Infrastructure
{
    /// <summary>
    /// Handles automatic registration of built-in FastUI themes.
    /// This class ensures that all predefined themes are registered
    /// in the global theme registry at application startup.
    /// </summary>
    static class ThemeRegistration
    {
        /// <summary>
        /// Static constructor responsible for registering
        /// all built-in and predefined themes.
        /// This constructor is executed once when the class
        /// is first loaded by the CLR.
        /// </summary>
        static ThemeRegistration()
        {
            // Collection of available themes mapped by their unique names
            var themes = new Dictionary<string, IFuiTheme>
            {
                ["Windows11"] = new Windows11Theme(),
                ["GoogleMaterial"] = new GoogleMaterialTheme(),
                ["Apple"] = new AppleTheme(),
                ["Mayora"] = new MayoraTheme(),

                // Register custom themes here by adding them to this dictionary
                // >>> ["YourThemeName1"] = new YourThemeClass1(), <<<
                // >>> ["YourThemeName2"] = new YourThemeClass2(), <<<
                // >>> ["YourThemeName3"] = new YourThemeClass3(), <<<
                // >>> ["YourThemeName4"] = new YourThemeClass4(), <<<
                // >>> ["YourThemeName5"] = new YourThemeClass5(), <<<

            };

            // Register each theme in the global theme registry
            foreach (var kv in themes)
                FuiThemeRegistry.Register(kv.Key, kv.Value);
        }

        /// <summary>
        /// Forces the CLR to load this class and execute
        /// its static constructor.
        /// This method is intentionally empty and exists
        /// only to trigger theme registration.
        /// </summary>
        public static void EnsureLoaded() { }

    }

}

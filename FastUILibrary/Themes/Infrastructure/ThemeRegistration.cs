using FastUI.FastUILibrary.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastUI.FastUILibrary.Themes.BuiltIn;

namespace FastUI.FastUILibrary.Themes.Infrastructure
{
    static class ThemeRegistration
    {
        static ThemeRegistration()
        {
            var themes = new Dictionary<string, IFuiTheme>
            {
                ["Windows11"] = new Windows11Theme(),
                ["GoogleMaterial"] = new GoogleMaterialTheme(),
                ["Apple"] = new AppleTheme(),
                ["Mayora"] = new MayoraTheme(),

                // Register your custom themes here
                // >>> ["YourThemeName1"] = new YourThemeClass1(), <<<
                // >>> ["YourThemeName2"] = new YourThemeClass2(), <<<
                // >>> ["YourThemeName3"] = new YourThemeClass3(), <<<
                // >>> ["YourThemeName4"] = new YourThemeClass4(), <<<
                // >>> ["YourThemeName5"] = new YourThemeClass5(), <<<

            };

            foreach (var kv in themes)
                FuiThemeRegistry.Register(kv.Key, kv.Value);
        }
        public static void EnsureLoaded() { /* يجبر CLR على تحميل الكلاس */ }

    }

}

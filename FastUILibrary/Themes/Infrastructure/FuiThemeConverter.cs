using System.ComponentModel;

namespace FastUI.FastUILibrary.Themes.Infrastructure
{
    public class FuiThemeConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            => true;

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            => true;

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            // 🔥 أجبر التحميل الآن!
            ThemeRegistration.EnsureLoaded();

            var names = FuiThemeRegistry.GetNames();
            return new StandardValuesCollection(names);
        }
    }


}

using System.ComponentModel;

namespace FastUI.FastUILibrary.Themes.Infrastructure
{
    /// <summary>
    /// Provides a design-time string converter for FastUI themes.
    /// 
    /// This converter enables a dropdown list of available themes
    /// inside the Visual Studio designer by querying the theme registry.
    /// </summary>
    public class FuiThemeConverter : StringConverter
    {
        /// <summary>
        /// Indicates whether this converter supports a standard set of values.
        /// Returning true enables a predefined list of selectable options.
        /// </summary>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            => true;

        /// <summary>
        /// Indicates whether the standard values are exclusive.
        /// Returning true restricts input to the provided list only.
        /// </summary>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            => true;

        /// <summary>
        /// Retrieves the list of available theme names to be displayed
        /// in the designer dropdown.
        /// 
        /// This method ensures that all built-in and custom themes
        /// are registered before querying the registry.
        /// </summary>
        /// <param name="context">Type descriptor context.</param>
        /// <returns>A collection of registered theme names.</returns>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            // Force static theme registration to execute
            ThemeRegistration.EnsureLoaded();

            var names = FuiThemeRegistry.GetNames();
            return new StandardValuesCollection(names);
        }
    }
}

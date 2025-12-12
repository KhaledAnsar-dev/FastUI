using FastUI.FastUILibrary.Themes.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.FastUILibrary.Themes.Interfaces
{
    /// <summary>
    /// Defines the contract for a FastUI theme.
    /// 
    /// A theme is responsible for providing visual presets
    /// for all supported FastUI components.
    /// 
    /// Each implementation represents a complete design system
    /// (e.g., Windows 11, Google Material, Apple-style UI).
    /// </summary>
    public interface IFuiTheme
    {
        /// <summary>
        /// Returns the visual preset used to style FastUI buttons.
        /// </summary>
        /// <returns>A <see cref="ButtonPreset"/> instance.</returns>
        ButtonPreset GetButtonPreset();

        /// <summary>
        /// Returns the visual preset used to style FastUI text boxes.
        /// </summary>
        /// <returns>A <see cref="TextBoxPreset"/> instance.</returns>
        TextBoxPreset GetTextBoxPreset();

        /// <summary>
        /// Returns the visual preset used to style FastUI combo boxes.
        /// </summary>
        /// <returns>A <see cref="ComboBoxPreset"/> instance.</returns>
        ComboBoxPreset GetComboBoxPreset();

        /// <summary>
        /// Returns the visual preset used to style FastUI tables.
        /// </summary>
        /// <returns>A <see cref="TablePreset"/> instance.</returns>
        TablePreset GetTablePreset();
    }
}


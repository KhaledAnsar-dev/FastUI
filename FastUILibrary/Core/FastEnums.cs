using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.FastUILibrary.Core
{
    public enum FastEnumPosition
    {
        Left,
        Center,
        Right
    }
    public enum FastEnumInputType
    {
        Text,
        Email,
        PhoneDZ,
        Integer,
        Decimal
    }
    public enum FastEnumStyle { normal, Windows11 };

    /// <summary>
    /// Determines whether the combo box should allow a "None" option.
    /// </summary>
    public enum FastNoneMode
    {
        Allowed,
        NotAllowed
    }
}

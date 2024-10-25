using System;
using System.Collections.Generic;
using System.IO;

namespace EntryTranslator.Const;

public static class GlobalSettings
{
    public static readonly string[] SpecialColNames =
    {
        Properties.Resources.ColNameError,
        Properties.Resources.ColNameKey,
        Properties.Resources.ColNameTranslated
    };

    public static readonly List<string> FilterColNames = new List<string>
    {
        Properties.Resources.ColNameTranslated,
        Properties.Resources.ColNameError,
    };

    public static readonly string LangDicPath = 
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LangDic", "SofarEntryDic.csv");

}

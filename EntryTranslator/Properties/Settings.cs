﻿using EntryTranslator.Utils;

namespace EntryTranslator.Properties
{
    internal sealed partial class Settings
    {
        static Settings()
        {
            Binder = new SettingBinder<Settings>(Default);
        }

        public static SettingBinder<Settings> Binder { get; }
    }
}
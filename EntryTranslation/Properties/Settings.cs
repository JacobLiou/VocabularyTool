﻿using EntryTranslation.Helpers;

namespace EntryTranslation.Properties
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
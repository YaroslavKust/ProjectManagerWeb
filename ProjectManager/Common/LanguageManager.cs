using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;

namespace ProjectManager.UI
{
    public static class LanguageManager
    {
        private const string _path = "Sources/Languages/lang.";

        private static List<CultureInfo> _languages = new List<CultureInfo>();

        static LanguageManager()
        {
            LanguageManager.Languages.Add(new CultureInfo("en-US"));
            LanguageManager.Languages.Add(new CultureInfo("ru-RU"));
        }

        public static List<CultureInfo> Languages => _languages;

        public static CultureInfo Language
        {
            get => Thread.CurrentThread.CurrentUICulture;

            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));

                Properties.Settings.Default.Language = value.Name;
                Properties.Settings.Default.Save();
            }
        }
    }
}
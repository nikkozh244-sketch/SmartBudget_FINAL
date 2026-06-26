namespace SmartBudget.ClassLibrary
{
    /// <summary>
    /// Статический класс для работы с режимом собачника
    /// </summary>
    public static class ThemeManager
    {
        private static SettingsService GetSettings()
        {
            return SettingsService.LoadSettings();
        }

        /// <summary>
        /// Свойство на проверку, включена ли тема собачника
        /// </summary>
        public static bool IsDogTheme
        {
            get
            {
                SettingsService settings = GetSettings();
                return settings != null && settings.IsDogTheme;
            }
        }

        public static void ReloadSettings()
        {
            // Просто перезагружаем - никакого кэширования
            GetSettings();
        }

        //Свойства для звук и фраз

        public static string Sound
        {
            get { return IsDogTheme ? "Гаф" : "Мяу"; }
        }

        public static string SoundAlt
        {
            get { return IsDogTheme ? "Ррраф" : "Мур"; }
        }

        public static string SoundHappy
        {
            get { return IsDogTheme ? "Ррраф-ррраф!" : "Муррр!"; }
        }

        public static string SoundSad
        {
            get { return IsDogTheme ? "Гав-гав..." : "Мяу..."; }
        }

        public static string SoundQuestion
        {
            get { return IsDogTheme ? "Гав?" : "Мяу?"; }
        }

        public static string SoundWelcome
        {
            get { return IsDogTheme ? "Ррраф! Добро пожаловать" : "Мур-р-р! Добро пожаловать"; }
        }

        public static string SoundError
        {
            get { return IsDogTheme ? "Гав! Ошибка" : "Мяу! Ошибка"; }
        }

        public static string SoundSuccess
        {
            get { return IsDogTheme ? "Ррраф!" : "Мур!"; }
        }

        public static string SoundAttention
        {
            get { return IsDogTheme ? "Гав! Внимание!" : "Мяу! Внимание!"; }
        }

        //Свойства для замены изображений

        public static string HelperImage
        {
            get { return IsDogTheme ? "pictureDogHelper" : "pictureCatHelper"; }
        }

        public static string HelperSmallerImage
        {
            get { return IsDogTheme ? "pictureDogHelperSmaller" : "pictureCatHelperSmaller"; }
        }
    }
}
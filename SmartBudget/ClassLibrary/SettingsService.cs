using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace SmartBudget.ClassLibrary
{
    internal class SettingsService
    {
        // Поля класса
        private string _language;
        private readonly string _path;
        private bool _isDark;
        private bool _isLeftHanded;
        private bool _isDogTheme;
        private float _dollarValue;

        // Свойства класса
        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }

        public bool IsDark
        {
            get { return _isDark; }
            set { _isDark = value; }
        }

        public bool IsLeftHanded
        {
            get { return _isLeftHanded; }
            set { _isLeftHanded = value; }
        }

        public bool IsDogTheme
        {
            get { return _isDogTheme; }
            set { _isDogTheme = value; }
        }

        public float DollarValue
        {
            get { return _dollarValue; }
            set
            {
                if (value < 0)
                    throw new Exception("Ошибка! Курс доллара не может быть отрицательным");
                if (value == 0)
                    throw new Exception("Ошибка! Курс доллара не может равняться нулю");
                if (value > 121)
                    throw new Exception("Ошибка! Курс доллара превышает исторический максимум!");
                _dollarValue = value;
            }
        }

        //Конструктор без параметров
        public SettingsService()
        {
            Language = "Русский";
            IsDark = false;
            IsLeftHanded = false;
            IsDogTheme = false;
            DollarValue = 80;
        }

        //Конструктор с параметрами
        public SettingsService(string language, bool isDark, bool isLeftHanded, bool isDogTheme, float dollarValue)
        {
            Language = language;
            IsDark = isDark;
            IsLeftHanded = isLeftHanded;
            IsDogTheme = isDogTheme;
            DollarValue = dollarValue;
        }

        /// <summary>
        /// Метод для выгрузки настроек из файла
        /// </summary>
        static public SettingsService LoadSettings()
        {
            try
            {
                string exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (string.IsNullOrEmpty(exeDirectory))
                {
                    exeDirectory = Directory.GetCurrentDirectory();
                }

                string path = Path.Combine(exeDirectory, "settings.json");

                if (!File.Exists(path))
                {
                    SettingsService defaultSettings = new SettingsService();
                    SaveSettings(defaultSettings);
                    return defaultSettings;
                }

                string serializedSettings = File.ReadAllText(path, Encoding.UTF8);

                if (string.IsNullOrWhiteSpace(serializedSettings))
                {
                    SettingsService defaultSettings = new SettingsService();
                    SaveSettings(defaultSettings);
                    return defaultSettings;
                }

                SettingsService settings = JsonConvert.DeserializeObject<SettingsService>(serializedSettings);

                if (settings == null)
                {
                    settings = new SettingsService();
                    SaveSettings(settings);
                }

                return settings;
            }
            catch
            {
                return new SettingsService();
            }
        }

        /// <summary>
        /// Метод по сохранению настроек в JSON файл 
        /// </summary>
        /// <param name="settings">Сохраняемые настройки</param>
        /// <returns></returns>
        static public bool SaveSettings(SettingsService settings)
        {
            try
            {
                if (settings == null)
                {
                    throw new ArgumentNullException(nameof(settings), "Объект настроек не может быть null");
                }

                string exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (string.IsNullOrEmpty(exeDirectory))
                {
                    exeDirectory = Directory.GetCurrentDirectory();
                }

                string path = Path.Combine(exeDirectory, "settings.json");

                JsonSerializerSettings jsonSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };

                string serializedSettings = JsonConvert.SerializeObject(settings, jsonSettings);
                File.WriteAllText(path, serializedSettings, Encoding.UTF8);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Метод, позволяющий сбросить настройки до тех, что стоят по умолчанию 
        /// </summary>
        public void ResetSettings()
        {
            this.Language = "Русский";
            this.IsDark = false;
            this.IsLeftHanded = false;
            this.IsDogTheme = false;
            this.DollarValue = 1;
        }
    }
}
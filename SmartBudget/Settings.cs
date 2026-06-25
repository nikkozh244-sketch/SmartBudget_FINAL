using SmartBudget.ClassLibrary;

namespace SmartBudget
{
    public partial class Settings : UserControl
    {
        // Поля
        private string _originalLabel;
        private System.Windows.Forms.Timer _messageTimer;
        private SettingsService _currentSettings;

        // События экрана
        public event EventHandler NavigateToHome;

        /// <summary>
        /// Инициализация экрана 
        /// </summary>
        public Settings()
        {
            InitializeComponent();

            // Сохраняем оригинальный текст
            _originalLabel = LabelSettings.Text;

            // Инициализация таймера для временных сообщений
            _messageTimer = new System.Windows.Forms.Timer();
            _messageTimer.Interval = 2000; // 2 секунды
            _messageTimer.Tick += MessageTimer_Tick;

            // Загружаем текущие настройки
            LoadCurrentSettings();

            // Подписываемся на события кнопок меню
            ButtonReturnToHome.Click += ButtonReturnToHome_Click;
            ButtonContinueWork.Click += ButtonContinueWork_Click;
        }

        /// <summary>
        /// Загружает текущие настройки в элементы управления
        /// </summary>
        private void LoadCurrentSettings()
        {
            _currentSettings = SettingsService.LoadSettings();

            if (_currentSettings == null)
                return;

            // Язык
            ComboBoxChooseLanguage.SelectedItem = _currentSettings.Language;

            // Тёмная тема
            CheckDarkTheme.Checked = _currentSettings.IsDark;

            // Режим левши
            CheckLeftHanded.Checked = _currentSettings.IsLeftHanded;

            // Режим собачника
            CheckDogMode.Checked = _currentSettings.IsDogTheme;

            // Курс доллара
            NumericDollarChoose.Value = (decimal)_currentSettings.DollarValue;
        }

        /// <summary>
        /// Собирает настройки из элементов управления
        /// </summary>
        private SettingsService CollectSettingsFromUI()
        {
            string language = ComboBoxChooseLanguage.SelectedItem?.ToString() ?? "Русский";
            bool isDark = CheckDarkTheme.Checked;
            bool isLeftHanded = CheckLeftHanded.Checked;
            bool isDogTheme = CheckDogMode.Checked;
            float dollarValue = (float)NumericDollarChoose.Value;

            return new SettingsService(language, isDark, isLeftHanded, isDogTheme, dollarValue);
        }

        /// <summary>
        /// Принимаемые настройки в функциональные элементы
        /// </summary>
        /// <param name="settings">Настройки</param>
        private void ApplySettingsToUI(SettingsService settings)
        {
            if (settings == null)
                return;

            ComboBoxChooseLanguage.SelectedItem = settings.Language;
            CheckDarkTheme.Checked = settings.IsDark;
            CheckLeftHanded.Checked = settings.IsLeftHanded;
            CheckDogMode.Checked = settings.IsDogTheme;
            NumericDollarChoose.Value = (decimal)settings.DollarValue;
        }

        /// <summary>
        /// Показать временное сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        private void ShowTemporaryMessage(string message)
        {
            _messageTimer.Stop();
            LabelSettings.Text = message;
            _messageTimer.Start();
        }

        /// <summary>
        /// Восстановление исходного текста после таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageTimer_Tick(object sender, EventArgs e)
        {
            _messageTimer.Stop();
            LabelSettings.Text = _originalLabel;
        }

        /// <summary>
        /// Метод для показа меню
        /// </summary>
        private void ShowMenu()
        {
            pnlMenu.BackColor = Color.White;
            pnlMenu.Visible = true;
        }

        /// <summary>
        /// Метод, чтобы спрятать меню
        /// </summary>
        private void HideMenu()
        {
            pnlMenu.Visible = false;
        }

        /// <summary>
        /// Применение настроек
        /// </summary>
        private void ButtonApplySettings_Click(object sender, EventArgs e)
        {
            try
            {
                // Собираем настройки из UI
                SettingsService newSettings = CollectSettingsFromUI();

                // Сохраняем в JSON файл
                bool saved = SettingsService.SaveSettings(newSettings);

                if (saved)
                {
                    // Обновляем текущие настройки
                    _currentSettings = newSettings;
                    ShowTemporaryMessage("Мур! Настройки успешно сохранены!");
                }

                else
                {
                    ShowTemporaryMessage("Мяу! Ошибка при сохранении настроек!");
                }
            }
            catch (Exception ex)
            {
                ShowTemporaryMessage($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Сброс настроек до базовых
        /// </summary>
        private void ButtonResetSettings_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Мур... Вы уверены, что хотите сбросить все настройки до базовых?",
                "Подтверждение сброса",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Создаем настройки по умолчанию
                    SettingsService defaultSettings = new SettingsService();

                    // Применяем к UI
                    ApplySettingsToUI(defaultSettings);

                    // Сохраняем в файл
                    bool saved = SettingsService.SaveSettings(defaultSettings);

                    if (saved)
                    {
                        _currentSettings = defaultSettings;
                        ShowTemporaryMessage("Мур! Настройки сброшены до изначальных!");
                    }
                    else
                    {
                        ShowTemporaryMessage("Мяу! Ошибка при сбросе настроек!");
                    }
                }
                catch (Exception ex)
                {
                    ShowTemporaryMessage($"Ошибка: {ex.Message}");
                }
            }
        }

        // Кнопки меню
        private void ButtonReturnToHome_Click(object sender, EventArgs e)
        {
            NavigateToHome?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonContinueWork_Click(object sender, EventArgs e)
        {
            NavigateToHome?.Invoke(this, EventArgs.Empty);
        }

        private void IconOpenMenu_Click(object sender, EventArgs e)
        {
            NavigateToHome?.Invoke(this, EventArgs.Empty);
        }
    }
}
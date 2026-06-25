using SmartBudget.ClassLibrary;

namespace SmartBudget
{
    public partial class Settings : UserControl
    {
        private string _originalLabel;
        private System.Windows.Forms.Timer _messageTimer;
        private SettingsService _currentSettings;
        private bool _settingsChanged = false;
        private bool _isWelcomeMessage = true;

        public event EventHandler ThemeChanged;
        public event EventHandler NavigateToHome;

        public Settings()
        {
            InitializeComponent();
            _originalLabel = LabelSettings.Text;
            _isWelcomeMessage = true;

            _messageTimer = new System.Windows.Forms.Timer();
            _messageTimer.Interval = 2000;
            _messageTimer.Tick += MessageTimer_Tick;

            ButtonReturnToHome.Click += ButtonReturnToHome_Click;
            ButtonContinueWork.Click += ButtonContinueWork_Click;

            ComboBoxChooseLanguage.SelectedIndexChanged += OnSettingsChanged;
            CheckDogMode.CheckedChanged += OnSettingsChanged;
            NumericDollarChoose.ValueChanged += OnSettingsChanged;

            LoadCurrentSettings();
            ApplyTheme();
            ApplyLocalization();
        }

        public void ApplyLocalization()
        {
            LabelChangeDollar.Text = LocalizationManager.GetString("Settings_ChangeDollar");
            LabelDollarDescriprtion.Text = LocalizationManager.GetString("Settings_DollarDescription");
            LabelChangeLanguage.Text = LocalizationManager.GetString("Settings_ChangeLanguage");
            LabelLanguageDescription.Text = LocalizationManager.GetString("Settings_LanguageDescription");
            LabelDogMode.Text = LocalizationManager.GetString("Settings_DogMode");
            LabelDogModeDescription.Text = LocalizationManager.GetString("Settings_DogModeDescription");
            ButtonApplySettings.Text = LocalizationManager.GetString("Settings_Apply");
            ButtonResetSettings.Text = LocalizationManager.GetString("Settings_Reset");
            label6.Text = LocalizationManager.GetString("Settings_Menu");
            ButtonReturnToHome.Text = LocalizationManager.GetString("Settings_ReturnToHome");

            // Обновляем выпадающий список языков
            string selectedLanguage = ComboBoxChooseLanguage.SelectedItem?.ToString();
            ComboBoxChooseLanguage.Items.Clear();
            ComboBoxChooseLanguage.Items.Add(LocalizationManager.GetString("Settings_Language_Russian"));
            ComboBoxChooseLanguage.Items.Add(LocalizationManager.GetString("Settings_Language_English"));

            // Восстанавливаем выбранный язык
            string currentLang = LocalizationManager.GetCurrentLanguage();
            if (currentLang == "Русский")
                ComboBoxChooseLanguage.SelectedItem = LocalizationManager.GetString("Settings_Language_Russian");
            else if (currentLang == "English")
                ComboBoxChooseLanguage.SelectedItem = LocalizationManager.GetString("Settings_Language_English");

            // Обновляем чекбокс
            CheckDogMode.Text = LocalizationManager.GetString("Settings_On");

            ApplyTheme();
        }

        public void RefreshSettings()
        {
            LoadCurrentSettings();
            ApplyTheme();
            ApplyLocalization();
        }

        public void ApplyTheme()
        {
            ThemeManager.ReloadSettings();

            if (ThemeManager.IsDogTheme)
            {
                PictureCat.Image = Properties.Resources.pictureDogHelperSmaller;
            }
            else
            {
                PictureCat.Image = Properties.Resources.pictureCatHelperSmaller;
            }

            if (_isWelcomeMessage)
            {
                UpdateWelcomeMessage();
            }
        }

        private void UpdateWelcomeMessage()
        {
            if (ThemeManager.IsDogTheme)
            {
                LabelSettings.Text = "Ррраф! Добро пожаловать в меню настроек! Здесь вы можете настроить приложение специально под себя!\r\n\r\n";
            }
            else
            {
                LabelSettings.Text = "Добро пожаловать в меню настроек, мяу! Здесь вы можете настроить приложение специально под себя!\r\n\r\n";
            }
            _originalLabel = LabelSettings.Text;
            _isWelcomeMessage = true;
        }

        private void LoadCurrentSettings()
        {
            _currentSettings = SettingsService.LoadSettings();

            if (_currentSettings == null)
                return;

            ComboBoxChooseLanguage.SelectedIndexChanged -= OnSettingsChanged;
            CheckDogMode.CheckedChanged -= OnSettingsChanged;
            NumericDollarChoose.ValueChanged -= OnSettingsChanged;

            // Язык
            string lang = _currentSettings.Language;
            if (lang == "Русский")
                ComboBoxChooseLanguage.SelectedItem = LocalizationManager.GetString("Settings_Language_Russian");
            else if (lang == "English")
                ComboBoxChooseLanguage.SelectedItem = LocalizationManager.GetString("Settings_Language_English");
            else
                ComboBoxChooseLanguage.SelectedItem = LocalizationManager.GetString("Settings_Language_Russian");

            // Режим собачника
            CheckDogMode.Checked = _currentSettings.IsDogTheme;

            // Курс доллара
            NumericDollarChoose.Value = (decimal)_currentSettings.DollarValue;

            ComboBoxChooseLanguage.SelectedIndexChanged += OnSettingsChanged;
            CheckDogMode.CheckedChanged += OnSettingsChanged;
            NumericDollarChoose.ValueChanged += OnSettingsChanged;

            _settingsChanged = false;
        }

        private SettingsService CollectSettingsFromUI()
        {
            string language = ComboBoxChooseLanguage.SelectedItem?.ToString() ?? "Русский";

            // Преобразуем название языка обратно в ключ
            if (language == LocalizationManager.GetString("Settings_Language_Russian"))
                language = "Русский";
            else if (language == LocalizationManager.GetString("Settings_Language_English"))
                language = "English";

            bool isDogTheme = CheckDogMode.Checked;
            float dollarValue = (float)NumericDollarChoose.Value;

            return new SettingsService(language, isDogTheme, dollarValue);
        }

        private void ApplySettingsToUI(SettingsService settings)
        {
            if (settings == null)
                return;

            ComboBoxChooseLanguage.SelectedIndexChanged -= OnSettingsChanged;
            CheckDogMode.CheckedChanged -= OnSettingsChanged;
            NumericDollarChoose.ValueChanged -= OnSettingsChanged;

            // Язык
            string lang = settings.Language;
            if (lang == "Русский")
                ComboBoxChooseLanguage.SelectedItem = LocalizationManager.GetString("Settings_Language_Russian");
            else if (lang == "English")
                ComboBoxChooseLanguage.SelectedItem = LocalizationManager.GetString("Settings_Language_English");
            else
                ComboBoxChooseLanguage.SelectedItem = LocalizationManager.GetString("Settings_Language_Russian");

            CheckDogMode.Checked = settings.IsDogTheme;
            NumericDollarChoose.Value = (decimal)settings.DollarValue;

            ComboBoxChooseLanguage.SelectedIndexChanged += OnSettingsChanged;
            CheckDogMode.CheckedChanged += OnSettingsChanged;
            NumericDollarChoose.ValueChanged += OnSettingsChanged;

            _settingsChanged = false;
        }

        private void OnSettingsChanged(object sender, EventArgs e)
        {
            _settingsChanged = true;
        }

        private bool CheckSettingsSaved()
        {
            if (!_settingsChanged)
                return true;

            DialogResult result = MessageBox.Show(
                $"{ThemeManager.SoundQuestion} Вы изменили настройки, но не сохранили их!\nХотите сохранить перед выходом?",
                "Настройки не сохранены",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                ButtonApplySettings_Click(this, EventArgs.Empty);
                return !_settingsChanged;
            }
            else if (result == DialogResult.Cancel)
            {
                return false;
            }

            return true;
        }

        private void ShowTemporaryMessage(string message)
        {
            _messageTimer.Stop();
            LabelSettings.Text = message;
            _isWelcomeMessage = false;
            _messageTimer.Start();
        }

        private void MessageTimer_Tick(object sender, EventArgs e)
        {
            _messageTimer.Stop();
            _isWelcomeMessage = true;
            UpdateWelcomeMessage();
        }

        private void ShowMenu()
        {
            pnlMenu.BackColor = Color.White;
            pnlMenu.Visible = true;
        }

        private void HideMenu()
        {
            pnlMenu.Visible = false;
        }

        private void ButtonApplySettings_Click(object sender, EventArgs e)
        {
            try
            {
                SettingsService newSettings = CollectSettingsFromUI();
                bool saved = SettingsService.SaveSettings(newSettings);

                if (saved)
                {
                    _currentSettings = newSettings;
                    _settingsChanged = false;

                    // Устанавливаем язык в LocalizationManager
                    LocalizationManager.SetLanguage(newSettings.Language);

                    ApplyTheme();
                    ApplyLocalization();

                    string successMessage = ThemeManager.IsDogTheme ? "Ррраф! Настройки успешно сохранены!" : "Мур! Настройки успешно сохранены!";
                    ShowTemporaryMessage(successMessage);

                    // Уведомляем всех об изменении темы и языка
                    ThemeChanged?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    string errorMessage = ThemeManager.IsDogTheme ? "Гав-гав... Ошибка при сохранении настроек!" : "Мяу... Ошибка при сохранении настроек!";
                    ShowTemporaryMessage(errorMessage);
                }
            }
            catch (Exception ex)
            {
                ShowTemporaryMessage($"Ошибка: {ex.Message}");
            }
        }

        private void ButtonResetSettings_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                $"{ThemeManager.SoundQuestion} Вы уверены, что хотите сбросить все настройки до базовых?",
                "Подтверждение сброса",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    SettingsService defaultSettings = new SettingsService();
                    ApplySettingsToUI(defaultSettings);
                    bool saved = SettingsService.SaveSettings(defaultSettings);

                    if (saved)
                    {
                        _currentSettings = defaultSettings;
                        _settingsChanged = false;

                        LocalizationManager.SetLanguage(defaultSettings.Language);
                        ApplyTheme();
                        ApplyLocalization();

                        string successMessage = ThemeManager.IsDogTheme ? "Ррраф! Настройки сброшены до изначальных!" : "Мур! Настройки сброшены до изначальных!";
                        ShowTemporaryMessage(successMessage);

                        ThemeChanged?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        string errorMessage = ThemeManager.IsDogTheme ? "Гав-гав... Ошибка при сбросе настроек!" : "Мяу... Ошибка при сбросе настроек!";
                        ShowTemporaryMessage(errorMessage);
                    }
                }
                catch (Exception ex)
                {
                    ShowTemporaryMessage($"Ошибка: {ex.Message}");
                }
            }
        }

        private void ButtonReturnToHome_Click(object sender, EventArgs e)
        {
            if (!CheckSettingsSaved())
                return;

            NavigateToHome?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonContinueWork_Click(object sender, EventArgs e)
        {
            if (!CheckSettingsSaved())
                return;

            NavigateToHome?.Invoke(this, EventArgs.Empty);
        }

        private void IconOpenMenu_Click(object sender, EventArgs e)
        {
            if (!CheckSettingsSaved())
                return;

            NavigateToHome?.Invoke(this, EventArgs.Empty);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            CheckDogMode.Checked = !CheckDogMode.Checked;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
using SmartBudget.ClassLibrary;

namespace SmartBudget
{
    public partial class Settings : UserControl
    {
        private string _originalLabel;
        private System.Windows.Forms.Timer _messageTimer;
        private SettingsService _currentSettings;
        private SettingsService _initialSettings; // Сохраняем начальные настройки для сравнения
        private bool _settingsChanged = false;
        private bool _isWelcomeMessage = true;
        private bool _isUpdatingUI = false;
        private bool _isSavingSettings = false;

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
            lblScanQr.Text = LocalizationManager.GetString("Settings_ScanQR");

            string selectedLanguage = ComboBoxChooseLanguage.SelectedItem?.ToString();
            ComboBoxChooseLanguage.Items.Clear();
            ComboBoxChooseLanguage.Items.Add("Русский");
            ComboBoxChooseLanguage.Items.Add("English");

            string currentLang = LocalizationManager.GetCurrentLanguage();
            if (currentLang == "Русский")
                ComboBoxChooseLanguage.SelectedItem = "Русский";
            else if (currentLang == "English")
                ComboBoxChooseLanguage.SelectedItem = "English";

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
            string welcomeText = LocalizationManager.GetString("Settings_Welcome");

            if (ThemeManager.IsDogTheme)
            {
                LabelSettings.Text = welcomeText
                    .Replace("Добро пожаловать в меню настроек, мяу!", "Ррраф! Добро пожаловать в меню настроек!")
                    .Replace("Welcome to the settings menu, meow!", "Woof! Welcome to the settings menu!");
            }
            else
            {
                LabelSettings.Text = welcomeText;
            }

            _originalLabel = LabelSettings.Text;
            _isWelcomeMessage = true;
        }

        private void LoadCurrentSettings()
        {
            _isUpdatingUI = true;

            _currentSettings = SettingsService.LoadSettings();
            _initialSettings = new SettingsService(
                _currentSettings.Language,
                _currentSettings.IsDogTheme,
                _currentSettings.DollarValue
            );

            if (_currentSettings == null)
            {
                _isUpdatingUI = false;
                return;
            }

            string lang = _currentSettings.Language;
            if (lang == "Русский")
                ComboBoxChooseLanguage.SelectedItem = "Русский";
            else if (lang == "English")
                ComboBoxChooseLanguage.SelectedItem = "English";
            else
                ComboBoxChooseLanguage.SelectedItem = "Русский";

            CheckDogMode.Checked = _currentSettings.IsDogTheme;
            NumericDollarChoose.Value = (decimal)_currentSettings.DollarValue;

            _settingsChanged = false;
            _isUpdatingUI = false;
        }

        private bool AreSettingsChanged()
        {
            if (_initialSettings == null || _currentSettings == null)
                return false;

            string currentLang = ComboBoxChooseLanguage.SelectedItem?.ToString() ?? "Русский";
            bool currentDog = CheckDogMode.Checked;
            float currentDollar = (float)NumericDollarChoose.Value;

            return currentLang != _initialSettings.Language ||
                   currentDog != _initialSettings.IsDogTheme ||
                   Math.Abs(currentDollar - _initialSettings.DollarValue) > 0.001f;
        }

        private SettingsService CollectSettingsFromUI()
        {
            string language = ComboBoxChooseLanguage.SelectedItem?.ToString() ?? "Русский";
            bool isDogTheme = CheckDogMode.Checked;
            float dollarValue = (float)NumericDollarChoose.Value;

            return new SettingsService(language, isDogTheme, dollarValue);
        }

        private void ApplySettingsToUI(SettingsService settings)
        {
            _isUpdatingUI = true;

            if (settings == null)
                return;

            string lang = settings.Language;
            if (lang == "Русский")
                ComboBoxChooseLanguage.SelectedItem = "Русский";
            else if (lang == "English")
                ComboBoxChooseLanguage.SelectedItem = "English";
            else
                ComboBoxChooseLanguage.SelectedItem = "Русский";

            CheckDogMode.Checked = settings.IsDogTheme;
            NumericDollarChoose.Value = (decimal)settings.DollarValue;

            _initialSettings = new SettingsService(
                settings.Language,
                settings.IsDogTheme,
                settings.DollarValue
            );

            _settingsChanged = false;
            _isUpdatingUI = false;
        }

        private void OnSettingsChanged(object sender, EventArgs e)
        {
            if (_isUpdatingUI)
                return;

            if (_isSavingSettings)
                return;

            // Проверяем, действительно ли изменились настройки
            _settingsChanged = AreSettingsChanged();
        }

        private bool CheckSettingsSaved()
        {
            // Если настройки не изменились - пропускаем
            if (!_settingsChanged)
                return true;

            string questionText;
            if (LocalizationManager.GetCurrentLanguage() == "English")
            {
                questionText = ThemeManager.IsDogTheme
                    ? "Woof? You changed the settings but haven't saved them!\nDo you want to save before exiting?"
                    : "Meow? You changed the settings but haven't saved them!\nDo you want to save before exiting?";
            }
            else
            {
                questionText = ThemeManager.IsDogTheme
                    ? "Гав? Вы изменили настройки, но не сохранили их!\nХотите сохранить перед выходом?"
                    : "Мяу? Вы изменили настройки, но не сохранили их!\nХотите сохранить перед выходом?";
            }

            DialogResult result = MessageBox.Show(
                questionText,
                LocalizationManager.GetString("Dialog_Title_SettingsNotSaved"),
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _isSavingSettings = true;
                ButtonApplySettings_Click(this, EventArgs.Empty);
                _isSavingSettings = false;

                if (_settingsChanged)
                {
                    _settingsChanged = false;
                }

                return true;
            }
            else if (result == DialogResult.Cancel)
            {
                return false;
            }

            _settingsChanged = false;
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
                bool wasSaving = _isSavingSettings;
                _isSavingSettings = true;

                SettingsService newSettings = CollectSettingsFromUI();
                bool saved = SettingsService.SaveSettings(newSettings);

                if (saved)
                {
                    _currentSettings = newSettings;
                    _settingsChanged = false;

                    LocalizationManager.SetLanguage(newSettings.Language);

                    ApplyTheme();
                    ApplyLocalization();

                    _settingsChanged = false;

                    string successMessage;
                    if (ThemeManager.IsDogTheme)
                    {
                        successMessage = LocalizationManager.GetString("Settings_Message_SaveSuccess_Dog");
                    }
                    else
                    {
                        successMessage = LocalizationManager.GetString("Settings_Message_SaveSuccess_Cat");
                    }
                    ShowTemporaryMessage(successMessage);

                    ThemeChanged?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    string errorMessage;
                    if (ThemeManager.IsDogTheme)
                    {
                        errorMessage = LocalizationManager.GetString("Settings_Message_SaveError_Dog");
                    }
                    else
                    {
                        errorMessage = LocalizationManager.GetString("Settings_Message_SaveError_Cat");
                    }
                    ShowTemporaryMessage(errorMessage);
                }

                _isSavingSettings = wasSaving;
            }
            catch (Exception ex)
            {
                ShowTemporaryMessage($"Ошибка: {ex.Message}");
                _isSavingSettings = false;
            }
        }

        private void ButtonResetSettings_Click(object sender, EventArgs e)
        {
            string questionText = ThemeManager.IsDogTheme
                ? "Woof? Are you sure you want to reset all settings to default?"
                : "Мяу? Вы уверены, что хотите сбросить все настройки до базовых?";

            DialogResult result = MessageBox.Show(
                questionText,
                LocalizationManager.GetString("Dialog_Title_Reset"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _isSavingSettings = true;

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

                        _settingsChanged = false;

                        string successMessage;
                        if (ThemeManager.IsDogTheme)
                        {
                            successMessage = LocalizationManager.GetString("Settings_Message_ResetSuccess_Dog");
                        }
                        else
                        {
                            successMessage = LocalizationManager.GetString("Settings_Message_ResetSuccess_Cat");
                        }
                        ShowTemporaryMessage(successMessage);

                        ThemeChanged?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        string errorMessage;
                        if (ThemeManager.IsDogTheme)
                        {
                            errorMessage = LocalizationManager.GetString("Settings_Message_SaveError_Dog");
                        }
                        else
                        {
                            errorMessage = LocalizationManager.GetString("Settings_Message_SaveError_Cat");
                        }
                        ShowTemporaryMessage(errorMessage);
                    }

                    _isSavingSettings = false;
                }
                catch (Exception ex)
                {
                    ShowTemporaryMessage($"Ошибка: {ex.Message}");
                    _isSavingSettings = false;
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
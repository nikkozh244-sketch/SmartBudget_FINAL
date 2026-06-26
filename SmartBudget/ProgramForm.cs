using SmartBudget.ClassLibrary;

namespace SmartBudget
{
    /// <summary>
    /// Основная форма программы
    /// </summary>
    public partial class ProgramForm : Form
    {
        // Поля с формами
        private readonly MainMenu _homeScreen;
        private readonly Settings _settingsScreen;
        private readonly AboutApplication _firstTimeInApplication;
        private readonly GetAnalys _getAnalysisScreen;
        private readonly StartNewWork _startNewWorkScreen;
        private UserControl _currentScreen;

        public ProgramForm()
        {
            InitializeComponent();

            _homeScreen = new MainMenu();
            _settingsScreen = new Settings();
            _firstTimeInApplication = new AboutApplication();
            _startNewWorkScreen = new StartNewWork();
            _getAnalysisScreen = new GetAnalys();

            //Загружаем настройки и применяем язык ПРИ ЗАПУСКЕ
            SettingsService settings = SettingsService.LoadSettings();
            if (settings != null && !string.IsNullOrEmpty(settings.Language))
                LocalizationManager.SetLanguage(settings.Language);
            else
                LocalizationManager.SetLanguage("Русский");

            // Передаем ссылку на GetAnalys в MainMenu
            _homeScreen.SetAnalysisScreen(_getAnalysisScreen);
            _getAnalysisScreen.SetStartNewWorkScreen(_startNewWorkScreen);

            //Подписки на события
            _homeScreen.NavigateToFirstTime += NavigateToFirstTime;
            _homeScreen.NavigateToSettings += NavigateToSettings;
            _homeScreen.CloseApplication += CloseApplication;
            _homeScreen.NavigateToStartNewWork += NavigateToStartNewWork;
            _homeScreen.NavigateToContinueWork += NavigateToContinueWork;

            _settingsScreen.NavigateToHome += NavigateToHome;
            _settingsScreen.ThemeChanged += OnThemeChanged;

            _firstTimeInApplication.NavigateToHome += NavigateToHome;

            _startNewWorkScreen.NavigateToHome += NavigateToHome;
            _startNewWorkScreen.NavigateToGetAnalysis += NavigateToGetAnalysis;
            _startNewWorkScreen.DataChanged += OnDataChanged;

            _getAnalysisScreen.NavigateToChangeData += NavigateToStartNewWorkFromAnalysis;
            _getAnalysisScreen.NavigateToHome += NavigateToHome;

            // Применяем локализацию ко всем экранам
            _homeScreen.UpdateLocalization();
            _settingsScreen.ApplyLocalization();
            _firstTimeInApplication.ApplyLocalization();
            _startNewWorkScreen.ApplyLocalization();
            _getAnalysisScreen.ApplyLocalization();

            // Настройка справки
            string helpPath = Path.Combine(Application.StartupPath, "Справочная служба.chm");
            if (File.Exists(helpPath))
            {
                helpProvider1.HelpNamespace = helpPath;
            }

            ShowScreen(_homeScreen);
        }

        /// <summary>
        /// Показываем форму и растягиваем ее при вызове
        /// </summary>
        /// <param name="newScreen">Растягиваемая форма</param>
        private void ShowScreen(UserControl newScreen)
        {
            if (_currentScreen == newScreen)
                return;

            if (_currentScreen != null)
                PanelContainer.Controls.Remove(_currentScreen);

            newScreen.Dock = DockStyle.Fill;
            PanelContainer.Controls.Add(newScreen);
            newScreen.BringToFront();

            _currentScreen = newScreen;
        }

        /// <summary>
        /// Обработчик изменения темы - обновляет все экраны
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnThemeChanged(object sender, EventArgs e)
        {
            ThemeManager.ReloadSettings();

            SettingsService settings = SettingsService.LoadSettings();
            if (settings != null && !string.IsNullOrEmpty(settings.Language))
            {
                LocalizationManager.SetLanguage(settings.Language);
            }

            // Обновляем все экраны
            _homeScreen.UpdateTheme();
            _homeScreen.UpdateLocalization();
            _firstTimeInApplication.ApplyTheme();
            _firstTimeInApplication.ApplyLocalization();
            _startNewWorkScreen.ApplyTheme();
            _startNewWorkScreen.ApplyLocalization();
            _getAnalysisScreen.ApplyTheme();
            _getAnalysisScreen.ApplyLocalization();
            _settingsScreen.ApplyTheme();
            _settingsScreen.ApplyLocalization();
        }

        /// <summary>
        /// Обработчик изменения данных в StartNewWork
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDataChanged(object sender, EventArgs e)
        {
            if (_currentScreen == _getAnalysisScreen)
            {
                List<ObjectOfAnalysis> operations = _startNewWorkScreen.GetOperations();
                if (operations != null && operations.Count > 0)
                    _getAnalysisScreen.RefreshData(operations);
            }
        }

        /// <summary>
        /// Ручное переопределение нажатия F1 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                ShowContextHelp();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        /// <summary>
        /// Метод для показа контекстной справки по экрану 
        /// </summary>
        private void ShowContextHelp()
        {
            string helpPath = Path.Combine(Application.StartupPath, "Справочная служба.chm");

            if (!File.Exists(helpPath))
                return;

            string keyword = GetHelpKeywordForCurrentScreen();

            if (!string.IsNullOrEmpty(keyword))
                Help.ShowHelp(this, helpPath, HelpNavigator.KeywordIndex, keyword);
            else
                Help.ShowHelp(this, helpPath);
        }

        /// <summary>
        /// Переопределение ключевых слов для справки 
        /// </summary>
        /// <returns>Строка для ссылки в файле chm</returns>
        private string GetHelpKeywordForCurrentScreen()
        {
            if (_currentScreen == null) return null;

            string screenName = _currentScreen.GetType().Name;

            return screenName switch
            {
                nameof(MainMenu) => "mainmenu",
                nameof(Settings) => "settings",
                nameof(StartNewWork) => "newwork",
                nameof(GetAnalys) => "analysis",
                nameof(AboutApplication) => "about",
                _ => null,
            };
        }

        //Навигация

        private void NavigateToHome(object sender, EventArgs e)
        {
            ShowScreen(_homeScreen);
        }

        private void NavigateToFirstTime(object sender, EventArgs e)
        {
            ShowScreen(_firstTimeInApplication);
        }

        private void NavigateToSettings(object sender, EventArgs e)
        {
            _settingsScreen.RefreshSettings();
            ShowScreen(_settingsScreen);
        }

        private void NavigateToStartNewWork(object sender, EventArgs e)
        {
            _startNewWorkScreen.ClearAllData();
            ShowScreen(_startNewWorkScreen);
        }

        private void NavigateToStartNewWorkFromAnalysis(object sender, EventArgs e)
        {
            ShowScreen(_startNewWorkScreen);
        }

        private void NavigateToContinueWork(object sender, EventArgs e)
        {
            ShowScreen(_getAnalysisScreen);
        }

        /// <summary>
        /// Закрытие приложения
        /// </summary>
        private void CloseApplication(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Исправленный метод - теперь использует DataTransferEventArgs из ClassLibrary
        private void NavigateToGetAnalysis(object sender, DataTransferEventArgs e)
        {
            if (e == null || e.OperationsData == null || e.OperationsData.Count == 0)
                return;

            _getAnalysisScreen.GetData(e.OperationsData);
            _getAnalysisScreen.UpdateButtonStyles(_getAnalysisScreen.btnTable);
            ShowScreen(_getAnalysisScreen);
        }
    }
}
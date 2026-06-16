using Smart_Budget.ClassLibrary;

namespace Smart_Budget
{
    public partial class ProgramForm : Form
    {
        private MainMenu _homeScreen;
        private Settings _settingsScreen;
        private AboutApplication _firstTimeInApplication;
        private GetAnalys _getAnalysisScreen;
        private StartNewWork _startNewWorkScreen;
        private UserControl _currentScreen;

        public ProgramForm()
        {
            InitializeComponent();

            _homeScreen = new MainMenu();
            _settingsScreen = new Settings();
            _firstTimeInApplication = new AboutApplication();
            _startNewWorkScreen = new StartNewWork();
            _getAnalysisScreen = new GetAnalys();

            // Подписка на события главного меню
            _homeScreen.NavigateToFirstTime += NavigateToFirstTime;
            _homeScreen.NavigateToSettings += NavigateToSettings;
            _homeScreen.CloseApplication += CloseApplication;
            _homeScreen.NavigateToStartNewWork += NavigateToStartNewWork;

            // Подписка на события настроек
            _settingsScreen.NavigateToHome += NavigateToHome;
            _settingsScreen.NavigateToFirstTime += NavigateToFirstTime;
            _settingsScreen.NavigateToStartNewWork += NavigateToStartNewWork;

            // Подписка на события экрана "О приложении"
            _firstTimeInApplication.NavigateToHome += NavigateToHome;

            // Подписка на события экрана ввода данных
            _startNewWorkScreen.NavigateToHome += NavigateToHome;
            _startNewWorkScreen.NavigateToGetAnalysis += NavigateToGetAnalysis;

            // Подписка на события экрана анализа
            _getAnalysisScreen.NavigateToChangeData += NavigateToStartNewWorkFromAnalysis;
            _getAnalysisScreen.NavigateToHome += NavigateToHome;

            // Настройка справки
            string helpPath = Path.Combine(Application.StartupPath, "Справочная служба.chm");
            if (File.Exists(helpPath))
            {
                helpProvider1.HelpNamespace = helpPath;
            }

            // Показываем главное меню
            ShowScreen(_homeScreen);
        }

        private void _getAnalysisScreen_NavigateToHome(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ShowScreen(UserControl newScreen)
        {
            if (_currentScreen == newScreen)
                return;

            if (_currentScreen != null)
            {
                PanelContainer.Controls.Remove(_currentScreen);
            }

            newScreen.Dock = DockStyle.Fill;
            PanelContainer.Controls.Add(newScreen);
            newScreen.BringToFront();

            _currentScreen = newScreen;

            //if (_currentScreen == _firstTimeInApplication)
            //{
            //    _firstTimeInApplication.StartVideo();
            //}
        }

        /// <summary>
        /// Ручное переопределение нажатия F1 
        /// </summary>
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
        private string GetHelpKeywordForCurrentScreen()
        {
            if (_currentScreen == null) return null;

            string screenName = _currentScreen.GetType().Name;

            switch (screenName)
            {
                case nameof(MainMenu):
                    return "mainmenu";
                case nameof(Settings):
                    return "settings";
                case nameof(StartNewWork):
                    return "newwork";
                case nameof(GetAnalys):
                    return "analysis";
                case nameof(AboutApplication):
                    return "about";
                default:
                    return null;
            }
        }

        private void NavigateToHome(object sender, EventArgs e)
        {
            ShowScreen(_homeScreen);
            //_firstTimeInApplication.StopVideo();
        }

        private void NavigateToFirstTime(object sender, EventArgs e)
        {
            ShowScreen(_firstTimeInApplication);
        }

        private void NavigateToSettings(object sender, EventArgs e)
        {
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

        /// <summary>
        /// Закрытие приложения
        /// </summary>
        private void CloseApplication(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NavigateToGetAnalysis(object sender, StartNewWork.DataTransferEventArgs e)
        {
            if (e == null || e.OperationsData == null || e.OperationsData.Count == 0)
            {
                return;
            }

            // Передаём данные на экран анализа
            _getAnalysisScreen.LoadData(e.OperationsData);

            // Показываем экран анализа
            ShowScreen(_getAnalysisScreen);
        }
    }
}
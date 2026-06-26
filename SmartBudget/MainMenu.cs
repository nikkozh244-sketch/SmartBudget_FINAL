using Newtonsoft.Json;
using SmartBudget.ClassLibrary;
using System.Reflection;
using System.Text;

namespace SmartBudget
{
    /// <summary>
    /// Класс для главного меню
    /// </summary>
    public partial class MainMenu : UserControl
    {
        // События для перехода
        public event EventHandler NavigateToSettings;
        public event EventHandler NavigateToFirstTime;
        public event EventHandler CloseApplication;
        public event EventHandler NavigateToStartNewWork;
        public event EventHandler NavigateToContinueWork;

        private GetAnalys _analysisScreen;

        public MainMenu()
        {
            InitializeComponent();
            ApplyTheme();
            UpdateLocalization();
        }

        /// <summary>
        /// Обновляем локализацию
        /// </summary>
        public void UpdateLocalization()
        {
            LabelOfApp.Text = LocalizationManager.GetString("MainMenu_Title");
            ButtonContinueWork.Text = LocalizationManager.GetString("MainMenu_ContinueWork");
            btnStartNewWork.Text = LocalizationManager.GetString("MainMenu_StartNewWork");
            ButtonFirstTime.Text = LocalizationManager.GetString("MainMenu_About");
            ButtonSettings.Text = LocalizationManager.GetString("MainMenu_Settings");
            ButtonExit.Text = LocalizationManager.GetString("MainMenu_Exit");
        }

        public void SetAnalysisScreen(GetAnalys analysisScreen)
        {
            _analysisScreen = analysisScreen;
        }

        public void UpdateTheme()
        {
            ThemeManager.ReloadSettings();
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            if (ThemeManager.IsDogTheme)
            {
                IconOfApplication.Image = Properties.Resources.pictureDogHelper;
            }
            else
            {
                IconOfApplication.Image = Properties.Resources.pictureCatHelper;
            }
        }

        #region Методы для кнопок

        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            NavigateToSettings?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonFirstTime_Click(object sender, EventArgs e)
        {
            NavigateToFirstTime?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            string questionText;
            if (LocalizationManager.GetCurrentLanguage() == "English") // Уточняем у пользователя его намерение выйти - вдруг случайно нажал
            {
                questionText = ThemeManager.IsDogTheme
                    ? "Woof? Are you sure you want to exit?"
                    : "Meow? Are you sure you want to exit?";
            }
            else
            {
                questionText = ThemeManager.IsDogTheme
                    ? "Гав? Вы уверены, что хотите выйти?"
                    : "Мяу? Вы уверены, что хотите выйти?";
            }

            DialogResult result = MessageBox.Show(questionText, LocalizationManager.GetString("Dialog_Title_Exit"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                CloseApplication?.Invoke(this, EventArgs.Empty);
        }

        private void BtnStartNewWork_Click(object sender, EventArgs e)
        {
            NavigateToStartNewWork?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Метод для продолжения работы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonContinueWork_Click(object sender, EventArgs e)
        {
            if (_analysisScreen == null)
            {
                string errorText = LocalizationManager.GetCurrentLanguage() == "English"
                    ? "Error: analysis screen not initialized!"
                    : "Ошибка: экран анализа не инициализирован!";

                MessageBox.Show(errorText, LocalizationManager.GetString("Dialog_Title_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> projectNames = GetAvailableProjects();

            if (projectNames.Count == 0)
            {
                string noProjectsText = LocalizationManager.GetCurrentLanguage() == "English"
                    ? "No saved projects!\nFirst create and save a project."
                    : "Нет сохраненных проектов!\nСначала создайте и сохраните проект.";

                MessageBox.Show(noProjectsText, LocalizationManager.GetString("Dialog_Title_Info"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string projectList = string.Join("\n", projectNames);
            string sound;

            if (LocalizationManager.GetCurrentLanguage() == "English")
                sound = ThemeManager.IsDogTheme ? "Woof" : "Meow";
            else
                sound = ThemeManager.IsDogTheme ? "Гав" : "Мур";

            string message = $"{sound}-р-р! Введите название сохраненного проекта из списка ниже:\n\n{projectList}\n\nВведите название:";

            if (LocalizationManager.GetCurrentLanguage() == "English")
                message = $"{sound}-r-r! Enter the name of the saved project from the list below:\n\n{projectList}\n\nEnter name:";

            string projectName = Microsoft.VisualBasic.Interaction.InputBox(message, LocalizationManager.GetString("Dialog_Title_LoadProject"), "");

            if (string.IsNullOrWhiteSpace(projectName))
            {
                if (projectName != null)
                {
                    string errorText = LocalizationManager.GetCurrentLanguage() == "English"
                        ? "You entered an empty string or clicked Cancel!"
                        : "Вы ввели пустую строку или нажали «Отмену»!";

                    MessageBox.Show(errorText,
                        LocalizationManager.GetString("Dialog_Title_Error"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                return;
            }

            if (!projectNames.Contains(projectName))
            {
                string errorText = LocalizationManager.GetCurrentLanguage() == "English"
                    ? $"Project \"{projectName}\" does not exist!\n\nAvailable projects:\n{string.Join("\n", projectNames)}"
                    : $"Проекта с именем \"{projectName}\" не существует!\n\nДоступные проекты:\n{string.Join("\n", projectNames)}";

                MessageBox.Show(errorText, LocalizationManager.GetString("Dialog_Title_Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool loaded = LoadProject(projectName);

            if (loaded)
            {
                string successText = LocalizationManager.GetCurrentLanguage() == "English"
                    ? $"Project \"{projectName}\" loaded successfully!"
                    : $"Проект \"{projectName}\" успешно загружен!";

                MessageBox.Show(successText, LocalizationManager.GetString("Dialog_Title_Success"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                NavigateToContinueWork?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                string errorText = LocalizationManager.GetCurrentLanguage() == "English"
                    ? "Error loading project!"
                    : "Ошибка при загрузке проекта!";

                MessageBox.Show(errorText, LocalizationManager.GetString("Dialog_Title_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Методы для работы с проектами

        /// <summary>
        /// Метод получения пути проекта
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        private static string GetProjectPath(string projectName)
        {
            string exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string projectsDirectory = Path.Combine(exeDirectory, "Projects");
            if (!Directory.Exists(projectsDirectory))
                Directory.CreateDirectory(projectsDirectory);

            string fileName = $"{projectName}.json";
            return Path.Combine(projectsDirectory, fileName);
        }

        /// <summary>
        /// Метод для получения проекта
        /// </summary>
        /// <returns>Список операций</returns>
        private static List<string> GetAvailableProjects()
        {
            List<string> projects = [];

            try
            {
                string exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string projectsDirectory = Path.Combine(exeDirectory, "Projects");

                if (!Directory.Exists(projectsDirectory))
                    return projects;

                string[] files = Directory.GetFiles(projectsDirectory, "*.json");

                foreach (string file in files)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    if (!string.IsNullOrWhiteSpace(fileName))
                        projects.Add(fileName);
                }

                projects.Sort(); //Для удобства ради
            }
            catch (Exception ex)
            {
                string errorText = LocalizationManager.GetCurrentLanguage() == "English"
                    ? $"Error getting project list: {ex.Message}"
                    : $"Ошибка при получении списка проектов: {ex.Message}";

                MessageBox.Show(errorText, LocalizationManager.GetString("Dialog_Title_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return projects;
        }

        /// <summary>
        /// Выгрузка проекта
        /// </summary>
        /// <param name="projectName">Название проекта</param>
        /// <returns>Был ли проект выгружен</returns>
        private bool LoadProject(string projectName)
        {
            try
            {
                string path = GetProjectPath(projectName);

                if (!File.Exists(path))
                    return false;

                string json = File.ReadAllText(path, Encoding.UTF8);
                List<ObjectOfAnalysis> loadedData = JsonConvert.DeserializeObject<List<ObjectOfAnalysis>>(json);

                if (loadedData == null || loadedData.Count == 0)
                {
                    string errorText = LocalizationManager.GetCurrentLanguage() == "English"
                        ? "Project file is empty or corrupted!"
                        : "Файл проекта пустой или поврежден!";

                    MessageBox.Show(errorText, LocalizationManager.GetString("Dialog_Title_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _analysisScreen.GetData(loadedData);
                _analysisScreen.UpdateStartNewWorkData(loadedData);

                return true;
            }
            catch (Exception ex)
            {
                string errorText = LocalizationManager.GetCurrentLanguage() == "English"
                    ? $"Error loading: {ex.Message}"
                    : $"Ошибка при загрузке: {ex.Message}";

                MessageBox.Show(errorText,
                    LocalizationManager.GetString("Dialog_Title_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion
    }
}
// MainMenu.cs
using Newtonsoft.Json;
using SmartBudget.ClassLibrary;
using System.Reflection;
using System.Text;

namespace SmartBudget
{
    public partial class MainMenu : UserControl
    {
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

        public void UpdateLocalization()
        {
            LabelOfApp.Text = LocalizationManager.GetString("MainMenu_Title");
            ButtonContinueWork.Text = LocalizationManager.GetString("MainMenu_ContinueWork");
            btnStartNewWork.Text = LocalizationManager.GetString("MainMenu_StartNewWork");
            ButtonFirstTime.Text = LocalizationManager.GetString("MainMenu_About");
            ButtonSettings.Text = LocalizationManager.GetString("MainMenu_Settings");
            ButtonExit.Text = LocalizationManager.GetString("MainMenu_Exit");
            // Приветствие не на главном меню, оно в LabelOfApp
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
                IconOfApplication.Image = SmartBudget.Properties.Resources.pictureCatHelper;
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
            DialogResult result = MessageBox.Show(
                $"{ThemeManager.SoundQuestion} Вы уверены, что хотите выйти?",
                "Подтверждение выхода",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                CloseApplication?.Invoke(this, EventArgs.Empty);
            }
        }

        private void btnStartNewWork_Click(object sender, EventArgs e)
        {
            NavigateToStartNewWork?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonContinueWork_Click(object sender, EventArgs e)
        {
            if (_analysisScreen == null)
            {
                MessageBox.Show($"{ThemeManager.SoundSad} Ошибка: экран анализа не инициализирован!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> projectNames = GetAvailableProjects();

            if (projectNames.Count == 0)
            {
                MessageBox.Show($"{ThemeManager.SoundSad} Нет сохраненных проектов!\nСначала создайте и сохраните проект.",
                    "Нет проектов", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string projectList = string.Join("\n", projectNames);
            string message = $"{ThemeManager.SoundAlt}-р-р! Введите название сохраненного проекта из списка ниже:\n\n{projectList}\n\nВведите название:";

            string projectName = Microsoft.VisualBasic.Interaction.InputBox(
                message,
                "Продолжить работу",
                "");

            if (string.IsNullOrWhiteSpace(projectName))
            {
                if (projectName != null)
                    MessageBox.Show($"{ThemeManager.SoundSad} Вы ввели пустую строку или нажали «Отмену»!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!projectNames.Contains(projectName))
            {
                MessageBox.Show($"{ThemeManager.SoundSad} Проекта с именем \"{projectName}\" не существует!\n\nДоступные проекты:\n{string.Join("\n", projectNames)}",
                    "Проект не найден", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool loaded = LoadProject(projectName);

            if (loaded)
            {
                MessageBox.Show($"{ThemeManager.SoundHappy} Проект \"{projectName}\" успешно загружен!",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NavigateToContinueWork?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show($"{ThemeManager.SoundSad} Ошибка при загрузке проекта!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Методы для работы с проектами

        private string GetProjectPath(string projectName)
        {
            string exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string projectsDirectory = Path.Combine(exeDirectory, "Projects");
            if (!Directory.Exists(projectsDirectory))
            {
                Directory.CreateDirectory(projectsDirectory);
            }

            string fileName = $"{projectName}.json";
            return Path.Combine(projectsDirectory, fileName);
        }

        private List<string> GetAvailableProjects()
        {
            List<string> projects = new List<string>();

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

                projects.Sort();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении списка проектов: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return projects;
        }

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
                    MessageBox.Show($"{ThemeManager.SoundSad} Файл проекта пустой или поврежден!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _analysisScreen.GetData(loadedData);
                _analysisScreen.UpdateStartNewWorkData(loadedData);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion
    }
}
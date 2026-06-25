using Newtonsoft.Json;
using SmartBudget.ClassLibrary;
using System.Reflection;
using System.Text;

namespace SmartBudget
{
    public partial class MainMenu : UserControl
    {
        // Событие для навигации (чтобы главная форма знала)
        public event EventHandler NavigateToSettings;
        public event EventHandler NavigateToFirstTime;
        public event EventHandler CloseApplication;
        public event EventHandler NavigateToStartNewWork;
        public event EventHandler NavigateToContinueWork;

        // Ссылка на экран GetAnalys для загрузки проекта
        private GetAnalys _analysisScreen;

        /// <summary>
        /// Инициализация экрана 
        /// </summary>
        public MainMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Устанавливает ссылку на экран анализа для загрузки проектов
        /// </summary>
        public void SetAnalysisScreen(GetAnalys analysisScreen)
        {
            _analysisScreen = analysisScreen;
        }

        #region Методы для кнопок, перенаправляющие на тот или иной экран

        /// <summary>
        /// Перенаправление на настройки 
        /// </summary>
        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            NavigateToSettings?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Перенаправление на обучающее видео
        /// </summary>
        private void ButtonFirstTime_Click(object sender, EventArgs e)
        {
            NavigateToFirstTime?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Выход из приложения
        /// </summary>
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            CloseApplication?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Перенаправление на организацию таблицы с операциями
        /// </summary>
        private void btnStartNewWork_Click(object sender, EventArgs e)
        {
            NavigateToStartNewWork?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Просьба ввести название проекта, к которому пользователь хотел бы вернуться
        /// </summary>
        private void ButtonContinueWork_Click(object sender, EventArgs e)
        {
            if (_analysisScreen == null)
            {
                MessageBox.Show("Мяу... Ошибка: экран анализа не инициализирован!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Получаем список доступных проектов
            List<string> projectNames = GetAvailableProjects();

            if (projectNames.Count == 0)
            {
                MessageBox.Show("Мяу... Нет сохраненных проектов!\nСначала создайте и сохраните проект.",
                    "Нет проектов", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Формируем сообщение со списком проектов
            string projectList = string.Join("\n", projectNames);
            string message = $"Мур-р-р! Введите название сохраненного проекта из списка ниже:\n\n{projectList}\n\nВведите название:";

            // Показываем InputBox
            string projectName = Microsoft.VisualBasic.Interaction.InputBox(
                message,
                "Продолжить работу",
                "");

            // Проверяем, что пользователь ввел название
            if (string.IsNullOrWhiteSpace(projectName))
            {
                if (projectName != null)
                    MessageBox.Show("Мяу! Вы ввели пустую строку или нажали «Отмену»!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверяем, существует ли проект с таким именем
            if (!projectNames.Contains(projectName))
            {
                MessageBox.Show($"Мяу... Проекта с именем \"{projectName}\" не существует!\n\nДоступные проекты:\n{string.Join("\n", projectNames)}",
                    "Проект не найден", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Загружаем проект
            bool loaded = LoadProject(projectName);

            if (loaded)
            {
                MessageBox.Show($"Муррр! Проект \"{projectName}\" успешно загружен!",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Переключаемся на экран GetAnalys
                NavigateToContinueWork?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Мяу! Ошибка при загрузке проекта!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Методы для работы с проектами

        /// <summary>
        /// Возвращает путь к файлу проекта
        /// </summary>
        private string GetProjectPath(string projectName)
        {
            string exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Создаем папку для проектов, если её нет
            string projectsDirectory = Path.Combine(exeDirectory, "Projects");
            if (!Directory.Exists(projectsDirectory))
            {
                Directory.CreateDirectory(projectsDirectory);
            }

            string fileName = $"{projectName}.json";
            return Path.Combine(projectsDirectory, fileName);
        }

        /// <summary>
        /// Получает список всех доступных проектов из папки Projects
        /// </summary>
        private List<string> GetAvailableProjects()
        {
            List<string> projects = new List<string>();

            try
            {
                string exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string projectsDirectory = Path.Combine(exeDirectory, "Projects");

                if (!Directory.Exists(projectsDirectory))
                    return projects;

                // Ищем все .json файлы в папке Projects
                string[] files = Directory.GetFiles(projectsDirectory, "*.json");

                foreach (string file in files)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    if (!string.IsNullOrWhiteSpace(fileName))
                        projects.Add(fileName);
                }

                projects.Sort(); // Сортируем для удобства
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении списка проектов: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return projects;
        }

        /// <summary>
        /// Загружает проект из JSON файла и передает данные на экран GetAnalys и StartNewWork
        /// </summary>
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
                    MessageBox.Show("Мяу... Файл проекта пустой или поврежден!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Передаем данные на экран анализа
                _analysisScreen.GetData(loadedData);

                // Также обновляем данные в StartNewWork через GetAnalys
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
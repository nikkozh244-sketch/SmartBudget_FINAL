using SmartBudget.ClassLibrary;

namespace SmartBudget
{
    /// <summary>
    ///  Класс для работы с обучающим экраном
    /// </summary>
    public partial class AboutApplication : UserControl
    {
        // События перехода
        public event EventHandler NavigateToHome;

        // Путь к папке с видео и справочной службой
        private string _resourcesDirectory;

        public AboutApplication()
        {
            InitializeComponent();

            // Инициализируем путь к папке с ресурсами
            _resourcesDirectory = Path.Combine(Application.StartupPath, "Resources");

            ApplyTheme();
            ApplyLocalization();
        }

        /// <summary>
        /// Публичный метод для запуска видео и его загрузки
        /// </summary>
        public void StartVideo()
        {
            LoadVideo();
        }

        /// <summary>
        /// Публичный метод для остановки видео
        /// </summary>
        public void StopVideo()
        {
            awmpStudyingVideo.Ctlcontrols.stop();
        }

        /// <summary>
        /// Возвращает путь к видео файлу для текущего языка
        /// </summary>
        private string GetVideoPath()
        {
            // Создаем папку Resources, если её нет
            if (!Directory.Exists(_resourcesDirectory))
                Directory.CreateDirectory(_resourcesDirectory);

            // Определяем имя файла в зависимости от языка
            string currentLang = LocalizationManager.GetCurrentLanguage();
            string fileName = currentLang == "Русский"
                ? "RU_LearningVideo.mp4"
                : "EN_LearningVideo.mp4";

            string videoPath = Path.Combine(_resourcesDirectory, fileName);

            // Если файла нет на диске — сохраняем его из ресурсов
            if (!File.Exists(videoPath))
            {
                try
                {
                    byte[] videoBytes = currentLang == "Русский"
                        ? Properties.Resources.RU_LearningVideo
                        : Properties.Resources.EN_LearningVideo;

                    if (videoBytes != null && videoBytes.Length > 0)
                    {
                        File.WriteAllBytes(videoPath, videoBytes);
                    }
                    else
                    {
                        MessageBox.Show($"Видео для языка '{currentLang}' не найдено в ресурсах!");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении видео: {ex.Message}");
                    return null;
                }
            }

            return videoPath;
        }

        /// <summary>
        /// Возвращает путь к файлу справочной службы для текущего языка
        /// </summary>
        private string GetHelpFilePath()
        {
            // Создаем папку Resources, если её нет
            if (!Directory.Exists(_resourcesDirectory))
                Directory.CreateDirectory(_resourcesDirectory);

            // Определяем имя файла в зависимости от языка
            string currentLang = LocalizationManager.GetCurrentLanguage();
            string fileName = currentLang == "Русский"
                ? "RU_Manual.chm"
                : "EN_Manual.chm";

            string helpPath = Path.Combine(_resourcesDirectory, fileName);

            // Если файла нет на диске — сохраняем его из ресурсов
            if (!File.Exists(helpPath))
            {
                try
                {
                    byte[] helpBytes = currentLang == "Русский"
                        ? Properties.Resources.RU_Manual
                        : Properties.Resources.EN_Manual;

                    if (helpBytes != null && helpBytes.Length > 0)
                    {
                        File.WriteAllBytes(helpPath, helpBytes);
                    }
                    else
                    {
                        MessageBox.Show($"Справочная служба для языка '{currentLang}' не найдена в ресурсах!");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении справочной службы: {ex.Message}");
                    return null;
                }
            }

            return helpPath;
        }

        /// <summary>
        /// Поиск и загрузка видео в плеер
        /// </summary>
        private void LoadVideo()
        {
            try
            {
                string videoPath = GetVideoPath();

                if (string.IsNullOrEmpty(videoPath) || !File.Exists(videoPath))
                {
                    MessageBox.Show($"Видео не найдено: {videoPath ?? "путь не указан"}");
                    return;
                }

                // Останавливаем текущее воспроизведение
                awmpStudyingVideo.Ctlcontrols.stop();

                // Загружаем видео
                awmpStudyingVideo.stretchToFit = true;
                awmpStudyingVideo.URL = videoPath;
                awmpStudyingVideo.uiMode = "full";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке видео: {ex.Message}");
            }
        }

        /// <summary>
        /// Открытие справочной службы
        /// </summary>
        private void OpenHelp()
        {
            try
            {
                string helpPath = GetHelpFilePath();

                if (string.IsNullOrEmpty(helpPath) || !File.Exists(helpPath))
                {
                    MessageBox.Show($"Файл справочной службы не найден: {helpPath ?? "путь не указан"}");
                    return;
                }

                // Открываем справочную службу
                System.Windows.Forms.Help.ShowHelp(null, helpPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии справочной службы: {ex.Message}");
            }
        }

        /// <summary>
        /// Принятие локализации
        /// </summary>
        public void ApplyLocalization()
        {
            btnOpenChmFile.Text = LocalizationManager.GetString("About_OpenHelp");
            ApplyTheme();
        }

        /// <summary>
        /// Применение темы вместе с локализацией
        /// </summary>
        public void ApplyTheme()
        {
            ThemeManager.ReloadSettings();

            if (ThemeManager.IsDogTheme)
            {
                PictureCat.Image = Properties.Resources.pictureDogHelperSmaller;
                string welcomeText = LocalizationManager.GetString("About_Welcome");

                if (LocalizationManager.GetCurrentLanguage() == "English")
                {
                    LabelAboutApp.Text = welcomeText.Replace("Meow!", "Woof!");
                }
                else
                {
                    LabelAboutApp.Text = welcomeText.Replace("Мур-р-р!", "Ррраф!");
                }
            }
            else
            {
                PictureCat.Image = Properties.Resources.pictureCatHelperSmaller;
                LabelAboutApp.Text = LocalizationManager.GetString("About_Welcome");
            }
        }

        /// <summary>
        /// Принудительная перезагрузка видео (при смене языка)
        /// </summary>
        public void ReloadVideo()
        {
            StopVideo();
            LoadVideo();
        }

        /// <summary>
        /// Принудительное обновление справочной службы (при смене языка)
        /// </summary>
        public void ReloadHelp()
        {
            // Просто проверяем, что файл существует и перезаписываем при необходимости
            GetHelpFilePath();
        }

        /// <summary>
        /// Возврат на меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenMenuIcon_Click_1(object sender, EventArgs e)
        {
            StopVideo();
            NavigateToHome?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Открытие справочной службы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenChmFile_Click(object sender, EventArgs e)
        {
            OpenHelp();
        }
    }
}
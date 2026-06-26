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

        public AboutApplication()
        {
            InitializeComponent();
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
        /// Поиск видео на устройстве и его выгрузка
        /// </summary>
        private void LoadVideo()
        {
            string videoRUPath = Path.Combine(Application.StartupPath, "RU_Video.mp4");

            if (File.Exists(videoRUPath))
            {
                awmpStudyingVideo.stretchToFit = true;
                awmpStudyingVideo.URL = videoRUPath;
                awmpStudyingVideo.Ctlcontrols.stop();
                awmpStudyingVideo.uiMode = "full";
            }
            else
                MessageBox.Show($"Видео не найдено: {videoRUPath}");
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
                LabelAboutApp.Text = welcomeText.Replace("Мур-р-р!", "Ррраф!");
            }
            else
            {
                PictureCat.Image = Properties.Resources.pictureCatHelperSmaller;
                LabelAboutApp.Text = LocalizationManager.GetString("About_Welcome");
            }
        }

        /// <summary>
        /// Возврат на меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenMenuIcon_Click_1(object sender, EventArgs e)
        {
            NavigateToHome?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Открытие справочной службы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenChmFile_Click(object sender, EventArgs e)
        {
            string helpFilePath = Application.StartupPath + "\\Справочная служба.chm";

            if (System.IO.File.Exists(helpFilePath))
                System.Windows.Forms.Help.ShowHelp(null, helpFilePath);
            else
                MessageBox.Show("Файл справки не найден: " + helpFilePath);
        }
    }
}
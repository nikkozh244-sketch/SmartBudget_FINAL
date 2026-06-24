namespace SmartBudget
{
    public partial class AboutApplication : UserControl
    {
        //События экрана
        public event EventHandler NavigateToHome;
        public event EventHandler NavigateToStartNewWork;
        public event EventHandler NavigateToSettings;

        /// <summary>
        /// Инициализация экрана
        /// </summary>
        public AboutApplication()
        {
            InitializeComponent();
        }

        /// <summary>
        ///Публичный метод, подгружающий обучающее видео (чтобы не трогать LoadVideo) 
        /// </summary>
        public void StartVideo()
        {
            LoadVideo();
        }

        /// <summary>
        ///Метод, останавливающий обучающее видео 
        /// </summary>
        public void StopVideo()
        {
            awmpStudyingVideo.Ctlcontrols.stop();
        }

        /// <summary>
        /// Метод для выгрузки обучающего видео
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

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void AxWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///Открытие меню 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenMenuIcon_Click_1(object sender, EventArgs e)
        {
            NavigateToHome?.Invoke(this, EventArgs.Empty);
        }

        private void btnOpenChmFile_Click(object sender, EventArgs e)
        {
            // Путь к файлу справки
            string helpFilePath = Application.StartupPath + "\\Справочная служба.chm";

            // Проверяем, существует ли файл
            if (System.IO.File.Exists(helpFilePath))
                System.Windows.Forms.Help.ShowHelp(null, helpFilePath);
            else
                MessageBox.Show("Файл справки не найден: " + helpFilePath);
        }

        private void PictureCat_Click(object sender, EventArgs e)
        {

        }
    }
}

using SmartBudget.ClassLibrary;

namespace SmartBudget
{
    public partial class AboutApplication : UserControl
    {
        public event EventHandler NavigateToHome;
        public event EventHandler NavigateToStartNewWork;
        public event EventHandler NavigateToSettings;

        public AboutApplication()
        {
            InitializeComponent();
            ApplyTheme();
            ApplyLocalization();
        }

        public void StartVideo()
        {
            LoadVideo();
        }

        public void StopVideo()
        {
            awmpStudyingVideo.Ctlcontrols.stop();
        }

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

        public void ApplyLocalization()
        {
            btnOpenChmFile.Text = LocalizationManager.GetString("About_OpenHelp");
            ApplyTheme();
        }

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

        private void OpenMenuIcon_Click_1(object sender, EventArgs e)
        {
            NavigateToHome?.Invoke(this, EventArgs.Empty);
        }

        private void btnOpenChmFile_Click(object sender, EventArgs e)
        {
            string helpFilePath = Application.StartupPath + "\\Справочная служба.chm";

            if (System.IO.File.Exists(helpFilePath))
                System.Windows.Forms.Help.ShowHelp(null, helpFilePath);
            else
                MessageBox.Show("Файл справки не найден: " + helpFilePath);
        }

        private void PictureCat_Click(object sender, EventArgs e) { }
    }
}
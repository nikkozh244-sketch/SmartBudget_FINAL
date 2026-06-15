using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Smart_Budget
{
    public partial class AboutApplication : UserControl
    {
        //События экрана
        public event EventHandler NavigateToHome;
        public event EventHandler NavigateToStartNewWork; //СПРОСИТЬ ПРО МЕНЮ!!!
        public event EventHandler NavigateToSettings;

        /// <summary>
        /// Инициализация экрана
        /// </summary>
        public AboutApplication()
        {
            InitializeComponent();
        }

        ///// <summary>
        /////Публичный метод, подгружающий обучающее видео (чтобы не трогать LoadVideo) 
        ///// </summary>
        //public void StartVideo()
        //{
        //    LoadVideo();
        //}

        /// <summary>
        ///Метод, останавливающий обучающее видео 
        /// </summary>
        //public void StopVideo()
        //{
        //    VideoTeaching.Ctlcontrols.stop();
        //}

        /// <summary>
        /// Метод для выгрузки обучающего видео
        ///// </summary>
        //private void LoadVideo()
        //{
        //    string videoPath = Path.Combine(Application.StartupPath, "Обучающее видео, заглушка.mp4");

        //    if (File.Exists(videoPath))
        //    {
        //        VideoTeaching.stretchToFit = true;
        //        VideoTeaching.URL = videoPath;
        //        VideoTeaching.Ctlcontrols.stop();
        //        VideoTeaching.uiMode = "full";
        //    }
        //    else
        //        MessageBox.Show($"Видео не найдено: {videoPath}");
        //}

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

        private void btnCHMFile_Click(object sender, EventArgs e)
        {
            // Путь к файлу справки
            string helpFilePath = Application.StartupPath + "\\Справочная служба.chm";

            // Проверяем, существует ли файл
            if (System.IO.File.Exists(helpFilePath))
                System.Windows.Forms.Help.ShowHelp(null, helpFilePath);
            else
                MessageBox.Show("Файл справки не найден: " + helpFilePath);
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
    }
}

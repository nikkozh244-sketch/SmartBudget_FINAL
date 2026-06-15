using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smart_Budget
{
    public partial class MainMenu : UserControl
    {
        // Событие для навигации (чтобы главная форма знала)
        public event EventHandler NavigateToSettings;
        public event EventHandler NavigateToFirstTime;
        public event EventHandler CloseApplication;
        public event EventHandler NavigateToStartNewWork;

        /// <summary>
        ///Инициализация экрана 
        /// </summary>
        public MainMenu()
        {
            InitializeComponent();
        }

        //Методы для кнопок, перенаправляющие на тот или иной экран
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
            CloseApplication?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonStartNewWork_Click(object sender, EventArgs e)
        {
            NavigateToStartNewWork?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonContinueWork_Click(object sender, EventArgs e)
        {
            MessageBox.Show("В разработке");
        }

        /// <summary>
        ///Кнопка для открытия справочной службы приложения 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpButton_Click(object sender, EventArgs e)
        {
            // Путь к файлу справки
            string helpFilePath = Application.StartupPath + "\\Справочная служба.chm";

            // Проверяем, существует ли файл
            if (System.IO.File.Exists(helpFilePath))
                System.Windows.Forms.Help.ShowHelp(null, helpFilePath);
            else
                MessageBox.Show("Файл справки не найден: " + helpFilePath);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnStartNewWork_Click(object sender, EventArgs e)
        {
            NavigateToStartNewWork?.Invoke(this, EventArgs.Empty);
        }
    }
}

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
    public partial class Settings : UserControl
    {
        // Поля
        private string _originalLabel;
        private System.Windows.Forms.Timer _messageTimer;

        // События экрана
        public event EventHandler NavigateToHome;
        public event EventHandler NavigateToFirstTime;
        public event EventHandler NavigateToStartNewWork;
        public event EventHandler NavigateToSettings;

        /// <summary>
        /// Инициализация экрана 
        /// </summary>
        public Settings()
        {
            InitializeComponent();

            // Сохраняем оригинальный текст
            _originalLabel = LabelSettings.Text;

            // Инициализация таймера для временных сообщений
            _messageTimer = new System.Windows.Forms.Timer();
            _messageTimer.Interval = 2000; // 2 секунды
            _messageTimer.Tick += MessageTimer_Tick;
        }

        /// <summary>
        /// Показать временное сообщение на 2 секунды
        /// </summary>
        /// <param name="message">Сообщение для отображения</param>
        private void ShowTemporaryMessage(string message)
        {
            // Останавливаем предыдущий таймер, если работал
            _messageTimer.Stop();

            // Меняем текст
            LabelSettings.Text = message;

            // Запускаем таймер для восстановления
            _messageTimer.Start();
        }

        /// <summary>
        /// Восстановление исходного текста после таймера
        /// </summary>
        private void MessageTimer_Tick(object sender, EventArgs e)
        {
            _messageTimer.Stop();
            LabelSettings.Text = _originalLabel;
        }

        /// <summary>
        /// Метод для показа меню
        /// </summary>
        private void ShowMenu()
        {
            pnlMenu.BackColor = Color.White;
            pnlMenu.Visible = true;
        }

        /// <summary>
        /// Метод, чтобы спрятать меню
        /// </summary>
        private void HideMenu()
        {
            pnlMenu.Visible = false;
        }

        // Кнопки
        private void OpenMenuIcon_Click(object sender, EventArgs e)
        {
            NavigateToHome?.Invoke(this, EventArgs.Empty);
            ShowTemporaryMessage("Добро пожаловать в меню настроек, мяу! Здесь вы можете настроить приложение специально под себя");
        }

        private void ButtonApplySettings_Click(object sender, EventArgs e)
        {
            ShowTemporaryMessage("Настройки успешно применены, мяу!");
        }

        private void ButtonResetSettings_Click(object sender, EventArgs e)
        {
            ShowTemporaryMessage("Мур! Настройки сброшены до изначальных!");
        }

        private void ButtonFirstTime_Click(object sender, EventArgs e)
        {
            NavigateToFirstTime?.Invoke(this, EventArgs.Empty);
            HideMenu();
        }

        private void ButtonStartNewWork_Click(object sender, EventArgs e)
        {
            NavigateToStartNewWork?.Invoke(this, EventArgs.Empty);
            HideMenu();
        }

        private void pnlOverlay_Click(object sender, PaintEventArgs e)
        {
            NavigateToSettings?.Invoke(this, EventArgs.Empty);
            HideMenu();
        }

        private void ButtonReturnToHome_Click(object sender, EventArgs e)
        {
            NavigateToHome?.Invoke(this, EventArgs.Empty);
            ShowTemporaryMessage("Добро пожаловать в меню настроек, мяу! Здесь вы можете настроить приложение специально под себя");
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
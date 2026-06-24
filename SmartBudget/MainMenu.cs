namespace SmartBudget
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

        /// <summary>
        ///Перенаправление на настройки 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            NavigateToSettings?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///Перенаправление на обучающее видео
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonFirstTime_Click(object sender, EventArgs e)
        {
            NavigateToFirstTime?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Выход из приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            CloseApplication?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Перенаправление на организацию таблицы с операциями
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartNewWork_Click(object sender, EventArgs e)
        {
            NavigateToStartNewWork?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Просьба ввести название проекта, к которому пользователь хотел бы вернуться
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonContinueWork_Click(object sender, EventArgs e)
        {
            // Показываем диалог с просьбой ввести название проекта
            string projectName = Microsoft.VisualBasic.Interaction.InputBox("Мур-р-р! Введите, пожалуйста, название сохраненного проекта", "Продолжить работу", "");

            // Проверяем, что пользователь ввел название (не отменил и не пустую строку)
            if (!string.IsNullOrWhiteSpace(projectName))
            {
                // Здесь можно добавить логику загрузки проекта
                MessageBox.Show($"Проект \"{projectName}\" успешно загружен!");
            }
            else if (projectName != null) // Пользователь нажал OK с пустой строкой или отмену
                MessageBox.Show("Мяу! Я, конечно, кот - финансовый помощник, а не гадалка, но вы, кажется, нажали «Отмену» или ввели пустую строку!");
        }
    }
}

using SmartBudget.ClassLibrary;

namespace SmartBudget
{
    /// <summary>
    ///Класс, отвечающий за функциональность экрана с вводом данных 
    /// </summary>
    public partial class StartNewWork : UserControl
    {
        // Поля
        private string _originalLabel; //Для сохранения оригинальныого сообщения в заголовке
        private System.Windows.Forms.Timer _messageTimer; //Инициализация таймера для вывода сообщений
        private List<ObjectOfAnalysis> _operations; //Список операций, который будет передоваться и меняться вместе с изменениями таблицы
        private BindingSource _bindingSource; //Компонент для связки таблицы на форме и списка
        private const int _maxDropdownItems = 7; //Константа для максимального количества элементов в выпадающих списках
        private const int _timerInterval = 5000; //Константа для определения того, насколько будет показываться сообщение
        private const int _maxOperations = 100; // Константа, определяющая максимальное количество операций

        //События
        public event EventHandler NavigateToHome;
        public event EventHandler<DataTransferEventArgs> NavigateToGetAnalysis;

        //Конструктор
        public StartNewWork()
        {
            InitializeComponent();

            dtpDate.MaxDate = DateTime.Today; //Ограничение для ввода даты - сегодняшний день
            dtpDate.Value = DateTime.Today;
            _originalLabel = lblMessage.Text;

            // Инициализация таймера
            _messageTimer = new System.Windows.Forms.Timer();
            _messageTimer.Interval = _timerInterval;
            _messageTimer.Tick += MessageTimer_Tick;

            // Инициализация списка и привязки
            _operations = new List<ObjectOfAnalysis>();
            _bindingSource = new BindingSource();
            _bindingSource.DataSource = _operations;

            // Настройка DataGridView
            SetupDataGridViewColumns();
            StyleDataGridView();
            SetupDataGridViewEvents();

            // Привязываем DataGridView
            dgvOperations.DataSource = _bindingSource;

            // Очистка полей ввода
            ClearInputFields();

            // Настройка ограничений на ввод
            SetupInputLimits();

            // Настройка состояния кнопок
            UpdateButtonsState();
        }

        /// <summary>
        /// Класс для передачи данных между экранами
        /// </summary>
        public class DataTransferEventArgs : EventArgs
        {
            public List<ObjectOfAnalysis> OperationsData { get; set; }

            public DataTransferEventArgs(List<ObjectOfAnalysis> data)
            {
                OperationsData = data;
            }
        }

        /// <summary>
        /// Добавление нового элемента в выпадающий список (с вытеснением самого старого)
        /// </summary>
        private void AddToDropdownWithLimit(ComboBox comboBox, string newItem)
        {
            if (string.IsNullOrWhiteSpace(newItem)) return;

            if (comboBox.Items.Contains(newItem)) return;

            comboBox.Items.Add(newItem);

            if (comboBox.Items.Count > _maxDropdownItems)
            {
                comboBox.Items.RemoveAt(0);
            }
        }

        /// <summary>
        /// Обновление состояния кнопок в зависимости от наличия данных в таблице
        /// </summary>
        private void UpdateButtonsState()
        {
            bool hasData = _operations.Count > 0;

            btnChange.Enabled = hasData;
            btnDelete.Enabled = hasData;

            if (hasData)
            {
                btnChange.FlatStyle = FlatStyle.Standard;
                btnDelete.FlatStyle = FlatStyle.Standard;
                btnChange.BackColor = default;
                btnDelete.BackColor = default;
                btnChange.UseVisualStyleBackColor = true;
                btnDelete.UseVisualStyleBackColor = true;
            }
            else
            {
                btnChange.BackColor = Color.LightGray;
                btnDelete.BackColor = Color.LightGray;
            }
        }

        /// <summary>
        /// Настройка ограничений на ввод
        /// </summary>
        private void SetupInputLimits()
        {
            cboCategory.MaxLength = 50;
            cboType.MaxLength = 50;
            numAmount.Maximum = decimal.MaxValue;
            numAmount.Minimum = - (decimal.MaxValue);
        }

        /// <summary>
        /// Настройка колонок DataGridView
        /// </summary>
        private void SetupDataGridViewColumns()
        {
            dgvOperations.AutoGenerateColumns = false;
            dgvOperations.Columns.Clear();

            //Колонка для номера строки
            DataGridViewTextBoxColumn colNumber = new DataGridViewTextBoxColumn();
            colNumber.Name = "colNumber";
            colNumber.HeaderText = "№";
            colNumber.ReadOnly = true;
            colNumber.Width = 50;
            colNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOperations.Columns.Add(colNumber);
            colNumber.ReadOnly = true;

            //Колонка "Размер операции"
            DataGridViewTextBoxColumn colAmount = new DataGridViewTextBoxColumn();
            colAmount.Name = "colAmount";
            colAmount.HeaderText = "Размер";
            colAmount.DataPropertyName = "Sum";
            colAmount.DefaultCellStyle.Format = "N2";
            colAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOperations.Columns.Add(colAmount);

            //Колонка "Тип операции"
            DataGridViewTextBoxColumn colType = new DataGridViewTextBoxColumn();
            colType.Name = "colType";
            colType.HeaderText = "Тип";
            colType.DataPropertyName = "TypeOfOperation";
            dgvOperations.Columns.Add(colType);

            //Колонка "Категория"
            DataGridViewTextBoxColumn colCategory = new DataGridViewTextBoxColumn();
            colCategory.Name = "colCategory";
            colCategory.HeaderText = "Категория";
            colCategory.DataPropertyName = "Category";
            dgvOperations.Columns.Add(colCategory);

            //Колонка "Валюта"
            DataGridViewTextBoxColumn colCurrency = new DataGridViewTextBoxColumn();
            colCurrency.Name = "colCurrency";
            colCurrency.HeaderText = "Валюта";
            colCurrency.DataPropertyName = "Currency";
            colCurrency.ReadOnly = true;
            dgvOperations.Columns.Add(colCurrency);

            //Колонка "Дата"
            DataGridViewTextBoxColumn colDate = new DataGridViewTextBoxColumn();
            colDate.Name = "colDate";
            colDate.HeaderText = "Дата";
            colDate.DataPropertyName = "Date";
            colDate.DefaultCellStyle.Format = "dd.MM.yyyy";
            dgvOperations.Columns.Add(colDate);
        }

        /// <summary>
        /// Обновление номеров строк
        /// </summary>
        private void UpdateRowNumbers()
        {
            for (int i = 0; i < dgvOperations.Rows.Count; i++)
            {
                if (dgvOperations.Rows[i].Cells["colNumber"] != null)
                {
                    dgvOperations.Rows[i].Cells["colNumber"].Value = (i + 1).ToString();
                }
            }
        }

        /// <summary>
        /// Настройка событий DataGridView
        /// </summary>
        private void SetupDataGridViewEvents()
        {
            dgvOperations.SelectionChanged += DataGridView1_SelectionChanged;
            dgvOperations.RowsAdded += (s, e) => UpdateRowNumbers();
            dgvOperations.RowsRemoved += (s, e) =>
            {
                UpdateRowNumbers();
                UpdateButtonsState();
            };
            dgvOperations.DataError += dgvOperations_DataError;
        }

        /// <summary>
        /// Обработка выделения строки в таблице
        /// </summary>
        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (_bindingSource.Current != null)
            {
                ObjectOfAnalysis current = (ObjectOfAnalysis)_bindingSource.Current;

                numAmount.Value = (decimal)current.Sum;
                cboType.Text = current.TypeOfOperation;
                cboCategory.Text = current.Category;
                cboCurrency.Text = current.Currency;
                dtpDate.Value = current.Date;
            }
        }

        /// <summary>
        /// Показать временное сообщение на 2 секунды
        /// </summary>
        private void ShowTemporaryMessage(string message)
        {
            _messageTimer.Stop();
            lblMessage.Text = message;
            _messageTimer.Start();
        }

        /// <summary>
        /// Восстановление исходного текста после таймера
        /// </summary>
        private void MessageTimer_Tick(object sender, EventArgs e)
        {
            _messageTimer.Stop();
            lblMessage.Text = _originalLabel;
        }

        /// <summary>
        /// Очистка полей ввода
        /// </summary>
        private void ClearInputFields()
        {
            numAmount.Value = 10;
            cboType.SelectedIndex = -1;
            cboType.Text = "";
            cboCategory.SelectedIndex = -1;
            cboCategory.Text = "";
            cboCurrency.SelectedIndex = -1;
            dtpDate.Value = DateTime.Today;
        }


        /// <summary>
        /// Приватный метод для выявления ошибок в вводе данных
        /// </summary>
        /// <returns>Истина, если данные введены корректно - ложь, если есть ошибка в вводе</returns>
        private bool ValidateInputs()
        {
            if (numAmount.Value == 0) //Размер операции
            {
                ShowTemporaryMessage("Мяу... Размер операции не может равняться нулю!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(cboType.Text)) //Тип операции
            {
                ShowTemporaryMessage("Мур... Пожалуйста, выберите тип операции!");
                return false;
            }

            if (cboType.Text.Length > 50)
            {
                ShowTemporaryMessage("Мяу! Тип операции не может быть длиннее 50 символов!"); //Длина типа операции
                return false;
            }

            if (string.IsNullOrWhiteSpace(cboCategory.Text))
            {
                ShowTemporaryMessage("Мур... Пожалуйста, выберите категорию!"); //Категория операции
                return false;
            }

            if (cboCategory.Text.Length > 50)
            {
                ShowTemporaryMessage("Мяу! Категория не может быть длиннее 50 символов!"); // Длина категории операции
                return false;
            }

            if (string.IsNullOrWhiteSpace(cboCurrency.Text)) //Выбор валюты
            {
                ShowTemporaryMessage("Мур... Пожалуйста, выберите валюту!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Добавление операции
        /// </summary>
        private void AddOperation()
        {
            // Проверка на максимальное количество операций
            if (_operations.Count >= _maxOperations)
            {
                ShowTemporaryMessage($"Мяу... Извините, но нельзя добавить более {_maxOperations} операций!");
                return;
            }

            if (!ValidateInputs()) return;

            string newType = cboType.Text.Trim();
            string newCategory = cboCategory.Text.Trim();

            ObjectOfAnalysis newOperation = new ObjectOfAnalysis(
                (float)numAmount.Value,
                newType,
                newCategory,
                cboCurrency.Text,
                dtpDate.Value
            );

            _operations.Add(newOperation);
            _bindingSource.ResetBindings(false);
            UpdateRowNumbers();
            UpdateButtonsState();

            AddToDropdownWithLimit(cboType, newType);
            AddToDropdownWithLimit(cboCategory, newCategory);

            ClearInputFields();
            ShowTemporaryMessage("Муррр! Новая операция успешно добавлена!");
        }

        /// <summary>
        /// Изменение выбранной операции
        /// </summary>
        private void UpdateOperation()
        {
            if (_operations.Count == 0)
            {
                ShowTemporaryMessage("Мяу... Нет операций для изменения! Сначала добавьте операцию.");
                return;
            }

            if (_bindingSource.Current == null)
            {
                ShowTemporaryMessage("Мяу... Сначала выберите операцию для изменения!");
                return;
            }

            if (!ValidateInputs()) return;

            string newType = cboType.Text.Trim();
            string newCategory = cboCategory.Text.Trim();

            ObjectOfAnalysis current = (ObjectOfAnalysis)_bindingSource.Current;

            current.Sum = (float)numAmount.Value;
            current.TypeOfOperation = newType;
            current.Category = newCategory;
            current.Currency = cboCurrency.Text;
            current.Date = dtpDate.Value;

            _bindingSource.ResetBindings(false);
            UpdateRowNumbers();

            AddToDropdownWithLimit(cboType, newType);
            AddToDropdownWithLimit(cboCategory, newCategory);

            ClearInputFields();
            ShowTemporaryMessage("Мур! Операция была успешно изменена!");
        }

        /// <summary>
        /// Удаление выбранной операции
        /// </summary>
        private void DeleteOperation()
        {
            if (_operations.Count == 0)
            {
                ShowTemporaryMessage("Мяу... Нет операций для удаления! Сначала добавьте операцию.");
                return;
            }

            if (_bindingSource.Current == null)
            {
                ShowTemporaryMessage("Мяу... Сначала выберите операцию для удаления!");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Мур... Вы уверены, что хотите удалить эту операцию?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _operations.Remove((ObjectOfAnalysis)_bindingSource.Current);
                _bindingSource.ResetBindings(false);
                UpdateRowNumbers();
                UpdateButtonsState();
                ClearInputFields();
                ShowTemporaryMessage("Мяу! Операция удалена!");
            }
        }

        /// <summary>
        /// Завершение ввода и переход к анализу
        /// </summary>
        private void FinishEntering()
        {
            // Проверка на пустоту таблицы с операциями
            if (_operations.Count == 0)
            {
                MessageBox.Show("Мяу... Нет операций для анализа! Пожалуйста, добавьте хотя бы одну операцию.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTransferEventArgs args = new DataTransferEventArgs(_operations);    
            NavigateToGetAnalysis?.Invoke(this, args);
        }

        /// <summary>
        /// Стилизация DataGridView
        /// </summary>
        private void StyleDataGridView()
        {
            dgvOperations.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgvOperations.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOperations.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 11, FontStyle.Bold);

            dgvOperations.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            dgvOperations.DefaultCellStyle.Font = new Font("Times New Roman", 11);
            dgvOperations.DefaultCellStyle.ForeColor = Color.Black;

            dgvOperations.AllowUserToAddRows = false;
            dgvOperations.AllowUserToDeleteRows = false;
            dgvOperations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOperations.MultiSelect = false;
            dgvOperations.ReadOnly = false;
            dgvOperations.RowHeadersVisible = false;

            dgvOperations.AllowUserToResizeRows = false;
        }

        /// <summary>
        /// Очистка всех данных на экране
        /// </summary>
        public void ClearAllData()
        {
            _operations.Clear();
            _bindingSource.ResetBindings(false);
            UpdateRowNumbers();
            UpdateButtonsState();
            ClearInputFields();
        }

        /// <summary>
        /// Обработка ошибок ввода в DataGridView
        /// </summary>
        private void dgvOperations_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Показываем понятное сообщение пользователю
            ShowTemporaryMessage("Мяу! Неверный формат данных! Для даты используйте формат ДД.ММ.ГГГГ");

            // Отменяем действие, чтобы программа не вылетела
            e.ThrowException = false;

            // Сбрасываем редактирование ячейки
            dgvOperations.CancelEdit();

            // Убираем выделение с проблемной ячейки
            dgvOperations.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddOperation();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            UpdateOperation();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteOperation();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            FinishEntering();
        }

        private void IconOpenMenu_Click(object sender, EventArgs e)
        {
            NavigateToHome?.Invoke(this, EventArgs.Empty);
        }






        /// <summary>
        /// Загружает данные операций в таблицу (используется при загрузке проекта)
        /// </summary>
        public void LoadData(List<ObjectOfAnalysis> operations)
        {
            if (operations == null || operations.Count == 0)
                return;

            // Очищаем текущие данные
            ClearAllData();

            // Добавляем загруженные операции
            foreach (ObjectOfAnalysis operation in operations)
            {
                _operations.Add(operation);
            }

            // Обновляем таблицу
            _bindingSource.ResetBindings(false);
            UpdateRowNumbers();
            UpdateButtonsState();

            ShowTemporaryMessage($"Муррр! Загружено {_operations.Count} операций из проекта!");
        }
    }
}
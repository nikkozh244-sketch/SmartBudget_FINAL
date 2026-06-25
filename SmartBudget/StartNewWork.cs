// StartNewWork.cs
using SmartBudget.ClassLibrary;

namespace SmartBudget
{
    public partial class StartNewWork : UserControl
    {
        private string _originalLabel;
        private System.Windows.Forms.Timer _messageTimer;
        private List<ObjectOfAnalysis> _operations;
        private BindingSource _bindingSource;
        private const int _maxDropdownItems = 7;
        private const int _timerInterval = 5000;
        private const int _maxOperations = 100;

        public event EventHandler NavigateToHome;
        public event EventHandler<DataTransferEventArgs> NavigateToGetAnalysis;
        public event EventHandler DataChanged;

        public StartNewWork()
        {
            InitializeComponent();
            ApplyTheme();

            dtpDate.MaxDate = DateTime.Today;
            dtpDate.Value = DateTime.Today;
            _originalLabel = lblMessage.Text;

            _messageTimer = new System.Windows.Forms.Timer();
            _messageTimer.Interval = _timerInterval;
            _messageTimer.Tick += MessageTimer_Tick;

            _operations = new List<ObjectOfAnalysis>();
            _bindingSource = new BindingSource();
            _bindingSource.DataSource = _operations;

            SetupDataGridViewColumns();
            StyleDataGridView();
            SetupDataGridViewEvents();

            dgvOperations.DataSource = _bindingSource;

            ClearInputFields();
            SetupInputLimits();
            UpdateButtonsState();

            dgvOperations.CellEndEdit += DgvOperations_CellEndEdit;
        }

        public void ApplyLocalization()
        {
            lblAmount.Text = LocalizationManager.GetString("StartNewWork_Amount");
            lblType.Text = LocalizationManager.GetString("StartNewWork_Type");
            lblCategory.Text = LocalizationManager.GetString("StartNewWork_Category");
            lblCurrency.Text = LocalizationManager.GetString("StartNewWork_Currency");
            lblDate.Text = LocalizationManager.GetString("StartNewWork_Date");
            btnAdd.Text = LocalizationManager.GetString("StartNewWork_Add");
            btnChange.Text = LocalizationManager.GetString("StartNewWork_Change");
            btnDelete.Text = LocalizationManager.GetString("StartNewWork_Delete");
            btnDone.Text = LocalizationManager.GetString("StartNewWork_Done");

            // Обновляем выпадающие списки
            UpdateDropdowns();
            ApplyTheme();
        }

        private void UpdateDropdowns()
        {
            // Сохраняем выбранные значения
            string selectedType = cboType.Text;
            string selectedCategory = cboCategory.Text;
            string selectedCurrency = cboCurrency.Text;

            // Обновляем типы операций
            cboType.Items.Clear();
            cboType.Items.Add(LocalizationManager.GetString("StartNewWork_Type_Replenishment"));
            cboType.Items.Add(LocalizationManager.GetString("StartNewWork_Type_Transfer"));
            cboType.Items.Add(LocalizationManager.GetString("StartNewWork_Type_Withdrawal"));
            cboType.Items.Add(LocalizationManager.GetString("StartNewWork_Type_WriteOff"));

            // Обновляем категории
            cboCategory.Items.Clear();
            cboCategory.Items.Add(LocalizationManager.GetString("StartNewWork_Category_Food"));
            cboCategory.Items.Add(LocalizationManager.GetString("StartNewWork_Category_Cafe"));
            cboCategory.Items.Add(LocalizationManager.GetString("StartNewWork_Category_Transport"));
            cboCategory.Items.Add(LocalizationManager.GetString("StartNewWork_Category_Delivery"));
            cboCategory.Items.Add(LocalizationManager.GetString("StartNewWork_Category_Clothes"));
            cboCategory.Items.Add(LocalizationManager.GetString("StartNewWork_Category_Electronics"));

            // Обновляем валюты
            cboCurrency.Items.Clear();
            cboCurrency.Items.Add(LocalizationManager.GetString("StartNewWork_Currency_RUB"));
            cboCurrency.Items.Add(LocalizationManager.GetString("StartNewWork_Currency_USD"));

            // Восстанавливаем выбранные значения (если они были)
            if (!string.IsNullOrEmpty(selectedType) && cboType.Items.Contains(selectedType))
                cboType.Text = selectedType;
            if (!string.IsNullOrEmpty(selectedCategory) && cboCategory.Items.Contains(selectedCategory))
                cboCategory.Text = selectedCategory;
            if (!string.IsNullOrEmpty(selectedCurrency) && cboCurrency.Items.Contains(selectedCurrency))
                cboCurrency.Text = selectedCurrency;
        }

        public void ApplyTheme()
        {
            ThemeManager.ReloadSettings();

            if (ThemeManager.IsDogTheme)
            {
                PictureCat.Image = Properties.Resources.pictureDogHelperSmaller;
                lblMessage.Text = "Ррраф! Для начала работы введите данные об операциях, и они будут записаны в таблицу!";
            }
            else
            {
                PictureCat.Image = Properties.Resources.pictureCatHelperSmaller;
                lblMessage.Text = "Мяу! Для начала работы введите данные об операциях, и они будут записаны в таблицу!";
            }
        }

        public class DataTransferEventArgs : EventArgs
        {
            public List<ObjectOfAnalysis> OperationsData { get; set; }
            public DataTransferEventArgs(List<ObjectOfAnalysis> data)
            {
                OperationsData = data;
            }
        }

        public List<ObjectOfAnalysis> GetOperations()
        {
            return _operations;
        }

        private void DgvOperations_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string columnName = dgvOperations.Columns[e.ColumnIndex].Name;
                if (columnName == "colAmount" || columnName == "colType" ||
                    columnName == "colCategory" || columnName == "colDate")
                {
                    _bindingSource.ResetBindings(false);
                    UpdateRowNumbers();
                    UpdateButtonsState();
                    OnDataChanged();
                }
            }
        }

        private void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }

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

        private void SetupInputLimits()
        {
            cboCategory.MaxLength = 50;
            cboType.MaxLength = 50;
            numAmount.Maximum = decimal.MaxValue;
            numAmount.Minimum = -(decimal.MaxValue);
        }

        private void SetupDataGridViewColumns()
        {
            dgvOperations.AutoGenerateColumns = false;
            dgvOperations.Columns.Clear();

            DataGridViewTextBoxColumn colNumber = new DataGridViewTextBoxColumn();
            colNumber.Name = "colNumber";
            colNumber.HeaderText = "№";
            colNumber.ReadOnly = true;
            colNumber.Width = 50;
            colNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOperations.Columns.Add(colNumber);

            DataGridViewTextBoxColumn colAmount = new DataGridViewTextBoxColumn();
            colAmount.Name = "colAmount";
            colAmount.HeaderText = "Размер";
            colAmount.DataPropertyName = "Sum";
            colAmount.DefaultCellStyle.Format = "N2";
            colAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOperations.Columns.Add(colAmount);

            DataGridViewTextBoxColumn colType = new DataGridViewTextBoxColumn();
            colType.Name = "colType";
            colType.HeaderText = "Тип";
            colType.DataPropertyName = "TypeOfOperation";
            dgvOperations.Columns.Add(colType);

            DataGridViewTextBoxColumn colCategory = new DataGridViewTextBoxColumn();
            colCategory.Name = "colCategory";
            colCategory.HeaderText = "Категория";
            colCategory.DataPropertyName = "Category";
            dgvOperations.Columns.Add(colCategory);

            DataGridViewTextBoxColumn colCurrency = new DataGridViewTextBoxColumn();
            colCurrency.Name = "colCurrency";
            colCurrency.HeaderText = "Валюта";
            colCurrency.DataPropertyName = "Currency";
            colCurrency.ReadOnly = true;
            dgvOperations.Columns.Add(colCurrency);

            DataGridViewTextBoxColumn colDate = new DataGridViewTextBoxColumn();
            colDate.Name = "colDate";
            colDate.HeaderText = "Дата";
            colDate.DataPropertyName = "Date";
            colDate.DefaultCellStyle.Format = "dd.MM.yyyy";
            dgvOperations.Columns.Add(colDate);
        }

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

        private void SetupDataGridViewEvents()
        {
            dgvOperations.SelectionChanged += DataGridView1_SelectionChanged;
            dgvOperations.RowsAdded += (s, e) => UpdateRowNumbers();
            dgvOperations.RowsRemoved += (s, e) =>
            {
                UpdateRowNumbers();
                UpdateButtonsState();
                OnDataChanged();
            };
            dgvOperations.DataError += dgvOperations_DataError;
        }

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

        private void ShowTemporaryMessage(string message)
        {
            _messageTimer.Stop();
            lblMessage.Text = message;
            _messageTimer.Start();
        }

        private void MessageTimer_Tick(object sender, EventArgs e)
        {
            _messageTimer.Stop();
            lblMessage.Text = _originalLabel;
        }

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

        private bool ValidateInputs()
        {
            if (numAmount.Value == 0)
            {
                ShowTemporaryMessage($"{ThemeManager.SoundSad} Размер операции не может равняться нулю!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(cboType.Text))
            {
                ShowTemporaryMessage($"{ThemeManager.SoundAlt}... Пожалуйста, выберите тип операции!");
                return false;
            }

            if (cboType.Text.Length > 50)
            {
                ShowTemporaryMessage($"{ThemeManager.Sound}! Тип операции не может быть длиннее 50 символов!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(cboCategory.Text))
            {
                ShowTemporaryMessage($"{ThemeManager.SoundAlt}... Пожалуйста, выберите категорию!");
                return false;
            }

            if (cboCategory.Text.Length > 50)
            {
                ShowTemporaryMessage($"{ThemeManager.Sound}! Категория не может быть длиннее 50 символов!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(cboCurrency.Text))
            {
                ShowTemporaryMessage($"{ThemeManager.SoundAlt}... Пожалуйста, выберите валюту!");
                return false;
            }

            return true;
        }

        private void AddOperation()
        {
            if (_operations.Count >= _maxOperations)
            {
                ShowTemporaryMessage($"{ThemeManager.SoundSad} Извините, но нельзя добавить более {_maxOperations} операций!");
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
            ShowTemporaryMessage($"{ThemeManager.SoundHappy} Новая операция успешно добавлена!");
            OnDataChanged();
        }

        private void UpdateOperation()
        {
            if (_operations.Count == 0)
            {
                ShowTemporaryMessage($"{ThemeManager.SoundSad} Нет операций для изменения! Сначала добавьте операцию.");
                return;
            }

            if (_bindingSource.Current == null)
            {
                ShowTemporaryMessage($"{ThemeManager.SoundSad} Сначала выберите операцию для изменения!");
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
            ShowTemporaryMessage($"{ThemeManager.SoundAlt}! Операция была успешно изменена!");
            OnDataChanged();
        }

        private void DeleteOperation()
        {
            if (_operations.Count == 0)
            {
                ShowTemporaryMessage($"{ThemeManager.SoundSad} Нет операций для удаления! Сначала добавьте операцию.");
                return;
            }

            if (_bindingSource.Current == null)
            {
                ShowTemporaryMessage($"{ThemeManager.SoundSad} Сначала выберите операцию для удаления!");
                return;
            }

            DialogResult result = MessageBox.Show(
                $"{ThemeManager.SoundAlt}... Вы уверены, что хотите удалить эту операцию?",
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
                ShowTemporaryMessage($"{ThemeManager.Sound}! Операция удалена!");
                OnDataChanged();
            }
        }

        private void FinishEntering()
        {
            if (_operations.Count == 0)
            {
                MessageBox.Show($"{ThemeManager.SoundSad} Нет операций для анализа! Пожалуйста, добавьте хотя бы одну операцию.",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTransferEventArgs args = new DataTransferEventArgs(_operations);
            NavigateToGetAnalysis?.Invoke(this, args);
        }

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

        public void ClearAllData()
        {
            _operations.Clear();
            _bindingSource.ResetBindings(false);
            UpdateRowNumbers();
            UpdateButtonsState();
            ClearInputFields();
        }

        private void dgvOperations_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            ShowTemporaryMessage($"{ThemeManager.Sound}! Неверный формат данных! Для даты используйте формат ДД.ММ.ГГГГ");

            e.ThrowException = false;
            dgvOperations.CancelEdit();
            dgvOperations.ClearSelection();
        }

        public void LoadData(List<ObjectOfAnalysis> operations)
        {
            if (operations == null || operations.Count == 0)
                return;

            ClearAllData();

            foreach (ObjectOfAnalysis operation in operations)
            {
                _operations.Add(operation);
            }

            _bindingSource.ResetBindings(false);
            UpdateRowNumbers();
            UpdateButtonsState();

            ShowTemporaryMessage($"{ThemeManager.SoundHappy} Загружено {_operations.Count} операций из проекта!");
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
    }
}
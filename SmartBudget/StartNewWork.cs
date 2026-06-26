using SmartBudget.ClassLibrary;

namespace SmartBudget
{
    public partial class StartNewWork : UserControl
    {
        private string _originalLabel;
        private readonly System.Windows.Forms.Timer _messageTimer;
        private readonly List<ObjectOfAnalysis> _operations;
        private readonly BindingSource _bindingSource;
        private const int _maxDropdownItems = 7;
        private const int _timerInterval = 5000;
        private const int _maxOperations = 100;
        private bool _isWelcomeMessage = true;

        public event EventHandler NavigateToHome;
        public event EventHandler<DataTransferEventArgs> NavigateToGetAnalysis;
        public event EventHandler DataChanged;

        public StartNewWork()
        {
            InitializeComponent();
            ApplyTheme();
            ApplyLocalization();

            dtpDate.MaxDate = DateTime.Today;
            dtpDate.Value = DateTime.Today;
            _originalLabel = lblMessage.Text;
            _isWelcomeMessage = true;

            _messageTimer = new System.Windows.Forms.Timer
            {
                Interval = _timerInterval
            };
            _messageTimer.Tick += MessageTimer_Tick;

            _operations = [];
            _bindingSource = new BindingSource
            {
                DataSource = _operations
            };

            SetupDataGridViewColumns();
            SetupDataGridViewEvents();

            dgvOperations.DataSource = _bindingSource;

            ClearInputFields();
            SetupInputLimits();
            UpdateButtonsState();

            dgvOperations.CellEndEdit += DgvOperations_CellEndEdit;

            StyleDataGridView();
        }

        public void ApplyLocalization()
        {
            // Основные лейблы
            lblAmount.Text = LocalizationManager.GetString("StartNewWork_Amount");
            lblType.Text = LocalizationManager.GetString("StartNewWork_Type");
            lblCategory.Text = LocalizationManager.GetString("StartNewWork_Category");
            lblCurrency.Text = LocalizationManager.GetString("StartNewWork_Currency");
            lblDate.Text = LocalizationManager.GetString("StartNewWork_Date");
            btnAdd.Text = LocalizationManager.GetString("StartNewWork_Add");
            btnChange.Text = LocalizationManager.GetString("StartNewWork_Change");
            btnDelete.Text = LocalizationManager.GetString("StartNewWork_Delete");
            btnDone.Text = LocalizationManager.GetString("StartNewWork_Done");

            UpdateDataGridViewHeaders(); // Обновляем заголовки колонок
            UpdateDropdowns(); // Обновляем выпадающие списки
            ApplyTheme(); // Принимаем настройки
        }

        private void UpdateDataGridViewHeaders()
        {
            if (dgvOperations.Columns.Count >= 6)
            {
                dgvOperations.Columns["colNumber"].HeaderText = LocalizationManager.GetString("StartNewWork_Column_Number");
                dgvOperations.Columns["colAmount"].HeaderText = LocalizationManager.GetString("StartNewWork_Column_Amount");
                dgvOperations.Columns["colType"].HeaderText = LocalizationManager.GetString("StartNewWork_Column_Type");
                dgvOperations.Columns["colCategory"].HeaderText = LocalizationManager.GetString("StartNewWork_Column_Category");
                dgvOperations.Columns["colCurrency"].HeaderText = LocalizationManager.GetString("StartNewWork_Column_Currency");
                dgvOperations.Columns["colDate"].HeaderText = LocalizationManager.GetString("StartNewWork_Column_Date");
            }
        }

        private void UpdateDropdowns()
        {
            string selectedType = cboType.Text;
            string selectedCategory = cboCategory.Text;
            string selectedCurrency = cboCurrency.Text;

            cboType.Items.Clear();
            cboType.Items.Add(LocalizationManager.GetString("StartNewWork_Type_Replenishment"));
            cboType.Items.Add(LocalizationManager.GetString("StartNewWork_Type_Transfer"));
            cboType.Items.Add(LocalizationManager.GetString("StartNewWork_Type_Withdrawal"));
            cboType.Items.Add(LocalizationManager.GetString("StartNewWork_Type_WriteOff"));

            cboCategory.Items.Clear();
            cboCategory.Items.Add(LocalizationManager.GetString("StartNewWork_Category_Food"));
            cboCategory.Items.Add(LocalizationManager.GetString("StartNewWork_Category_Cafe"));
            cboCategory.Items.Add(LocalizationManager.GetString("StartNewWork_Category_Transport"));
            cboCategory.Items.Add(LocalizationManager.GetString("StartNewWork_Category_Delivery"));
            cboCategory.Items.Add(LocalizationManager.GetString("StartNewWork_Category_Clothes"));
            cboCategory.Items.Add(LocalizationManager.GetString("StartNewWork_Category_Electronics"));

            cboCurrency.Items.Clear();
            cboCurrency.Items.Add(LocalizationManager.GetString("StartNewWork_Currency_RUB"));
            cboCurrency.Items.Add(LocalizationManager.GetString("StartNewWork_Currency_USD"));

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
                PictureCat.Image = Properties.Resources.pictureDogHelperSmaller;
            else
                PictureCat.Image = Properties.Resources.pictureCatHelperSmaller;

            if (_isWelcomeMessage)
                UpdateWelcomeMessage();
        }

        private void UpdateWelcomeMessage()
        {
            string welcomeText = LocalizationManager.GetString("StartNewWork_Welcome");
            string currentLang = LocalizationManager.GetCurrentLanguage();

            if (ThemeManager.IsDogTheme)
            {
                if (currentLang == "English")
                    lblMessage.Text = welcomeText.Replace("Meow!", "Woof!").Replace("For expenses, write a negative amount, meow!", "For expenses, write a negative amount, woof!");
                else
                    lblMessage.Text = welcomeText
                        .Replace("Мяу!", "Ррраф!")
                        .Replace("Для расходов записывайте отрицательный размер операции, мяу!", "Для расходов записывайте отрицательный размер операции, гаф!");
            }

            else
                lblMessage.Text = welcomeText;

            _originalLabel = lblMessage.Text;
            _isWelcomeMessage = true;
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
                if (columnName == "colAmount" || columnName == "colType" || columnName == "colCategory" || columnName == "colDate")
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

        private static void AddToDropdownWithLimit(ComboBox comboBox, string newItem)
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

            DataGridViewTextBoxColumn colNumber = new()
            {
                Name = "colNumber",
                HeaderText = LocalizationManager.GetString("StartNewWork_Column_Number"),
                ReadOnly = true,
                Width = 50
            };
            colNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOperations.Columns.Add(colNumber);

            DataGridViewTextBoxColumn colAmount = new()
            {
                Name = "colAmount",
                HeaderText = LocalizationManager.GetString("StartNewWork_Column_Amount"),
                DataPropertyName = "Sum"
            };
            colAmount.DefaultCellStyle.Format = "N2";
            colAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOperations.Columns.Add(colAmount);

            DataGridViewTextBoxColumn colType = new()
            {
                Name = "colType",
                HeaderText = LocalizationManager.GetString("StartNewWork_Column_Type"),
                DataPropertyName = "TypeOfOperation"
            };
            dgvOperations.Columns.Add(colType);

            DataGridViewTextBoxColumn colCategory = new()
            {
                Name = "colCategory",
                HeaderText = LocalizationManager.GetString("StartNewWork_Column_Category"),
                DataPropertyName = "Category"
            };
            dgvOperations.Columns.Add(colCategory);

            DataGridViewTextBoxColumn colCurrency = new()
            {
                Name = "colCurrency",
                HeaderText = LocalizationManager.GetString("StartNewWork_Column_Currency"),
                DataPropertyName = "Currency",
                ReadOnly = true
            };
            dgvOperations.Columns.Add(colCurrency);

            DataGridViewTextBoxColumn colDate = new()
            {
                Name = "colDate",
                HeaderText = LocalizationManager.GetString("StartNewWork_Column_Date"),
                DataPropertyName = "Date"
            };
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
            dgvOperations.DataError += DgvOperations_DataError;
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
            _isWelcomeMessage = false;
            _messageTimer.Start();
        }

        private void MessageTimer_Tick(object sender, EventArgs e)
        {
            _messageTimer.Stop();
            _isWelcomeMessage = true;
            UpdateWelcomeMessage();
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

        private static string GetLocalizedSound()
        {
            string currentLang = LocalizationManager.GetCurrentLanguage();
            if (currentLang == "English")
            {
                return ThemeManager.IsDogTheme ? "Woof" : "Meow";
            }
            else
            {
                return ThemeManager.IsDogTheme ? "Гав" : "Мяу";
            }
        }

        private static string GetLocalizedSoundAlt()
        {
            string currentLang = LocalizationManager.GetCurrentLanguage();
            if (currentLang == "English")
            {
                return ThemeManager.IsDogTheme ? "Woof..." : "Meow...";
            }
            else
            {
                return ThemeManager.IsDogTheme ? "Гав-гав..." : "Мяу...";
            }
        }

        private static string GetLocalizedSoundHappy()
        {
            string currentLang = LocalizationManager.GetCurrentLanguage();
            if (currentLang == "English")
            {
                return ThemeManager.IsDogTheme ? "Woof-woof!" : "Meow-meow!";
            }
            else
            {
                return ThemeManager.IsDogTheme ? "Ррраф-ррраф!" : "Муррр!";
            }
        }

        private bool ValidateInputs()
        {
            string sound = GetLocalizedSound();
            string soundAlt = GetLocalizedSoundAlt();

            if (numAmount.Value == 0)
            {
                ShowTemporaryMessage($"{soundAlt} {LocalizationManager.GetString("StartNewWork_Message_AmountZero")}");
                return false;
            }

            if (string.IsNullOrWhiteSpace(cboType.Text))
            {
                ShowTemporaryMessage($"{soundAlt} {LocalizationManager.GetString("StartNewWork_Message_SelectType")}");
                return false;
            }

            if (cboType.Text.Length > 50)
            {
                ShowTemporaryMessage($"{sound}! {LocalizationManager.GetString("StartNewWork_Message_TypeMaxLength")}");
                return false;
            }

            if (string.IsNullOrWhiteSpace(cboCategory.Text))
            {
                ShowTemporaryMessage($"{soundAlt} {LocalizationManager.GetString("StartNewWork_Message_SelectCategory")}");
                return false;
            }

            if (cboCategory.Text.Length > 50)
            {
                ShowTemporaryMessage($"{sound}! {LocalizationManager.GetString("StartNewWork_Message_CategoryMaxLength")}");
                return false;
            }

            if (string.IsNullOrWhiteSpace(cboCurrency.Text))
            {
                ShowTemporaryMessage($"{soundAlt} {LocalizationManager.GetString("StartNewWork_Message_SelectCurrency")}");
                return false;
            }

            return true;
        }

        private void AddOperation()
        {
            string soundSad = GetLocalizedSoundAlt();
            string soundHappy = GetLocalizedSoundHappy();

            if (_operations.Count >= _maxOperations)
            {
                ShowTemporaryMessage($"{soundSad} {LocalizationManager.GetString("StartNewWork_Message_MaxOperations", _maxOperations)}");
                return;
            }

            if (!ValidateInputs()) return;

            string newType = cboType.Text.Trim();
            string newCategory = cboCategory.Text.Trim();

            ObjectOfAnalysis newOperation = new((float)numAmount.Value, newType, newCategory, cboCurrency.Text, dtpDate.Value);

            _operations.Add(newOperation);
            _bindingSource.ResetBindings(false);
            UpdateRowNumbers();
            UpdateButtonsState();

            AddToDropdownWithLimit(cboType, newType);
            AddToDropdownWithLimit(cboCategory, newCategory);

            ClearInputFields();
            ShowTemporaryMessage($"{soundHappy} {LocalizationManager.GetString("StartNewWork_Message_AddSuccess")}");
            OnDataChanged();
        }

        private void UpdateOperation()
        {
            string soundSad = GetLocalizedSoundAlt();
            string soundAlt = GetLocalizedSoundAlt();

            if (_operations.Count == 0)
            {
                ShowTemporaryMessage($"{soundSad} {LocalizationManager.GetString("StartNewWork_Message_NoDataForChange")}");
                return;
            }

            if (_bindingSource.Current == null)
            {
                ShowTemporaryMessage($"{soundSad} {LocalizationManager.GetString("StartNewWork_Message_SelectForChange")}");
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
            ShowTemporaryMessage($"{soundAlt} {LocalizationManager.GetString("StartNewWork_Message_ChangeSuccess")}");
            OnDataChanged();
        }

        private void DeleteOperation()
        {
            string soundSad = GetLocalizedSoundAlt();
            string soundAlt = GetLocalizedSoundAlt();

            if (_operations.Count == 0)
            {
                ShowTemporaryMessage($"{soundSad} {LocalizationManager.GetString("StartNewWork_Message_NoDataForDelete")}");
                return;
            }

            if (_bindingSource.Current == null)
            {
                ShowTemporaryMessage($"{soundSad} {LocalizationManager.GetString("StartNewWork_Message_SelectForDelete")}");
                return;
            }

            DialogResult result = MessageBox.Show($"{soundAlt} {LocalizationManager.GetString("StartNewWork_Message_DeleteConfirm")}", LocalizationManager.GetString("Dialog_Title_Delete"), MessageBoxButtons.YesNo,  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _operations.Remove((ObjectOfAnalysis)_bindingSource.Current);
                _bindingSource.ResetBindings(false);
                UpdateRowNumbers();
                UpdateButtonsState();
                ClearInputFields();
                string sound = GetLocalizedSound();
                ShowTemporaryMessage($"{sound}! {LocalizationManager.GetString("StartNewWork_Message_DeleteSuccess")}");
                OnDataChanged();
            }
        }

        private void FinishEntering()
        {
            string soundSad = GetLocalizedSoundAlt();

            if (_operations.Count == 0)
            {
                MessageBox.Show($"{soundSad} {LocalizationManager.GetString("StartNewWork_Message_NoData")}", LocalizationManager.GetString("Dialog_Title_Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTransferEventArgs args = new(_operations);
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

        private void DgvOperations_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            string sound = GetLocalizedSound();
            ShowTemporaryMessage($"{sound}! {LocalizationManager.GetString("StartNewWork_Message_DataError")}");

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

            string soundHappy = GetLocalizedSoundHappy();
            ShowTemporaryMessage($"{soundHappy} {LocalizationManager.GetString("StartNewWork_Message_LoadSuccess", _operations.Count)}");
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddOperation();
        }

        private void BtnChange_Click(object sender, EventArgs e)
        {
            UpdateOperation();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteOperation();
        }

        private void BtnDone_Click(object sender, EventArgs e)
        {
            FinishEntering();
        }

        private void IconOpenMenu_Click(object sender, EventArgs e)
        {
            NavigateToHome?.Invoke(this, EventArgs.Empty);
        }
    }
}
using ScottPlot;
using SmartBudget.ClassLibrary;
using Newtonsoft.Json;
using System.Text;
using System.Reflection;

namespace SmartBudget
{
    public partial class GetAnalys : UserControl
    {
        private List<ObjectOfAnalysis> _operationsData;
        private Button _activeButton;
        private bool _isProjectSaved = false;
        private float _dollarRate = 80;
        private bool _isWelcomeMessage = true;

        // Поля для хранения вычисленных данных отчета
        private int _totalCount;
        private float _balance, _expenses, _incomes;
        private string _topIncomeCategory, _topExpenseCategory;
        private string _topIncomeType, _topExpenseType;
        private Dictionary<string, float> _incomeCategoryShares;
        private Dictionary<string, float> _expenseCategoryShares;
        private Dictionary<string, float> _incomeTypeShares;
        private Dictionary<string, float> _expenseTypeShares;
        private Dictionary<string, (float TotalAmount, int Count, float Share)> _expenseStructure;
        private (float AverageDaily, float MaxDaily, float MinDaily, int ActiveDays) _dailyStats;
        private (DateTime Date, float Amount) _topDay;
        private Dictionary<string, float> _incomeByCurrency;
        private Dictionary<string, float> _expenseByCurrency;
        private Dictionary<string, int> _countByCurrency;
        private Dictionary<string, float> _monthlyDynamics;

        private StartNewWork _startNewWorkScreen;

        public event EventHandler NavigateToChangeData;
        public event EventHandler NavigateToHome;

        public GetAnalys()
        {
            InitializeComponent();

            LoadDollarRate();
            ApplyTheme();
            ApplyLocalization();

            pnlMain.AutoScroll = true;
            pnlMain.VerticalScroll.Enabled = false;
            pnlMain.VerticalScroll.Visible = false;
            pnlMain.HorizontalScroll.Enabled = true;
            pnlMain.HorizontalScroll.Visible = true;

            dgvTable.Visible = true;
            formsPlot.Visible = false;
            _activeButton = btnTable;
            btnTable.BackColor = SystemColors.Control;

            formsPlot.UserInputProcessor.Disable();

            SetupDataGridViewColumns();
            SetupDataGridViewEvents();
            StyleDataGridView();

            UpdateButtonStyles(btnTable);
        }

        public void ApplyLocalization()
        {
            lblReportHeader.Text = LocalizationManager.GetString("GetAnalys_ReportHeader");
            lblChartTypesHeader.Text = LocalizationManager.GetString("GetAnalys_ChartTypes");
            lblActionsHeader.Text = LocalizationManager.GetString("GetAnalys_Actions");
            btnSaveReport.Text = LocalizationManager.GetString("GetAnalys_SaveReport");
            btnBackToData.Text = LocalizationManager.GetString("GetAnalys_BackToData");
            btnTable.Text = LocalizationManager.GetString("GetAnalys_Table");
            btnGraph.Text = LocalizationManager.GetString("GetAnalys_Graph");
            btnCircleDiagram.Text = LocalizationManager.GetString("GetAnalys_CircleDiagram");
            btnScatterPlot.Text = LocalizationManager.GetString("GetAnalys_ScatterPlot");
            btnGistogram.Text = LocalizationManager.GetString("GetAnalys_Gistogram");
            btnRadarDiagram.Text = LocalizationManager.GetString("GetAnalys_RadarDiagram");

            UpdateDataGridViewHeaders();
            ApplyTheme();
        }

        private void UpdateDataGridViewHeaders()
        {
            if (dgvTable.Columns.Count >= 6)
            {
                dgvTable.Columns["colNumber"].HeaderText = LocalizationManager.GetString("StartNewWork_Column_Number");
                dgvTable.Columns["colAmount"].HeaderText = LocalizationManager.GetString("StartNewWork_Column_Amount");
                dgvTable.Columns["colType"].HeaderText = LocalizationManager.GetString("StartNewWork_Column_Type");
                dgvTable.Columns["colCategory"].HeaderText = LocalizationManager.GetString("StartNewWork_Column_Category");
                dgvTable.Columns["colCurrency"].HeaderText = LocalizationManager.GetString("StartNewWork_Column_Currency");
                dgvTable.Columns["colDate"].HeaderText = LocalizationManager.GetString("StartNewWork_Column_Date");
            }
        }

        public void ApplyTheme()
        {
            ThemeManager.ReloadSettings();

            if (ThemeManager.IsDogTheme)
            {
                pictureBox1.Image = Properties.Resources.pictureDogHelperSmaller;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.pictureCatHelperSmaller;
            }

            if (_isWelcomeMessage)
            {
                UpdateWelcomeMessage();
            }
        }

        private void UpdateWelcomeMessage()
        {
            string welcomeText = LocalizationManager.GetString("GetAnalys_Message");
            string currentLang = LocalizationManager.GetCurrentLanguage();

            if (ThemeManager.IsDogTheme)
            {
                if (currentLang == "English")
                {
                    lblMessage.Text = welcomeText
                        .Replace("Meow!", "Woof!")
                        .Replace("meow!", "woof!");
                }
                else
                {
                    lblMessage.Text = welcomeText
                        .Replace("Мяу!", "Ррраф!")
                        .Replace("мур!", "ррраф!");
                }
            }
            else
            {
                lblMessage.Text = welcomeText;
            }

            _isWelcomeMessage = true;
        }

        private void ShowTemporaryMessage(string message)
        {
            lblMessage.Text = message;
            _isWelcomeMessage = false;
        }

        private string GetLocalizedSound(string soundKey)
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

        private string GetLocalizedSoundAlt()
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

        private string GetLocalizedSoundHappy()
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

        private string GetLocalizedSoundQuestion()
        {
            string currentLang = LocalizationManager.GetCurrentLanguage();
            if (currentLang == "English")
            {
                return ThemeManager.IsDogTheme ? "Woof?" : "Meow?";
            }
            else
            {
                return ThemeManager.IsDogTheme ? "Гав?" : "Мяу?";
            }
        }

        private void LoadDollarRate()
        {
            try
            {
                SettingsService settings = SettingsService.LoadSettings();
                if (settings != null && settings.DollarValue > 0)
                {
                    _dollarRate = settings.DollarValue;
                }
            }
            catch
            {
                _dollarRate = 80;
            }
        }

        private float ConvertToRubles(float sum, string currency)
        {
            if (string.IsNullOrEmpty(currency))
                return sum;

            string currencyLower = currency.ToLower().Trim();

            if (currencyLower.Contains("доллар") ||
                currencyLower.Contains("usd") ||
                currencyLower.Contains("$") ||
                currencyLower == "доллар сша")
            {
                return sum * _dollarRate;
            }

            return sum;
        }

        public void RefreshData(List<ObjectOfAnalysis> operations)
        {
            if (operations == null || operations.Count == 0)
                return;

            _operationsData = operations;
            _isProjectSaved = false;

            LoadDollarRate();

            CalculateAllStatistics();
            GenerateReport();

            if (_activeButton == btnTable)
            {
                ShowTable();
            }
            else
            {
                RefreshCurrentChart();
            }
        }

        private void RefreshCurrentChart()
        {
            if (_activeButton == btnGraph)
                btnGraph_Click(this, EventArgs.Empty);
            else if (_activeButton == btnCircleDiagram)
                btnCircleDiagram_Click(this, EventArgs.Empty);
            else if (_activeButton == btnScatterPlot)
                btnScatterPlot_Click(this, EventArgs.Empty);
            else if (_activeButton == btnGistogram)
                btnGistogram_Click(this, EventArgs.Empty);
            else if (_activeButton == btnRadarDiagram)
                btnRadarDiagram_Click(this, EventArgs.Empty);
        }

        public void GetData(List<ObjectOfAnalysis> operations)
        {
            _operationsData = operations;
            _isProjectSaved = false;

            LoadDollarRate();

            if (_operationsData != null && _operationsData.Count > 0)
            {
                CalculateAllStatistics();
                GenerateReport();
                ShowTable();
            }
        }

        #region Методы для базового анализа данных (расчет статистики)

        private void CalculateAllStatistics()
        {
            if (_operationsData == null || _operationsData.Count == 0) return;

            List<ObjectOfAnalysis> convertedData = new List<ObjectOfAnalysis>();
            foreach (ObjectOfAnalysis op in _operationsData)
            {
                float convertedSum = ConvertToRubles(op.Sum, op.Currency);
                convertedData.Add(new ObjectOfAnalysis(
                    convertedSum,
                    op.TypeOfOperation,
                    op.Category,
                    "Рубль",
                    op.Date
                ));
            }

            _totalCount = OperationCount(_operationsData);
            (_balance, _expenses, _incomes) = OperationCalculateSummary(convertedData);
            (_topIncomeCategory, _topExpenseCategory) = GetTopCategories(_operationsData);
            (_topIncomeType, _topExpenseType) = GetTopTypes(_operationsData);
            (_incomeCategoryShares, _expenseCategoryShares) = GetCategoryShares(convertedData);
            (_incomeTypeShares, _expenseTypeShares) = GetTypeShares(convertedData);
            _expenseStructure = GetExpenseStructure(convertedData);
            _dailyStats = GetDailyStatistics(convertedData);
            _topDay = GetTopDay(convertedData);
            (_incomeByCurrency, _expenseByCurrency, _countByCurrency) = GetCurrencyStatistics(_operationsData);
            _monthlyDynamics = GetMonthlyDynamics(convertedData);
        }

        private static int OperationCount(List<ObjectOfAnalysis> operations)
        {
            return operations.Count;
        }

        private static (float Balance, float Expenses, float Incomes) OperationCalculateSummary(List<ObjectOfAnalysis> operations)
        {
            if (operations == null || operations.Count == 0)
                return (0, 0, 0);

            float incomes = operations.Where(o => o.Sum > 0).Sum(o => o.Sum);
            float expenses = operations.Where(o => o.Sum < 0).Sum(o => o.Sum);
            float balance = incomes + expenses;

            return (balance, expenses, incomes);
        }

        private static (string topIncomeCategory, string topExpenseCategory) GetTopCategories(List<ObjectOfAnalysis> operations)
        {
            if (operations == null || operations.Count == 0)
                return (LocalizationManager.GetString("Report_NoIncomeData"), LocalizationManager.GetString("Report_NoExpenseData"));

            Dictionary<string, int> incomeCategories = new Dictionary<string, int>();
            Dictionary<string, int> expenseCategories = new Dictionary<string, int>();

            foreach (ObjectOfAnalysis operation in operations)
            {
                if (operation.Sum > 0)
                {
                    incomeCategories.TryGetValue(operation.Category, out int count);
                    incomeCategories[operation.Category] = count + 1;
                }
                else if (operation.Sum < 0)
                {
                    expenseCategories.TryGetValue(operation.Category, out int count);
                    expenseCategories[operation.Category] = count + 1;
                }
            }

            string topIncomeCategory = LocalizationManager.GetString("Report_NoIncomeData");
            int maxIncomeCount = 0;
            foreach (KeyValuePair<string, int> pair in incomeCategories)
            {
                if (pair.Value > maxIncomeCount)
                {
                    maxIncomeCount = pair.Value;
                    topIncomeCategory = pair.Key;
                }
            }

            string topExpenseCategory = LocalizationManager.GetString("Report_NoExpenseData");
            int maxExpenseCount = 0;
            foreach (KeyValuePair<string, int> pair in expenseCategories)
            {
                if (pair.Value > maxExpenseCount)
                {
                    maxExpenseCount = pair.Value;
                    topExpenseCategory = pair.Key;
                }
            }

            return (topIncomeCategory, topExpenseCategory);
        }

        private static (string TopIncomeType, string TopExpenseType) GetTopTypes(List<ObjectOfAnalysis> operations)
        {
            if (operations == null || operations.Count == 0)
                return (LocalizationManager.GetString("Report_NoIncomeData"), LocalizationManager.GetString("Report_NoExpenseData"));

            Dictionary<string, int> incomeTypes = new Dictionary<string, int>();
            Dictionary<string, int> expenseTypes = new Dictionary<string, int>();

            foreach (ObjectOfAnalysis operation in operations)
            {
                if (operation.Sum > 0)
                {
                    incomeTypes.TryGetValue(operation.TypeOfOperation, out int count);
                    incomeTypes[operation.TypeOfOperation] = count + 1;
                }
                else if (operation.Sum < 0)
                {
                    expenseTypes.TryGetValue(operation.TypeOfOperation, out int count);
                    expenseTypes[operation.TypeOfOperation] = count + 1;
                }
            }

            string topIncomeType = LocalizationManager.GetString("Report_NoIncomeData");
            int maxIncomeCount = 0;
            foreach (KeyValuePair<string, int> pair in incomeTypes)
            {
                if (pair.Value > maxIncomeCount)
                {
                    maxIncomeCount = pair.Value;
                    topIncomeType = pair.Key;
                }
            }

            string topExpenseType = LocalizationManager.GetString("Report_NoExpenseData");
            int maxExpenseCount = 0;
            foreach (KeyValuePair<string, int> pair in expenseTypes)
            {
                if (pair.Value > maxExpenseCount)
                {
                    maxExpenseCount = pair.Value;
                    topExpenseType = pair.Key;
                }
            }

            return (topIncomeType, topExpenseType);
        }

        private static (Dictionary<string, float> IncomeCategoryShares, Dictionary<string, float> ExpenseCategoryShares) GetCategoryShares(List<ObjectOfAnalysis> operations)
        {
            if (operations == null || operations.Count == 0)
                return (new Dictionary<string, float>(), new Dictionary<string, float>());

            Dictionary<string, float> incomeCategorySums = new Dictionary<string, float>();
            Dictionary<string, float> expenseCategorySums = new Dictionary<string, float>();

            float totalIncomes = 0;
            float totalExpenses = 0;

            foreach (ObjectOfAnalysis operation in operations)
            {
                if (operation.Sum > 0)
                {
                    totalIncomes += (float)operation.Sum;
                    incomeCategorySums.TryGetValue(operation.Category, out float currentSum);
                    incomeCategorySums[operation.Category] = currentSum + (float)operation.Sum;
                }
                else if (operation.Sum < 0)
                {
                    totalExpenses += (float)operation.Sum;
                    expenseCategorySums.TryGetValue(operation.Category, out float currentSum);
                    expenseCategorySums[operation.Category] = currentSum + (float)operation.Sum;
                }
            }

            Dictionary<string, float> incomeShares = new Dictionary<string, float>();
            Dictionary<string, float> expenseShares = new Dictionary<string, float>();

            if (totalIncomes > 0)
            {
                foreach (KeyValuePair<string, float> pair in incomeCategorySums)
                {
                    float share = (pair.Value / totalIncomes) * 100;
                    incomeShares[pair.Key] = (float)Math.Round(share, 2);
                }
            }

            if (totalExpenses < 0)
            {
                float totalExpensesAbs = Math.Abs(totalExpenses);
                foreach (KeyValuePair<string, float> pair in expenseCategorySums)
                {
                    float share = (Math.Abs(pair.Value) / totalExpensesAbs) * 100;
                    expenseShares[pair.Key] = (float)Math.Round(share, 2);
                }
            }

            return (incomeShares, expenseShares);
        }

        private static (Dictionary<string, float> IncomeTypeShares, Dictionary<string, float> ExpenseTypeShares) GetTypeShares(List<ObjectOfAnalysis> operations)
        {
            if (operations == null || operations.Count == 0)
                return (new Dictionary<string, float>(), new Dictionary<string, float>());

            Dictionary<string, float> incomeTypeSums = new Dictionary<string, float>();
            Dictionary<string, float> expenseTypeSums = new Dictionary<string, float>();

            float totalIncomes = 0;
            float totalExpenses = 0;

            foreach (ObjectOfAnalysis operation in operations)
            {
                if (operation.Sum > 0)
                {
                    totalIncomes += (float)operation.Sum;
                    incomeTypeSums.TryGetValue(operation.TypeOfOperation, out float currentSum);
                    incomeTypeSums[operation.TypeOfOperation] = currentSum + (float)operation.Sum;
                }
                else if (operation.Sum < 0)
                {
                    totalExpenses += (float)operation.Sum;
                    expenseTypeSums.TryGetValue(operation.TypeOfOperation, out float currentSum);
                    expenseTypeSums[operation.TypeOfOperation] = currentSum + (float)operation.Sum;
                }
            }

            Dictionary<string, float> incomeShares = new Dictionary<string, float>();
            Dictionary<string, float> expenseShares = new Dictionary<string, float>();

            if (totalIncomes > 0)
            {
                foreach (KeyValuePair<string, float> pair in incomeTypeSums)
                {
                    float share = (pair.Value / totalIncomes) * 100;
                    incomeShares[pair.Key] = (float)Math.Round(share, 2);
                }
            }

            if (totalExpenses < 0)
            {
                float totalExpensesAbs = Math.Abs(totalExpenses);
                foreach (KeyValuePair<string, float> pair in expenseTypeSums)
                {
                    float share = (Math.Abs(pair.Value) / totalExpensesAbs) * 100;
                    expenseShares[pair.Key] = (float)Math.Round(share, 2);
                }
            }

            return (incomeShares, expenseShares);
        }

        private static Dictionary<string, (float TotalAmount, int Count, float Share)> GetExpenseStructure(List<ObjectOfAnalysis> operations)
        {
            if (operations == null || operations.Count == 0)
                return new Dictionary<string, (float TotalAmount, int Count, float Share)>();

            Dictionary<string, (float TotalAmount, int Count)> expenseData = new Dictionary<string, (float TotalAmount, int Count)>();
            float totalExpenses = 0;

            foreach (ObjectOfAnalysis operation in operations)
            {
                if (operation.Sum < 0)
                {
                    totalExpenses += (float)operation.Sum;

                    if (expenseData.TryGetValue(operation.Category, out (float TotalAmount, int Count) value))
                    {
                        expenseData[operation.Category] = (value.TotalAmount + (float)operation.Sum, value.Count + 1);
                    }
                    else
                    {
                        expenseData[operation.Category] = ((float)operation.Sum, 1);
                    }
                }
            }

            Dictionary<string, (float TotalAmount, int Count, float Share)> result = new Dictionary<string, (float TotalAmount, int Count, float Share)>();

            if (totalExpenses < 0)
            {
                float totalExpensesAbs = Math.Abs(totalExpenses);
                foreach (KeyValuePair<string, (float TotalAmount, int Count)> pair in expenseData)
                {
                    float share = (Math.Abs(pair.Value.TotalAmount) / totalExpensesAbs) * 100;
                    result[pair.Key] = (Math.Abs(pair.Value.TotalAmount), pair.Value.Count, (float)Math.Round(share, 2));
                }
            }

            return result;
        }

        private Dictionary<string, float> GetMonthlyDynamics(List<ObjectOfAnalysis> operations)
        {
            if (operations == null || operations.Count == 0)
                return new Dictionary<string, float>();

            Dictionary<string, float> monthlyData = new Dictionary<string, float>();

            foreach (ObjectOfAnalysis operation in operations)
            {
                string monthKey = operation.Date.ToString("MMMM yyyy");
                monthlyData.TryGetValue(monthKey, out float currentSum);
                monthlyData[monthKey] = currentSum + (float)operation.Sum;
            }

            return monthlyData;
        }

        private (Dictionary<string, float> IncomeByCurrency, Dictionary<string, float> ExpenseByCurrency, Dictionary<string, int> OperationCountByCurrency) GetCurrencyStatistics(List<ObjectOfAnalysis> operations)
        {
            if (operations == null || operations.Count == 0)
                return (new Dictionary<string, float>(), new Dictionary<string, float>(), new Dictionary<string, int>());

            Dictionary<string, float> incomeByCurrency = new Dictionary<string, float>();
            Dictionary<string, float> expenseByCurrency = new Dictionary<string, float>();
            Dictionary<string, int> countByCurrency = new Dictionary<string, int>();

            foreach (ObjectOfAnalysis operation in operations)
            {
                string currency = operation.Currency;

                countByCurrency.TryGetValue(currency, out int currentCount);
                countByCurrency[currency] = currentCount + 1;

                if (operation.Sum > 0)
                {
                    incomeByCurrency.TryGetValue(currency, out float currentIncome);
                    incomeByCurrency[currency] = currentIncome + operation.Sum;
                }
                else if (operation.Sum < 0)
                {
                    expenseByCurrency.TryGetValue(currency, out float currentExpense);
                    expenseByCurrency[currency] = currentExpense + operation.Sum;
                }
            }

            return (incomeByCurrency, expenseByCurrency, countByCurrency);
        }

        private (float AverageDaily, float MaxDaily, float MinDaily, int ActiveDays) GetDailyStatistics(List<ObjectOfAnalysis> operations)
        {
            if (operations == null || operations.Count == 0)
                return (0, 0, 0, 0);

            Dictionary<DateTime, float> dailySums = new Dictionary<DateTime, float>();

            foreach (ObjectOfAnalysis operation in operations)
            {
                DateTime date = operation.Date.Date;
                dailySums.TryGetValue(date, out float currentSum);
                dailySums[date] = currentSum + (float)operation.Sum;
            }

            float total = 0;
            float max = float.MinValue;
            float min = float.MaxValue;
            int activeDays = dailySums.Count;

            foreach (KeyValuePair<DateTime, float> pair in dailySums)
            {
                total += pair.Value;
                if (pair.Value > max) max = pair.Value;
                if (pair.Value < min) min = pair.Value;
            }

            float average = activeDays > 0 ? total / activeDays : 0;

            return (average, max, min, activeDays);
        }

        private (DateTime Date, float Amount) GetTopDay(List<ObjectOfAnalysis> operations)
        {
            if (operations == null || operations.Count == 0)
                return (DateTime.MinValue, 0);

            Dictionary<DateTime, float> dailySums = new Dictionary<DateTime, float>();

            foreach (ObjectOfAnalysis operation in operations)
            {
                DateTime date = operation.Date.Date;
                dailySums.TryGetValue(date, out float currentSum);
                dailySums[date] = currentSum + (float)operation.Sum;
            }

            DateTime topDate = DateTime.MinValue;
            float topAmount = 0;

            foreach (KeyValuePair<DateTime, float> pair in dailySums)
            {
                if (pair.Value > topAmount)
                {
                    topAmount = pair.Value;
                    topDate = pair.Key;
                }
            }

            return (topDate, topAmount);
        }

        #endregion

        #region Генерация отчета

        private void GenerateReport()
        {
            if (_operationsData == null || _operationsData.Count == 0)
            {
                rtbReport.Text = LocalizationManager.GetString("GetAnalys_Message_NoDataForAnalysis");
                return;
            }

            rtbReport.Clear();

            string currentLang = LocalizationManager.GetCurrentLanguage();
            string rubSymbol = currentLang == "English" ? "RUB" : "₽";
            string currencyLabel = currentLang == "English" ? "RUB" : "руб.";

            // Заголовок отчета
            rtbReport.Font = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.AppendText($"{LocalizationManager.GetString("Report_DollarRate")}: {_dollarRate:F2} {currencyLabel}\n");
            rtbReport.AppendText($"{LocalizationManager.GetString("Report_YouCanChangeDollarRate")}\n");
            rtbReport.AppendText($"{LocalizationManager.GetString("Report_AllAmountsInRubles")}\n\n");

            // 1. Общая информация
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText(LocalizationManager.GetString("Report_GeneralInfo") + "\n");

            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");
            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText($"{LocalizationManager.GetString("Report_OperationCount")}: {_totalCount}\n");
            rtbReport.AppendText($"{LocalizationManager.GetString("Report_TotalIncome")}: {_incomes:F2} {rubSymbol}\n");
            rtbReport.AppendText($"{LocalizationManager.GetString("Report_TotalExpense")}: {Math.Abs(_expenses):F2} {rubSymbol}\n");
            rtbReport.AppendText($"{LocalizationManager.GetString("Report_Balance")}: {_balance:F2} {rubSymbol}\n");
            rtbReport.AppendText($"{LocalizationManager.GetString("Report_Period")}: {_operationsData.Min(o => o.Date):dd.MM.yyyy} - {_operationsData.Max(o => o.Date):dd.MM.yyyy}\n");
            rtbReport.AppendText($"{LocalizationManager.GetString("Report_ActiveDays")}: {_dailyStats.ActiveDays}\n\n");

            // 2. Самые частые категории
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText(LocalizationManager.GetString("Report_TopCategories") + "\n");
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");
            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText($"{LocalizationManager.GetString("Report_Income")}: {_topIncomeCategory}\n");
            rtbReport.AppendText($"{LocalizationManager.GetString("Report_Expense")}: {_topExpenseCategory}\n\n");

            // 3. Доли категорий в доходах
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText(LocalizationManager.GetString("Report_IncomeCategoryShares") + "\n");
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");
            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            if (_incomeCategoryShares.Count > 0)
            {
                foreach (KeyValuePair<string, float> pair in _incomeCategoryShares)
                {
                    rtbReport.AppendText($"{pair.Key}: {pair.Value:F2}%\n");
                }
            }
            else
            {
                rtbReport.AppendText(LocalizationManager.GetString("Error_NoData") + "\n");
            }
            rtbReport.AppendText("\n");

            // 4. Доли категорий в расходах
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText(LocalizationManager.GetString("Report_ExpenseCategoryShares") + "\n");
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");
            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            if (_expenseCategoryShares.Count > 0)
            {
                foreach (KeyValuePair<string, float> pair in _expenseCategoryShares)
                {
                    rtbReport.AppendText($"{pair.Key}: {pair.Value:F2}%\n");
                }
            }
            else
            {
                rtbReport.AppendText(LocalizationManager.GetString("Error_NoData") + "\n");
            }
            rtbReport.AppendText("\n");

            // 5. Самые частые типы
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText(LocalizationManager.GetString("Report_TopTypes") + "\n");
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");
            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText($"{LocalizationManager.GetString("Report_Income")}: {_topIncomeType}\n");
            rtbReport.AppendText($"{LocalizationManager.GetString("Report_Expense")}: {_topExpenseType}\n\n");

            // 6. Доли типов в доходах
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText(LocalizationManager.GetString("Report_IncomeTypeShares") + "\n");
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");
            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            if (_incomeTypeShares.Count > 0)
            {
                foreach (KeyValuePair<string, float> pair in _incomeTypeShares)
                {
                    rtbReport.AppendText($"{pair.Key}: {pair.Value:F2}%\n");
                }
            }
            else
            {
                rtbReport.AppendText(LocalizationManager.GetString("Error_NoData") + "\n");
            }
            rtbReport.AppendText("\n");

            // 7. Доли типов в расходах
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText(LocalizationManager.GetString("Report_ExpenseTypeShares") + "\n");
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");
            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            if (_expenseTypeShares.Count > 0)
            {
                foreach (KeyValuePair<string, float> pair in _expenseTypeShares)
                {
                    rtbReport.AppendText($"{pair.Key}: {pair.Value:F2}%\n");
                }
            }
            else
            {
                rtbReport.AppendText(LocalizationManager.GetString("Error_NoData") + "\n");
            }
            rtbReport.AppendText("\n");

            // 8. Структура расходов
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText(LocalizationManager.GetString("Report_ExpenseStructure") + "\n");
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");
            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            if (_expenseStructure.Count > 0)
            {
                foreach (KeyValuePair<string, (float TotalAmount, int Count, float Share)> pair in _expenseStructure)
                {
                    rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Bold);
                    rtbReport.AppendText($"{pair.Key}:\n");
                    rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
                    rtbReport.AppendText($"    {LocalizationManager.GetString("Report_Amount")}: {pair.Value.TotalAmount:F2} {rubSymbol}\n");
                    rtbReport.AppendText($"    {LocalizationManager.GetString("Report_OperationCount")}: {pair.Value.Count}\n");
                    rtbReport.AppendText($"    {LocalizationManager.GetString("Report_Expense")}: {pair.Value.Share:F2}%\n");
                    rtbReport.AppendText("\n");
                }
            }
            else
            {
                rtbReport.AppendText(LocalizationManager.GetString("Error_NoData") + "\n\n");
            }

            // 9. Анализ по валютам
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText(LocalizationManager.GetString("Report_CurrencyAnalysis") + "\n");
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");

            foreach (KeyValuePair<string, int> pair in _countByCurrency)
            {
                rtbReport.SelectionFont = new Font("Times New Roman", 14, System.Drawing.FontStyle.Bold);
                rtbReport.AppendText($"{LocalizationManager.GetString("Report_Currency_Column")}: {pair.Key}\n");
                rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
                rtbReport.AppendText($"    {LocalizationManager.GetString("Report_Operations_Column")}: {pair.Value}\n");

                if (_incomeByCurrency.ContainsKey(pair.Key))
                {
                    rtbReport.AppendText($"    {LocalizationManager.GetString("Report_Incomes_Column")}: {_incomeByCurrency[pair.Key]:F2} {pair.Key}\n");
                }

                if (_expenseByCurrency.ContainsKey(pair.Key))
                {
                    rtbReport.AppendText($"    {LocalizationManager.GetString("Report_Expenses_Column")}: {Math.Abs(_expenseByCurrency[pair.Key]):F2} {pair.Key}\n");
                }
                rtbReport.AppendText("\n");
            }

            // 10. Статистика по дням
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText(LocalizationManager.GetString("Report_DailyStatistics") + "\n");
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");
            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText($"{LocalizationManager.GetString("Report_DailyAvg")}: {_dailyStats.AverageDaily:F2} {rubSymbol}\n");
            rtbReport.AppendText($"{LocalizationManager.GetString("Report_DailyMax")}: {_dailyStats.MaxDaily:F2} {rubSymbol}\n");
            rtbReport.AppendText($"{LocalizationManager.GetString("Report_DailyMin")}: {_dailyStats.MinDaily:F2} {rubSymbol}\n");
            rtbReport.AppendText($"{LocalizationManager.GetString("Report_BestDay")}: {_topDay.Date:dd.MM.yyyy} ({LocalizationManager.GetString("Report_Amount")}: {_topDay.Amount:F2} {rubSymbol})\n\n");

            // 11. Динамика по месяцам
            if (_monthlyDynamics.Count > 0)
            {
                rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
                rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
                rtbReport.AppendText(LocalizationManager.GetString("Report_MonthlyDynamics") + "\n");
                rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
                rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
                rtbReport.AppendText("\n");
                rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);

                foreach (KeyValuePair<string, float> pair in _monthlyDynamics)
                {
                    rtbReport.AppendText($"{pair.Key}: {pair.Value:F2} {rubSymbol}\n");
                }
                rtbReport.AppendText("\n\n\n");
            }

            // Итог
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Italic);
            rtbReport.AppendText($"{LocalizationManager.GetString("Report_Generated")} {DateTime.Now:dd.MM.yyyy HH:mm:ss}\n");
            rtbReport.AppendText($"{LocalizationManager.GetString("Report_ByProgram")}\n");
        }

        #endregion

        #region Методы сохранения проекта

        private void btnSaveReport_Click(object sender, EventArgs e)
        {
            string soundSad = GetLocalizedSoundAlt();

            if (_operationsData == null || _operationsData.Count == 0)
            {
                MessageBox.Show($"{soundSad} {LocalizationManager.GetString("GetAnalys_Message_NoData")}",
                    LocalizationManager.GetString("Dialog_Title_Warning"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            List<string> existingProjects = GetAvailableProjects();
            if (existingProjects.Count >= 10)
            {
                string projectsDirectory = GetProjectsDirectory();
                MessageBox.Show(
                    $"{GetLocalizedSound("Warning")}! {LocalizationManager.GetString("GetAnalys_Message_ProjectLimit", projectsDirectory)}",
                    LocalizationManager.GetString("Dialog_Title_Warning"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string projectName = ShowSaveDialog();

            if (string.IsNullOrWhiteSpace(projectName))
                return;

            if (ProjectExists(projectName))
            {
                DialogResult result = MessageBox.Show(
                    $"{GetLocalizedSoundQuestion()} {LocalizationManager.GetString("GetAnalys_Message_ProjectExists", projectName)}",
                    LocalizationManager.GetString("Dialog_Title_Warning"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;
            }

            bool saved = SaveProject(projectName);

            if (saved)
            {
                _isProjectSaved = true;
                string happySound = GetLocalizedSoundHappy();
                MessageBox.Show($"{happySound} {LocalizationManager.GetString("GetAnalys_Message_SaveSuccess", projectName)}",
                    LocalizationManager.GetString("Dialog_Title_Success"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"{soundSad} {LocalizationManager.GetString("GetAnalys_Message_SaveError")}",
                    LocalizationManager.GetString("Dialog_Title_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string ShowSaveDialog()
        {
            List<string> existingProjects = GetAvailableProjects();

            if (existingProjects.Count >= 10)
            {
                string projectsDirectory = GetProjectsDirectory();
                MessageBox.Show(
                    $"{GetLocalizedSound("Warning")}! {LocalizationManager.GetString("GetAnalys_Message_ProjectLimit", projectsDirectory)}",
                    LocalizationManager.GetString("Dialog_Title_Warning"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return null;
            }

            string projectList = existingProjects.Count > 0
                ? $"\n\n{LocalizationManager.GetString("GetAnalys_Message_ExistingProjects", existingProjects.Count)}:\n{string.Join("\n", existingProjects)}\n\n{GetLocalizedSound("Attention")} {LocalizationManager.GetString("GetAnalys_Message_ProjectExistsHint")}"
                : $"\n\n{LocalizationManager.GetString("GetAnalys_Message_NoProjects")}";

            string sound = GetLocalizedSoundAlt();
            string message = $"{sound}! {LocalizationManager.GetString("GetAnalys_Message_SaveDialog")}{projectList}\n\n{LocalizationManager.GetString("GetAnalys_Message_ProjectName")}:";

            string projectName = Microsoft.VisualBasic.Interaction.InputBox(
                message,
                LocalizationManager.GetString("Dialog_Title_SaveProject"),
                "");

            if (string.IsNullOrWhiteSpace(projectName))
            {
                if (projectName != null)
                    MessageBox.Show($"{GetLocalizedSound("Error")}! {LocalizationManager.GetString("GetAnalys_Message_EmptyName")}",
                        LocalizationManager.GetString("Dialog_Title_Error"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                return null;
            }

            return projectName;
        }

        private string GetProjectsDirectory()
        {
            string exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(exeDirectory, "Projects");
        }

        private List<string> GetAvailableProjects()
        {
            List<string> projects = new List<string>();

            try
            {
                string projectsDirectory = GetProjectsDirectory();

                if (!Directory.Exists(projectsDirectory))
                    return projects;

                string[] files = Directory.GetFiles(projectsDirectory, "*.json");

                foreach (string file in files)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    if (!string.IsNullOrWhiteSpace(fileName))
                        projects.Add(fileName);
                }

                projects.Sort();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{GetLocalizedSound("Error")} {LocalizationManager.GetString("Error_LoadProject")}: {ex.Message}",
                    LocalizationManager.GetString("Dialog_Title_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            return projects;
        }

        private bool ProjectExists(string projectName)
        {
            string path = GetProjectPath(projectName);
            return File.Exists(path);
        }

        private string GetProjectPath(string projectName)
        {
            string exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string projectsDirectory = Path.Combine(exeDirectory, "Projects");
            if (!Directory.Exists(projectsDirectory))
            {
                Directory.CreateDirectory(projectsDirectory);
            }

            string fileName = $"{projectName}.json";
            return Path.Combine(projectsDirectory, fileName);
        }

        private bool SaveProject(string projectName)
        {
            try
            {
                string path = GetProjectPath(projectName);

                string json = JsonConvert.SerializeObject(_operationsData, Formatting.Indented);
                File.WriteAllText(path, json, Encoding.UTF8);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{GetLocalizedSound("Error")} {LocalizationManager.GetString("Error_SaveSettings")}: {ex.Message}",
                    LocalizationManager.GetString("Dialog_Title_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        private bool CheckProjectSaved()
        {
            if (_operationsData == null || _operationsData.Count == 0)
                return true;

            if (_isProjectSaved)
                return true;

            string question = GetLocalizedSoundQuestion();
            DialogResult result = MessageBox.Show(
                $"{question} {LocalizationManager.GetString("GetAnalys_Message_NotSaved")}",
                LocalizationManager.GetString("Dialog_Title_Warning"),
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                btnSaveReport_Click(this, EventArgs.Empty);
                return _isProjectSaved;
            }
            else if (result == DialogResult.Cancel)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Методы визуализации

        private void ShowTable()
        {
            if (_operationsData == null || _operationsData.Count == 0)
                return;

            formsPlot.Visible = false;
            dgvTable.Visible = true;
            dgvTable.BringToFront();

            dgvTable.DataSource = null;
            dgvTable.DataSource = _operationsData;
            dgvTable.Refresh();
        }

        private void ResetPlot()
        {
            formsPlot.Plot.Clear();
            formsPlot.Plot.Title("");
            formsPlot.Plot.XLabel("");
            formsPlot.Plot.YLabel("");
            formsPlot.Plot.Grid.MajorLineColor = Colors.Gray.WithAlpha(0.2);

            formsPlot.Plot.Axes.AutoScale();
            formsPlot.Plot.Axes.Rules.Clear();

            formsPlot.Plot.Legend.IsVisible = false;
            formsPlot.Reset();
        }

        private void ShowPlot()
        {
            formsPlot.Visible = true;
            dgvTable.Visible = false;
            formsPlot.BringToFront();

            ResetPlot();
        }

        public void UpdateButtonStyles(Button activeButton)
        {
            Button[] buttons = { btnTable, btnGraph, btnCircleDiagram, btnScatterPlot, btnGistogram, btnRadarDiagram };

            foreach (Button btn in buttons)
            {
                if (btn == activeButton)
                {
                    btn.BackColor = SystemColors.Control;
                    btn.FlatStyle = FlatStyle.Standard;
                    btn.UseVisualStyleBackColor = true;
                }
                else
                {
                    btn.BackColor = System.Drawing.Color.LightGray;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.UseVisualStyleBackColor = false;
                }
            }

            _activeButton = activeButton;
        }

        private bool HasEnoughData(int numberOfOperations)
        {
            if (_operationsData == null || _operationsData.Count < numberOfOperations)
            {
                MessageBox.Show($"{GetLocalizedSoundAlt()} {LocalizationManager.GetString("Error_NotEnoughData", numberOfOperations)}",
                    LocalizationManager.GetString("Dialog_Title_Warning"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool HasEnoughExpenseCategories(int minCategories = 2)
        {
            if (_operationsData == null || _operationsData.Count == 0)
            {
                MessageBox.Show(LocalizationManager.GetString("Error_NoData"),
                    LocalizationManager.GetString("Dialog_Title_Warning"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            HashSet<string> expenseCategories = new HashSet<string>();

            foreach (ObjectOfAnalysis operation in _operationsData)
            {
                if (operation.Sum < 0)
                    expenseCategories.Add(operation.Category);
            }

            if (expenseCategories.Count < minCategories)
            {
                MessageBox.Show($"{GetLocalizedSoundAlt()} {LocalizationManager.GetString("Error_NotEnoughCategories", minCategories, expenseCategories.Count)}",
                    LocalizationManager.GetString("Dialog_Title_Warning"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void StyleDataGridView()
        {
            dgvTable.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Navy;
            dgvTable.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvTable.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 11, System.Drawing.FontStyle.Bold);
            dgvTable.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dgvTable.DefaultCellStyle.Font = new Font("Times New Roman", 11);
            dgvTable.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            dgvTable.AllowUserToAddRows = false;
            dgvTable.AllowUserToDeleteRows = false;
            dgvTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTable.MultiSelect = false;
            dgvTable.ReadOnly = true;
            dgvTable.RowHeadersVisible = false;
            dgvTable.AllowUserToResizeRows = false;
            dgvTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTable.Columns["colNumber"].MinimumWidth = 40;
            dgvTable.Columns["colNumber"].Width = 40;
            dgvTable.Columns["colNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvTable.Columns["colAmount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTable.Columns["colType"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTable.Columns["colCategory"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTable.Columns["colCurrency"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTable.Columns["colDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void SetupDataGridViewColumns()
        {
            dgvTable.AutoGenerateColumns = false;
            dgvTable.Columns.Clear();

            DataGridViewTextBoxColumn colNumber = new DataGridViewTextBoxColumn();
            colNumber.Name = "colNumber";
            colNumber.HeaderText = LocalizationManager.GetString("StartNewWork_Column_Number");
            colNumber.ReadOnly = true;
            colNumber.Width = 50;
            colNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTable.Columns.Add(colNumber);
            colNumber.ReadOnly = true;

            DataGridViewTextBoxColumn colAmount = new DataGridViewTextBoxColumn();
            colAmount.Name = "colAmount";
            colAmount.HeaderText = LocalizationManager.GetString("StartNewWork_Column_Amount");
            colAmount.DataPropertyName = "Sum";
            colAmount.DefaultCellStyle.Format = "N2";
            colAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTable.Columns.Add(colAmount);

            DataGridViewTextBoxColumn colType = new DataGridViewTextBoxColumn();
            colType.Name = "colType";
            colType.HeaderText = LocalizationManager.GetString("StartNewWork_Column_Type");
            colType.DataPropertyName = "TypeOfOperation";
            dgvTable.Columns.Add(colType);

            DataGridViewTextBoxColumn colCategory = new DataGridViewTextBoxColumn();
            colCategory.Name = "colCategory";
            colCategory.HeaderText = LocalizationManager.GetString("StartNewWork_Column_Category");
            colCategory.DataPropertyName = "Category";
            dgvTable.Columns.Add(colCategory);

            DataGridViewTextBoxColumn colCurrency = new DataGridViewTextBoxColumn();
            colCurrency.Name = "colCurrency";
            colCurrency.HeaderText = LocalizationManager.GetString("StartNewWork_Column_Currency");
            colCurrency.DataPropertyName = "Currency";
            colCurrency.ReadOnly = true;
            dgvTable.Columns.Add(colCurrency);

            DataGridViewTextBoxColumn colDate = new DataGridViewTextBoxColumn();
            colDate.Name = "colDate";
            colDate.HeaderText = LocalizationManager.GetString("StartNewWork_Column_Date");
            colDate.DataPropertyName = "Date";
            colDate.DefaultCellStyle.Format = "dd.MM.yyyy";
            dgvTable.Columns.Add(colDate);
        }

        private void UpdateRowNumbers()
        {
            for (int i = 0; i < dgvTable.Rows.Count; i++)
            {
                if (dgvTable.Rows[i].Cells["colNumber"] != null)
                    dgvTable.Rows[i].Cells["colNumber"].Value = (i + 1).ToString();
            }
        }

        private void SetupDataGridViewEvents()
        {
            dgvTable.RowsAdded += (s, e) => UpdateRowNumbers();
        }

        #endregion

        #region Обработчики кнопок визуализации

        private void btnTable_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(btnTable);
            ShowTable();
        }

        private void btnGraph_Click(object sender, EventArgs e)
        {
            if (!HasEnoughData(3))
                return;

            UpdateButtonStyles(btnGraph);
            ShowPlot();

            List<double> xIncomes = new List<double>();
            List<double> yIncomes = new List<double>();
            List<double> xExpenses = new List<double>();
            List<double> yExpenses = new List<double>();
            List<double> xAll = new List<double>();
            List<double> yAll = new List<double>();

            for (int i = 0; i < _operationsData.Count; i++)
            {
                float convertedSum = ConvertToRubles(_operationsData[i].Sum, _operationsData[i].Currency);
                xAll.Add(i);
                yAll.Add((double)convertedSum);

                if (convertedSum > 0)
                {
                    xIncomes.Add(i);
                    yIncomes.Add((double)convertedSum);
                }
                else if (convertedSum < 0)
                {
                    xExpenses.Add(i);
                    yExpenses.Add((double)convertedSum);
                }
            }

            var allPlot = formsPlot.Plot.Add.Scatter(xAll.ToArray(), yAll.ToArray());
            allPlot.Color = Colors.Gray.WithAlpha(0.3);
            allPlot.LineWidth = 1;
            allPlot.MarkerSize = 3;
            allPlot.Label = LocalizationManager.GetString("GetAnalys_Chart_AllOperations");

            if (xIncomes.Count > 0)
            {
                var incomePlot = formsPlot.Plot.Add.Scatter(xIncomes.ToArray(), yIncomes.ToArray());
                incomePlot.Color = Colors.Green;
                incomePlot.LineWidth = 2;
                incomePlot.MarkerSize = 6;
                incomePlot.Label = LocalizationManager.GetString("Report_Income");
            }

            if (xExpenses.Count > 0)
            {
                var expensePlot = formsPlot.Plot.Add.Scatter(xExpenses.ToArray(), yExpenses.ToArray());
                expensePlot.Color = Colors.Red;
                expensePlot.LineWidth = 2;
                expensePlot.MarkerSize = 6;
                expensePlot.Label = LocalizationManager.GetString("Report_Expense");
            }

            var zeroLine = formsPlot.Plot.Add.HorizontalLine(0);
            zeroLine.Color = Colors.Black.WithAlpha(0.3);
            zeroLine.LineWidth = 1;

            formsPlot.Plot.Title(LocalizationManager.GetString("GetAnalys_Chart_GraphTitle"));
            formsPlot.Plot.XLabel(LocalizationManager.GetString("GetAnalys_Chart_XLabel"));
            formsPlot.Plot.YLabel(LocalizationManager.GetString("GetAnalys_Chart_YLabel"));

            formsPlot.Plot.Grid.MajorLineColor = Colors.Gray.WithAlpha(0.2);
            formsPlot.Plot.ShowLegend(Alignment.UpperLeft);
            formsPlot.Plot.Axes.AutoScale();
            formsPlot.Refresh();
        }

        private void btnCircleDiagram_Click(object sender, EventArgs e)
        {
            if (!HasEnoughExpenseCategories(2)) return;

            UpdateButtonStyles(btnCircleDiagram);
            ShowPlot();

            Dictionary<string, float> expenseByCategory = new Dictionary<string, float>();

            foreach (ObjectOfAnalysis operation in _operationsData)
            {
                if (operation.Sum < 0)
                {
                    float convertedSum = ConvertToRubles(operation.Sum, operation.Currency);
                    if (expenseByCategory.ContainsKey(operation.Category))
                        expenseByCategory[operation.Category] += convertedSum;
                    else
                        expenseByCategory[operation.Category] = convertedSum;
                }
            }

            if (expenseByCategory.Count == 0)
            {
                formsPlot.Plot.Title(LocalizationManager.GetString("GetAnalys_Message_NoExpenseData"));
                formsPlot.Refresh();
                return;
            }

            List<double> values = new List<double>();
            List<string> categoryNames = new List<string>();

            foreach (KeyValuePair<string, float> pair in expenseByCategory)
            {
                values.Add(Math.Abs((double)pair.Value));
                categoryNames.Add(pair.Key);
            }

            var pie = formsPlot.Plot.Add.Pie(values.ToArray());
            pie.ExplodeFraction = 0;
            pie.SliceLabelDistance = 0;

            double total = pie.Slices.Select(x => x.Value).Sum();
            double[] percentages = pie.Slices.Select(x => x.Value / total * 100).ToArray();

            for (int i = 0; i < pie.Slices.Count; i++)
            {
                pie.Slices[i].LegendText = $"{categoryNames[i]} ({percentages[i]:F1}%)";
            }

            formsPlot.Plot.Axes.Frameless();
            formsPlot.Plot.HideGrid();

            formsPlot.Plot.Title(LocalizationManager.GetString("GetAnalys_Chart_PieTitle"));
            formsPlot.Plot.ShowLegend(Alignment.UpperRight);

            formsPlot.Plot.Axes.AutoScale();
            formsPlot.Refresh();
        }

        private void btnScatterPlot_Click(object sender, EventArgs e)
        {
            if (!HasEnoughData(10)) return;

            UpdateButtonStyles(btnScatterPlot);
            ShowPlot();

            List<ObjectOfAnalysis> sorted = _operationsData.OrderBy(o => o.Date).ToList();

            List<double> xValues = new List<double>();
            List<double> yValues = new List<double>();
            List<ScottPlot.Color> colors = new List<ScottPlot.Color>();

            for (int i = 0; i < sorted.Count; i++)
            {
                float convertedSum = ConvertToRubles(sorted[i].Sum, sorted[i].Currency);
                xValues.Add(sorted[i].Date.ToOADate());
                yValues.Add((double)convertedSum);

                if (convertedSum > 0)
                    colors.Add(Colors.Green);
                else if (convertedSum < 0)
                    colors.Add(Colors.Red);
                else
                    colors.Add(Colors.Gray);
            }

            for (int i = 0; i < sorted.Count; i++)
            {
                var marker = formsPlot.Plot.Add.Marker(xValues[i], yValues[i]);
                marker.Color = colors[i];
                marker.MarkerSize = 10;
                marker.MarkerShape = MarkerShape.FilledCircle;
            }

            var zeroLine = formsPlot.Plot.Add.HorizontalLine(0);
            zeroLine.Color = Colors.Black.WithAlpha(0.3);
            zeroLine.LineWidth = 1;

            formsPlot.Plot.Title(LocalizationManager.GetString("GetAnalys_Chart_ScatterTitle"));
            formsPlot.Plot.XLabel(LocalizationManager.GetString("GetAnalys_Chart_ScatterXLabel"));
            formsPlot.Plot.YLabel(LocalizationManager.GetString("GetAnalys_Chart_ScatterYLabel"));

            formsPlot.Plot.Axes.DateTimeTicksBottom();
            formsPlot.Plot.Grid.MajorLineColor = Colors.Gray.WithAlpha(0.2);
            formsPlot.Plot.Axes.AutoScale();
            formsPlot.Refresh();
        }

        private void btnGistogram_Click(object sender, EventArgs e)
        {
            if (!HasEnoughData(3)) return;

            UpdateButtonStyles(btnGistogram);
            ShowPlot();
            ResetPlot();

            List<double> values = new List<double>();
            foreach (ObjectOfAnalysis operation in _operationsData)
            {
                float convertedSum = ConvertToRubles(operation.Sum, operation.Currency);
                values.Add((double)convertedSum);
            }

            if (values.Count == 0)
            {
                formsPlot.Plot.Title(LocalizationManager.GetString("GetAnalys_Chart_NoData"));
                formsPlot.Refresh();
                return;
            }

            try
            {
                var hist = ScottPlot.Statistics.Histogram.WithBinCount(10, values.ToArray());
                var histPlot = formsPlot.Plot.Add.Histogram(hist);
                histPlot.BarWidthFraction = 0.8;

                var zeroLine = formsPlot.Plot.Add.HorizontalLine(0);
                zeroLine.Color = Colors.Black.WithAlpha(0.3);
                zeroLine.LineWidth = 1;

                formsPlot.Plot.Title(LocalizationManager.GetString("GetAnalys_Chart_HistogramTitle"));
                formsPlot.Plot.XLabel(LocalizationManager.GetString("GetAnalys_Chart_HistogramXLabel"));
                formsPlot.Plot.YLabel(LocalizationManager.GetString("GetAnalys_Chart_HistogramYLabel"));

                formsPlot.Plot.Grid.MajorLineColor = Colors.Gray.WithAlpha(0.2);
                formsPlot.Plot.Axes.Margins(bottom: 0);
                formsPlot.Plot.Axes.AutoScale();
            }
            catch (ArgumentException)
            {
                formsPlot.Plot.Clear();

                double min = values.Min();
                double max = values.Max();

                if (min == max)
                {
                    double[] bins = new double[] { min };
                    double[] counts = new double[] { values.Count };
                    var barPlot = formsPlot.Plot.Add.Bars(bins, counts);

                    string rubSymbol = LocalizationManager.GetCurrentLanguage() == "English" ? "RUB" : "₽";
                    formsPlot.Plot.Title(string.Format(LocalizationManager.GetString("GetAnalys_Chart_AllValuesSame"), min));
                    formsPlot.Plot.XLabel(LocalizationManager.GetString("GetAnalys_Chart_Value"));
                    formsPlot.Plot.YLabel(LocalizationManager.GetString("GetAnalys_Chart_Count"));
                }
                else
                {
                    int binCount = Math.Min(5, values.Count);
                    double binWidth = (max - min) / binCount;

                    double[] bins = new double[binCount];
                    double[] counts = new double[binCount];

                    for (int i = 0; i < binCount; i++)
                    {
                        bins[i] = min + i * binWidth + binWidth / 2;
                    }

                    foreach (double value in values)
                    {
                        int index = (int)((value - min) / binWidth);
                        if (index >= binCount) index = binCount - 1;
                        if (index < 0) index = 0;
                        counts[index]++;
                    }

                    var barPlot = formsPlot.Plot.Add.Bars(bins, counts);

                    formsPlot.Plot.Title(LocalizationManager.GetString("GetAnalys_Chart_HistogramTitle"));
                    formsPlot.Plot.XLabel(LocalizationManager.GetString("GetAnalys_Chart_HistogramXLabel"));
                    formsPlot.Plot.YLabel(LocalizationManager.GetString("GetAnalys_Chart_HistogramYLabel"));
                }

                formsPlot.Plot.Grid.MajorLineColor = Colors.Gray.WithAlpha(0.2);
                formsPlot.Plot.Axes.Margins(bottom: 0);
                formsPlot.Plot.Axes.AutoScale();
            }

            formsPlot.Refresh();
        }

        private void btnRadarDiagram_Click(object sender, EventArgs e)
        {
            if (!HasEnoughData(3)) return;

            UpdateButtonStyles(btnRadarDiagram);
            ShowPlot();

            Dictionary<string, float> categoryTotals = new Dictionary<string, float>();

            foreach (ObjectOfAnalysis operation in _operationsData)
            {
                float convertedSum = ConvertToRubles(operation.Sum, operation.Currency);
                categoryTotals.TryGetValue(operation.Category, out float currentSum);
                categoryTotals[operation.Category] = currentSum + convertedSum;
            }

            if (categoryTotals.Count == 0)
            {
                formsPlot.Plot.Title(LocalizationManager.GetString("GetAnalys_Chart_NoData"));
                formsPlot.Refresh();
                return;
            }

            List<KeyValuePair<string, float>> sortedCategories = categoryTotals
                .OrderByDescending(p => Math.Abs(p.Value))
                .Take(10)
                .ToList();

            List<double> values = new List<double>();
            List<string> labels = new List<string>();

            float maxAbs = sortedCategories.Max(p => Math.Abs(p.Value));
            if (maxAbs == 0) maxAbs = 1;

            foreach (KeyValuePair<string, float> pair in sortedCategories)
            {
                values.Add((Math.Abs((double)pair.Value) / maxAbs) * 100);
                labels.Add(pair.Key);
            }

            var radar = formsPlot.Plot.Add.Radar(values.ToArray());
            radar.PolarAxis.SetSpokes(labels.ToArray(), 100);

            double[] tickPositions = { 25, 50, 75, 100 };
            string[] tickLabels = tickPositions.Select(x => x.ToString() + "%").ToArray();
            radar.PolarAxis.SetCircles(tickPositions, tickLabels);

            radar.Series[0].FillColor = Colors.Blue.WithAlpha(0.3);
            radar.Series[0].LineColor = Colors.Blue;
            radar.Series[0].LineWidth = 2;

            formsPlot.Plot.Title(LocalizationManager.GetString("GetAnalys_Chart_RadarTitle"));
            formsPlot.Plot.Grid.MajorLineColor = Colors.Gray.WithAlpha(0.2);
            formsPlot.Plot.Axes.AutoScale();
            formsPlot.Refresh();
        }

        #endregion

        #region Навигация

        private void pbxOpenMenu_Click(object sender, EventArgs e)
        {
            if (!CheckProjectSaved())
                return;

            NavigateToHome?.Invoke(this, EventArgs.Empty);
        }

        private void btnBackToData_Click(object sender, EventArgs e)
        {
            _isProjectSaved = false;
            NavigateToChangeData?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        public void SetStartNewWorkScreen(StartNewWork startNewWorkScreen)
        {
            _startNewWorkScreen = startNewWorkScreen;
        }

        public void UpdateStartNewWorkData(List<ObjectOfAnalysis> data)
        {
            if (_startNewWorkScreen != null && data != null && data.Count > 0)
            {
                _startNewWorkScreen.LoadData(data);
            }
        }
    }
}
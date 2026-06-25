using ScottPlot;
using SmartBudget.ClassLibrary;
using Newtonsoft.Json;
using System.Text;
using System.Reflection;

namespace SmartBudget
{
    public partial class GetAnalys : UserControl
    {
        private List<ObjectOfAnalysis> _operationsData; // Поле для хранения данных
        private Button _activeButton; // Текущая активная кнопка
        private bool _isProjectSaved = false; // Флаг сохранения проекта

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

        //Для сохранения связи между двумя экранами
        private StartNewWork _startNewWorkScreen;

        // События
        public event EventHandler NavigateToChangeData;
        public event EventHandler NavigateToHome;

        /// <summary>
        /// Инициализация экрана 
        /// </summary>
        public GetAnalys()
        {
            InitializeComponent();

            //Работа с прокруткой основной панели, возникали случаи, когда она сама по себе отключается
            pnlMain.AutoScroll = true;
            pnlMain.VerticalScroll.Enabled = false;
            pnlMain.VerticalScroll.Visible = false;
            pnlMain.HorizontalScroll.Enabled = true;
            pnlMain.HorizontalScroll.Visible = true;

            //Изначально показываем таблицу
            dgvTable.Visible = true;
            formsPlot.Visible = false;
            _activeButton = btnTable;
            btnTable.BackColor = SystemColors.Control;

            //Отключение интерактивности
            formsPlot.UserInputProcessor.Disable();

            // Настройка таблицы для начального изображения
            SetupDataGridViewColumns();
            SetupDataGridViewEvents();
            StyleDataGridView();

            UpdateButtonStyles(btnTable);
        }

        /// <summary>
        /// Получение данных об операциях из экрана с вводом данных
        /// </summary>
        /// <param name="operations">Список операций</param>
        public void GetData(List<ObjectOfAnalysis> operations)
        {
            _operationsData = operations;
            _isProjectSaved = false; // Сброс флага при получении новых данных

            if (_operationsData != null && _operationsData.Count > 0)
            {
                CalculateAllStatistics();
                GenerateReport();
                ShowTable();
            }
        }

        /// <summary>
        /// Сбрасывает флаг сохранения (вызывается из ProgramForm при изменении данных)
        /// </summary>
        public void ResetSavedFlag()
        {
            _isProjectSaved = false;
        }

        #region Методы для базового анализа данных (расчет статистики)

        /// <summary>
        /// Вычисляет всю статистику один раз
        /// </summary>
        private void CalculateAllStatistics()
        {
            if (_operationsData == null || _operationsData.Count == 0) return;

            _totalCount = OperationCount(_operationsData);
            (_balance, _expenses, _incomes) = OperationCalculateSummary(_operationsData);
            (_topIncomeCategory, _topExpenseCategory) = GetTopCategories(_operationsData);
            (_topIncomeType, _topExpenseType) = GetTopTypes(_operationsData);
            (_incomeCategoryShares, _expenseCategoryShares) = GetCategoryShares(_operationsData);
            (_incomeTypeShares, _expenseTypeShares) = GetTypeShares(_operationsData);
            _expenseStructure = GetExpenseStructure(_operationsData);
            _dailyStats = GetDailyStatistics(_operationsData);
            _topDay = GetTopDay(_operationsData);
            (_incomeByCurrency, _expenseByCurrency, _countByCurrency) = GetCurrencyStatistics(_operationsData);
            _monthlyDynamics = GetMonthlyDynamics(_operationsData);
        }

        /// <summary>
        /// Метод для подсчета количества операций
        /// </summary>
        /// <param name="operations">Список операций</param>
        /// <returns>Количество операций в списке</returns>
        private static int OperationCount(List<ObjectOfAnalysis> operations)
        {
            return operations.Count;
        }

        /// <summary>
        ///Определяет баланс, сумму доходов и сумму расходов 
        /// </summary>
        /// <param name="operations">Список операций для анализа</param>
        /// <returns>Кортеж с данными о балансе, расходах и доходах</returns>
        private static (float Balance, float Expenses, float Incomes) OperationCalculateSummary(List<ObjectOfAnalysis> operations)
        {
            if (operations == null || operations.Count == 0)
                return (0, 0, 0);

            float incomes = operations.Where(o => o.Sum > 0).Sum(o => o.Sum);
            float expenses = operations.Where(o => o.Sum < 0).Sum(o => o.Sum);
            float balance = incomes + expenses;

            return (balance, expenses, incomes);
        }

        /// <summary>
        /// Определяет самую частую категорию для доходов и расходов
        /// </summary>
        private static (string topIncomeCategory, string topExpenseCategory) GetTopCategories(List<ObjectOfAnalysis> operations)
        {
            if (operations == null || operations.Count == 0)
                return ("Нет данных о доходах!", "Нет данных о расходах!");

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

            string topIncomeCategory = "Нет данных о доходах!";
            int maxIncomeCount = 0;
            foreach (KeyValuePair<string, int> pair in incomeCategories)
            {
                if (pair.Value > maxIncomeCount)
                {
                    maxIncomeCount = pair.Value;
                    topIncomeCategory = pair.Key;
                }
            }

            string topExpenseCategory = "Нет данных о расходах!";
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

        /// <summary>
        /// Определяет самый частый тип для доходов и расходов
        /// </summary>
        private static (string TopIncomeType, string TopExpenseType) GetTopTypes(List<ObjectOfAnalysis> operations)
        {
            if (operations == null || operations.Count == 0)
                return ("Нет данных о доходах!", "Нет данных о расходах!");

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

            string topIncomeType = "Нет данных о доходах!";
            int maxIncomeCount = 0;
            foreach (KeyValuePair<string, int> pair in incomeTypes)
            {
                if (pair.Value > maxIncomeCount)
                {
                    maxIncomeCount = pair.Value;
                    topIncomeType = pair.Key;
                }
            }

            string topExpenseType = "Нет данных о расходах!";
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

        /// <summary>
        /// Рассчитывает доли каждой категории среди доходов и расходов
        /// </summary>
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

        /// <summary>
        /// Рассчитывает доли каждого типа среди доходов и расходов
        /// </summary>
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

        /// <summary>
        /// Возвращает структуру расходов по категориям (сумма, количество, доля)
        /// </summary>
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

        /// <summary>
        /// Анализирует динамику операций по месяцам
        /// </summary>
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

        /// <summary>
        /// Анализирует распределение операций по валютам
        /// </summary>
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
                    incomeByCurrency[currency] = currentIncome + (float)operation.Sum;
                }
                else if (operation.Sum < 0)
                {
                    expenseByCurrency.TryGetValue(currency, out float currentExpense);
                    expenseByCurrency[currency] = currentExpense + (float)operation.Sum;
                }
            }

            return (incomeByCurrency, expenseByCurrency, countByCurrency);
        }

        /// <summary>
        /// Рассчитывает дневную статистику по операциям
        /// </summary>
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

        /// <summary>
        /// Определяет день с максимальной суммой операций
        /// </summary>
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

        /// <summary>
        /// Формирует полный финансовый отчет из уже вычисленных данных
        /// </summary>
        private void GenerateReport()
        {
            if (_operationsData == null || _operationsData.Count == 0)
            {
                rtbReport.Text = "Ошибка! Недостаточно данных для анализа! для формирования отчета";
                return;
            }

            rtbReport.Clear();

            //Общая информация
            rtbReport.Font = new Font("Times New Roman", 14, System.Drawing.FontStyle.Regular);

            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText("1. Общая информация\n");

            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");

            rtbReport.AppendText($"Количество операций: {_totalCount}\n");
            rtbReport.AppendText($"Общий доход: {_incomes:F2}\n");
            rtbReport.AppendText($"Общий расход: {Math.Abs(_expenses):F2}\n");
            rtbReport.AppendText($"Итоговый баланс: {_balance:F2}\n");
            rtbReport.AppendText($"Период анализа: {_operationsData.Min(o => o.Date):dd.MM.yyyy} - {_operationsData.Max(o => o.Date):dd.MM.yyyy}\n");
            rtbReport.AppendText($"Активных дней: {_dailyStats.ActiveDays}\n\n");

            //Самые частые категории 
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText("2. Самые частые категории\n");
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");
            rtbReport.AppendText($"Доходы: {_topIncomeCategory}\n");
            rtbReport.AppendText($"Расходы: {_topExpenseCategory}\n\n");

            // Доли категорий в доходах
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText("3. Доли категории в доходах\n");
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");
            if (_incomeCategoryShares.Count > 0)
            {
                foreach (KeyValuePair<string, float> pair in _incomeCategoryShares)
                {
                    rtbReport.AppendText($"{pair.Key}: {pair.Value:F2}%\n");
                }
            }
            else
            {
                rtbReport.AppendText("Ошибка! Недостаточно данных для анализа!\n");
            }
            rtbReport.AppendText("\n");

            // Доли категорий в расходах
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText("4. Доли категорий в расходах\n");
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");
            if (_expenseCategoryShares.Count > 0)
            {
                foreach (KeyValuePair<string, float> pair in _expenseCategoryShares)
                {
                    rtbReport.AppendText($"{pair.Key}: {pair.Value:F2}%\n");
                }
            }
            else
            {
                rtbReport.AppendText("Недостаточно данных для анализа!\n");
            }
            rtbReport.AppendText("\n");

            // Самые частые типы
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText("5. Самые частые типы операций\n");
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");
            rtbReport.AppendText($"Доходы: {_topIncomeType}\n");
            rtbReport.AppendText($"Расходы: {_topExpenseType}\n\n");

            // Доли типов в доходах
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText("6. Доли типов в доходах\n");
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");
            if (_incomeTypeShares.Count > 0)
            {
                foreach (KeyValuePair<string, float> pair in _incomeTypeShares)
                {
                    rtbReport.AppendText($"{pair.Key}: {pair.Value:F2}%\n");
                }
            }
            else
            {
                rtbReport.AppendText("Недостаточно данных для анализа!\n");
            }
            rtbReport.AppendText("\n");

            // Доли типов в расходах
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText("7. Доли типов в расходах\n");
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");
            if (_expenseTypeShares.Count > 0)
            {
                foreach (KeyValuePair<string, float> pair in _expenseTypeShares)
                {
                    rtbReport.AppendText($"{pair.Key}: {pair.Value:F2}%\n");
                }
            }
            else
            {
                rtbReport.AppendText("Недостаточно данных для анализа!\n");
            }
            rtbReport.AppendText("\n");

            //Структура расходов
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText("8. Структура расходов по категориям\n");
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");
            if (_expenseStructure.Count > 0)
            {
                foreach (KeyValuePair<string, (float TotalAmount, int Count, float Share)> pair in _expenseStructure)
                {
                    rtbReport.AppendText($"{pair.Key}:\n");
                    rtbReport.AppendText($"    Сумма: {pair.Value.TotalAmount:F2}\n");
                    rtbReport.AppendText($"    Количество: {pair.Value.Count}\n");
                    rtbReport.AppendText($"    Доля: {pair.Value.Share:F2}%\n");
                    rtbReport.AppendText("\n");
                }
            }
            else
            {
                rtbReport.AppendText("Недостаточно данных для анализа!\n\n");
            }

            //Анализ по валюте
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText("9. Анализ по валютам\n");
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");

            foreach (KeyValuePair<string, int> pair in _countByCurrency)
            {
                rtbReport.SelectionFont = new Font("Times New Roman", 14, System.Drawing.FontStyle.Bold);
                rtbReport.AppendText($"Валюта: {pair.Key}\n");
                rtbReport.AppendText($"    Количество операций: {pair.Value}\n");

                if (_incomeByCurrency.ContainsKey(pair.Key))
                {
                    rtbReport.AppendText($"    Доходы: {_incomeByCurrency[pair.Key]:F2}\n");
                }

                if (_expenseByCurrency.ContainsKey(pair.Key))
                {
                    rtbReport.AppendText($"    Расходы: {Math.Abs(_expenseByCurrency[pair.Key]):F2}\n");
                }
                rtbReport.AppendText("\n");
            }

            // Статистика по дням
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText("10. Статистика по дням\n");
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");
            rtbReport.AppendText($"Средняя сумма в день: {_dailyStats.AverageDaily:F2}\n");
            rtbReport.AppendText($"Максимальная сумма в день: {_dailyStats.MaxDaily:F2}\n");
            rtbReport.AppendText($"Минимальная сумма в день: {_dailyStats.MinDaily:F2}\n");
            rtbReport.AppendText($"Лучший день: {_topDay.Date:dd.MM.yyyy} (сумма: {_topDay.Amount:F2})\n\n");

            //Месячная динамика
            if (_monthlyDynamics.Count > 0)
            {
                rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
                rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
                rtbReport.AppendText("11. Динамика по месяцам\n");
                rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
                rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
                rtbReport.AppendText("\n");

                foreach (KeyValuePair<string, float> pair in _monthlyDynamics)
                {
                    rtbReport.AppendText($"{pair.Key}: {pair.Value:F2}\n");
                }
                rtbReport.AppendText("\n\n\n");
            }

            //Конец отчета, дата формирование оного
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 14, System.Drawing.FontStyle.Italic);
            rtbReport.AppendText($"Отчет сформирован {DateTime.Now:dd.MM.yyyy HH:mm:ss}\nпри помощи программы Smart Budget\n");
        }

        #endregion

        #region Методы сохранения проекта

        /// <summary>
        /// Сохранение отчета в JSON файл
        /// </summary>
        private void btnSaveReport_Click(object sender, EventArgs e)
        {
            if (_operationsData == null || _operationsData.Count == 0)
            {
                MessageBox.Show("Мяу... Нет данных для сохранения! Сначала добавьте операции.",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Запрашиваем имя проекта у пользователя
            string projectName = ShowSaveDialog();

            if (string.IsNullOrWhiteSpace(projectName))
                return;

            // Проверяем, существует ли уже проект с таким именем
            if (ProjectExists(projectName))
            {
                DialogResult result = MessageBox.Show(
                    $"Мяу... Проект с именем \"{projectName}\" уже существует!\nХотите перезаписать его?",
                    "Проект существует",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;
            }

            // Сохраняем проект
            bool saved = SaveProject(projectName);

            if (saved)
            {
                _isProjectSaved = true;
                MessageBox.Show($"Муррр! Проект \"{projectName}\" успешно сохранен!",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Мяу! Ошибка при сохранении проекта!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Показывает диалог ввода имени проекта
        /// </summary>
        private string ShowSaveDialog()
        {
            using (Form dialog = new Form())
            {
                dialog.Text = "Сохранение проекта";
                dialog.Size = new Size(400, 180);
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;

                System.Windows.Forms.Label label = new()
                {
                    Text = "Введите имя проекта:",
                    Location = new Point(20, 20),
                    Size = new Size(350, 25),
                    Font = new Font("Times New Roman", 12)
                };

                TextBox textBox = new TextBox();
                textBox.Location = new Point(20, 55);
                textBox.Size = new Size(350, 25);
                textBox.Font = new Font("Times New Roman", 12);

                Button okButton = new Button();
                okButton.Text = "Сохранить";
                okButton.Size = new Size(100, 35);
                okButton.Location = new Point(150, 100);
                okButton.Font = new Font("Times New Roman", 12);
                okButton.DialogResult = DialogResult.OK;

                Button cancelButton = new Button();
                cancelButton.Text = "Отмена";
                cancelButton.Size = new Size(100, 35);
                cancelButton.Location = new Point(270, 100);
                cancelButton.Font = new Font("Times New Roman", 12);
                cancelButton.DialogResult = DialogResult.Cancel;

                dialog.Controls.Add(label);
                dialog.Controls.Add(textBox);
                dialog.Controls.Add(okButton);
                dialog.Controls.Add(cancelButton);

                dialog.AcceptButton = okButton;
                dialog.CancelButton = cancelButton;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string name = textBox.Text.Trim();
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        MessageBox.Show("Мяу! Имя проекта не может быть пустым!",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return ShowSaveDialog(); // Рекурсивный вызов для повторного ввода
                    }
                    return name;
                }

                return null;
            }
        }

        /// <summary>
        /// Проверяет, существует ли проект с указанным именем
        /// </summary>
        private bool ProjectExists(string projectName)
        {
            string path = GetProjectPath(projectName);
            return File.Exists(path);
        }

        /// <summary>
        /// Возвращает путь к файлу проекта
        /// </summary>
        private string GetProjectPath(string projectName)
        {
            string exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Создаем папку для проектов, если её нет
            string projectsDirectory = Path.Combine(exeDirectory, "Projects");
            if (!Directory.Exists(projectsDirectory))
            {
                Directory.CreateDirectory(projectsDirectory);
            }

            string fileName = $"{projectName}.json";
            return Path.Combine(projectsDirectory, fileName);
        }

        /// <summary>
        /// Сохраняет проект в JSON файл
        /// </summary>
        private bool SaveProject(string projectName)
        {
            try
            {
                string path = GetProjectPath(projectName);

                // Сериализуем список операций
                string json = JsonConvert.SerializeObject(_operationsData, Formatting.Indented);
                File.WriteAllText(path, json, Encoding.UTF8);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Проверяет, сохранен ли проект перед выходом
        /// </summary>
        private bool CheckProjectSaved()
        {
            if (_operationsData == null || _operationsData.Count == 0)
                return true; // Нет данных - нечего сохранять

            if (_isProjectSaved)
                return true; // Уже сохранено

            DialogResult result = MessageBox.Show(
                "Мяу... Вы не сохранили проект!\nХотите сохранить перед выходом?",
                "Проект не сохранен",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                btnSaveReport_Click(this, EventArgs.Empty);
                return _isProjectSaved; // Возвращаем, сохранилось ли
            }
            else if (result == DialogResult.Cancel)
            {
                return false; // Отмена выхода
            }

            return true; // Нет - выход без сохранения
        }

        #endregion

        #region Методы визуализации

        /// <summary>
        /// Показывает таблицу с данными
        /// </summary>
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

        /// <summary>
        /// Полностью сбрасывает график к стандартным настройкам
        /// </summary>
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

        /// <summary>
        /// Показывает график на панели formsPlot и сбрасывает его настройки
        /// </summary>
        private void ShowPlot()
        {
            formsPlot.Visible = true;
            dgvTable.Visible = false;
            formsPlot.BringToFront();

            ResetPlot();
        }

        /// <summary>
        /// Метод для обновления внешнего вида кнопок
        /// </summary>
        /// <param name="activeButton">Текущая активная кнопка</param>
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

        /// <summary>
        /// Проверка того, что для построения диаграммы есть достаточно операций 
        /// </summary>
        /// <param name="numberOfOperations">Минимальное количество операций для диаграммы</param>
        /// <returns></returns>
        private bool HasEnoughData(int numberOfOperations)
        {
            if (_operationsData == null || _operationsData.Count < numberOfOperations)
            {
                MessageBox.Show($"Мяу-мяу-мяу! Для построения данной диаграммы необходимо минимум {numberOfOperations} операции!", "Недостаточно данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Проверяет, есть ли минимум N разных категорий расходов
        /// </summary>
        /// <param name="minCategories">Минимальное количество категорий расходов</param>
        /// <returns>true - если достаточно категорий, false - если нет</returns>
        private bool HasEnoughExpenseCategories(int minCategories = 2)
        {
            if (_operationsData == null || _operationsData.Count == 0)
            {
                MessageBox.Show("Нет данных для анализа!", "Недостаточно данных",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show($"Мяу, мяу! Для построения круговой диаграммы необходимо минимум {minCategories} разные категории расходов!\n" +
                    $"Найдено: {expenseCategories.Count} категорий.",
                    "Недостаточно данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Стилизация DataGridView для отображения данных
        /// </summary>
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

        /// <summary>
        /// Организация колонок
        /// </summary>
        private void SetupDataGridViewColumns()
        {
            dgvTable.AutoGenerateColumns = false;
            dgvTable.Columns.Clear();

            DataGridViewTextBoxColumn colNumber = new DataGridViewTextBoxColumn();
            colNumber.Name = "colNumber";
            colNumber.HeaderText = "№";
            colNumber.ReadOnly = true;
            colNumber.Width = 50;
            colNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTable.Columns.Add(colNumber);
            colNumber.ReadOnly = true;

            DataGridViewTextBoxColumn colAmount = new DataGridViewTextBoxColumn();
            colAmount.Name = "colAmount";
            colAmount.HeaderText = "Размер";
            colAmount.DataPropertyName = "Sum";
            colAmount.DefaultCellStyle.Format = "N2";
            colAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTable.Columns.Add(colAmount);

            DataGridViewTextBoxColumn colType = new DataGridViewTextBoxColumn();
            colType.Name = "colType";
            colType.HeaderText = "Тип";
            colType.DataPropertyName = "TypeOfOperation";
            dgvTable.Columns.Add(colType);

            DataGridViewTextBoxColumn colCategory = new DataGridViewTextBoxColumn();
            colCategory.Name = "colCategory";
            colCategory.HeaderText = "Категория";
            colCategory.DataPropertyName = "Category";
            dgvTable.Columns.Add(colCategory);

            DataGridViewTextBoxColumn colCurrency = new DataGridViewTextBoxColumn();
            colCurrency.Name = "colCurrency";
            colCurrency.HeaderText = "Валюта";
            colCurrency.DataPropertyName = "Currency";
            colCurrency.ReadOnly = true;
            dgvTable.Columns.Add(colCurrency);

            DataGridViewTextBoxColumn colDate = new DataGridViewTextBoxColumn();
            colDate.Name = "colDate";
            colDate.HeaderText = "Дата";
            colDate.DataPropertyName = "Date";
            colDate.DefaultCellStyle.Format = "dd.MM.yyyy";
            dgvTable.Columns.Add(colDate);
        }

        /// <summary>
        /// Обновление номеров строк
        /// </summary>
        private void UpdateRowNumbers()
        {
            for (int i = 0; i < dgvTable.Rows.Count; i++)
            {
                if (dgvTable.Rows[i].Cells["colNumber"] != null)
                    dgvTable.Rows[i].Cells["colNumber"].Value = (i + 1).ToString();
            }
        }

        /// <summary>
        /// Настройка событий DataGridView
        /// </summary>
        private void SetupDataGridViewEvents()
        {
            dgvTable.RowsAdded += (s, e) => UpdateRowNumbers();
        }

        #endregion

        #region Обработчики кнопок визуализации

        /// <summary>
        /// Работа с таблицей
        /// </summary>
        private void btnTable_Click(object sender, EventArgs e)
        {
            UpdateButtonStyles(btnTable);
            ShowTable();
        }

        /// <summary>
        /// Работа с линейным графиком (по порядковому номеру записи)
        /// </summary>
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
                xAll.Add(i);
                yAll.Add((double)_operationsData[i].Sum);

                if (_operationsData[i].Sum > 0)
                {
                    xIncomes.Add(i);
                    yIncomes.Add((double)_operationsData[i].Sum);
                }
                else if (_operationsData[i].Sum < 0)
                {
                    xExpenses.Add(i);
                    yExpenses.Add((double)_operationsData[i].Sum);
                }
            }

            var allPlot = formsPlot.Plot.Add.Scatter(xAll.ToArray(), yAll.ToArray());
            allPlot.Color = Colors.Gray.WithAlpha(0.3);
            allPlot.LineWidth = 1;
            allPlot.MarkerSize = 3;
            allPlot.Label = "Все операции";

            if (xIncomes.Count > 0)
            {
                var incomePlot = formsPlot.Plot.Add.Scatter(xIncomes.ToArray(), yIncomes.ToArray());
                incomePlot.Color = Colors.Green;
                incomePlot.LineWidth = 2;
                incomePlot.MarkerSize = 6;
                incomePlot.Label = "Доходы";
            }

            if (xExpenses.Count > 0)
            {
                var expensePlot = formsPlot.Plot.Add.Scatter(xExpenses.ToArray(), yExpenses.ToArray());
                expensePlot.Color = Colors.Red;
                expensePlot.LineWidth = 2;
                expensePlot.MarkerSize = 6;
                expensePlot.Label = "Расходы";
            }

            var zeroLine = formsPlot.Plot.Add.HorizontalLine(0);
            zeroLine.Color = Colors.Black;
            zeroLine.LineWidth = 1;

            formsPlot.Plot.Title("Динамика доходов и расходов");
            formsPlot.Plot.XLabel("Порядковый номер операции");
            formsPlot.Plot.YLabel("Сумма операции");

            formsPlot.Plot.Grid.MajorLineColor = Colors.Gray.WithAlpha(0.2);
            formsPlot.Plot.ShowLegend(Alignment.UpperLeft);
            formsPlot.Plot.Axes.AutoScale();
            formsPlot.Refresh();
        }

        /// <summary>
        /// Работа с круговой диаграммой
        /// </summary>
        private void btnCircleDiagram_Click(object sender, EventArgs e)
        {
            if (!HasEnoughExpenseCategories(2)) return;

            UpdateButtonStyles(btnCircleDiagram);
            ShowPlot();

            if (_expenseStructure == null || _expenseStructure.Count == 0)
            {
                formsPlot.Plot.Title("Нет данных о расходах");
                formsPlot.Refresh();
                return;
            }

            // Подготовка данных
            List<double> values = new List<double>();
            List<string> categoryNames = new List<string>();

            foreach (KeyValuePair<string, (float TotalAmount, int Count, float Share)> pair in _expenseStructure)
            {
                values.Add(pair.Value.TotalAmount);
                categoryNames.Add(pair.Key);
            }

            // Создаем круговую диаграмму
            var pie = formsPlot.Plot.Add.Pie(values.ToArray());
            pie.ExplodeFraction = 0;
            pie.SliceLabelDistance = 0;

            // Определяем проценты для каждого среза
            double total = pie.Slices.Select(x => x.Value).Sum();
            double[] percentages = pie.Slices.Select(x => x.Value / total * 100).ToArray();

            // Добавляем легенду с названиями категорий и процентами
            for (int i = 0; i < pie.Slices.Count; i++)
            {
                pie.Slices[i].LegendText = $"{categoryNames[i]} ({percentages[i]:F1}%)";
            }

            // Настройка осей
            formsPlot.Plot.Axes.Frameless();
            formsPlot.Plot.HideGrid();

            formsPlot.Plot.Title("Структура расходов по категориям");
            formsPlot.Plot.ShowLegend(Alignment.UpperRight);

            formsPlot.Plot.Axes.AutoScale();
            formsPlot.Refresh();
        }

        /// <summary>
        /// Работа с точечной диаграммой по датам
        /// </summary>
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
                xValues.Add(sorted[i].Date.ToOADate());
                yValues.Add((double)sorted[i].Sum);

                if (sorted[i].Sum > 0)
                    colors.Add(Colors.Green);
                else if (sorted[i].Sum < 0)
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

            formsPlot.Plot.Title("Точечная диаграмма операций по датам (зеленый - доход, красный - расход)");
            formsPlot.Plot.XLabel("Дата операции");
            formsPlot.Plot.YLabel("Сумма операции");

            formsPlot.Plot.Axes.DateTimeTicksBottom();
            formsPlot.Plot.Grid.MajorLineColor = Colors.Gray.WithAlpha(0.2);
            formsPlot.Plot.Axes.AutoScale();
            formsPlot.Refresh();
        }

        /// <summary>
        /// Работа с гистограммой
        /// </summary>
        private void btnGistogram_Click(object sender, EventArgs e)
        {
            if (!HasEnoughData(3)) return;

            UpdateButtonStyles(btnGistogram);
            ShowPlot();
            ResetPlot();

            // Подготовка данных
            List<double> values = new List<double>();
            foreach (ObjectOfAnalysis operation in _operationsData)
            {
                values.Add((double)operation.Sum);
            }

            if (values.Count == 0)
            {
                formsPlot.Plot.Title("Нет данных");
                formsPlot.Refresh();
                return;
            }

            try
            {
                // Пытаемся создать гистограмму
                var hist = ScottPlot.Statistics.Histogram.WithBinCount(10, values.ToArray());
                var histPlot = formsPlot.Plot.Add.Histogram(hist);
                histPlot.BarWidthFraction = 0.8;

                var zeroLine = formsPlot.Plot.Add.HorizontalLine(0);
                zeroLine.Color = Colors.Black.WithAlpha(0.3);
                zeroLine.LineWidth = 1;

                formsPlot.Plot.Title("Гистограмма распределения сумм операций");
                formsPlot.Plot.XLabel("Диапазон сумм");
                formsPlot.Plot.YLabel("Количество операций");

                formsPlot.Plot.Grid.MajorLineColor = Colors.Gray.WithAlpha(0.2);
                formsPlot.Plot.Axes.Margins(bottom: 0);
                formsPlot.Plot.Axes.AutoScale();
            }
            catch (ArgumentException)
            {
                // Если все значения одинаковые или другие проблемы - показываем простую гистограмму
                formsPlot.Plot.Clear();

                double min = values.Min();
                double max = values.Max();

                if (min == max)
                {
                    // Все значения одинаковые
                    double[] bins = new double[] { min };
                    double[] counts = new double[] { values.Count };
                    var barPlot = formsPlot.Plot.Add.Bars(bins, counts);

                    formsPlot.Plot.Title($"Все значения одинаковы ({min:F2})");
                    formsPlot.Plot.XLabel("Значение");
                    formsPlot.Plot.YLabel("Количество");
                }
                else
                {
                    // Ручное построение гистограммы
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

                    formsPlot.Plot.Title("Гистограмма распределения сумм операций");
                    formsPlot.Plot.XLabel("Диапазон сумм");
                    formsPlot.Plot.YLabel("Количество операций");
                }

                formsPlot.Plot.Grid.MajorLineColor = Colors.Gray.WithAlpha(0.2);
                formsPlot.Plot.Axes.Margins(bottom: 0);
                formsPlot.Plot.Axes.AutoScale();
            }

            formsPlot.Refresh();
        }

        /// <summary>
        /// Работа с лепестковой диаграммой
        /// </summary>
        private void btnRadarDiagram_Click(object sender, EventArgs e)
        {
            if (!HasEnoughData(3)) return;

            UpdateButtonStyles(btnRadarDiagram);
            ShowPlot();

            Dictionary<string, float> categoryTotals = new Dictionary<string, float>();

            foreach (ObjectOfAnalysis operation in _operationsData)
            {
                categoryTotals.TryGetValue(operation.Category, out float currentSum);
                categoryTotals[operation.Category] = currentSum + (float)operation.Sum;
            }

            if (categoryTotals.Count == 0)
            {
                formsPlot.Plot.Title("Нет данных");
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

            formsPlot.Plot.Title("Лепестковая диаграмма по категориям");
            formsPlot.Plot.Grid.MajorLineColor = Colors.Gray.WithAlpha(0.2);
            formsPlot.Plot.Axes.AutoScale();
            formsPlot.Refresh();
        }

        #endregion

        #region Навигация

        private void pbxOpenMenu_Click(object sender, EventArgs e)
        {
            // Проверяем, сохранен ли проект перед выходом
            if (!CheckProjectSaved())
                return;

            NavigateToHome?.Invoke(this, EventArgs.Empty);
        }

        private void btnBackToData_Click(object sender, EventArgs e)
        {
            // Сбрасываем флаг сохранения, так как пользователь возвращается к редактированию
            _isProjectSaved = false;
            NavigateToChangeData?.Invoke(this, EventArgs.Empty);
        }

        #endregion







        /// <summary>
        /// Устанавливает ссылку на экран ввода данных
        /// </summary>
        public void SetStartNewWorkScreen(StartNewWork startNewWorkScreen)
        {
            _startNewWorkScreen = startNewWorkScreen;
        }

        /// <summary>
        /// Обновляет данные в StartNewWork при загрузке проекта
        /// </summary>
        public void UpdateStartNewWorkData(List<ObjectOfAnalysis> data)
        {
            if (_startNewWorkScreen != null && data != null && data.Count > 0)
            {
                _startNewWorkScreen.LoadData(data);
            }
        }
    }
}
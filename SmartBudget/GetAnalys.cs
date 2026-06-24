using ScottPlot;
using SmartBudget.ClassLibrary;

namespace SmartBudget
{
    public partial class GetAnalys : UserControl
    {
        private List<ObjectOfAnalysis> _operationsData; // Поле для хранения данных

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
        }

        /// <summary>
        /// Получение данных об операциях из экрана с вводом данных
        /// </summary>
        /// <param name="operations">Список операций</param>
        public void GetData(List<ObjectOfAnalysis> operations)
        {
            _operationsData = operations;

            if (_operationsData != null && _operationsData.Count > 0)
                GenerateReport(_operationsData);
        }

        #region Методы для базового анализа данных (а также сама генерация отчета)

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
            // Проверка на пустоту принимаемых параметров
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
        /// <param name="operations">Список операций для анализа</param>
        /// <returns>Кортеж с двумя значениями: (самая частая категория доходов, самая частая категория расходов)</returns>
        private static (string topIncomeCategory, string topExpenseCategory) GetTopCategories(List<ObjectOfAnalysis> operations)
        {
            // Проверка на пустоту принимаемых параметров
            if (operations == null || operations.Count == 0)
                return ("Нет данных о доходах!", "Нет данных о расходах!");

            // Словари для подсчета количества операций по категориям (ключ - категория, значение - кол-во операций)
            Dictionary<string, int> incomeCategories = new Dictionary<string, int>(); 
            Dictionary<string, int> expenseCategories = new Dictionary<string, int>();

            // Проходим по всем операциям
            foreach (ObjectOfAnalysis operation in operations)
            {
                if (operation.Sum > 0) // Доход
                {
                    if (incomeCategories.ContainsKey(operation.Category)) //Если уже содержит ключ-категорию, то просто прибавляем значение
                        incomeCategories[operation.Category]++;
                    else
                        incomeCategories[operation.Category] = 1; // В противном случае создаем новый ключ, а значениб присваиваем единицу
                }

                else if (operation.Sum < 0) // Расход
                {
                    if (expenseCategories.ContainsKey(operation.Category))
                        expenseCategories[operation.Category]++;
                    else
                        expenseCategories[operation.Category] = 1;
                }
            }

            // Поиск самой частой категории доходов
            string topIncomeCategory = "Нет данных о доходах!";
            int maxIncomeCount = 0;
            foreach (KeyValuePair<string, int> pair in incomeCategories) //Проходимся циклом по словарю
            {
                if (pair.Value > maxIncomeCount)
                {
                    maxIncomeCount = pair.Value;
                    topIncomeCategory = pair.Key;
                }
            }

            // Поиск самой частой категории расходов
            string topExpenseCategory = "Нет данных о расходах!";
            int maxExpenseCount = 0;
            foreach (KeyValuePair<string, int> pair in expenseCategories) //Проходимся циклом по словарю
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
        /// <param name="operations">Список операций для анализа</param>
        /// <returns>Кортеж с двумя значениями: (самый частый тип доходов, самый частый тип расходов)</returns>
        private static (string TopIncomeType, string TopExpenseType) GetTopTypes(List<ObjectOfAnalysis> operations)
        {
            // Проверка на пустоту принимаемых параметров
            if (operations == null || operations.Count == 0)
                return ("Нет данных о доходах!", "Нет данных о расходах!");

            // Словари для подсчета количества операций по типам
            Dictionary<string, int> incomeTypes = new Dictionary<string, int>();
            Dictionary<string, int> expenseTypes = new Dictionary<string, int>();

            // Проходим по всем операциям
            foreach (ObjectOfAnalysis operation in operations)
            {
                if (operation.Sum > 0) // Доход
                {
                    if (incomeTypes.ContainsKey(operation.TypeOfOperation))
                        incomeTypes[operation.TypeOfOperation]++;
                    else
                        incomeTypes[operation.TypeOfOperation] = 1;
                }

                else if (operation.Sum < 0) // Расход
                {
                    if (expenseTypes.ContainsKey(operation.TypeOfOperation))
                        expenseTypes[operation.TypeOfOperation]++;
                    else
                        expenseTypes[operation.TypeOfOperation] = 1;
                }
            }

            // Поиск самого частого типа доходов
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

            // Поиск самого частого типа расходов
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
        /// <param name="operations">Список операций для анализа</param>
        /// <returns>Кортеж со словарями: (доли категорий доходов, доли категорий расходов)</returns>
        private static (Dictionary<string, float> IncomeCategoryShares, Dictionary<string, float> ExpenseCategoryShares) GetCategoryShares(List<ObjectOfAnalysis> operations)
        {
            // Проверка на пустоту принимаемых параметров
            if (operations == null || operations.Count == 0)
                return (new Dictionary<string, float>(), new Dictionary<string, float>());

            // Словари для хранения сумм по категориям
            Dictionary<string, float> incomeCategorySums = new Dictionary<string, float>();
            Dictionary<string, float> expenseCategorySums = new Dictionary<string, float>();

            // Общие суммы доходов и расходов
            float totalIncomes = 0;
            float totalExpenses = 0;

            // Проходим по всем операциям
            foreach (ObjectOfAnalysis operation in operations)
            {
                if (operation.Sum > 0) // Доход
                {
                    totalIncomes += (float)operation.Sum;

                    if (incomeCategorySums.ContainsKey(operation.Category))
                        incomeCategorySums[operation.Category] += (float)operation.Sum;
                    else
                        incomeCategorySums[operation.Category] = (float)operation.Sum;
                }
                else if (operation.Sum < 0) // Расход
                {
                    totalExpenses += (float)operation.Sum; // Отрицательное значение

                    if (expenseCategorySums.ContainsKey(operation.Category))
                        expenseCategorySums[operation.Category] += (float)operation.Sum;
                    else
                        expenseCategorySums[operation.Category] = (float)operation.Sum;
                }
            }

            // Словари для долей
            Dictionary<string, float> incomeShares = new Dictionary<string, float>();
            Dictionary<string, float> expenseShares = new Dictionary<string, float>();

            // Расчет долей для доходов (в процентах)
            if (totalIncomes > 0)
            {
                foreach (KeyValuePair<string, float> pair in incomeCategorySums)
                {
                    float share = (pair.Value / totalIncomes) * 100;
                    incomeShares[pair.Key] = (float)Math.Round(share, 2); // Округляем до 2 знаков
                }
            }

            // Расчет долей для расходов (в процентах)
            if (totalExpenses < 0) // Расходы отрицательные, берем модуль
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
        /// <param name="operations">Список операций для анализа</param>
        /// <returns>Кортеж со словарями: (доли типов доходов, доли типов расходов)</returns>
        private static (Dictionary<string, float> IncomeTypeShares, Dictionary<string, float> ExpenseTypeShares) GetTypeShares(List<ObjectOfAnalysis> operations)
        {
            // Проверка на пустоту принимаемых параметров
            if (operations == null || operations.Count == 0)
                return (new Dictionary<string, float>(), new Dictionary<string, float>());

            // Словари для хранения сумм по типам
            Dictionary<string, float> incomeTypeSums = new Dictionary<string, float>();
            Dictionary<string, float> expenseTypeSums = new Dictionary<string, float>();

            // Общие суммы доходов и расходов
            float totalIncomes = 0;
            float totalExpenses = 0;

            // Проходим по всем операциям
            foreach (ObjectOfAnalysis operation in operations)
            {
                if (operation.Sum > 0) // Доход
                {
                    totalIncomes += (float)operation.Sum;

                    if (incomeTypeSums.ContainsKey(operation.TypeOfOperation))
                        incomeTypeSums[operation.TypeOfOperation] += (float)operation.Sum;
                    else
                        incomeTypeSums[operation.TypeOfOperation] = (float)operation.Sum;
                }

                else if (operation.Sum < 0) // Расход
                {
                    totalExpenses += (float)operation.Sum; // Отрицательное значение

                    if (expenseTypeSums.ContainsKey(operation.TypeOfOperation))
                        expenseTypeSums[operation.TypeOfOperation] += (float)operation.Sum;
                    else
                        expenseTypeSums[operation.TypeOfOperation] = (float)operation.Sum;
                }
            }

            // Словари для долей
            Dictionary<string, float> incomeShares = new Dictionary<string, float>();
            Dictionary<string, float> expenseShares = new Dictionary<string, float>();

            // Расчет долей для доходов (в процентах)
            if (totalIncomes > 0)
            {
                foreach (KeyValuePair<string, float> pair in incomeTypeSums)
                {
                    float share = (pair.Value / totalIncomes) * 100;
                    incomeShares[pair.Key] = (float)Math.Round(share, 2);
                }
            }

            // Расчет долей для расходов (в процентах)
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
        /// <param name="operations">Список операций для анализа</param>
        /// <returns>Словарь с информацией о каждой категории расходов</returns>
        private static Dictionary<string, (float TotalAmount, int Count, float Share)> GetExpenseStructure(List<ObjectOfAnalysis> operations)
        {
            // Проверка на пустоту принимаемых параметров
            if (operations == null || operations.Count == 0)
                return new Dictionary<string, (float TotalAmount, int Count, float Share)>();

            // Словарь для хранения информации о категориях расходов
            Dictionary<string, (float TotalAmount, int Count)> expenseData = new Dictionary<string, (float TotalAmount, int Count)>();

            float totalExpenses = 0;

            // Проходим по всем операциям (только расходы)
            foreach (ObjectOfAnalysis operation in operations)
            {
                if (operation.Sum < 0) // Только расходы
                {
                    totalExpenses += (float)operation.Sum;

                    if (expenseData.ContainsKey(operation.Category))
                    {
                        // Обновляем существующую категорию
                        (float total, int count) = expenseData[operation.Category];
                        expenseData[operation.Category] = (total + (float)operation.Sum, count + 1);
                    }
                    else
                    {
                        // Добавляем новую категорию
                        expenseData[operation.Category] = ((float)operation.Sum, 1);
                    }
                }
            }

            // Формируем результат с долями
            Dictionary<string, (float TotalAmount, int Count, float Share)> result = [];

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
        /// Формирует полный финансовый отчет и выводит его в RichTextBox
        /// </summary>
        /// <param name="operations">Список операций для анализа</param>
        private void GenerateReport(List<ObjectOfAnalysis> operations)
        {
            //Проверка на наличие данных
            if (operations == null || operations.Count == 0)
            {
                rtbReport.Text = "Ошибка! Недостаточно данных для анализа! для формирования отчета";
                return;
            }

            //Получаем все необходимые данные
            int totalCount = OperationCount(operations);
            (float balance, float expenses, float incomes) = OperationCalculateSummary(operations);
            (string topIncomeCategory, string topExpenseCategory) = GetTopCategories(operations);
            (string topIncomeType, string topExpenseType) = GetTopTypes(operations);
            (Dictionary<string, float> incomeCategoryShares, Dictionary<string, float> expenseCategoryShares) = GetCategoryShares(operations);
            (Dictionary<string, float> incomeTypeShares, Dictionary<string, float> expenseTypeShares) = GetTypeShares(operations);
            Dictionary<string, (float TotalAmount, int Count, float Share)> expenseStructure = GetExpenseStructure(operations);

            rtbReport.Clear();

            //Общая информация
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText("1. Общая информация\n");

            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");

            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText($"Количество операций: {totalCount}\n");
            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText($"Общий доход: {incomes:F2}\n");
            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText($"Общий расход: {Math.Abs(expenses):F2}\n");
            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText($"Итоговый баланс: {balance:F2}\n\n");

            //Самые частые категории 
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText("2. Самые частые категории\n");
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");
            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText($"Доходы: {topIncomeCategory}\n");
            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText($"Расходы: {topExpenseCategory}\n\n");

            // 4. ДОЛИ КАТЕГОРИЙ ДОХОДОВ (заголовок по центру)
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText("3. Доли категории доходов\n");

            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");
            if (incomeCategoryShares.Count > 0)
            {
                foreach (KeyValuePair<string, float> pair in incomeCategoryShares)
                {
                    rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
                    rtbReport.AppendText($"{pair.Key}: {pair.Value:F2}%\n");
                }
            }

            else
            {
                rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
                rtbReport.AppendText("Ошибка! Недостаточно данных для анализа!\n");
            }

            rtbReport.AppendText("\n");

            // 5. ДОЛИ КАТЕГОРИЙ В РАСХОДАХ (заголовок по центру)
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText("4. Доли категорий в расходах\n");

            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");
            if (expenseCategoryShares.Count > 0)
            {
                foreach (KeyValuePair<string, float> pair in expenseCategoryShares)
                {
                    rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
                    rtbReport.AppendText($"{pair.Key}: {pair.Value:F2}%\n");
                }
            }

            else
            {
                rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
                rtbReport.AppendText("Недостаточно данных для анализа!\n");
            }

            rtbReport.AppendText("\n");

            // 6. САМЫЕ ЧАСТЫЕ ТИПЫ (заголовок по центру)
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText("5. Самые частые типы операций\n");

            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");

            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText($"Доходы: {topIncomeType}\n");
            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText($"Расходы: {topExpenseType}\n\n");

            // 7. ДОЛИ ТИПОВ В ДОХОДАХ (заголовок по центру)
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText("6. Доли типов в доходах\n");

            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");

            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            if (incomeTypeShares.Count > 0)
            {
                foreach (KeyValuePair<string, float> pair in incomeTypeShares)
                {
                    rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
                    rtbReport.AppendText($"{pair.Key}: {pair.Value:F2}%\n");
                }
            }

            else
            {
                rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
                rtbReport.AppendText("Недостаточно данных для анализа!\n");
            }

            rtbReport.AppendText("\n");

            // 8. ДОЛИ ТИПОВ В РАСХОДАХ (заголовок по центру)
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText("7. Доли типов в расходах\n");

            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");

            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            if (expenseTypeShares.Count > 0)
            {
                foreach (KeyValuePair<string, float> pair in expenseTypeShares)
                {
                    rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
                    rtbReport.AppendText($"{pair.Key}: {pair.Value:F2}%\n");
                }
            }

            else
            {
                rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
                rtbReport.AppendText("Недостаточно данных для анализа!\n");
            }

            rtbReport.AppendText("\n");

            // 9. СТРУКТУРА РАСХОДОВ ПО КАТЕГОРИЯМ (заголовок по центру)
            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            rtbReport.AppendText("8. Структура расходов по категориям\n");

            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            rtbReport.SelectionFont = new Font("Times New Roman", 5, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");

            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            if (expenseStructure.Count > 0)
            {
                foreach (KeyValuePair<string, (float TotalAmount, int Count, float Share)> pair in expenseStructure)
                {
                    rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Bold);
                    rtbReport.AppendText($"{pair.Key}:\n");

                    rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
                    rtbReport.AppendText($"    Сумма: {pair.Value.TotalAmount:F2}\n");
                    rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
                    rtbReport.AppendText($"    Количество: {pair.Value.Count}\n");
                    rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
                    rtbReport.AppendText($"    Доля: {pair.Value.Share:F2}%\n");
                    rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
                    rtbReport.AppendText("\n");
                }
            }

            else
            {
                rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
                rtbReport.AppendText("Недостаточно данных для анализа!\n\n");
            }

            rtbReport.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Regular);
            rtbReport.AppendText("\n");

            rtbReport.SelectionFont = new Font("Times New Roman", 13, System.Drawing.FontStyle.Italic);
            rtbReport.AppendText($"Отчет сформирован: {DateTime.Now:dd.MM.yyyy HH:mm:ss} при помощи программы Smart Budget");
        }

        #endregion

        private void pbxOpenMenu_Click(object sender, EventArgs e)
        {
            NavigateToHome?.Invoke(this, EventArgs.Empty);
        }

        private void btnBackToData_Click(object sender, EventArgs e)
        {
            NavigateToChangeData?.Invoke(this, EventArgs.Empty);
        }

        private void btnSaveReport_Click(object sender, EventArgs e)
        {

        }

        private void btnTable_Click(object sender, EventArgs e)
        {

        }

        private void btnGraph_Click(object sender, EventArgs e)
        {

        }

        private void btnCircleDiagram_Click(object sender, EventArgs e)
        {

        }

        private void btnScatterPlot_Click(object sender, EventArgs e)
        {

        }

        private void btnGistogram_Click(object sender, EventArgs e)
        {

        }

        private void btnRadarDiagram_Click(object sender, EventArgs e)
        {

        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
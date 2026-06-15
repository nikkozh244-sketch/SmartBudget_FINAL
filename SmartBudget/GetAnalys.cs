using ScottPlot;
using Smart_Budget.ClassLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace Smart_Budget
{
    public partial class GetAnalys : UserControl
    {
        //private List<ObjectOfAnalysis> _operations; // Таблица данных, полученная во время ввода операций
        //private bool _isProjectSaved = false; //Флаг, был ли проект сохранен
        //private string _saveFolderPath; // Путь для сохранения JSON

        //// События для навигации
        //public event EventHandler NavigateToHome;
        //public event EventHandler NavigateToStartNewWork;

        //// Класс для сохранения данных в JSON
        //[Serializable]
        //private class AnalysisSaveData
        //{
        //    public List<ObjectOfAnalysis> Operations { get; set; }
        //    public string ReportText { get; set; }
        //    public string ReportRtf { get; set; }
        //    public DateTime SaveDate { get; set; }
        //}

        //// Конструктор
        //public GetAnalys()
        //{
        //    InitializeComponent();

        //    // Настройка путей для сохранения
        //    _saveFolderPath = Path.Combine(Application.StartupPath, "Saves");
        //    if (!Directory.Exists(_saveFolderPath)) //Если директория отсутствует, то создаем мечто под нее
        //        Directory.CreateDirectory(_saveFolderPath);

        //    // Изначально все кнопки графиков выключены (кроме таблицы)
        //    SetChartButtonsEnabled(false);
        //    btnTable.Enabled = true;

        //    //Подписка на события кнопок меню
        //    pbxOpenMenu.Click += new EventHandler(pbxOpenMenu_Click);
        //}

        ///// <summary>
        ///// Обработчик клика по иконке меню для возврата на главный экран
        ///// </summary>
        //private void pbxOpenMenu_Click(object sender, EventArgs e)
        //{
        //    NavigateToHome?.Invoke(this, EventArgs.Empty);
        //}

        ///// <summary>
        ///// Выключить все кнопки типов визуализации (кроме таблицы)
        ///// </summary>
        //private void SetChartButtonsEnabled()
        //{
        //    // Помечаем все объеккты как выключеными
        //    btnGraph.Enabled = false;
        //    btnCircleDiagram.Enabled = false;
        //    btnScatterPlot.Enabled = false;
        //    btnGistogram.Enabled = false;
        //    btnRadarDiagram.Enabled = false;

        //    // Визуальное отображение (серые, если выключены)
        //    System.Drawing.Color backColor = false ? SystemColors.Control : System.Drawing.Color.LightGray;
        //    btnGraph.BackColor = backColor;
        //    btnCircleDiagram.BackColor = backColor;
        //    btnScatterPlot.BackColor = backColor;
        //    btnGistogram.BackColor = backColor;
        //    btnRadarDiagram.BackColor = backColor;
        //}

        ///// <summary>
        ///// Инициализация данных из StartNewWork (вызывается из ProgramForm)
        ///// </summary>
        //public void InitializeData(List<ObjectOfAnalysis> operations)
        //{
        //    _operations = new List<ObjectOfAnalysis>(operations);

        //    //Заполняем таблицу,
        //    FillDataGridView();

        //    // Генерируем отчёт (один раз)
        //    GenerateReport();

        //    // Включаем кнопки графиков после получения данных
        //    SetChartButtonsEnabled();

        //    // По умолчанию показываем таблицу
        //    ShowTableView();
        //}

        ///// <summary>
        ///// Заполнение DataGridView в GetAnalysis копией данных из StartNewWork
        ///// </summary>
        //private void FillDataGridView()
        //{
        //    // Очищаем имеющуюся таблицу
        //    dgvTableData.Rows.Clear();
        //    dgvTableData.Columns.Clear();

        //    // Настройка колонок
        //    DataGridViewTextBoxColumn colNumber = new DataGridViewTextBoxColumn(); //Номер операции
        //    colNumber.Name = "colNumber";
        //    colNumber.HeaderText = "№";
        //    colNumber.Width = 50;
        //    colNumber.ReadOnly = true;
        //    colNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    dgvTableData.Columns.Add(colNumber);

        //    DataGridViewTextBoxColumn colAmount = new DataGridViewTextBoxColumn(); //Размер операции
        //    colAmount.Name = "colAmount";
        //    colAmount.HeaderText = "Размер";
        //    colAmount.Width = 100;
        //    colAmount.DefaultCellStyle.Format = "N2";
        //    colAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgvTableData.Columns.Add(colAmount);

        //    DataGridViewTextBoxColumn colType = new DataGridViewTextBoxColumn(); // Тип операции
        //    colType.Name = "colType";
        //    colType.HeaderText = "Тип";
        //    colType.Width = 120;
        //    dgvTableData.Columns.Add(colType);

        //    DataGridViewTextBoxColumn colCategory = new DataGridViewTextBoxColumn(); // Категория операции
        //    colCategory.Name = "colCategory";
        //    colCategory.HeaderText = "Категория";
        //    colCategory.Width = 120;
        //    dgvTableData.Columns.Add(colCategory);

        //    DataGridViewTextBoxColumn colCurrency = new DataGridViewTextBoxColumn(); //Валюта операции
        //    colCurrency.Name = "colCurrency";
        //    colCurrency.HeaderText = "Валюта";
        //    colCurrency.Width = 80;
        //    dgvTableData.Columns.Add(colCurrency);

        //    DataGridViewTextBoxColumn colDate = new DataGridViewTextBoxColumn(); // Дата операции
        //    colDate.Name = "colDate";
        //    colDate.HeaderText = "Дата";
        //    colDate.Width = 100;
        //    colDate.DefaultCellStyle.Format = "dd.MM.yyyy";
        //    dgvTableData.Columns.Add(colDate);

        //    // Заполнение данными, проходимся циклом по имеющейся таблице данных
        //    for (int i = 0; i < _operations.Count; i++)
        //    {
        //        ObjectOfAnalysis op = _operations[i];
        //        dgvTableData.Rows.Add((i + 1).ToString(), op.Sum, op.TypeOfOperation, op.Category, op.Currency, op.Date);
        //    }

        //    dgvTableData.ClearSelection();
        //}

        ///// <summary>
        ///// Генерация форматированного отчета
        ///// </summary>
        //private void GenerateReport()
        //{
        //    rtbReport.Clear(); //Очищаем имеющийся отчет, если отчет уже был какой-то сгенерирован, но у нас поменялись данные

        //    // Подсчёт основных показателей: итоговая сумма, 
        //    decimal totalAmount = 0;
        //    decimal maxAmount = _operations[0].Sum;
        //    decimal minAmount = _operations[0].Sum;

        //    for (int i = 0; i < _operations.Count; i++)
        //    {
        //        totalAmount += _operations[i].Sum;
        //        if (_operations[i].Sum > maxAmount) maxAmount = _operations[i].Sum;
        //        if (_operations[i].Sum < minAmount) minAmount = _operations[i].Sum;
        //    }

        //    decimal averageAmount = totalAmount / _operations.Count;
        //    decimal range = maxAmount - minAmount;

        //    // === ШАПКА ===
        //    AppendFormattedText("╔══════════════════════════════════════════════════════════════════╗\n", System.Drawing.Color.Black);
        //    AppendFormattedText("║                    ОТЧЁТ ПО АНАЛИЗУ ОПЕРАЦИЙ                     ║\n", System.Drawing.Color.DarkBlue, true);
        //    AppendFormattedText("╚══════════════════════════════════════════════════════════════════╝\n\n", System.Drawing.Color.Black);

        //    // === 1. ОБЩАЯ СТАТИСТИКА ===
        //    AppendFormattedText("▶ 1. ОБЩАЯ СТАТИСТИКА\n", System.Drawing.Color.DarkGreen, true);
        //    AppendFormattedText("────────────────────────────────────────────────────────────────────\n", System.Drawing.Color.Gray);

        //    AppendFormattedText($"   • Всего операций:           {_operations.Count}\n", System.Drawing.Color.Black);
        //    AppendFormattedText($"   • Общая сумма:              {totalAmount:N2} руб.\n", System.Drawing.Color.Black);
        //    AppendFormattedText($"   • Средний чек:              {averageAmount:N2} руб.\n", System.Drawing.Color.Black);
        //    AppendFormattedText($"   • Максимальная операция:    {maxAmount:N2} руб.\n", System.Drawing.Color.Black);
        //    AppendFormattedText($"   • Минимальная операция:     {minAmount:N2} руб.\n", System.Drawing.Color.Black);
        //    AppendFormattedText($"   • Размах (max-min):         {range:N2} руб.\n\n", System.Drawing.Color.Black);

        //    // === 2. АНАЛИЗ ДЛЯ КРУГОВОЙ ДИАГРАММЫ (по категориям) ===
        //    AppendFormattedText("▶ 2. АНАЛИЗ ДЛЯ КРУГОВОЙ ДИАГРАММЫ (распределение по категориям)\n", System.Drawing.Color.DarkGreen, true);
        //    AppendFormattedText("────────────────────────────────────────────────────────────────────\n", System.Drawing.Color.Gray);

        //    // Группировка по категориям
        //    Dictionary<string, decimal> categorySums = new Dictionary<string, decimal>();
        //    for (int i = 0; i < _operations.Count; i++)
        //    {
        //        string category = _operations[i].Category;
        //        if (categorySums.ContainsKey(category))
        //        {
        //            categorySums[category] += _operations[i].Sum;
        //        }
        //        else
        //        {
        //            categorySums[category] = _operations[i].Sum;
        //        }
        //    }

        //    foreach (KeyValuePair<string, decimal> pair in categorySums)
        //    {
        //        double percentage = (double)(pair.Value / totalAmount) * 100;
        //        AppendFormattedText($"   • {pair.Key}: {pair.Value:N2} руб. ({percentage:F1}%)\n", System.Drawing.Color.Black);
        //    }
        //    AppendFormattedText("\n");

        //    // === 3. АНАЛИЗ ДЛЯ ГИСТОГРАММЫ (распределение сумм по интервалам) ===
        //    AppendFormattedText("▶ 3. АНАЛИЗ ДЛЯ ГИСТОГРАММЫ (распределение сумм операций)\n", System.Drawing.Color.DarkGreen, true);
        //    AppendFormattedText("────────────────────────────────────────────────────────────────────\n", System.Drawing.Color.Gray);

        //    // Создаём 5 интервалов
        //    decimal step = (maxAmount - minAmount) / 5;

        //    for (int i = 0; i < 5; i++)
        //    {
        //        decimal lower = minAmount + i * step;
        //        decimal upper = (i == 4) ? maxAmount : minAmount + (i + 1) * step;

        //        int count = 0;
        //        for (int j = 0; j < _operations.Count; j++)
        //        {
        //            if (_operations[j].Sum >= lower && _operations[j].Sum <= upper)
        //            {
        //                count++;
        //            }
        //        }

        //        double percent = (double)count / _operations.Count * 100;
        //        AppendFormattedText($"   • {lower:N0} - {upper:N0} руб.: {count} операций ({percent:F1}%)\n", System.Drawing.Color.Black);
        //    }
        //    AppendFormattedText("\n");

        //    // === 4. АНАЛИЗ ДЛЯ ТОЧЕЧНОЙ ДИАГРАММЫ (тренд по датам) ===
        //    AppendFormattedText("▶ 4. АНАЛИЗ ДЛЯ ТОЧЕЧНОЙ ДИАГРАММЫ (тренд по датам)\n", System.Drawing.Color.DarkGreen, true);
        //    AppendFormattedText("────────────────────────────────────────────────────────────────────\n", System.Drawing.Color.Gray);

        //    // Сортировка по дате
        //    List<ObjectOfAnalysis> sortedByDate = new List<ObjectOfAnalysis>(_operations);
        //    for (int i = 0; i < sortedByDate.Count - 1; i++)
        //    {
        //        for (int j = i + 1; j < sortedByDate.Count; j++)
        //        {
        //            if (sortedByDate[i].Date > sortedByDate[j].Date)
        //            {
        //                ObjectOfAnalysis temp = sortedByDate[i];
        //                sortedByDate[i] = sortedByDate[j];
        //                sortedByDate[j] = temp;
        //            }
        //        }
        //    }

        //    AppendFormattedText("   • Динамика сумм по датам (все операции):\n", System.Drawing.Color.Black);
        //    for (int i = 0; i < sortedByDate.Count; i++)
        //    {
        //        AppendFormattedText($"        {i + 1}. {sortedByDate[i].Date:dd.MM.yyyy}: {sortedByDate[i].Sum:N2} руб.\n", System.Drawing.Color.Black);
        //    }

        //    // Простой расчёт тренда (увеличение/уменьшение)
        //    int increasingCount = 0;
        //    int decreasingCount = 0;
        //    for (int i = 1; i < sortedByDate.Count; i++)
        //    {
        //        if (sortedByDate[i].Sum > sortedByDate[i - 1].Sum)
        //            increasingCount++;
        //        else if (sortedByDate[i].Sum < sortedByDate[i - 1].Sum)
        //            decreasingCount++;
        //    }

        //    AppendFormattedText($"\n   • Количество увеличений суммы: {increasingCount}\n", System.Drawing.Color.Black);
        //    AppendFormattedText($"   • Количество уменьшений суммы: {decreasingCount}\n\n", System.Drawing.Color.Black);

        //    // === 5. АНАЛИЗ ДЛЯ ЛИНЕЙНОГО ГРАФИКА ===
        //    AppendFormattedText("▶ 5. АНАЛИЗ ДЛЯ ЛИНЕЙНОГО ГРАФИКА (изменение во времени)\n", System.Drawing.Color.DarkGreen, true);
        //    AppendFormattedText("────────────────────────────────────────────────────────────────────\n", System.Drawing.Color.Gray);

        //    if (sortedByDate.Count >= 2)
        //    {
        //        decimal firstAmount = sortedByDate[0].Sum;
        //        decimal lastAmount = sortedByDate[sortedByDate.Count - 1].Sum;
        //        decimal change = lastAmount - firstAmount;
        //        string trend = change >= 0 ? "рост" : "снижение";

        //        AppendFormattedText($"   • Начальная сумма: {firstAmount:N2} руб.\n", System.Drawing.Color.Black);
        //        AppendFormattedText($"   • Конечная сумма: {lastAmount:N2} руб.\n", System.Drawing.Color.Black);
        //        AppendFormattedText($"   • Общее изменение: {change:N2} руб. ({trend})\n", System.Drawing.Color.Black);
        //    }
        //    AppendFormattedText("\n");

        //    // === 6. АНАЛИЗ ДЛЯ ЛЕПЕСТКОВОЙ ДИАГРАММЫ (по типам операций) ===
        //    AppendFormattedText("▶ 6. АНАЛИЗ ДЛЯ ЛЕПЕСТКОВОЙ ДИАГРАММЫ (распределение по типам операций)\n", System.Drawing.Color.DarkGreen, true);
        //    AppendFormattedText("────────────────────────────────────────────────────────────────────\n", System.Drawing.Color.Gray);

        //    // Группировка по типам
        //    Dictionary<string, decimal> typeSums = new Dictionary<string, decimal>();
        //    for (int i = 0; i < _operations.Count; i++)
        //    {
        //        string type = _operations[i].TypeOfOperation;
        //        if (typeSums.ContainsKey(type))
        //        {
        //            typeSums[type] += _operations[i].Sum;
        //        }
        //        else
        //        {
        //            typeSums[type] = _operations[i].Sum;
        //        }
        //    }

        //    foreach (KeyValuePair<string, decimal> pair in typeSums)
        //    {
        //        double percentage = (double)(pair.Value / totalAmount) * 100;
        //        AppendFormattedText($"   • {pair.Key}: {pair.Value:N2} руб. ({percentage:F1}%)\n", System.Drawing.Color.Black);
        //    }
        //    AppendFormattedText("\n");

        //    // === 7. ДИСПЕРСИЯ И СТАНДАРТНОЕ ОТКЛОНЕНИЕ ===
        //    AppendFormattedText("▶ 7. МЕРЫ РАЗБРОСА (1 курс, простые формулы)\n", System.Drawing.Color.DarkGreen, true);
        //    AppendFormattedText("────────────────────────────────────────────────────────────────────\n", System.Drawing.Color.Gray);

        //    double variance = 0;
        //    double stdDev = 0;
        //    if (_operations.Count > 1)
        //    {
        //        double avg = (double)averageAmount;
        //        double sumSquaredDiffs = 0;
        //        for (int i = 0; i < _operations.Count; i++)
        //        {
        //            double diff = (double)_operations[i].Sum - avg;
        //            sumSquaredDiffs += diff * diff;
        //        }
        //        variance = sumSquaredDiffs / (_operations.Count - 1);
        //        stdDev = Math.Sqrt(variance);
        //    }

        //    AppendFormattedText($"   • Дисперсия:                 {variance:F2}\n", System.Drawing.Color.Black);
        //    AppendFormattedText($"   • Стандартное отклонение (σ): {stdDev:F2} руб.\n", System.Drawing.Color.Black);
        //    if (averageAmount != 0)
        //    {
        //        AppendFormattedText($"   • Коэффициент вариации:      {(stdDev / (double)averageAmount * 100):F1}%\n", System.Drawing.Color.Black);
        //    }
        //    AppendFormattedText("\n");

        //    // === 8. МЕДИАНА ===
        //    AppendFormattedText("▶ 8. МЕДИАНА (значение в середине ряда)\n", System.Drawing.Color.DarkGreen, true);
        //    AppendFormattedText("────────────────────────────────────────────────────────────────────\n", System.Drawing.Color.Gray);

        //    List<decimal> sortedAmounts = new List<decimal>();
        //    for (int i = 0; i < _operations.Count; i++)
        //    {
        //        sortedAmounts.Add(_operations[i].Sum);
        //    }
        //    for (int i = 0; i < sortedAmounts.Count - 1; i++)
        //    {
        //        for (int j = i + 1; j < sortedAmounts.Count; j++)
        //        {
        //            if (sortedAmounts[i] > sortedAmounts[j])
        //            {
        //                decimal temp = sortedAmounts[i];
        //                sortedAmounts[i] = sortedAmounts[j];
        //                sortedAmounts[j] = temp;
        //            }
        //        }
        //    }

        //    decimal median;
        //    if (sortedAmounts.Count % 2 == 0)
        //    {
        //        median = (sortedAmounts[sortedAmounts.Count / 2 - 1] + sortedAmounts[sortedAmounts.Count / 2]) / 2;
        //    }
        //    else
        //    {
        //        median = sortedAmounts[sortedAmounts.Count / 2];
        //    }

        //    AppendFormattedText($"   • Медиана: {median:N2} руб.\n", System.Drawing.Color.Black);
        //    AppendFormattedText($"   • Среднее: {averageAmount:N2} руб.\n", System.Drawing.Color.Black);
        //    AppendFormattedText($"   • Разница: {(averageAmount - median):N2} руб.\n", System.Drawing.Color.Black);

        //    if (averageAmount > median)
        //    {
        //        AppendFormattedText("   • Вывод: Распределение смещено вправо (есть крупные операции)\n", System.Drawing.Color.DarkOrange);
        //    }
        //    else if (averageAmount < median)
        //    {
        //        AppendFormattedText("   • Вывод: Распределение смещено влево (есть мелкие операции)\n", System.Drawing.Color.DarkOrange);
        //    }
        //    else
        //    {
        //        AppendFormattedText("   • Вывод: Распределение симметричное\n", System.Drawing.Color.DarkOrange);
        //    }
        //    AppendFormattedText("\n");

        //    // === 9. МОДА ===
        //    AppendFormattedText("▶ 9. МОДА (наиболее часто встречающаяся сумма)\n", System.Drawing.Color.DarkGreen, true);
        //    AppendFormattedText("────────────────────────────────────────────────────────────────────\n", System.Drawing.Color.Gray);

        //    Dictionary<decimal, int> frequency = new Dictionary<decimal, int>();
        //    for (int i = 0; i < _operations.Count; i++)
        //    {
        //        decimal sum = _operations[i].Sum;
        //        if (frequency.ContainsKey(sum))
        //        {
        //            frequency[sum]++;
        //        }
        //        else
        //        {
        //            frequency[sum] = 1;
        //        }
        //    }

        //    int maxFrequency = 0;
        //    decimal mode = 0;
        //    foreach (KeyValuePair<decimal, int> pair in frequency)
        //    {
        //        if (pair.Value > maxFrequency)
        //        {
        //            maxFrequency = pair.Value;
        //            mode = pair.Key;
        //        }
        //    }

        //    if (maxFrequency > 1)
        //    {
        //        AppendFormattedText($"   • Мода: {mode:N2} руб. (встречается {maxFrequency} раз)\n", System.Drawing.Color.Black);
        //    }
        //    else
        //    {
        //        AppendFormattedText("   • Мода: нет повторяющихся значений\n", System.Drawing.Color.Black);
        //    }
        //    AppendFormattedText("\n");

        //    // === 10. АНАЛИЗ ПО ВАЛЮТАМ ===
        //    AppendFormattedText("▶ 10. АНАЛИЗ ПО ВАЛЮТАМ\n", System.Drawing.Color.DarkGreen, true);
        //    AppendFormattedText("────────────────────────────────────────────────────────────────────\n", System.Drawing.Color.Gray);

        //    Dictionary<string, decimal> currencySums = new Dictionary<string, decimal>();
        //    for (int i = 0; i < _operations.Count; i++)
        //    {
        //        string currency = _operations[i].Currency;
        //        if (currencySums.ContainsKey(currency))
        //        {
        //            currencySums[currency] += _operations[i].Sum;
        //        }
        //        else
        //        {
        //            currencySums[currency] = _operations[i].Sum;
        //        }
        //    }

        //    foreach (KeyValuePair<string, decimal> pair in currencySums)
        //    {
        //        AppendFormattedText($"   • {pair.Key}: {pair.Value:N2}\n", System.Drawing.Color.Black);
        //    }
        //    AppendFormattedText("\n");

        //    // === ПОДПИСЬ ===
        //    AppendFormattedText("══════════════════════════════════════════════════════════════════\n", System.Drawing.Color.Gray);
        //    AppendFormattedText("Отчёт сгенерирован автоматически. Мяу! 🐱\n", System.Drawing.Color.DarkOrange);
        //    AppendFormattedText($"Дата: {DateTime.Now:dd.MM.yyyy HH:mm:ss}\n", System.Drawing.Color.Gray);
        //}

        ///// <summary>
        ///// Вспомогательный метод для добавления форматированного текста в RichTextBox
        ///// </summary>
        //private void AppendFormattedText(string text, System.Drawing.Color color, bool isBold = false)
        //{
        //    rtbReport.SelectionStart = rtbReport.TextLength;
        //    rtbReport.SelectionLength = 0;
        //    rtbReport.SelectionColor = color;
        //    if (isBold)
        //    {
        //        rtbReport.SelectionFont = new Font(rtbReport.Font, System.Drawing.FontStyle.Bold);
        //    }
        //    else
        //    {
        //        rtbReport.SelectionFont = new Font(rtbReport.Font, System.Drawing.FontStyle.Regular);
        //    }
        //    rtbReport.AppendText(text);
        //    rtbReport.SelectionColor = rtbReport.ForeColor;
        //}

        //private void AppendFormattedText(string text, bool isBold = false)
        //{
        //    rtbReport.SelectionStart = rtbReport.TextLength;
        //    rtbReport.SelectionLength = 0;
        //    if (isBold)
        //    {
        //        rtbReport.SelectionFont = new Font(rtbReport.Font, System.Drawing.FontStyle.Bold);
        //    }
        //    else
        //    {
        //        rtbReport.SelectionFont = new Font(rtbReport.Font, System.Drawing.FontStyle.Regular);
        //    }
        //    rtbReport.AppendText(text);
        //    rtbReport.SelectionColor = rtbReport.ForeColor;
        //}

        //// ==================== ВИЗУАЛИЗАЦИЯ ====================

        ///// <summary>
        ///// Показать таблицу (по умолчанию)
        ///// </summary>
        //private void ShowTableView()
        //{
        //    dgvTableData.Visible = true;
        //    pnlChart.Visible = false;
        //    formsPlot.Visible = false;
        //}

        ///// <summary>
        ///// Показать график в ScottPlot
        ///// </summary>
        //private void ShowPlot(Action<FormsPlot> drawAction)
        //{
        //    dgvTableData.Visible = false;
        //    pnlChart.Visible = true;
        //    formsPlot.Visible = true;
        //    drawAction(formsPlot);
        //}

        ///// <summary>
        ///// Линейный график (по датам)
        ///// </summary>
        //private void DrawLineChart()
        //{
        //    if (_operations == null || _operations.Count == 0) return;

        //    // Сортировка по дате
        //    List<ObjectOfAnalysis> sortedByDate = new List<ObjectOfAnalysis>(_operations);
        //    for (int i = 0; i < sortedByDate.Count - 1; i++)
        //    {
        //        for (int j = i + 1; j < sortedByDate.Count; j++)
        //        {
        //            if (sortedByDate[i].Date > sortedByDate[j].Date)
        //            {
        //                ObjectOfAnalysis temp = sortedByDate[i];
        //                sortedByDate[i] = sortedByDate[j];
        //                sortedByDate[j] = temp;
        //            }
        //        }
        //    }

        //    double[] xs = new double[sortedByDate.Count];
        //    double[] ys = new double[sortedByDate.Count];
        //    for (int i = 0; i < sortedByDate.Count; i++)
        //    {
        //        xs[i] = i + 1;
        //        ys[i] = (double)sortedByDate[i].Sum;
        //    }

        //    ShowPlot(delegate (FormsPlot plot)
        //    {
        //        plot.Plot.Clear();
        //        var scatter = plot.Plot.Add.Scatter(xs, ys);
        //        scatter.Label = "Сумма операции";
        //        scatter.Color = ScottPlot.Color.FromHex("#1E88E5");
        //        scatter.LineWidth = 2;
        //        plot.Plot.XLabel("Номер операции (по дате)");
        //        plot.Plot.YLabel("Сумма (руб.)");
        //        plot.Plot.Title("Линейный график операций");
        //        plot.Plot.ShowLegend();
        //        plot.Refresh();
        //    });
        //}

        ///// <summary>
        ///// Круговая диаграмма (по категориям)
        ///// </summary>
        //private void DrawPieChart()
        //{
        //    if (_operations == null || _operations.Count == 0) return;

        //    // Группировка по категориям
        //    Dictionary<string, double> categorySums = new Dictionary<string, double>();
        //    for (int i = 0; i < _operations.Count; i++)
        //    {
        //        string category = _operations[i].Category;
        //        double sum = (double)_operations[i].Sum;
        //        if (categorySums.ContainsKey(category))
        //        {
        //            categorySums[category] += sum;
        //        }
        //        else
        //        {
        //            categorySums[category] = sum;
        //        }
        //    }

        //    ShowPlot(delegate (FormsPlot plot)
        //    {
        //        plot.Plot.Clear();

        //        double[] values = new double[categorySums.Values.Count];
        //        categorySums.Values.CopyTo(values, 0);

        //         pie = plot.Plot.Add.Pie(values);
        //        pie.ShowLabels = true;
        //        pie.SliceLabelDistance = 0.5;

        //        string[] categories = new string[categorySums.Keys.Count];
        //        categorySums.Keys.CopyTo(categories, 0);

        //        for (int i = 0; i < categories.Length; i++)
        //        {
        //            pie.Slices[i].Label = categories[i];
        //        }

        //        plot.Plot.Title("Круговая диаграмма по категориям");
        //        plot.Plot.ShowLegend();
        //        plot.Refresh();
        //    });
        //}

        ///// <summary>
        ///// Точечная диаграмма (сумма vs номер операции)
        ///// </summary>
        //private void DrawScatterPlot()
        //{
        //    if (_operations == null || _operations.Count == 0) return;

        //    double[] xs = new double[_operations.Count];
        //    double[] ys = new double[_operations.Count];
        //    for (int i = 0; i < _operations.Count; i++)
        //    {
        //        xs[i] = i + 1;
        //        ys[i] = (double)_operations[i].Sum;
        //    }

        //    ShowPlot(delegate (FormsPlot plot)
        //    {
        //        plot.Plot.Clear();
        //        var scatter = plot.Plot.Add.Scatter(xs, ys);
        //        scatter.Label = "Операции";
        //        scatter.Color = ScottPlot.Color.FromHex("#E53935");
        //        scatter.MarkerSize = 8;
        //        scatter.LineWidth = 0;
        //        plot.Plot.XLabel("Номер операции");
        //        plot.Plot.YLabel("Сумма (руб.)");
        //        plot.Plot.Title("Точечная диаграмма операций");
        //        plot.Plot.ShowLegend();
        //        plot.Refresh();
        //    });
        //}

        ///// <summary>
        ///// Гистограмма (распределение сумм)
        ///// </summary>
        //private void DrawHistogram()
        //{
        //    if (_operations == null || _operations.Count == 0) return;

        //    double[] values = new double[_operations.Count];
        //    for (int i = 0; i < _operations.Count; i++)
        //    {
        //        values[i] = (double)_operations[i].Sum;
        //    }

        //    ShowPlot(delegate (FormsPlot plot)
        //    {
        //        plot.Plot.Clear();
        //        var histogram = plot.Plot.Add.Histogram();
        //        histogram.BarColor = ScottPlot.Color.FromHex("#43A047");
        //        plot.Plot.XLabel("Сумма операции (руб.)");
        //        plot.Plot.YLabel("Частота");
        //        plot.Plot.Title("Гистограмма распределения сумм");
        //        plot.Refresh();
        //    });
        //}

        ///// <summary>
        ///// Лепестковая диаграмма (по типам операций)
        ///// </summary>
        //private void DrawRadarDiagram()
        //{
        //    if (_operations == null || _operations.Count == 0) return;

        //    // Группировка по типам
        //    Dictionary<string, double> typeSums = new Dictionary<string, double>();
        //    double maxTypeSum = 0;

        //    for (int i = 0; i < _operations.Count; i++)
        //    {
        //        string type = _operations[i].TypeOfOperation;
        //        double sum = (double)_operations[i].Sum;
        //        if (typeSums.ContainsKey(type))
        //        {
        //            typeSums[type] += sum;
        //        }
        //        else
        //        {
        //            typeSums[type] = sum;
        //        }
        //        if (typeSums[type] > maxTypeSum) maxTypeSum = typeSums[type];
        //    }

        //    // Нормализация значений (0-1)
        //    string[] types = new string[typeSums.Keys.Count];
        //    double[] values = new double[typeSums.Values.Count];
        //    typeSums.Keys.CopyTo(types, 0);

        //    int index = 0;
        //    foreach (KeyValuePair<string, double> pair in typeSums)
        //    {
        //        values[index] = pair.Value / maxTypeSum;
        //        index++;
        //    }

        //    ShowPlot(delegate (FormsPlot plot)
        //    {
        //        plot.Plot.Clear();
        //        var radar = plot.Plot.Add.Radar(values);
        //        radar.FillColor = ScottPlot.Color.FromHex("#FF9800").WithAlpha(80);
        //        radar.LineColor = ScottPlot.Color.FromHex("#E65100");

        //        for (int i = 0; i < types.Length; i++)
        //        {
        //            radar.Categories[i] = types[i];
        //        }

        //        plot.Plot.Title("Лепестковая диаграмма (типы операций)");
        //        plot.Plot.ShowLegend();
        //        plot.Refresh();
        //    });
        //}

        //// ==================== ОБРАБОТЧИКИ КНОПОК ====================

        ///// <summary>
        ///// Показать таблицу
        ///// </summary>
        //private void btnTable_Click(object sender, EventArgs e)
        //{
        //    ShowTableView();

        //    // Визуальное выделение активной кнопки
        //    ResetChartButtonsColor();
        //    btnTable.BackColor = Color.LightGreen;
        //}

        ///// <summary>
        ///// Показать линейный график
        ///// </summary>
        //private void btnGraph_Click(object sender, EventArgs e)
        //{
        //    DrawLineChart();
        //    ResetChartButtonsColor();
        //    btnGraph.BackColor = Color.LightGreen;
        //}

        ///// <summary>
        ///// Показать круговую диаграмму
        ///// </summary>
        //private void btnCircleDiagram_Click(object sender, EventArgs e)
        //{
        //    DrawPieChart();
        //    ResetChartButtonsColor();
        //    btnCircleDiagram.BackColor = Color.LightGreen;
        //}

        ///// <summary>
        ///// Показать точечную диаграмму
        ///// </summary>
        //private void btnScatterPlot_Click(object sender, EventArgs e)
        //{
        //    DrawScatterPlot();
        //    ResetChartButtonsColor();
        //    btnScatterPlot.BackColor = Color.LightGreen;
        //}

        ///// <summary>
        ///// Показать гистограмму
        ///// </summary>
        //private void btnGistogram_Click(object sender, EventArgs e)
        //{
        //    DrawHistogram();
        //    ResetChartButtonsColor();
        //    btnGistogram.BackColor = Color.LightGreen;
        //}

        ///// <summary>
        ///// Показать лепестковую диаграмму
        ///// </summary>
        //private void btnRadarDiagram_Click(object sender, EventArgs e)
        //{
        //    DrawRadarDiagram();
        //    ResetChartButtonsColor();
        //    btnRadarDiagram.BackColor = Color.LightGreen;
        //}

        ///// <summary>
        ///// Сброс цвета всех кнопок визуализации
        ///// </summary>
        //private void ResetChartButtonsColor()
        //{
        //    btnTable.BackColor = SystemColors.Control;
        //    btnGraph.BackColor = SystemColors.Control;
        //    btnCircleDiagram.BackColor = SystemColors.Control;
        //    btnScatterPlot.BackColor = SystemColors.Control;
        //    btnGistogram.BackColor = SystemColors.Control;
        //    btnRadarDiagram.BackColor = SystemColors.Control;
        //}

        ///// <summary>
        ///// Сохранение отчёта в JSON
        ///// </summary>
        //private void btnSaveReport_Click(object sender, EventArgs e)
        //{
        //    if (_operations == null || _operations.Count == 0)
        //    {
        //        MessageBox.Show("Нет данных для сохранения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    // Диалог выбора имени файла
        //    SaveFileDialog saveDialog = new SaveFileDialog();
        //    saveDialog.Title = "Сохранить отчёт и данные";
        //    saveDialog.Filter = "JSON файлы (*.json)|*.json|Все файлы (*.*)|*.*";
        //    saveDialog.DefaultExt = "json";
        //    saveDialog.InitialDirectory = _saveFolderPath;

        //    if (saveDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        try
        //        {
        //            AnalysisSaveData saveData = new AnalysisSaveData();
        //            saveData.Operations = _operations;
        //            saveData.ReportText = rtbReport.Text;
        //            saveData.ReportRtf = rtbReport.Rtf;
        //            saveData.SaveDate = DateTime.Now;

        //            string jsonString = JsonSerializer.Serialize(saveData, new JsonSerializerOptions { WriteIndented = true });
        //            File.WriteAllText(saveDialog.FileName, jsonString);

        //            MessageBox.Show($"Данные успешно сохранены!\nПуть: {saveDialog.FileName}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}

        ///// <summary>
        ///// Возврат к данным (на экран StartNewWork)
        ///// </summary>
        //private void btnBackToData_Click(object sender, EventArgs e)
        //{
        //    NavigateToStartNewWork?.Invoke(this, EventArgs.Empty);
        //}

        ///// <summary>
        ///// Обработчик для кнопки "Перейти в режим предсказания" (пока заглушка)
        ///// </summary>
        //private void btnSetPredictionMode_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("Режим предсказания будет реализован позже.", "В разработке", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}

        ///// <summary>
        ///// Обработчик клика по lblMessage (оставлен для совместимости с дизайнером)
        ///// </summary>
        //private void lblMessage_Click(object sender, EventArgs e)
        //{
        //    // Пока ничего не делает
        //}
    }
}
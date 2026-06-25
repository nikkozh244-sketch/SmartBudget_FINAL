using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace SmartBudget
{
    public static class LocalizationManager
    {
        private static string _currentLanguage = "Русский";
        private static Dictionary<string, Dictionary<string, string>> _translations;

        static LocalizationManager()
        {
            LoadTranslations();
        }

        public static string CurrentLanguage
        {
            get { return _currentLanguage; }
            set
            {
                _currentLanguage = value;
                Thread.CurrentThread.CurrentCulture = new CultureInfo(value == "Русский" ? "ru-RU" : "en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(value == "Русский" ? "ru-RU" : "en-US");
            }
        }

        private static void LoadTranslations()
        {
            _translations = new Dictionary<string, Dictionary<string, string>>();

            // ==================== РУССКИЙ ЯЗЫК ====================
            var ru = new Dictionary<string, string>();

            // Главное меню
            ru["MainMenu_Title"] = "Smart Budget";
            ru["MainMenu_ContinueWork"] = "Продолжить работу";
            ru["MainMenu_StartNewWork"] = "Начать новую работу";
            ru["MainMenu_About"] = "О приложении";
            ru["MainMenu_Settings"] = "Настройки";
            ru["MainMenu_Exit"] = "Выход";

            // О приложении
            ru["About_Title"] = "О приложении";
            ru["About_Welcome"] = "Мур-р-р! Добро пожаловать в Smart Budget - приложение, которое поможет вам с работой с личными финансами! Для ознакомления с работой просмотрите видео или прочтите справочник пользователя";
            ru["About_OpenHelp"] = "Открыть справочник пользователя";

            // Настройки
            ru["Settings_Welcome"] = "Добро пожаловать в меню настроек, мяу! Здесь вы можете настроить приложение специально под себя!";
            ru["Settings_ChangeDollar"] = "Смена курса доллара";
            ru["Settings_DollarDescription"] = "Введите текущий курс доллара:";
            ru["Settings_ChangeLanguage"] = "Смена языка";
            ru["Settings_LanguageDescription"] = "Выберете язык приложения:";
            ru["Settings_DarkTheme"] = "Тёмная тема";
            ru["Settings_DarkDescription"] = "Тёмный фон, светлый текст:";
            ru["Settings_LeftHanded"] = "\"Режим левши\"";
            ru["Settings_LeftHandedDescription"] = "Отзеркаливает интерфейс:";
            ru["Settings_DogMode"] = "\"Режим собачника\"";
            ru["Settings_DogModeDescription"] = "Меняет помощника на собаку:";
            ru["Settings_Apply"] = "Применить настройки";
            ru["Settings_Reset"] = "Сбросить настройки";
            ru["Settings_Menu"] = "Меню";
            ru["Settings_ReturnToHome"] = "Вернуться в главное меню";
            ru["Settings_Language_Russian"] = "Русский";
            ru["Settings_Language_English"] = "English";
            ru["Settings_On"] = "Вкл";
            ru["Settings_Off"] = "Выкл";

            // StartNewWork
            ru["StartNewWork_Welcome"] = "Мяу! Для начала работы введите данные об операциях, и они будут записаны в таблицу!";
            ru["StartNewWork_Amount"] = "Размер операции";
            ru["StartNewWork_Type"] = "Тип операции";
            ru["StartNewWork_Category"] = "Категория операции";
            ru["StartNewWork_Currency"] = "Валюта";
            ru["StartNewWork_Date"] = "Дата операции";
            ru["StartNewWork_Add"] = "Добавить";
            ru["StartNewWork_Change"] = "Изменить";
            ru["StartNewWork_Delete"] = "Удалить";
            ru["StartNewWork_Done"] = "Готово";
            ru["StartNewWork_Currency_RUB"] = "Рубли";
            ru["StartNewWork_Currency_USD"] = "Доллары";

            // Типы операций
            ru["StartNewWork_Type_Transfer"] = "Перевод";
            ru["StartNewWork_Type_Withdrawal"] = "Снятие";
            ru["StartNewWork_Type_WriteOff"] = "Списание";
            ru["StartNewWork_Type_Replenishment"] = "Зачисление";

            // Категории операций
            ru["StartNewWork_Category_Food"] = "Продукты";
            ru["StartNewWork_Category_Cafe"] = "Кафе";
            ru["StartNewWork_Category_Transport"] = "Транспорт";
            ru["StartNewWork_Category_Delivery"] = "Доставка";
            ru["StartNewWork_Category_Clothes"] = "Одежда";
            ru["StartNewWork_Category_Electronics"] = "Электротехника";

            // Сообщения StartNewWork
            ru["StartNewWork_Message_AddSuccess"] = "Новая операция успешно добавлена!";
            ru["StartNewWork_Message_ChangeSuccess"] = "Операция была успешно изменена!";
            ru["StartNewWork_Message_DeleteSuccess"] = "Операция удалена!";
            ru["StartNewWork_Message_NoData"] = "Нет операций для анализа! Пожалуйста, добавьте хотя бы одну операцию.";
            ru["StartNewWork_Message_NoDataForChange"] = "Нет операций для изменения! Сначала добавьте операцию.";
            ru["StartNewWork_Message_SelectForChange"] = "Сначала выберите операцию для изменения!";
            ru["StartNewWork_Message_NoDataForDelete"] = "Нет операций для удаления! Сначала добавьте операцию.";
            ru["StartNewWork_Message_SelectForDelete"] = "Сначала выберите операцию для удаления!";
            ru["StartNewWork_Message_DeleteConfirm"] = "Вы уверены, что хотите удалить эту операцию?";
            ru["StartNewWork_Message_DataError"] = "Неверный формат данных! Для даты используйте формат ДД.ММ.ГГГГ";
            ru["StartNewWork_Message_AmountZero"] = "Размер операции не может равняться нулю!";
            ru["StartNewWork_Message_SelectType"] = "Пожалуйста, выберите тип операции!";
            ru["StartNewWork_Message_TypeMaxLength"] = "Тип операции не может быть длиннее 50 символов!";
            ru["StartNewWork_Message_SelectCategory"] = "Пожалуйста, выберите категорию!";
            ru["StartNewWork_Message_CategoryMaxLength"] = "Категория не может быть длиннее 50 символов!";
            ru["StartNewWork_Message_SelectCurrency"] = "Пожалуйста, выберите валюту!";
            ru["StartNewWork_Message_MaxOperations"] = "Извините, но нельзя добавить более {0} операций!";
            ru["StartNewWork_Message_LoadSuccess"] = "Загружено {0} операций из проекта!";

            // GetAnalys
            ru["GetAnalys_Message"] = "Анализ данных завершен! Мяу! Если захотите изменить данные об операциях, то нажмите на кнопку \"Назад к данным\", а когда закончите работать - не забудьте \"Сохранить отчет\", мур!";
            ru["GetAnalys_ReportHeader"] = "ОТЧЁТ ПО АНАЛИЗУ ОПЕРАЦИЙ";
            ru["GetAnalys_ChartTypes"] = "ТИПЫ ВИЗУАЛИЗАЦИИ";
            ru["GetAnalys_Actions"] = "ДЕЙСТВИЯ";
            ru["GetAnalys_SaveReport"] = "Сохранить отчет";
            ru["GetAnalys_BackToData"] = "Назад к данным";
            ru["GetAnalys_Table"] = "Таблица";
            ru["GetAnalys_Graph"] = "График";
            ru["GetAnalys_CircleDiagram"] = "Круг. диаграмма";
            ru["GetAnalys_ScatterPlot"] = "Точечная";
            ru["GetAnalys_Gistogram"] = "Гистограмма";
            ru["GetAnalys_RadarDiagram"] = "Лепестковая";

            // Сообщения GetAnalys
            ru["GetAnalys_Message_NoData"] = "Нет данных для сохранения! Сначала добавьте операции.";
            ru["GetAnalys_Message_ProjectLimit"] = "Достигнут лимит проектов (максимум 10)!\n\nПожалуйста, удалите ненужные проекты вручную в папке:\n{0}\n\nЗатем перезапустите приложение для продолжения работы.";
            ru["GetAnalys_Message_ProjectExists"] = "Проект с именем \"{0}\" уже существует!\nХотите перезаписать его?";
            ru["GetAnalys_Message_SaveSuccess"] = "Проект \"{0}\" успешно сохранен!";
            ru["GetAnalys_Message_SaveError"] = "Ошибка при сохранении проекта!";
            ru["GetAnalys_Message_EmptyName"] = "Имя проекта не может быть пустым!";
            ru["GetAnalys_Message_SaveDialog"] = "Введите имя для проекта, который будет сохраняться:";
            ru["GetAnalys_Message_NoProjects"] = "У вас пока нет сохраненных проектов.";
            ru["GetAnalys_Message_ExistingProjects"] = "Существующие проекты ({0}/10):";
            ru["GetAnalys_Message_NotSaved"] = "Вы не сохранили проект!\nХотите сохранить перед выходом?";
            ru["GetAnalys_Message_NoDataForAnalysis"] = "Ошибка! Недостаточно данных для анализа! для формирования отчета";
            ru["GetAnalys_Message_NoExpenseData"] = "Нет данных о расходах";

            // Сообщения для диаграмм
            ru["GetAnalys_Chart_GraphTitle"] = "Динамика доходов и расходов (в рублях)";
            ru["GetAnalys_Chart_XLabel"] = "Порядковый номер операции";
            ru["GetAnalys_Chart_YLabel"] = "Сумма (₽)";
            ru["GetAnalys_Chart_PieTitle"] = "Структура расходов по категориям (в рублях)";
            ru["GetAnalys_Chart_ScatterTitle"] = "Точечная диаграмма операций по датам (в рублях)";
            ru["GetAnalys_Chart_HistogramTitle"] = "Гистограмма распределения сумм операций (в рублях)";
            ru["GetAnalys_Chart_RadarTitle"] = "Лепестковая диаграмма по категориям (в рублях)";
            ru["GetAnalys_Chart_NoData"] = "Нет данных";
            ru["GetAnalys_Chart_AllValuesSame"] = "Все значения одинаковы ({0:F2} ₽)";
            ru["GetAnalys_Chart_HistogramXLabel"] = "Диапазон сумм (₽)";
            ru["GetAnalys_Chart_HistogramYLabel"] = "Количество операций";
            ru["GetAnalys_Chart_ScatterXLabel"] = "Дата операции";
            ru["GetAnalys_Chart_ScatterYLabel"] = "Сумма (₽)";

            // Сообщения ошибок
            ru["Error_NotEnoughData"] = "Для построения данной диаграммы необходимо минимум {0} операции!";
            ru["Error_NotEnoughCategories"] = "Для построения круговой диаграммы необходимо минимум {0} разные категории расходов!\nНайдено: {1} категорий.";
            ru["Error_NoData"] = "Нет данных для анализа!";
            ru["Error_SaveSettings"] = "Ошибка при сохранении настроек!";
            ru["Error_LoadSettings"] = "Ошибка при загрузке настроек!";
            ru["Error_LoadProject"] = "Ошибка при загрузке проекта!";
            ru["Error_ProjectEmpty"] = "Файл проекта пустой или поврежден!";

            // Диалоги подтверждения
            ru["Dialog_ExitConfirm"] = "Вы уверены, что хотите выйти?";
            ru["Dialog_ResetSettingsConfirm"] = "Вы уверены, что хотите сбросить все настройки до базовых?";
            ru["Dialog_SettingsNotSaved"] = "Вы изменили настройки, но не сохранили их!\nХотите сохранить перед выходом?";
            ru["Dialog_DeleteConfirm"] = "Вы уверены, что хотите удалить эту операцию?";

            // Заголовки диалогов
            ru["Dialog_Title_Exit"] = "Подтверждение выхода";
            ru["Dialog_Title_Reset"] = "Подтверждение сброса";
            ru["Dialog_Title_SettingsNotSaved"] = "Настройки не сохранены";
            ru["Dialog_Title_Delete"] = "Подтверждение удаления";
            ru["Dialog_Title_Error"] = "Ошибка";
            ru["Dialog_Title_Warning"] = "Внимание";
            ru["Dialog_Title_Success"] = "Успех";
            ru["Dialog_Title_Info"] = "Информация";
            ru["Dialog_Title_SaveProject"] = "Сохранение проекта";
            ru["Dialog_Title_LoadProject"] = "Продолжить работу";

            // Текст отчета
            ru["Report_GeneralInfo"] = "1. Общая информация";
            ru["Report_TopCategories"] = "2. Самые частые категории";
            ru["Report_IncomeCategoryShares"] = "3. Доли категории в доходах";
            ru["Report_ExpenseCategoryShares"] = "4. Доли категорий в расходах";
            ru["Report_TopTypes"] = "5. Самые частые типы операций";
            ru["Report_IncomeTypeShares"] = "6. Доли типов в доходах";
            ru["Report_ExpenseTypeShares"] = "7. Доли типов в расходах";
            ru["Report_ExpenseStructure"] = "8. Структура расходов по категориям";
            ru["Report_CurrencyAnalysis"] = "9. Анализ по валютам";
            ru["Report_DailyStatistics"] = "10. Статистика по дням";
            ru["Report_MonthlyDynamics"] = "11. Динамика по месяцам";
            ru["Report_OperationCount"] = "Количество операций";
            ru["Report_TotalIncome"] = "Общий доход";
            ru["Report_TotalExpense"] = "Общий расход";
            ru["Report_Balance"] = "Итоговый баланс";
            ru["Report_Period"] = "Период анализа";
            ru["Report_ActiveDays"] = "Активных дней";
            ru["Report_DollarRate"] = "Курс доллара";
            ru["Report_Currency"] = "руб.";
            ru["Report_AllAmountsInRubles"] = "Все суммы приведены к рублям";
            ru["Report_YouCanChangeDollarRate"] = "Вы можете изменить курс доллара в настройках";
            ru["Report_DailyAvg"] = "Средняя сумма в день";
            ru["Report_DailyMax"] = "Максимальная сумма в день";
            ru["Report_DailyMin"] = "Минимальная сумма в день";
            ru["Report_BestDay"] = "Лучший день";
            ru["Report_Amount"] = "сумма";
            ru["Report_Generated"] = "Отчет сформирован";
            ru["Report_ByProgram"] = "при помощи программы Smart Budget";
            ru["Report_Income"] = "Доходы";
            ru["Report_Expense"] = "Расходы";
            ru["Report_SameValues"] = "Все значения одинаковы";
            ru["Report_Currency_Column"] = "Валюта";
            ru["Report_Operations_Column"] = "Количество операций";
            ru["Report_Incomes_Column"] = "Доходы";
            ru["Report_Expenses_Column"] = "Расходы";

            // ==================== АНГЛИЙСКИЙ ЯЗЫК ====================
            var en = new Dictionary<string, string>();

            // Главное меню
            en["MainMenu_Title"] = "Smart Budget";
            en["MainMenu_ContinueWork"] = "Continue work";
            en["MainMenu_StartNewWork"] = "Start new work";
            en["MainMenu_About"] = "About";
            en["MainMenu_Settings"] = "Settings";
            en["MainMenu_Exit"] = "Exit";

            // О приложении
            en["About_Title"] = "About";
            en["About_Welcome"] = "Meow! Welcome to Smart Budget - an application that will help you manage your personal finances! To learn how it works, watch the video or read the user manual";
            en["About_OpenHelp"] = "Open user manual";

            // Настройки
            en["Settings_Welcome"] = "Welcome to the settings menu, meow! Here you can customize the application to your needs!";
            en["Settings_ChangeDollar"] = "Change dollar rate";
            en["Settings_DollarDescription"] = "Enter the current dollar rate:";
            en["Settings_ChangeLanguage"] = "Change language";
            en["Settings_LanguageDescription"] = "Select application language:";
            en["Settings_DarkTheme"] = "Dark theme";
            en["Settings_DarkDescription"] = "Dark background, light text:";
            en["Settings_LeftHanded"] = "\"Left-handed mode\"";
            en["Settings_LeftHandedDescription"] = "Mirrors the interface:";
            en["Settings_DogMode"] = "\"Dog mode\"";
            en["Settings_DogModeDescription"] = "Changes assistant to a dog:";
            en["Settings_Apply"] = "Apply settings";
            en["Settings_Reset"] = "Reset settings";
            en["Settings_Menu"] = "Menu";
            en["Settings_ReturnToHome"] = "Return to main menu";
            en["Settings_Language_Russian"] = "Russian";
            en["Settings_Language_English"] = "English";
            en["Settings_On"] = "On";
            en["Settings_Off"] = "Off";

            // StartNewWork
            en["StartNewWork_Welcome"] = "Meow! To get started, enter the data about operations and they will be recorded in the table!";
            en["StartNewWork_Amount"] = "Amount";
            en["StartNewWork_Type"] = "Type";
            en["StartNewWork_Category"] = "Category";
            en["StartNewWork_Currency"] = "Currency";
            en["StartNewWork_Date"] = "Date";
            en["StartNewWork_Add"] = "Add";
            en["StartNewWork_Change"] = "Change";
            en["StartNewWork_Delete"] = "Delete";
            en["StartNewWork_Done"] = "Done";
            en["StartNewWork_Currency_RUB"] = "RUB";
            en["StartNewWork_Currency_USD"] = "USD";

            // Типы операций
            en["StartNewWork_Type_Transfer"] = "Transfer";
            en["StartNewWork_Type_Withdrawal"] = "Withdrawal";
            en["StartNewWork_Type_WriteOff"] = "Write-off";
            en["StartNewWork_Type_Replenishment"] = "Replenishment";

            // Категории операций
            en["StartNewWork_Category_Food"] = "Food";
            en["StartNewWork_Category_Cafe"] = "Cafe";
            en["StartNewWork_Category_Transport"] = "Transport";
            en["StartNewWork_Category_Delivery"] = "Delivery";
            en["StartNewWork_Category_Clothes"] = "Clothes";
            en["StartNewWork_Category_Electronics"] = "Electronics";

            // Сообщения StartNewWork
            en["StartNewWork_Message_AddSuccess"] = "New operation successfully added!";
            en["StartNewWork_Message_ChangeSuccess"] = "Operation successfully changed!";
            en["StartNewWork_Message_DeleteSuccess"] = "Operation deleted!";
            en["StartNewWork_Message_NoData"] = "No operations for analysis! Please add at least one operation.";
            en["StartNewWork_Message_NoDataForChange"] = "No operations to change! Add an operation first.";
            en["StartNewWork_Message_SelectForChange"] = "Please select an operation to change!";
            en["StartNewWork_Message_NoDataForDelete"] = "No operations to delete! Add an operation first.";
            en["StartNewWork_Message_SelectForDelete"] = "Please select an operation to delete!";
            en["StartNewWork_Message_DeleteConfirm"] = "Are you sure you want to delete this operation?";
            en["StartNewWork_Message_DataError"] = "Invalid data format! For date use DD.MM.YYYY format";
            en["StartNewWork_Message_AmountZero"] = "Operation amount cannot be zero!";
            en["StartNewWork_Message_SelectType"] = "Please select operation type!";
            en["StartNewWork_Message_TypeMaxLength"] = "Operation type cannot be longer than 50 characters!";
            en["StartNewWork_Message_SelectCategory"] = "Please select category!";
            en["StartNewWork_Message_CategoryMaxLength"] = "Category cannot be longer than 50 characters!";
            en["StartNewWork_Message_SelectCurrency"] = "Please select currency!";
            en["StartNewWork_Message_MaxOperations"] = "Sorry, but you cannot add more than {0} operations!";
            en["StartNewWork_Message_LoadSuccess"] = "Loaded {0} operations from project!";

            // GetAnalys
            en["GetAnalys_Message"] = "Data analysis completed! Meow! If you want to change the data about operations, click the \"Back to data\" button, and when you're done, don't forget to \"Save report\", meow!";
            en["GetAnalys_ReportHeader"] = "OPERATION ANALYSIS REPORT";
            en["GetAnalys_ChartTypes"] = "CHART TYPES";
            en["GetAnalys_Actions"] = "ACTIONS";
            en["GetAnalys_SaveReport"] = "Save report";
            en["GetAnalys_BackToData"] = "Back to data";
            en["GetAnalys_Table"] = "Table";
            en["GetAnalys_Graph"] = "Graph";
            en["GetAnalys_CircleDiagram"] = "Pie chart";
            en["GetAnalys_ScatterPlot"] = "Scatter";
            en["GetAnalys_Gistogram"] = "Histogram";
            en["GetAnalys_RadarDiagram"] = "Radar";

            // Сообщения GetAnalys
            en["GetAnalys_Message_NoData"] = "No data to save! Please add operations first.";
            en["GetAnalys_Message_ProjectLimit"] = "Project limit reached (maximum 10)!\n\nPlease delete unnecessary projects manually in the folder:\n{0}\n\nThen restart the application to continue.";
            en["GetAnalys_Message_ProjectExists"] = "Project named \"{0}\" already exists!\nDo you want to overwrite it?";
            en["GetAnalys_Message_SaveSuccess"] = "Project \"{0}\" successfully saved!";
            en["GetAnalys_Message_SaveError"] = "Error saving project!";
            en["GetAnalys_Message_EmptyName"] = "Project name cannot be empty!";
            en["GetAnalys_Message_SaveDialog"] = "Enter the name for the project to be saved:";
            en["GetAnalys_Message_NoProjects"] = "You have no saved projects yet.";
            en["GetAnalys_Message_ExistingProjects"] = "Existing projects ({0}/10):";
            en["GetAnalys_Message_NotSaved"] = "You haven't saved the project!\nDo you want to save before exiting?";
            en["GetAnalys_Message_NoDataForAnalysis"] = "Error! Not enough data for analysis!";
            en["GetAnalys_Message_NoExpenseData"] = "No expense data";

            // Сообщения для диаграмм
            en["GetAnalys_Chart_GraphTitle"] = "Income and expenses dynamics (in RUB)";
            en["GetAnalys_Chart_XLabel"] = "Operation number";
            en["GetAnalys_Chart_YLabel"] = "Amount (RUB)";
            en["GetAnalys_Chart_PieTitle"] = "Expense structure by category (in RUB)";
            en["GetAnalys_Chart_ScatterTitle"] = "Scatter chart of operations by date (in RUB)";
            en["GetAnalys_Chart_HistogramTitle"] = "Histogram of operation amounts distribution (in RUB)";
            en["GetAnalys_Chart_RadarTitle"] = "Radar chart by category (in RUB)";
            en["GetAnalys_Chart_NoData"] = "No data";
            en["GetAnalys_Chart_AllValuesSame"] = "All values are the same ({0:F2} RUB)";
            en["GetAnalys_Chart_HistogramXLabel"] = "Amount range (RUB)";
            en["GetAnalys_Chart_HistogramYLabel"] = "Number of operations";
            en["GetAnalys_Chart_ScatterXLabel"] = "Operation date";
            en["GetAnalys_Chart_ScatterYLabel"] = "Amount (RUB)";

            // Сообщения ошибок
            en["Error_NotEnoughData"] = "At least {0} operations are required to build this chart!";
            en["Error_NotEnoughCategories"] = "At least {0} different expense categories are required for the pie chart!\nFound: {1} categories.";
            en["Error_NoData"] = "No data for analysis!";
            en["Error_SaveSettings"] = "Error saving settings!";
            en["Error_LoadSettings"] = "Error loading settings!";
            en["Error_LoadProject"] = "Error loading project!";
            en["Error_ProjectEmpty"] = "Project file is empty or corrupted!";

            // Диалоги подтверждения
            en["Dialog_ExitConfirm"] = "Are you sure you want to exit?";
            en["Dialog_ResetSettingsConfirm"] = "Are you sure you want to reset all settings to default?";
            en["Dialog_SettingsNotSaved"] = "You have changed settings but haven't saved them!\nDo you want to save before exiting?";
            en["Dialog_DeleteConfirm"] = "Are you sure you want to delete this operation?";

            // Заголовки диалогов
            en["Dialog_Title_Exit"] = "Confirm exit";
            en["Dialog_Title_Reset"] = "Confirm reset";
            en["Dialog_Title_SettingsNotSaved"] = "Settings not saved";
            en["Dialog_Title_Delete"] = "Confirm deletion";
            en["Dialog_Title_Error"] = "Error";
            en["Dialog_Title_Warning"] = "Warning";
            en["Dialog_Title_Success"] = "Success";
            en["Dialog_Title_Info"] = "Information";
            en["Dialog_Title_SaveProject"] = "Save project";
            en["Dialog_Title_LoadProject"] = "Continue work";

            // Текст отчета
            en["Report_GeneralInfo"] = "1. General information";
            en["Report_TopCategories"] = "2. Most frequent categories";
            en["Report_IncomeCategoryShares"] = "3. Income category shares";
            en["Report_ExpenseCategoryShares"] = "4. Expense category shares";
            en["Report_TopTypes"] = "5. Most frequent operation types";
            en["Report_IncomeTypeShares"] = "6. Income type shares";
            en["Report_ExpenseTypeShares"] = "7. Expense type shares";
            en["Report_ExpenseStructure"] = "8. Expense structure by category";
            en["Report_CurrencyAnalysis"] = "9. Currency analysis";
            en["Report_DailyStatistics"] = "10. Daily statistics";
            en["Report_MonthlyDynamics"] = "11. Monthly dynamics";
            en["Report_OperationCount"] = "Number of operations";
            en["Report_TotalIncome"] = "Total income";
            en["Report_TotalExpense"] = "Total expense";
            en["Report_Balance"] = "Total balance";
            en["Report_Period"] = "Analysis period";
            en["Report_ActiveDays"] = "Active days";
            en["Report_DollarRate"] = "Dollar rate";
            en["Report_Currency"] = "RUB";
            en["Report_AllAmountsInRubles"] = "All amounts are converted to RUB";
            en["Report_YouCanChangeDollarRate"] = "You can change the dollar rate in settings";
            en["Report_DailyAvg"] = "Average daily amount";
            en["Report_DailyMax"] = "Maximum daily amount";
            en["Report_DailyMin"] = "Minimum daily amount";
            en["Report_BestDay"] = "Best day";
            en["Report_Amount"] = "amount";
            en["Report_Generated"] = "Report generated";
            en["Report_ByProgram"] = "using Smart Budget";
            en["Report_Income"] = "Income";
            en["Report_Expense"] = "Expense";
            en["Report_SameValues"] = "All values are the same";
            en["Report_Currency_Column"] = "Currency";
            en["Report_Operations_Column"] = "Number of operations";
            en["Report_Incomes_Column"] = "Incomes";
            en["Report_Expenses_Column"] = "Expenses";

            _translations["Русский"] = ru;
            _translations["English"] = en;
        }

        public static string GetString(string key)
        {
            if (_translations != null && _translations.ContainsKey(_currentLanguage) &&
                _translations[_currentLanguage].ContainsKey(key))
            {
                return _translations[_currentLanguage][key];
            }
            return key;
        }

        public static string GetString(string key, params object[] args)
        {
            string template = GetString(key);
            return string.Format(template, args);
        }

        public static void SetLanguage(string language)
        {
            if (_translations.ContainsKey(language))
            {
                CurrentLanguage = language;
            }
        }

        public static string GetCurrentLanguage()
        {
            return _currentLanguage;
        }
    }
}
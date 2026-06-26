using System.Globalization;

namespace SmartBudget.ClassLibrary
{
    /// <summary>
    /// Статический класс, занимающийся организацией локализации
    /// </summary>
    public static class LocalizationManager
    {
        private static string _currentLanguage = "Русский"; // Поле для текущего языка - по умолчанию русский
        private static Dictionary<string, Dictionary<string, string>> _translations; // Поле для хранения переводов

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        static LocalizationManager()
        {
            LoadTranslations();
        }

        /// <summary>
        /// Свойство для получения текущего языка
        /// </summary>
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
        
        /// <summary>
        /// Выгрузка переводов из словаря
        /// </summary>
        private static void LoadTranslations()
        {
            _translations = []; //Словарь с переводами - ключом является язык, в то время как значение - другой словарь с ключом в виде названия элемента для перевода и значением - переводом

            //РУССКИЙ ЯЗЫК
            Dictionary<string, string> ru = new()
            {
                // Главное меню
                ["MainMenu_Title"] = "Smart Budget",
                ["MainMenu_ContinueWork"] = "Продолжить работу",
                ["MainMenu_StartNewWork"] = "Начать новую работу",
                ["MainMenu_About"] = "О приложении",
                ["MainMenu_Settings"] = "Настройки",
                ["MainMenu_Exit"] = "Выход",

                // О приложении
                ["About_Title"] = "О приложении",
                ["About_Welcome"] = "Мур-р-р! Добро пожаловать в Smart Budget - приложение, которое поможет вам с работой с личными финансами! Для ознакомления с работой просмотрите видео или прочтите справочник пользователя",
                ["About_OpenHelp"] = "Открыть справочник пользователя",

                // Настройки
                ["Settings_Welcome"] = "Добро пожаловать в меню настроек, мяу! Здесь вы можете настроить приложение специально под себя!",
                ["Settings_ChangeDollar"] = "Смена курса доллара",
                ["Settings_DollarDescription"] = "Введите текущий курс доллара:",
                ["Settings_ChangeLanguage"] = "Смена языка",
                ["Settings_LanguageDescription"] = "Выберете язык приложения:",
                ["Settings_DogMode"] = "\"Режим собачника\"",
                ["Settings_DogModeDescription"] = "Меняет помощника на собаку:",
                ["Settings_Apply"] = "Применить настройки",
                ["Settings_Reset"] = "Сбросить настройки",
                ["Settings_Menu"] = "Меню",
                ["Settings_ReturnToHome"] = "Вернуться в главное меню",
                ["Settings_Language_Russian"] = "Русский",
                ["Settings_Language_English"] = "English",
                ["Settings_On"] = "Вкл/выкл",
                ["Settings_Off"] = "Выкл",
                ["Settings_Message_SaveSuccess_Cat"] = "Мур! Настройки успешно сохранены!",
                ["Settings_Message_SaveSuccess_Dog"] = "Ррраф! Настройки успешно сохранены!",
                ["Settings_Message_SaveError_Cat"] = "Мяу... Ошибка при сохранении настроек!",
                ["Settings_Message_SaveError_Dog"] = "Гав-гав... Ошибка при сохранении настроек!",
                ["Settings_Message_ResetSuccess_Cat"] = "Мур! Настройки сброшены до изначальных!",
                ["Settings_Message_ResetSuccess_Dog"] = "Ррраф! Настройки сброшены до изначальных!",
                ["Settings_ScanQR"] = "Возникли вопросы? Отсканируйте QR-код для связи с разработчиками!",

                // StartNewWork
                ["StartNewWork_Welcome"] = "Мяу! Для начала работы введите данные об операциях, и они будут записаны в таблицу!",
                ["StartNewWork_Amount"] = "Размер операции",
                ["StartNewWork_Type"] = "Тип операции",
                ["StartNewWork_Category"] = "Категория операции",
                ["StartNewWork_Currency"] = "Валюта",
                ["StartNewWork_Date"] = "Дата операции",
                ["StartNewWork_Add"] = "Добавить",
                ["StartNewWork_Change"] = "Изменить",
                ["StartNewWork_Delete"] = "Удалить",
                ["StartNewWork_Done"] = "Готово",
                ["StartNewWork_Currency_RUB"] = "Рубли",
                ["StartNewWork_Currency_USD"] = "Доллары",

                // Типы операций
                ["StartNewWork_Type_Transfer"] = "Перевод",
                ["StartNewWork_Type_Withdrawal"] = "Снятие",
                ["StartNewWork_Type_WriteOff"] = "Списание",
                ["StartNewWork_Type_Replenishment"] = "Зачисление",

                // Категории операций
                ["StartNewWork_Category_Food"] = "Продукты",
                ["StartNewWork_Category_Cafe"] = "Кафе",
                ["StartNewWork_Category_Transport"] = "Транспорт",
                ["StartNewWork_Category_Delivery"] = "Доставка",
                ["StartNewWork_Category_Clothes"] = "Одежда",
                ["StartNewWork_Category_Electronics"] = "Электротехника",

                // Сообщения StartNewWork
                ["StartNewWork_Message_AddSuccess"] = "Новая операция успешно добавлена!",
                ["StartNewWork_Message_ChangeSuccess"] = "Операция была успешно изменена!",
                ["StartNewWork_Message_DeleteSuccess"] = "Операция удалена!",
                ["StartNewWork_Message_NoData"] = "Нет операций для анализа! Пожалуйста, добавьте хотя бы одну операцию.",
                ["StartNewWork_Message_NoDataForChange"] = "Нет операций для изменения! Сначала добавьте операцию.",
                ["StartNewWork_Message_SelectForChange"] = "Сначала выберите операцию для изменения!",
                ["StartNewWork_Message_NoDataForDelete"] = "Нет операций для удаления! Сначала добавьте операцию.",
                ["StartNewWork_Message_SelectForDelete"] = "Сначала выберите операцию для удаления!",
                ["StartNewWork_Message_DeleteConfirm"] = "Вы уверены, что хотите удалить эту операцию?",
                ["StartNewWork_Message_DataError"] = "Неверный формат данных! Для даты используйте формат ДД.ММ.ГГГГ",
                ["StartNewWork_Message_AmountZero"] = "Размер операции не может равняться нулю!",
                ["StartNewWork_Message_SelectType"] = "Пожалуйста, выберите тип операции!",
                ["StartNewWork_Message_TypeMaxLength"] = "Тип операции не может быть длиннее 50 символов!",
                ["StartNewWork_Message_SelectCategory"] = "Пожалуйста, выберите категорию!",
                ["StartNewWork_Message_CategoryMaxLength"] = "Категория не может быть длиннее 50 символов!",
                ["StartNewWork_Message_SelectCurrency"] = "Пожалуйста, выберите валюту!",
                ["StartNewWork_Message_MaxOperations"] = "Извините, но нельзя добавить более {0} операций!",
                ["StartNewWork_Message_LoadSuccess"] = "Загружено {0} операций из проекта!",
                ["StartNewWork_Column_Number"] = "№",
                ["StartNewWork_Column_Amount"] = "Размер",
                ["StartNewWork_Column_Type"] = "Тип",
                ["StartNewWork_Column_Category"] = "Категория",
                ["StartNewWork_Column_Currency"] = "Валюта",
                ["StartNewWork_Column_Date"] = "Дата",

                // GetAnalys
                ["GetAnalys_Message"] = "Анализ данных завершен! Мяу! Если захотите изменить данные об операциях, то нажмите на кнопку \"Назад к данным\", а когда закончите работать - не забудьте \"Сохранить отчет\", мур!",
                ["GetAnalys_ReportHeader"] = "ОТЧЁТ ПО АНАЛИЗУ ОПЕРАЦИЙ",
                ["GetAnalys_ChartTypes"] = "ТИПЫ ВИЗУАЛИЗАЦИИ",
                ["GetAnalys_Actions"] = "ДЕЙСТВИЯ",
                ["GetAnalys_SaveReport"] = "Сохранить отчет",
                ["GetAnalys_BackToData"] = "Назад к данным",
                ["GetAnalys_Table"] = "Таблица",
                ["GetAnalys_Graph"] = "График",
                ["GetAnalys_CircleDiagram"] = "Круг. диаграмма",
                ["GetAnalys_ScatterPlot"] = "Точечная",
                ["GetAnalys_Gistogram"] = "Гистограмма",
                ["GetAnalys_RadarDiagram"] = "Лепестковая",

                // Сообщения GetAnalys
                ["GetAnalys_Message_NoData"] = "Нет данных для сохранения! Сначала добавьте операции.",
                ["GetAnalys_Message_ProjectLimit"] = "Достигнут лимит проектов (максимум 10)!\n\nПожалуйста, удалите ненужные проекты вручную в папке:\n{0}\n\nЗатем перезапустите приложение для продолжения работы.",
                ["GetAnalys_Message_ProjectExists"] = "Проект с именем \"{0}\" уже существует!\nХотите перезаписать его?",
                ["GetAnalys_Message_SaveSuccess"] = "Проект \"{0}\" успешно сохранен!",
                ["GetAnalys_Message_SaveError"] = "Ошибка при сохранении проекта!",
                ["GetAnalys_Message_EmptyName"] = "Имя проекта не может быть пустым!",
                ["GetAnalys_Message_SaveDialog"] = "Введите имя для проекта, который будет сохраняться:",
                ["GetAnalys_Message_NoProjects"] = "У вас пока нет сохраненных проектов.",
                ["GetAnalys_Message_ExistingProjects"] = "Существующие проекты ({0}/10):",
                ["GetAnalys_Message_NotSaved"] = "Вы не сохранили проект!\nХотите сохранить перед выходом?",
                ["GetAnalys_Message_NoDataForAnalysis"] = "Ошибка! Недостаточно данных для анализа! для формирования отчета",
                ["GetAnalys_Message_NoExpenseData"] = "Нет данных о расходах",
                ["GetAnalys_Chart_AllOperations"] = "Все операции",
                ["GetAnalys_Message_ProjectExistsHint"] = "Если вы хотите перезаписать проект, то введите название из списка выше",
                ["GetAnalys_Message_ProjectName"] = "Имя проекта",

                // Сообщения для диаграмм
                ["GetAnalys_Chart_GraphTitle"] = "Динамика доходов и расходов (в рублях)",
                ["GetAnalys_Chart_XLabel"] = "Порядковый номер операции",
                ["GetAnalys_Chart_YLabel"] = "Сумма (₽)",
                ["GetAnalys_Chart_PieTitle"] = "Структура расходов по категориям (в рублях)",
                ["GetAnalys_Chart_ScatterTitle"] = "Точечная диаграмма операций по датам (в рублях)",
                ["GetAnalys_Chart_HistogramTitle"] = "Гистограмма распределения сумм операций (в рублях)",
                ["GetAnalys_Chart_RadarTitle"] = "Лепестковая диаграмма по категориям (в рублях)",
                ["GetAnalys_Chart_NoData"] = "Нет данных",
                ["GetAnalys_Chart_AllValuesSame"] = "Все значения одинаковы ({0:F2} ₽)",
                ["GetAnalys_Chart_HistogramXLabel"] = "Диапазон сумм (₽)",
                ["GetAnalys_Chart_HistogramYLabel"] = "Количество операций",
                ["GetAnalys_Chart_ScatterXLabel"] = "Дата операции",
                ["GetAnalys_Chart_ScatterYLabel"] = "Сумма (₽)",

                // Сообщения ошибок
                ["Error_NotEnoughData"] = "Для построения данной диаграммы необходимо минимум {0} операции!",
                ["Error_NotEnoughCategories"] = "Для построения круговой диаграммы необходимо минимум {0} разные категории расходов!\nНайдено: {1} категорий.",
                ["Error_NoData"] = "Нет данных для анализа!",
                ["Error_SaveSettings"] = "Ошибка при сохранении настроек!",
                ["Error_LoadSettings"] = "Ошибка при загрузке настроек!",
                ["Error_LoadProject"] = "Ошибка при загрузке проекта!",
                ["Error_ProjectEmpty"] = "Файл проекта пустой или поврежден!",

                // Диалоги подтверждения
                ["Dialog_ExitConfirm"] = "Вы уверены, что хотите выйти?",
                ["Dialog_ResetSettingsConfirm"] = "Вы уверены, что хотите сбросить все настройки до базовых?",
                ["Dialog_SettingsNotSaved"] = "Вы изменили настройки, но не сохранили их!\nХотите сохранить перед выходом?",
                ["Dialog_DeleteConfirm"] = "Вы уверены, что хотите удалить эту операцию?",

                // Заголовки диалогов
                ["Dialog_Title_Exit"] = "Подтверждение выхода",
                ["Dialog_Title_Reset"] = "Подтверждение сброса",
                ["Dialog_Title_SettingsNotSaved"] = "Настройки не сохранены",
                ["Dialog_Title_Delete"] = "Подтверждение удаления",
                ["Dialog_Title_Error"] = "Ошибка",
                ["Dialog_Title_Warning"] = "Внимание",
                ["Dialog_Title_Success"] = "Успех",
                ["Dialog_Title_Info"] = "Информация",
                ["Dialog_Title_SaveProject"] = "Сохранение проекта",
                ["Dialog_Title_LoadProject"] = "Продолжить работу",

                // Текст отчета
                ["Report_GeneralInfo"] = "1. Общая информация",
                ["Report_TopCategories"] = "2. Самые частые категории",
                ["Report_IncomeCategoryShares"] = "3. Доли категории в доходах",
                ["Report_ExpenseCategoryShares"] = "4. Доли категорий в расходах",
                ["Report_TopTypes"] = "5. Самые частые типы операций",
                ["Report_IncomeTypeShares"] = "6. Доли типов в доходах",
                ["Report_ExpenseTypeShares"] = "7. Доли типов в расходах",
                ["Report_ExpenseStructure"] = "8. Структура расходов по категориям",
                ["Report_CurrencyAnalysis"] = "9. Анализ по валютам",
                ["Report_DailyStatistics"] = "10. Статистика по дням",
                ["Report_MonthlyDynamics"] = "11. Динамика по месяцам",
                ["Report_OperationCount"] = "Количество операций",
                ["Report_TotalIncome"] = "Общий доход",
                ["Report_TotalExpense"] = "Общий расход",
                ["Report_Balance"] = "Итоговый баланс",
                ["Report_Period"] = "Период анализа",
                ["Report_ActiveDays"] = "Активных дней",
                ["Report_DollarRate"] = "Курс доллара",
                ["Report_Currency"] = "руб.",
                ["Report_AllAmountsInRubles"] = "Все суммы приведены к рублям",
                ["Report_YouCanChangeDollarRate"] = "Вы можете изменить курс доллара в настройках",
                ["Report_DailyAvg"] = "Средняя сумма в день",
                ["Report_DailyMax"] = "Максимальная сумма в день",
                ["Report_DailyMin"] = "Минимальная сумма в день",
                ["Report_BestDay"] = "Лучший день",
                ["Report_Amount"] = "сумма",
                ["Report_Generated"] = "Отчет сформирован",
                ["Report_ByProgram"] = "при помощи программы Smart Budget",
                ["Report_Income"] = "Доходы",
                ["Report_Expense"] = "Расходы",
                ["Report_SameValues"] = "Все значения одинаковы",
                ["Report_Currency_Column"] = "Валюта",
                ["Report_Operations_Column"] = "Количество операций",
                ["Report_Incomes_Column"] = "Доходы",
                ["Report_Expenses_Column"] = "Расходы",
                ["Report_NoIncomeData"] = "Нет данных о доходах",
                ["Report_NoExpenseData"] = "Нет данных о расходах",
            };

            //АНГЛИЙСКИЙ ЯЗЫК
            Dictionary<string, string> en = new()
            {
                ["MainMenu_Title"] = "Smart Budget",
                ["MainMenu_ContinueWork"] = "Continue work",
                ["MainMenu_StartNewWork"] = "Start new work",
                ["MainMenu_About"] = "About",
                ["MainMenu_Settings"] = "Settings",
                ["MainMenu_Exit"] = "Exit",

                // О приложении
                ["About_Title"] = "About",
                ["About_Welcome"] = "Meow! Welcome to Smart Budget - an application that will help you manage your personal finances! To learn how it works, watch the video or read the user manual",
                ["About_OpenHelp"] = "Open user manual",

                // Настройки
                ["Settings_Welcome"] = "Welcome to the settings menu, meow! Here you can customize the application to your needs!",
                ["Settings_ChangeDollar"] = "Change dollar rate",
                ["Settings_DollarDescription"] = "Enter the current dollar rate:",
                ["Settings_ChangeLanguage"] = "Change language",
                ["Settings_LanguageDescription"] = "Select application language:",
                ["Settings_DarkTheme"] = "Dark theme",
                ["Settings_DarkDescription"] = "Dark background, light text:",
                ["Settings_LeftHanded"] = "\"Left-handed mode\"",
                ["Settings_LeftHandedDescription"] = "Mirrors the interface:",
                ["Settings_DogMode"] = "\"Dog mode\"",
                ["Settings_DogModeDescription"] = "Changes assistant to a dog:",
                ["Settings_Apply"] = "Apply settings",
                ["Settings_Reset"] = "Reset settings",
                ["Settings_Menu"] = "Menu",
                ["Settings_ReturnToHome"] = "Return to main menu",
                ["Settings_Language_Russian"] = "Russian",
                ["Settings_Language_English"] = "English",
                ["Settings_On"] = "On/off",
                ["Settings_Off"] = "Off",
                ["Settings_Message_SaveSuccess_Cat"] = "Meow! Settings saved successfully!",
                ["Settings_Message_SaveSuccess_Dog"] = "Woof! Settings saved successfully!",
                ["Settings_Message_SaveError_Cat"] = "Meow... Error saving settings!",
                ["Settings_Message_SaveError_Dog"] = "Woof... Error saving settings!",
                ["Settings_Message_ResetSuccess_Cat"] = "Meow! Settings reset to default!",
                ["Settings_Message_ResetSuccess_Dog"] = "Woof! Settings reset to default!",
                ["Settings_ScanQR"] = "Have questions? Scan the QR code to contact the developers!",

                // StartNewWork
                ["StartNewWork_Welcome"] = "Meow! To get started, enter the data about operations and they will be recorded in the table!",
                ["StartNewWork_Amount"] = "Amount",
                ["StartNewWork_Type"] = "Type",
                ["StartNewWork_Category"] = "Category",
                ["StartNewWork_Currency"] = "Currency",
                ["StartNewWork_Date"] = "Date",
                ["StartNewWork_Add"] = "Add",
                ["StartNewWork_Change"] = "Change",
                ["StartNewWork_Delete"] = "Delete",
                ["StartNewWork_Done"] = "Done",
                ["StartNewWork_Currency_RUB"] = "RUB",
                ["StartNewWork_Currency_USD"] = "USD",

                // Типы операций
                ["StartNewWork_Type_Transfer"] = "Transfer",
                ["StartNewWork_Type_Withdrawal"] = "Withdrawal",
                ["StartNewWork_Type_WriteOff"] = "Write-off",
                ["StartNewWork_Type_Replenishment"] = "Replenishment",

                // Категории операций
                ["StartNewWork_Category_Food"] = "Food",
                ["StartNewWork_Category_Cafe"] = "Cafe",
                ["StartNewWork_Category_Transport"] = "Transport",
                ["StartNewWork_Category_Delivery"] = "Delivery",
                ["StartNewWork_Category_Clothes"] = "Clothes",
                ["StartNewWork_Category_Electronics"] = "Electronics",

                // Сообщения StartNewWork
                ["StartNewWork_Message_AddSuccess"] = "New operation successfully added!",
                ["StartNewWork_Message_ChangeSuccess"] = "Operation successfully changed!",
                ["StartNewWork_Message_DeleteSuccess"] = "Operation deleted!",
                ["StartNewWork_Message_NoData"] = "No operations for analysis! Please add at least one operation.",
                ["StartNewWork_Message_NoDataForChange"] = "No operations to change! Add an operation first.",
                ["StartNewWork_Message_SelectForChange"] = "Please select an operation to change!",
                ["StartNewWork_Message_NoDataForDelete"] = "No operations to delete! Add an operation first.",
                ["StartNewWork_Message_SelectForDelete"] = "Please select an operation to delete!",
                ["StartNewWork_Message_DeleteConfirm"] = "Are you sure you want to delete this operation?",
                ["StartNewWork_Message_DataError"] = "Invalid data format! For date use DD.MM.YYYY format",
                ["StartNewWork_Message_AmountZero"] = "Operation amount cannot be zero!",
                ["StartNewWork_Message_SelectType"] = "Please select operation type!",
                ["StartNewWork_Message_TypeMaxLength"] = "Operation type cannot be longer than 50 characters!",
                ["StartNewWork_Message_SelectCategory"] = "Please select category!",
                ["StartNewWork_Message_CategoryMaxLength"] = "Category cannot be longer than 50 characters!",
                ["StartNewWork_Message_SelectCurrency"] = "Please select currency!",
                ["StartNewWork_Message_MaxOperations"] = "Sorry, but you cannot add more than {0} operations!",
                ["StartNewWork_Message_LoadSuccess"] = "Loaded {0} operations from project!",
                ["StartNewWork_Column_Number"] = "№",
                ["StartNewWork_Column_Amount"] = "Amount",
                ["StartNewWork_Column_Type"] = "Type",
                ["StartNewWork_Column_Category"] = "Category",
                ["StartNewWork_Column_Currency"] = "Currency",
                ["StartNewWork_Column_Date"] = "Date",


                // GetAnalys
                ["GetAnalys_Message"] = "Data analysis completed! Meow! If you want to change the data about operations, click the \"Back to data\" button, and when you're done, don't forget to \"Save report\", meow!",
                ["GetAnalys_ReportHeader"] = "OPERATION ANALYSIS REPORT",
                ["GetAnalys_ChartTypes"] = "CHART TYPES",
                ["GetAnalys_Actions"] = "ACTIONS",
                ["GetAnalys_SaveReport"] = "Save report",
                ["GetAnalys_BackToData"] = "Back to data",
                ["GetAnalys_Table"] = "Table",
                ["GetAnalys_Graph"] = "Graph",
                ["GetAnalys_CircleDiagram"] = "Pie chart",
                ["GetAnalys_ScatterPlot"] = "Scatter",
                ["GetAnalys_Gistogram"] = "Histogram",
                ["GetAnalys_RadarDiagram"] = "Radar",

                // Сообщения GetAnalys
                ["GetAnalys_Message_NoData"] = "No data to save! Please add operations first.",
                ["GetAnalys_Message_ProjectLimit"] = "Project limit reached (maximum 10)!\n\nPlease delete unnecessary projects manually in the folder:\n{0}\n\nThen restart the application to continue.",
                ["GetAnalys_Message_ProjectExists"] = "Project named \"{0}\" already exists!\nDo you want to overwrite it?",
                ["GetAnalys_Message_SaveSuccess"] = "Project \"{0}\" successfully saved!",
                ["GetAnalys_Message_SaveError"] = "Error saving project!",
                ["GetAnalys_Message_EmptyName"] = "Project name cannot be empty or it seems like you have clicked on the Cancel!",
                ["GetAnalys_Message_SaveDialog"] = "Enter the name for the project to be saved:",
                ["GetAnalys_Message_NoProjects"] = "You have no saved projects yet.",
                ["GetAnalys_Message_ExistingProjects"] = "Existing projects ({0}/10):",
                ["GetAnalys_Message_NotSaved"] = "You haven't saved the project!\nDo you want to save before exiting?",
                ["GetAnalys_Message_NoDataForAnalysis"] = "Error! Not enough data for analysis!",
                ["GetAnalys_Message_NoExpenseData"] = "No expense data",
                ["GetAnalys_Message_ProjectExistsHint"] = "If you want to overwrite the project, enter a name from the list above",
                ["GetAnalys_Message_ProjectName"] = "Project name",

                // Сообщения для диаграмм
                ["GetAnalys_Chart_GraphTitle"] = "Income and expenses dynamics (in RUB)",
                ["GetAnalys_Chart_XLabel"] = "Operation number",
                ["GetAnalys_Chart_YLabel"] = "Amount (RUB)",
                ["GetAnalys_Chart_PieTitle"] = "Expense structure by category (in RUB)",
                ["GetAnalys_Chart_ScatterTitle"] = "Scatter chart of operations by date (in RUB)",
                ["GetAnalys_Chart_HistogramTitle"] = "Histogram of operation amounts distribution (in RUB)",
                ["GetAnalys_Chart_RadarTitle"] = "Radar chart by category (in RUB)",
                ["GetAnalys_Chart_NoData"] = "No data",
                ["GetAnalys_Chart_AllValuesSame"] = "All values are the same ({0:F2} RUB)",
                ["GetAnalys_Chart_HistogramXLabel"] = "Amount range (RUB)",
                ["GetAnalys_Chart_HistogramYLabel"] = "Number of operations",
                ["GetAnalys_Chart_ScatterXLabel"] = "Operation date",
                ["GetAnalys_Chart_ScatterYLabel"] = "Amount (RUB)",
                ["GetAnalys_Chart_AllOperations"] = "All operations",

                // Сообщения ошибок
                ["Error_NotEnoughData"] = "At least {0} operations are required to build this chart!",
                ["Error_NotEnoughCategories"] = "At least {0} different expense categories are required for the pie chart!\nFound: {1} categories.",
                ["Error_NoData"] = "No data for analysis!",
                ["Error_SaveSettings"] = "Error saving settings!",
                ["Error_LoadSettings"] = "Error loading settings!",
                ["Error_LoadProject"] = "Error loading project!",
                ["Error_ProjectEmpty"] = "Project file is empty or corrupted!",

                // Диалоги подтверждения
                ["Dialog_ExitConfirm"] = "Are you sure you want to exit?",
                ["Dialog_ResetSettingsConfirm"] = "Are you sure you want to reset all settings to default?",
                ["Dialog_SettingsNotSaved"] = "You have changed settings but haven't saved them!\nDo you want to save before exiting?",
                ["Dialog_DeleteConfirm"] = "Are you sure you want to delete this operation?",

                // Заголовки диалогов
                ["Dialog_Title_Exit"] = "Confirm exit",
                ["Dialog_Title_Reset"] = "Confirm reset",
                ["Dialog_Title_SettingsNotSaved"] = "Settings not saved",
                ["Dialog_Title_Delete"] = "Confirm deletion",
                ["Dialog_Title_Error"] = "Error",
                ["Dialog_Title_Warning"] = "Warning",
                ["Dialog_Title_Success"] = "Success",
                ["Dialog_Title_Info"] = "Information",
                ["Dialog_Title_SaveProject"] = "Save project",
                ["Dialog_Title_LoadProject"] = "Continue work",

                // Текст отчета
                ["Report_GeneralInfo"] = "1. General information",
                ["Report_TopCategories"] = "2. Most frequent categories",
                ["Report_IncomeCategoryShares"] = "3. Income category shares",
                ["Report_ExpenseCategoryShares"] = "4. Expense category shares",
                ["Report_TopTypes"] = "5. Most frequent operation types",
                ["Report_IncomeTypeShares"] = "6. Income type shares",
                ["Report_ExpenseTypeShares"] = "7. Expense type shares",
                ["Report_ExpenseStructure"] = "8. Expense structure by category",
                ["Report_CurrencyAnalysis"] = "9. Currency analysis",
                ["Report_DailyStatistics"] = "10. Daily statistics",
                ["Report_MonthlyDynamics"] = "11. Monthly dynamics",
                ["Report_OperationCount"] = "Number of operations",
                ["Report_TotalIncome"] = "Total income",
                ["Report_TotalExpense"] = "Total expense",
                ["Report_Balance"] = "Total balance",
                ["Report_Period"] = "Analysis period",
                ["Report_ActiveDays"] = "Active days",
                ["Report_DollarRate"] = "Dollar rate",
                ["Report_Currency"] = "RUB",
                ["Report_AllAmountsInRubles"] = "All amounts are converted to RUB",
                ["Report_YouCanChangeDollarRate"] = "You can change the dollar rate in settings",
                ["Report_DailyAvg"] = "Average daily amount",
                ["Report_DailyMax"] = "Maximum daily amount",
                ["Report_DailyMin"] = "Minimum daily amount",
                ["Report_BestDay"] = "Best day",
                ["Report_Amount"] = "amount",
                ["Report_Generated"] = "Report generated",
                ["Report_ByProgram"] = "using Smart Budget",
                ["Report_Income"] = "Income",
                ["Report_Expense"] = "Expense",
                ["Report_SameValues"] = "All values are the same",
                ["Report_Currency_Column"] = "Currency",
                ["Report_Operations_Column"] = "Number of operations",
                ["Report_Incomes_Column"] = "Incomes",
                ["Report_Expenses_Column"] = "Expenses",
                ["Report_NoIncomeData"] = "No income data",
                ["Report_NoExpenseData"] = "No expense data",
            };

            _translations["Русский"] = ru;
            _translations["English"] = en;
        }

        /// <summary>
        /// Метод по получению конкретной строки из словаря языков
        /// </summary>
        /// <param name="key">Ключ для словаря, в котором есть искомая строка</param>
        /// <returns></returns>
        public static string GetString(string key)
        {
            if (_translations != null && _translations.TryGetValue(_currentLanguage, out Dictionary<string, string>? value1) && value1.TryGetValue(key, out string? value))
                return value;
            return key;
        }

        public static string GetString(string key, params object[] args)
        {
            string template = GetString(key);
            return string.Format(template, args);
        }

        /// <summary>
        /// Усиановка языка
        /// </summary>
        /// <param name="language">Устанавливаемый язык</param>
        public static void SetLanguage(string language)
        {
            if (_translations.ContainsKey(language))
            {
                CurrentLanguage = language;
            }
        }

        /// <summary>
        /// Получение текущего языка
        /// </summary>
        /// <returns>Строчка с текущим языком</returns>
        public static string GetCurrentLanguage()
        {
            return _currentLanguage;
        }
    }
}
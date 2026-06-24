namespace SmartBudget.ClassLibrary
{
    public class ObjectOfAnalysis
    {
        // Поля класса
        private float _sum;
        private string _typeOfOperation;
        private string _category;
        private string _currency;
        private DateTime _date;

        // Свойства класса
        public float Sum
        {
            get { return _sum; }
            set { _sum = value; }
        }

        public string TypeOfOperation
        {
            get { return _typeOfOperation; }
            set { _typeOfOperation = value; }
        }

        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public string Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        // Конструктор без параметров
        public ObjectOfAnalysis()
        {
            Sum = 0;
            TypeOfOperation = "";
            Category = "";
            Currency = "";
            Date = DateTime.Now;
        }

        // Конструктор с параметрами (все поля)
        public ObjectOfAnalysis(float sum, string typeOfOperation, string category, string currency, DateTime date)
        {
            Sum = sum;
            TypeOfOperation = typeOfOperation;
            Category = category;
            Currency = currency;
            Date = date;
        }

        // Метод для заполнения из UI
        public void Init(float sum, string typeOfOperation, string category, string currency, DateTime date)
        {
            Sum = sum;
            TypeOfOperation = typeOfOperation;
            Category = category;
            Currency = currency;
            Date = date;
        }

        // Изменение объекта
        public void ChangeObject(ObjectOfAnalysis other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other), "Передан пустой объект");

            this.Sum = other.Sum;
            this.TypeOfOperation = other.TypeOfOperation;
            this.Category = other.Category;
            this.Currency = other.Currency;
            this.Date = other.Date;
        }

        // Перегрузка ToString
        public override string ToString()
        {
            return $"{Sum} {TypeOfOperation} {Category} ({Currency}) - {Date.ToShortDateString()}";
        }
    }
}
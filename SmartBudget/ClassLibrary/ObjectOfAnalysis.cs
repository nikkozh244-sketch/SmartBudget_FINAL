using System;

namespace Smart_Budget.ClassLibrary 
{
    public class ObjectOfAnalysis  // Меняем internal на public, чтобы был виден из UI
    {
        // Поля класса
        private decimal _sum;  // Меняем int на decimal (для денег важны копейки)
        private string _typeOfOperation;
        private string _category;
        private string _currency;  // ДОБАВЛЯЕМ: валюту (у вас она есть в UI)
        private DateTime _date;

        // Свойства класса
        public decimal Sum
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
        public ObjectOfAnalysis(decimal sum, string typeOfOperation, string category, string currency, DateTime date)
        {
            Sum = sum;
            TypeOfOperation = typeOfOperation;
            Category = category;
            Currency = currency;
            Date = date;
        }

        // Метод для заполнения из UI
        public void Init(decimal sum, string typeOfOperation, string category, string currency, DateTime date)
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
        public override string ToString()  // override, а не virtual (так правильно)
        {
            return $"{Sum} {TypeOfOperation} {Category} ({Currency}) - {Date.ToShortDateString()}";
        }
    }
}
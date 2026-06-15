using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Smart_Budget.ClassLibrary
{
    public class BudgetService<Transactions> 
    {
        private List<Transactions> _transactions;

        // Для быстрой группировки по категориям — кэш (можно перестраивать при изменении)
        private Dictionary<string, List<Transactions>> _byCategoryCache;

        public BudgetService()
        {
            _transactions = new List<Transactions>();
        }

        public void AddTransaction(Transactions t)
        {
            _transactions.Add(t);
            _byCategoryCache = null; // Сбросить кэш
        }

        //public List<Transactions> GetByCategory(string category)
        //{
        //    // Если кэш пуст — перестроить
        //    if (_byCategoryCache == null)
        //    {
        //        _byCategoryCache = _transactions
        //            .GroupBy(t => t.Category)
        //            .ToDictionary(g => g.Key, g => g.ToList());
        //    }
        //    return _byCategoryCache.GetValueOrDefault(category, new List<Transactions>());
        //}
    }
}

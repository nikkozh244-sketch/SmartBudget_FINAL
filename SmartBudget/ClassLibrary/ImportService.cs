using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Budget.ClassLibrary
{
    internal class ImportService
    {
        //Метод, занимающийся работой с экселькой (открытие, закртыие, и так далее)
        public void ReadExcel()
        {
            throw new NotImplementedException();
        }

        //Метод, занимающийся проверкой корректности эксельки
        public bool CheckFileStructure()
        {
            throw new NotImplementedException();
        }

        //Метод, занимающийся преобразованием таблички в коллекцию
        public BudgetService<ObjectOfAnalysis> ConvertRowBudgetEntry()
        {
            throw new NotImplementedException();
        }
    }
}

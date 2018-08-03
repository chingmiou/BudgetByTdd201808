using System;
using System.Linq;

namespace BudgetByTdd
{
    public class Accounting
    {
        private IBudgetRepository _budgetRepository;

        public Accounting(IBudgetRepository budgetRepository)
        {
            this._budgetRepository = budgetRepository;
        }

        public decimal TotalAmount(DateTime start, DateTime end)
        {
            var period = new Period(start, end);
            return _budgetRepository.GetAll().Sum(x => x.OverlapAmount(period));
        }
    }
}
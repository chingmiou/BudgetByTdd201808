using System;
using System.Linq;

namespace BudgetByTdd
{
    internal class Accounting
    {
        private IBudgetRepository _budgetRepository;

        public Accounting(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public decimal TotalAmount(DateTime start, DateTime end)
        {
            var budgets = _budgetRepository.GetAll();
            var period = new Period(start, end);
            if (budgets.Any())
            {
                return period.OverLappingDays(budgets[0]);
            }
            return 0;
        }
    }
}
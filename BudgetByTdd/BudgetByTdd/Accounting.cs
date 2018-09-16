using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetByTdd
{
    internal class Accounting
    {
        private IBudgetRepository _budgetRepository;

        public Accounting(IBudgetRepository budgetRepository)
        {
            this._budgetRepository = budgetRepository;
        }

        public decimal TotalAmount(DateTime start, DateTime end)
        {
            var budgets = _budgetRepository.GetAll();
            var period = new Period(start, end);
            if (budgets.Any())
            {
                var budget = budgets[0];
                return budget.DailyAmount() * period.OverlappingDays(new Period(budget.FirstDay, budget.LastDay));
            }
            return 0;
        }
    }
}
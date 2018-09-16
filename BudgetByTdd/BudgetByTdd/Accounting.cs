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
                var dailyAmount = budget.DailyAmount();
                var overlappingDays = period.OverlappingDays(budget);
                return dailyAmount * overlappingDays;
            }
            return 0;
        }
    }
}
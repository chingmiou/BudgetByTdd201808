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
                var budget = budgets[0];
                var budgetAmount = budget.Amount;
                var budgetDaysInMonth = budget.DaysInMonth;
                var overLappingDays = period.OverLappingDays(budget);
                var dailyAmount = budgetAmount / budgetDaysInMonth;
                return overLappingDays * dailyAmount;
            }
            return 0;
        }
    }
}
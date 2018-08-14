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
                var total = 0m;
                foreach (var budget in budgets)
                {
                    var amount = period.OverLappingDays(new Period(budget.FirstDay, budget.LastDay)) * budget.DailyAmount();
                    total += amount;
                }
                return total;
            }
            return 0;
        }
    }
}
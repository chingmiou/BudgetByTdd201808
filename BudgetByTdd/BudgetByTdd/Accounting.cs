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
            var budgets = _budgetRepository.GetAll();
            var period = new Period(start, end);
            if (budgets.Any())
            {
                decimal total = 0m;
                foreach (var budget in budgets)
                {
                    var overlapAmount = budget.OverlapAmount(period);
                    total += overlapAmount;
                }

                return total;
            }
            return 0m;
        }
    }
}
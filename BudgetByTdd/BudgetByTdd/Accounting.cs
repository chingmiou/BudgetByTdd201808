using System;
using System.Linq;

namespace BudgetByTdd
{
    internal class Period
    {
        public Period(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public int Days()
        {
            return (End - Start).Days + 1;
        }

        public decimal OverLappingDays(Budget budget)
        {
            if (HasNoOverlap(budget))
            {
                return 0;
            }

            return Days();
        }

        private bool HasNoOverlap(Budget budget)
        {
            return End < budget.FirstDay || Start > budget.LastDay;
        }
    }

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
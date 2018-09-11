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
                return period.OverlappingDays(budgets);
            }
            return 0;
        }
    }

    internal class Period
    {
        public Period(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime End { get; private set; }
        public DateTime Start { get; private set; }

        public int Days()
        {
            var days = (End - Start).Days + 1;
            return days;
        }

        public decimal OverlappingDays(List<Budget> budgets)
        {
            if (End < budgets[0].FirstDay)
            {
                return 0;
            }

            return Days();
        }
    }
}
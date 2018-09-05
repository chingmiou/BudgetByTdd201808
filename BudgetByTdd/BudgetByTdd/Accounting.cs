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
    }

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
                return Days(period);
            }
            return 0;
        }

        private static int Days(Period period)
        {
            var days = (period.End - period.Start).Days + 1;
            return days;
        }
    }
}
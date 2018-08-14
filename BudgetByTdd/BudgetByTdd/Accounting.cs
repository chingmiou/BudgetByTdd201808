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
            if (budgets.Any())
            {
                var days = new Period(start, end).Days();
                return days;
            }
            return 0;
        }
    }
}
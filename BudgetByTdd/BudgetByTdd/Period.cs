using System;

namespace BudgetByTdd
{
    public class Period
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

        public decimal OverlappingDays(Budget budget)
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
}
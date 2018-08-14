using System;

namespace BudgetByTdd
{
    internal class Period
    {
        public Period(DateTime start, DateTime end)
        {
            if (end < start)
            {
                throw new ArgumentException();
            }
            Start = start;
            End = end;
        }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public decimal OverLappingDays(Budget budget)
        {
            if (HasNoOverlap(budget))
            {
                return 0;
            }

            var overlapStart = Start > budget.FirstDay ? Start : budget.FirstDay;
            var overlapEnd = End < budget.LastDay ? End : budget.LastDay;

            return (overlapEnd - overlapStart).Days + 1;
        }

        private bool HasNoOverlap(Budget budget)
        {
            return End < budget.FirstDay || Start > budget.LastDay;
        }
    }
}
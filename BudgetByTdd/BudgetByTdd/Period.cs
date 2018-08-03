using System;

namespace BudgetByTdd
{
    public class Period
    {
        public Period(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new ArgumentException();
            }
            Start = start;
            End = end;
        }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public decimal OverlappingDays(Period otherPeriod)
        {
            if (HasNoOverlap(otherPeriod))
            {
                return 0;
            }

            var overlapStart = Start > otherPeriod.Start ? Start : otherPeriod.Start;
            var overlapEnd = End < otherPeriod.End ? End : otherPeriod.End;

            return (overlapEnd - overlapStart).Days + 1;
        }

        private bool HasNoOverlap(Period otherPeriod)
        {
            return End < otherPeriod.Start || Start > otherPeriod.End;
        }
    }
}
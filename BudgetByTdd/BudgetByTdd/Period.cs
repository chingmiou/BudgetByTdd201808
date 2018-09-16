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

        public DateTime End { get; private set; }
        public DateTime Start { get; private set; }

        public decimal OverlappingDays(Period otherPeriod)
        {
            if (End < otherPeriod.Start || Start > otherPeriod.End)
            {
                return 0;
            }

            var overlapStart = Start < otherPeriod.Start ? otherPeriod.Start : Start;
            var overlapEnd = End > otherPeriod.End ? otherPeriod.End : End;

            return (overlapEnd - overlapStart).Days + 1;
        }
    }
}
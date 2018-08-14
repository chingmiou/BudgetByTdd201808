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

        public decimal OverLappingDays(Period otherPeriod)
        {
            if (End < otherPeriod.Start || Start > otherPeriod.End)
            {
                return 0;
            }

            var overlapStart = Start > otherPeriod.Start ? Start : otherPeriod.Start;
            var overlapEnd = End < otherPeriod.End ? End : otherPeriod.End;

            return (overlapEnd - overlapStart).Days + 1;
        }
    }
}
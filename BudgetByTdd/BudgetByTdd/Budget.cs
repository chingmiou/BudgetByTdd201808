﻿using System;

namespace BudgetByTdd
{
    public class Budget
    {
        public int Amount { get; set; }
        public DateTime FirstDay { get { return DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null); } }
        public string YearMonth { get; set; }

        public DateTime LastDay
        {
            get
            {
                return DateTime.ParseExact(YearMonth + DaysInMonth, "yyyyMMdd", null);
            }
        }

        public int DaysInMonth
        {
            get
            {
                return DateTime.DaysInMonth(FirstDay.Year, FirstDay.Month);
            }
        }

        private int DailyAmount()
        {
            return Amount / DaysInMonth;
        }

        public decimal OverlapAmount(Period period)
        {
            return period.OverlappingDays(PeriodFromBudget()) * DailyAmount();
        }

        private Period PeriodFromBudget()
        {
            return new Period(FirstDay, LastDay);
        }
    }
}
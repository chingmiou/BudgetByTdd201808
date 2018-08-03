﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetByTdd
{
    [TestClass]
    public class AccountingTests
    {
        private IBudgetRepository _budgetRepository = Substitute.For<IBudgetRepository>();
        private Accounting _accounting;

        [TestMethod]
        public void no_budgets()
        {
            GivenBudgets();
            AmountShouldBe(0m, "20180601", "20180601");
        }

        [TestMethod]
        public void period_inside_budget_month()
        {
            GivenBudgets(new Budget { YearMonth = "201806", Amount = 30 });
            AmountShouldBe(1m, "20180601", "20180601");
        }

        [TestMethod]
        public void no_overlap_period_before_budget_firstday()
        {
            GivenBudgets(new Budget { YearMonth = "201807", Amount = 31 });
            AmountShouldBe(0m, "20180601", "20180601");
        }

        [TestMethod]
        public void no_overlap_period_after_budget_lastday()
        {
            GivenBudgets(new Budget { YearMonth = "201805", Amount = 31 });
            AmountShouldBe(0m, "20180601", "20180601");
        }

        [TestMethod]
        public void period_overlap_budget_lastday()
        {
            GivenBudgets(new Budget { YearMonth = "201805", Amount = 31 });
            AmountShouldBe(1m, "20180531", "20180601");
        }

        [TestMethod]
        public void period_overlap_budget_firstday()
        {
            GivenBudgets(new Budget { YearMonth = "201805", Amount = 31 });
            AmountShouldBe(1m, "20180430", "20180501");
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void invalid_period()
        {
            var start = DateTime.ParseExact("20180501", "yyyyMMdd", null);
            var end = DateTime.ParseExact("20170501", "yyyyMMdd", null);
            _accounting.TotalAmount(start, end);
        }

        private void GivenBudgets(params Budget[] budgets)
        {
            _budgetRepository.GetAll().Returns(budgets.ToList());
        }

        private void AmountShouldBe(decimal expected, string startTime, string endTime)
        {
            var start = DateTime.ParseExact(startTime, "yyyyMMdd", null);
            var end = DateTime.ParseExact(endTime, "yyyyMMdd", null);
            Assert.AreEqual(expected, _accounting.TotalAmount(start, end));
        }

        [TestInitialize]
        public void TestInit()
        {
            _accounting = new Accounting(_budgetRepository);
        }
    }

    public class Budget
    {
        public string YearMonth { get; set; }
        public int Amount { get; set; }

        public DateTime FirstDay
        {
            get { return DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null); }
        }

        public DateTime LastDay
        {
            get
            {
                var daysInMonth = DateTime.DaysInMonth(FirstDay.Year, FirstDay.Month);
                return DateTime.ParseExact(YearMonth + daysInMonth, "yyyyMMdd", null);
            }
        }
    }
}
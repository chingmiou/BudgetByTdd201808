using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

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

        [TestMethod]
        public void dailyAmount_not_1()
        {
            GivenBudgets(new Budget { YearMonth = "201805", Amount = 62 });
            AmountShouldBe(6m, "20180501", "20180503");
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void invalid_period()
        {
            var start = DateTime.ParseExact("20180601", "yyyyMMdd", null);
            var end = DateTime.ParseExact("20170601", "yyyyMMdd", null);
            _accounting.TotalAmount(start, end);
        }

        private void GivenBudgets(params Budget[] budgets)
        {
            _budgetRepository.GetAll().Returns(budgets.ToList());
        }

        public void AmountShouldBe(decimal expected, string startTime, string endTime)
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
}
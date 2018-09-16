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
        private Accounting _accounting;
        private IBudgetRepository _budgetRepository = Substitute.For<IBudgetRepository>();

        [TestMethod]
        public void no_budgets()
        {
            GivenBudgets();
            AmountShouldBe(0m, "20180601", "20180601");
        }

        [TestMethod]
        public void no_period_overlap_before_budget_firstday()
        {
            GivenBudgets(new Budget { YearMonth = "201807", Amount = 31 });
            AmountShouldBe(0m, "20180601", "20180601");
        }

        [TestMethod]
        public void no_period_overlap_after_budget_lastday()
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
        public void dailyAmount_not_1()
        {
            GivenBudgets(new Budget { YearMonth = "201805", Amount = 62 });
            AmountShouldBe(6m, "20180501", "20180503");
        }

        [TestMethod]
        public void multiple_budgets()
        {
            GivenBudgets(
                new Budget { YearMonth = "201805", Amount = 31 },
                new Budget { YearMonth = "201806", Amount = 300 }
            );
            AmountShouldBe(27m, "20180525", "20180602");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void invalid_period()
        {
            var startTIme = DateTime.ParseExact("20180501", "yyyyMMdd", null);
            var endTIme = DateTime.ParseExact("20170503", "yyyyMMdd", null);
            _accounting.TotalAmount(startTIme, endTIme);
        }

        [TestMethod]
        public void period_overlap_budget_firstday()
        {
            GivenBudgets(new Budget { YearMonth = "201805", Amount = 31 });
            AmountShouldBe(1m, "20180430", "20180501");
        }

        [TestMethod]
        public void period_inside_budget_month()
        {
            GivenBudgets(new Budget { YearMonth = "201806", Amount = 30 });
            AmountShouldBe(1m, "20180601", "20180601");
        }

        [TestInitialize]
        public void TestInit()
        {
            _accounting = new Accounting(_budgetRepository);
        }

        private void AmountShouldBe(decimal expected, string startTime, string endTime)
        {
            var start = DateTime.ParseExact(startTime, "yyyyMMdd", null);
            var end = DateTime.ParseExact(endTime, "yyyyMMdd", null);
            var totalAmount = _accounting.TotalAmount(start, end);
            Assert.AreEqual(expected, totalAmount);
        }

        private void GivenBudgets(params Budget[] budgets)
        {
            _budgetRepository.GetAll().Returns(budgets.ToList());
        }
    }
}